using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Discount
{
    public class DeleteDiscountValidator : AbstractValidator<DeleteDiscountCommand>
    {
        public DeleteDiscountValidator()
        {
            RuleFor(discount => discount.Id).NotEmpty();
        }
    }

    public class DeleteDiscountCommand : IRequest
    {
        public DeleteDiscountCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Unit>
    {
        private readonly IDiscountRepository discountRepository;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public Task<Unit> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            discountRepository.Delete(new Domain.Models.Discount
            {
                Id = request.Id
            });
            return Unit.Task;
        }
    }
}
