using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Plane
{
    public class UpdatePlaneValidator : AbstractValidator<UpdatePlaneCommand>
    {
        public UpdatePlaneValidator()
        {
            RuleFor(p => p.Registration).NotEmpty();
            RuleFor(p => p.Type).NotEmpty();
            RuleFor(p => p.FuselageLength).GreaterThan(0.0);
            RuleFor(p => p.Wingspan).GreaterThan(0.0);
            RuleFor(p => p.SeatColCount).GreaterThan(0);
            RuleFor(p => p.SeatRowCount).GreaterThan(0);
            RuleFor(p => p.FlightDistance).GreaterThan(0);
            RuleFor(p => p.CruiseHeight).GreaterThan(0);
            RuleFor(p => p.CruiseSpeed).GreaterThan(0.0);
        }
    }

    public class UpdatePlaneCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Registration { get; set; }
        public string Type { get; set; }
        public double FuselageLength { get; set; }
        public double Wingspan { get; set; }
        public int SeatColCount { get; set; }
        public int SeatRowCount { get; set; }
        public int FlightDistance { get; set; }
        public int CruiseHeight { get; set; }
        public double CruiseSpeed { get; set; }
    }

    public class UpdatePlaneCommandHandler : IRequestHandler<UpdatePlaneCommand, Unit>
    {
        private readonly IPlaneRepository planeRepository;

        public UpdatePlaneCommandHandler(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }

        public async Task<Unit> Handle(UpdatePlaneCommand request, CancellationToken cancellationToken)
        {
            var plane = await planeRepository.GetAsync(request.Id, cancellationToken);

            plane.Registration = request.Registration;
            plane.Type = request.Type;
            plane.FuselageLength = request.FuselageLength;
            plane.Wingspan = request.Wingspan;
            plane.SeatColCount = request.SeatColCount;
            plane.SeatRowCount = request.SeatRowCount;
            plane.FlightDistance = request.FlightDistance;
            plane.CruiseHeight = request.CruiseHeight;
            plane.CruiseSpeed = request.CruiseSpeed;

            planeRepository.Update(plane);
            return Unit.Value;
        }
    }
}
