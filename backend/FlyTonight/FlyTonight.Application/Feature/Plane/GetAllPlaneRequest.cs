using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Plane
{
    public class GetAllPlaneRequest : IRequest<GetAllPlaneResponse> { }

    public class GetAllPlaneResponse
    {
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
        public List<Plane> Planes { get; set; }
    }

    public class GetAllPlaneRequestHandler : IRequestHandler<GetAllPlaneRequest, GetAllPlaneResponse>
    {
        private readonly IPlaneRepository planeRepository;

        public GetAllPlaneRequestHandler(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }

        public async Task<GetAllPlaneResponse> Handle(GetAllPlaneRequest request, CancellationToken cancellationToken)
        {
            var planes = await planeRepository.GetAllAsync(cancellationToken);
            return new GetAllPlaneResponse
            {
                Planes = planes.Select(p => new GetAllPlaneResponse.Plane()
                {
                    Id = p.Id,
                    Registration = p.Registration,
                    Type = p.Type,
                    FuselageLength = p.FuselageLength,
                    Wingspan = p.Wingspan,
                    SeatCount = p.SeatCount,
                    FlightDistance = p.FlightDistance,
                    CruiseHeight = p.CruiseHeight,
                    CruiseSpeed = p.CruiseSpeed
                }).ToList()
            };
        }
    }
}
