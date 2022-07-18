using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;
using FluentValidation;

namespace FlyTonight.Application.Feature.Discount
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator()
        {
            RuleFor(discount => discount.Name).NotEmpty().Length(1, 30);
            RuleFor(discount => discount.Value).InclusiveBetween(0.0, 1.0);
            RuleFor(discount => discount.Base64Image).NotEmpty();
        }
    }

    public  class CreateDiscountCommand : IRequest
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Base64Image { get; set; }
    }

    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Unit>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IStorage blobStorage;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IStorage blobStorage)
        {
            this.discountRepository = discountRepository;
            this.blobStorage = blobStorage;
        }

        public async Task<Unit> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var url = await blobStorage.UploadDiscountImage(request.Base64Image);
            discountRepository.Add(new Domain.Models.Discount
            {
                Name = request.Name,
                Value = request.Value,
                BlobUrl = url
            });
            return Unit.Value;
        }
    }
}
