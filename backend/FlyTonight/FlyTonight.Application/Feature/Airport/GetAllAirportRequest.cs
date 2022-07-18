using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Airport
{
    public class GetAllAirportRequest : IRequest<GetAllAirportResponse> { }

    public class GetAllAirportResponse
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

        public List<Airport> Airports { get; set; }
    }

    public class GetAllAirportRequestHandler : IRequestHandler<GetAllAirportRequest, GetAllAirportResponse>
    {
        private readonly IAirportRepository airportRepository;

        public GetAllAirportRequestHandler(IAirportRepository airportRepository)
        {
            this.airportRepository = airportRepository;
        }

        public async Task<GetAllAirportResponse> Handle(GetAllAirportRequest request, CancellationToken cancellationToken)
        {
            var airports = await airportRepository.GetAllAsync(cancellationToken);
            return new GetAllAirportResponse
            {
                Airports = airports.Select(a => new GetAllAirportResponse.Airport
                {
                    Id = a.Id,
                    Name = a.Name,
                    CityName = a.CityName,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    BlobUrl = a.BlobUrl
                }).ToList()
            };
        }
    }
}
