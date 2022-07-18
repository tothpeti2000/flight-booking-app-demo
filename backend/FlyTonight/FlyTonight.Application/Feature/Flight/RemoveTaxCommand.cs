using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class RemoveTaxValidator : AbstractValidator<RemoveTaxCommand>
    {
        public RemoveTaxValidator()
        {
            RuleFor(f => f.FlightId).NotEmpty();
            RuleFor(f => f.TaxId).NotEmpty();
        }
    }

    public class RemoveTaxCommand : IRequest
    {
        public Guid FlightId { get; set; }
        public Guid TaxId { get; set; }
    }

    public class RemoveTaxCommandHandler : IRequestHandler<RemoveTaxCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;
        private readonly ITaxRepository taxRepository;

        public RemoveTaxCommandHandler(IFlightRepository flightRepository, ITaxRepository taxRepository)
        {
            this.flightRepository = flightRepository;
            this.taxRepository = taxRepository;
        }

        public async Task<Unit> Handle(RemoveTaxCommand request, CancellationToken cancellationToken)
        {
            var tax = await taxRepository.GetAsync(request.TaxId, cancellationToken);
            var flight = await flightRepository.GetAsync(request.FlightId, cancellationToken);
            flight.Taxes.Remove(tax);

            return Unit.Value;
        }
    }
}
