using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Airport
{
    public class GetAirportRequest : IRequest<GetAirportRespone>
    {
        public GetAirportRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetAirportRespone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BlobUrl { get; set; }
    }

    public class GetAirportRequestHandler : IRequestHandler<GetAirportRequest, GetAirportRespone>
    {
        private readonly IAirportRepository airportRepository;

        public GetAirportRequestHandler(IAirportRepository airportRepository)
        {
            this.airportRepository = airportRepository;
        }

        public async Task<GetAirportRespone> Handle(GetAirportRequest request, CancellationToken cancellationToken)
        {
            var airport = await airportRepository.GetAsync(request.Id, cancellationToken);
            return new GetAirportRespone
            {
                Id = airport.Id,
                Name = airport.Name,
                CityName = airport.CityName,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                BlobUrl = airport.BlobUrl,
            };
        }
    }
}
