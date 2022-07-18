using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class RemoveDiscountValidator : AbstractValidator<RemoveDiscountCommand>
    {
        public RemoveDiscountValidator()
        {
            RuleFor(f => f.FlightId).NotEmpty();
            RuleFor(f => f.DiscountId).NotEmpty();
        }
    }

    public class RemoveDiscountCommand : IRequest
    {
        public Guid FlightId { get; set; }
        public Guid DiscountId { get; set; }
    }

    public class RemoveDiscountCommandHandler : IRequestHandler<RemoveDiscountCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;
        private readonly IDiscountRepository discountRepository;

        public RemoveDiscountCommandHandler(IFlightRepository flightRepository, IDiscountRepository discountRepository)
        {
            this.flightRepository = flightRepository;
            this.discountRepository = discountRepository;
        }

        public async Task<Unit> Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await discountRepository.GetAsync(request.DiscountId);
            var flight = await flightRepository.GetAsync(request.FlightId);
            flight.Discounts.Remove(discount);

            return Unit.Value;
        }
    }
}
