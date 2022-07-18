using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class GetFlightRequest : IRequest<GetFlightResponse>
    {
        public GetFlightRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetFlightResponse
    {
        public class Airport
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string CityName { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string BlobUrl { get; set; }
        }
        public class Plane
        {
            public Guid Id { get; set; }
            public string Registration { get; set; }
            public string Type { get; set; }
            public double FuselageLength { get; set; }
            public double Wingspan { get; set; }
            public int SeatCount { get; set; }
            public int FlightDistance { get; set; }
            public int CruiseHeight { get; set; }
            public double CruiseSpeed { get; set; }
        }
        public class Discount
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
            public string BlobUrl { get; set; }
        }
        public class Tax
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }
        public class SeatReservation
        {
            public Guid Id { get; set; }
            public int ColNum { get; set; }
            public int RowNum { get; set; }
            public Guid FlightId { get; set; }
            public Guid TicketId { get; set; }
        }

        public Guid Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public Plane Airplane { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Tax> Taxes { get; set; }
        public ICollection<SeatReservation> Reservations { get; set; }

        public static GetFlightResponse MapFromFlightModel(Domain.Models.Flight flight)
        {
            return new()
            {
                Id = flight.Id,
                TimeOfDeparture = flight.TimeOfDeparture,
                From = new()
                {
                    Id = flight.From.Id,
                    Name = flight.From.Name,
                    CityName = flight.From.CityName,
                    Latitude = flight.From.Latitude,
                    Longitude = flight.From.Longitude,
                    BlobUrl = flight.From.BlobUrl
                },
                To = new()
                {
                    Id = flight.To.Id,
                    Name = flight.To.Name,
                    CityName = flight.To.CityName,
                    Latitude = flight.To.Latitude,
                    Longitude = flight.To.Longitude,
                    BlobUrl = flight.To.BlobUrl
                },
                Airplane = new()
                {
                    Id = flight.Airplane.Id,
                    CruiseHeight = flight.Airplane.CruiseHeight,
                    CruiseSpeed = flight.Airplane.CruiseSpeed,
                    FlightDistance = flight.Airplane.FlightDistance,
                    FuselageLength = flight.Airplane.FuselageLength,
                    Registration = flight.Airplane.Registration,
                    SeatCount = flight.Airplane.SeatCount,
                    Type = flight.Airplane.Type,
                    Wingspan = flight.Airplane.Wingspan
                },
                Taxes = flight.Taxes?.Select(t => new Tax
                {
                    Id = t.Id,
                    Name = t.Name,
                    Value = t.Value
                }).ToList(),
                Discounts = flight.Discounts?.Select(d => new Discount
                {
                    Id = d.Id,
                    Name = d.Name,
                    Value = d.Value,
                    BlobUrl = d.BlobUrl
                }).ToList(),
                Reservations = flight.Reservations?.Select(r => new SeatReservation
                {
                    Id = r.Id,
                    ColNum = r.ColNum,
                    FlightId = r.FlightId,
                    RowNum = r.RowNum,
                    TicketId = r.TicketId
                }).ToList()
            };
        }
    }

    public class GetFlightRequestHandler : IRequestHandler<GetFlightRequest, GetFlightResponse>
    {
        private readonly IFlightRepository flightRepository;

        public GetFlightRequestHandler(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task<GetFlightResponse> Handle(GetFlightRequest request, CancellationToken cancellationToken)
        {
            var flight = await flightRepository.GetAsync(request.Id, cancellationToken);
            return GetFlightResponse.MapFromFlightModel(flight);
        }
    }
}
