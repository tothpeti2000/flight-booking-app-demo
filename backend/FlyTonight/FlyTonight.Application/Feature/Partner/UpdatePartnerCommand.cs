using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Partner
{
    public class UpdatePartnerValidator : AbstractValidator<UpdatePartnerCommand>
    {
        public UpdatePartnerValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Base64Image).NotEmpty();
        }
    }

    public class UpdatePartnerCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Base64Image { get; set; }
    }

    public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, Unit>
    {
        private readonly IPartnerRepository partnerRepository;
        private readonly IStorage blobStorage;

        public UpdatePartnerCommandHandler(IPartnerRepository partnerRepository, IStorage blobStorage)
        {
            this.partnerRepository = partnerRepository;
            this.blobStorage = blobStorage;
        }

        public async Task<Unit> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await partnerRepository.GetAsync(request.Id, cancellationToken);
            var url = await blobStorage.UploadPartnerImage(request.Base64Image);

            partner.Name = request.Name;
            partner.BlobUrl = url;

            partnerRepository.Update(partner);
            return Unit.Value;
        }
    }
}
