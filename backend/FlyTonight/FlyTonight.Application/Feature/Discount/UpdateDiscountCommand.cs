using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Discount
{
    public class UpdateDiscountValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountValidator()
        {
            RuleFor(discount => discount.Name).NotEmpty().Length(1, 30);
            RuleFor(discount => discount.Value).InclusiveBetween(0.0, 1.0);
            RuleFor(discount => discount.Base64Image).NotEmpty();
        }
    }

    public class UpdateDiscountCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string Base64Image { get; set; }
    }

    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, Unit>
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IStorage blobStorage;

        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IStorage blobStorage)
        {
            this.discountRepository = discountRepository;
            this.blobStorage = blobStorage;
        }

        public async Task<Unit> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await discountRepository.GetAsync(request.Id, cancellationToken);
            var url = await blobStorage.UploadDiscountImage(request.Base64Image);

            discount.Name = request.Name;
            discount.Value = request.Value;
            discount.BlobUrl = url;

            discountRepository.Update(discount);
            return Unit.Value;
        }
    }
}
