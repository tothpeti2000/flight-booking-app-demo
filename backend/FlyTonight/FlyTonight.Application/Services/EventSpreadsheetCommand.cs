using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;
using System.Globalization;

namespace FlyTonight.Application.Services
{
    public class EventSpreadsheetCommandValidator : AbstractValidator<EventSpreadsheetCommand>
    {
        public EventSpreadsheetCommandValidator()
        {
            RuleFor(x => x.Week).Must(x => x.DayOfWeek == DayOfWeek.Sunday).WithMessage($"Adj meg egy vasárnapi napot!");
            RuleFor(x => x.Email).EmailAddress();
        }
    }

    public class EventSpreadsheetCommand : IRequest
    {
        public DateTime Week { get; set; }
        public string Email { get; set; }
    }

    public class SpreadsheetData
    {
        public class IncidentData
        {
            public string Name { get; set; }
            public string PlaneId { get; set; }
            public int PassengerCount { get; set; }
            public string FlightId { get; set; }
            public int DelayMinutes { get; set; }
        }

        public class FlightData
        {
            public string FlightId { get; set; }
            public string PlaneId { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public int PassengerCount { get; set; }
            public DateTime PlannedDeparture { get; set; }
            public DateTime ActualDeparture { get; set; }
            public int DelayMinutes { get; set; }
            public DateTime ArrivalTime { get; set; }
            public int FlightDistanceKm { get; set; }
            public int FlightTimeMinutes { get; set; }
            public bool Incident { get; set; }
            public int TotalIncome { get; set; }
        }

        public class PlaneData
        {
            public string PlaneId { get; set; }
            public string PlaneType { get; set; }
            public double MachSpeed { get; set; }
            public int Height { get; set; }
            public double Length { get; set; }
            public double Wingspan { get; set; }
            public int FlightCount { get; set; }
            public int IncidentCount { get; set; }
        }

        public List<IncidentData> Incident { get; set; }
        public List<FlightData> Flights { get; set; }
        public List<PlaneData> Planes { get; set; }
    }

    public class EventSpreadsheetCommandHandler : IRequestHandler<EventSpreadsheetCommand, Unit>
    {
        private readonly IEventReportGeneratorService eventReportGeneratorService;
        private readonly IEmailService emailService;
        private readonly IFlightRepository flightRepository;

        public EventSpreadsheetCommandHandler(IEventReportGeneratorService eventReportGeneratorService, IEmailService emailService, IFlightRepository flightRepository)
        {
            this.eventReportGeneratorService = eventReportGeneratorService;
            this.emailService = emailService;
            this.flightRepository = flightRepository;
        }

        public async Task<Unit> Handle(EventSpreadsheetCommand request, CancellationToken cancellationToken)
        {
            var startDate = request.Week - TimeSpan.FromDays(7);
            var endDate = request.Week;

            var flights = await flightRepository.GetFlightsBetweenDates(startDate, endDate);

            int week = GetWeekNumber(request.Week);

            using Stream stream = new MemoryStream();

            SpreadsheetData dto = new()
            {
                Flights = GetFlightData(flights),
                Incident = GetIncidentData(flights),
                Planes = GetPlaneData(flights)
            };

            eventReportGeneratorService.GenerateSpreadsheet(dto, week, stream);

            await emailService.SendEmailWithExcelAttachment(request.Email, $"{week}. heti incidens adatok", "", stream);

            return Unit.Value;
        }

        private int GetWeekNumber(DateTime date)
        {
            CultureInfo myCI = new CultureInfo("hu-HU");
            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(date, myCWR, myFirstDOW);
        }

        private List<SpreadsheetData.PlaneData> GetPlaneData(List<Domain.Models.Flight> flights)
        {
            return flights
                .GroupBy(f => f.Airplane)
                .ToDictionary(g => g.Key, g => g.ToList())
                .Select(d => new SpreadsheetData.PlaneData
                {
                    PlaneId = d.Key.Id.ToString(),
                    PlaneType = d.Key.Type,
                    MachSpeed = d.Key.CruiseSpeed,
                    Height = d.Key.CruiseHeight,
                    Length = d.Key.FuselageLength,
                    Wingspan = d.Key.Wingspan,
                    FlightCount = d.Value.Count(),
                    IncidentCount = d.Value.Count(f => f.EnvEvent != null)
                }).ToList();
        }

        private List<SpreadsheetData.IncidentData> GetIncidentData(List<Domain.Models.Flight> flights)
        {
            return flights.Where(f => f.EnvEvent != null)
                .Select(f =>
                {
                    return new SpreadsheetData.IncidentData()
                    {
                        Name = f.EnvEvent.EventName,
                        PlaneId = f.Airplane.Registration,
                        PassengerCount = f.Reservations.Count(),
                        FlightId = f.Id.ToString(),
                        DelayMinutes = f.FlightDelay
                    };
                }).ToList();
        }
        
        private List<SpreadsheetData.FlightData> GetFlightData(List<Domain.Models.Flight> flights)
        {
            return flights.Select(f => 
            {
                return new SpreadsheetData.FlightData()
                {
                    FlightId = f.Id.ToString(),
                    PlaneId = f.Airplane.Registration,
                    From = f.From.CityName,
                    To = f.To.CityName,
                    PassengerCount = f.Reservations.Count(),
                    PlannedDeparture = f.TimeOfDeparture,
                    ActualDeparture = f.ActualTimeOfDeparture,
                    DelayMinutes = f.FlightDelay,
                    ArrivalTime = f.ActualTimeOfDeparture.AddMinutes(f.FlightTime),
                    FlightDistanceKm = f.TravelDistance,
                    FlightTimeMinutes = f.FlightTime,
                    Incident = f.EnvEvent is not null,
                    TotalIncome = f.Reservations.Sum(r => r.Ticket.Price)
                };
            }).ToList();
        }
    }
}
