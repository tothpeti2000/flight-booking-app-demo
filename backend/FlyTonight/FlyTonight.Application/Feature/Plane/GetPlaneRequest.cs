using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Plane
{
    public class GetPlaneRequest : IRequest<GetPlaneResponse>
    {
        public GetPlaneRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetPlaneResponse
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

    public class GetPlaneRequestHandler : IRequestHandler<GetPlaneRequest, GetPlaneResponse>
    {
        private readonly IPlaneRepository planeRepository;

        public GetPlaneRequestHandler(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }

        public async Task<GetPlaneResponse> Handle(GetPlaneRequest request, CancellationToken cancellationToken)
        {
            var plane = await planeRepository.GetAsync(request.Id, cancellationToken);
            return new GetPlaneResponse()
            {
                Id = plane.Id,
                Registration = plane.Registration,
                Type = plane.Type,
                FuselageLength = plane.FuselageLength,
                Wingspan = plane.Wingspan,
                SeatCount = plane.SeatCount,
                FlightDistance = plane.FlightDistance,
                CruiseHeight = plane.CruiseHeight,
                CruiseSpeed = plane.CruiseSpeed
            };
        }
    }
}
