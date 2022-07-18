using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Discount
{
    public class GetDiscountValidator : AbstractValidator<GetDiscountRequest>
    {
        public GetDiscountValidator()
        {
            RuleFor(discount => discount.Id).NotEmpty();
        }
    }

    public class GetDiscountRequest : IRequest<GetDiscountResponse>
    {
        public GetDiscountRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetDiscountResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string BlobUrl { get; set; }
    }

    public class GetDiscountRequestHandler : IRequestHandler<GetDiscountRequest, GetDiscountResponse>
    {
        private readonly IDiscountRepository discountRepository;

        public GetDiscountRequestHandler(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public async Task<GetDiscountResponse> Handle(GetDiscountRequest request, CancellationToken cancellationToken)
        {
            var discount = await discountRepository.GetAsync(request.Id, cancellationToken);
            return new GetDiscountResponse
            {
                Name = discount.Name,
                Value = discount.Value,
                BlobUrl = discount.BlobUrl,
                Id = discount.Id
            };
        }
    }
}
