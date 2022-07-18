using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class CreateFlightValidator : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightValidator()
        {
            RuleFor(f => f.FromId).NotEmpty();
            RuleFor(f => f.ToId).NotEmpty();
            RuleFor(f => f.TimeOfDeparture).NotEmpty();
            RuleFor(f => f.AirplaneId).NotEmpty();
        }
    }

    public class CreateFlightCommand : IRequest
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public Guid AirplaneId { get; set; }
    }

    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;

        public CreateFlightCommandHandler(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public Task<Unit> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            flightRepository.Add(new()
            {
                FromId = request.FromId,
                ToId = request.ToId,
                TimeOfDeparture = request.TimeOfDeparture,
                AirplaneId = request.AirplaneId
            });
            return Unit.Task;
        }
    }
}
