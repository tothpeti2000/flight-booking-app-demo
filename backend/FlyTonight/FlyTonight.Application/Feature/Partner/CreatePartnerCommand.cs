using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Partner
{
    public class CreatePartnerValidator : AbstractValidator<CreatePartnerCommand> 
    {
        public CreatePartnerValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Base64Image).NotEmpty();
        }
    }

    public class CreatePartnerCommand : IRequest
    {
        public string Name { get; set; }
        public string Base64Image { get; set; }
    }

    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Unit>
    {
        private readonly IPartnerRepository partnerRepository;
        private readonly IStorage blobStorage;

        public CreatePartnerCommandHandler(IPartnerRepository partnerRepository, IStorage blobStorage)
        {
            this.partnerRepository = partnerRepository;
            this.blobStorage = blobStorage;
        }

        public async Task<Unit> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var url = await blobStorage.UploadPartnerImage(request.Base64Image);
            partnerRepository.Add(new()
            {
                Name = request.Name,
                BlobUrl = url
            });
            return Unit.Value;
        }
    }
}
