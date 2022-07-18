using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class SetTaxValidator : AbstractValidator<SetTaxCommand>
    {
        public SetTaxValidator()
        {
            RuleFor(f => f.FlightId).NotEmpty();
            RuleFor(f => f.TaxId).NotEmpty();
        }
    }

    public class SetTaxCommand : IRequest
    {
        public Guid FlightId { get; set; }
        public Guid TaxId { get; set; }
    }

    public class SetTaxCommandHandler : IRequestHandler<SetTaxCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;
        private readonly ITaxRepository taxRepository;

        public SetTaxCommandHandler(IFlightRepository flightRepository, ITaxRepository taxRepository)
        {
            this.flightRepository = flightRepository;
            this.taxRepository = taxRepository;
        }

        public async Task<Unit> Handle(SetTaxCommand request, CancellationToken cancellationToken)
        {
            var tax = await taxRepository.GetAsync(request.TaxId, cancellationToken);
            var flight = await flightRepository.GetAsync(request.FlightId, cancellationToken);
            flight.Taxes.Add(tax);

            return Unit.Value;
        }
    }
}
