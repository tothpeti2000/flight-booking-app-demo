using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class UpdateFlightValidator : AbstractValidator<UpdateFlightCommand>
    {
        public UpdateFlightValidator()
        {
            RuleFor(f => f.FromId).NotEmpty();
            RuleFor(f => f.ToId).NotEmpty();
            RuleFor(f => f.TimeOfDeparture).NotEmpty();
            RuleFor(f => f.AirplaneId).NotEmpty();
        }
    }

    public class UpdateFlightCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public Guid AirplaneId { get; set; }
    }

    public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;

        public UpdateFlightCommandHandler(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task<Unit> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = await flightRepository.GetAsync(request.Id, cancellationToken);

            flight.FromId = request.FromId;
            flight.ToId = request.ToId;
            flight.TimeOfDeparture = request.TimeOfDeparture;
            flight.AirplaneId = request.AirplaneId;

            flightRepository.Update(flight);
            return Unit.Value;
        }
    }
}
