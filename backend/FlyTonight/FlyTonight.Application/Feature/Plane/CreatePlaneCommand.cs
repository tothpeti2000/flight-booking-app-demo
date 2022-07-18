using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Plane
{
    public class CreatePlaneValidator : AbstractValidator<CreatePlaneCommand>
    {
        public CreatePlaneValidator()
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

    public class CreatePlaneCommand : IRequest
    {
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

    public class CreatePlaneCommandHandler : IRequestHandler<CreatePlaneCommand, Unit>
    {
        private readonly IPlaneRepository planeRepository;

        public CreatePlaneCommandHandler(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }

        public Task<Unit> Handle(CreatePlaneCommand request, CancellationToken cancellationToken)
        {
            planeRepository.Add(new()
            {
                Registration = request.Registration,
                Type = request.Type,
                FuselageLength = request.FuselageLength,
                Wingspan = request.Wingspan,
                SeatColCount = request.SeatColCount,
                SeatRowCount = request.SeatRowCount,
                FlightDistance = request.FlightDistance,
                CruiseHeight = request.CruiseHeight,
                CruiseSpeed = request.CruiseSpeed
            });
            return Unit.Task;
        }
    }
}
