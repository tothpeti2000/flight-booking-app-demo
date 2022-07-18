using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class SetDiscountValidator : AbstractValidator<SetDiscountCommand>
    {
        public SetDiscountValidator()
        {
            RuleFor(f => f.FlightId).NotEmpty();
            RuleFor(f => f.DiscountId).NotEmpty();
        }
    }

    public class SetDiscountCommand : IRequest
    {
        public Guid FlightId { get; set; }
        public Guid DiscountId { get; set; }
    }

    public class SetDiscountCommandHandler : IRequestHandler<SetDiscountCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;
        private readonly IDiscountRepository discountRepository;

        public SetDiscountCommandHandler(IFlightRepository flightRepository, IDiscountRepository discountRepository)
        {
            this.flightRepository = flightRepository;
            this.discountRepository = discountRepository;
        }

        public async Task<Unit> Handle(SetDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await discountRepository.GetAsync(request.DiscountId, cancellationToken);
            var flight = await flightRepository.GetAsync(request.FlightId, cancellationToken);
            flight.Discounts.Add(discount);

            return Unit.Value;
        }
    }
}
