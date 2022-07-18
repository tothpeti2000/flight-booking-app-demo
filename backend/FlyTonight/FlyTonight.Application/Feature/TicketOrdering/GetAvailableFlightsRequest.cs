using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.TicketOrdering
{
    public class GetAvailableFlightsValidator : AbstractValidator<GetAvailableFlightsRequest>
    {
        public GetAvailableFlightsValidator()
        {
            RuleFor(f => f.From).NotEmpty();
            RuleFor(f => f.To).NotEmpty();
            RuleFor(f => f.DepartureDate.Date).GreaterThan(DateTime.Today);
            RuleFor(f => f.ReturnDate).GreaterThan(f => f.DepartureDate.Date);
            RuleFor(f => f.PassengerCount).GreaterThan(0);
        }
    }

    public class GetAvailableFlightsRequest : IRequest<GetAvailableFlightsResponse>
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int PassengerCount { get; set; }
    }

    public class GetAvailableFlightsResponse
    {
        public class Discount
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        public class Flight
        {
            public Guid FlightId { get; set; }
            public string DepartureAirport { get; set; }
            public string DepartureCity { get; set; }
            public string ArrivalAirport { get; set; }
            public string ArrivalCity { get; set; }
            public DateTime DepartureTime { get; set; }
            public DateTime ArrivalTime { get; set; }
            public int FlightTimeMinutes { get; set; }
            public int Price { get; set; }
            public Guid PlaneId { get; set; }
            public IList<Discount> Discounts { get; set; }
        }

        public IList<Flight> ToFlights { get; set; }
        public IList<Flight> ReturnFlights { get; set; } = new List<Flight>();
    }

    public class GetAvailableFlightsHandler : IRequestHandler<GetAvailableFlightsRequest, GetAvailableFlightsResponse>
    {
        private readonly IFlightRepository flightRepository;

        public GetAvailableFlightsHandler(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task<GetAvailableFlightsResponse> Handle(GetAvailableFlightsRequest request, CancellationToken cancellationToken)
        {
            GetAvailableFlightsResponse response = new GetAvailableFlightsResponse();

            var availableFlights = await flightRepository.GetFlightsAsync(request.From, request.To, request.DepartureDate, cancellationToken);
            response.ToFlights = GetAvailableFlights(availableFlights, request.PassengerCount);

            if (request.ReturnDate != null)
            {
                var availableReturnFlights = await flightRepository.GetFlightsAsync(request.To, request.From, request.ReturnDate.Value, cancellationToken);
                response.ReturnFlights = GetAvailableFlights(availableReturnFlights, request.PassengerCount);
            }

            return response;
        }

        private List<GetAvailableFlightsResponse.Flight> GetAvailableFlights(IEnumerable<Domain.Models.Flight> availableFlights, int passengerCount)
        {
            return availableFlights
                .Where(f => f.Reservations.Count < (f.Airplane.SeatCount - passengerCount))
                .Select(f =>
                {
                    GetAvailableFlightsResponse.Flight flight = new()
                    {
                        FlightId = f.Id,
                        PlaneId = f.AirplaneId,
                        DepartureAirport = f.From.Name,
                        DepartureCity = f.From.CityName,
                        ArrivalAirport = f.To.Name,
                        ArrivalCity = f.To.CityName,
                        DepartureTime = f.TimeOfDeparture,
                        FlightTimeMinutes = f.FlightTime,
                        Discounts = f.Discounts.Select(d =>
                        {
                            return new GetAvailableFlightsResponse.Discount
                            {
                                Name = d.Name,
                                Value = d.Value
                            };
                        }).ToList(),
                        ArrivalTime = f.TimeOfDeparture.AddMinutes(f.FlightTime),
                        Price = f.Price,
                    };

                    return flight;
                })
                .OrderBy(f => f.DepartureTime)
                .ToList();
        }
    }
}