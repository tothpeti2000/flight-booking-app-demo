using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Partner
{
    public class GetPartnerRequest : IRequest<GetPartnerResponse>
    {
        public GetPartnerRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class GetPartnerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BlobUrl { get; set; }
    }

    public class GetPartnerRequestHandler : IRequestHandler<GetPartnerRequest, GetPartnerResponse>
    {
        private readonly IPartnerRepository partnerRepository;

        public GetPartnerRequestHandler(IPartnerRepository partnerRepository)
        {
            this.partnerRepository = partnerRepository;
        }

        public async Task<GetPartnerResponse> Handle(GetPartnerRequest request, CancellationToken cancellationToken)
        {
            var partner = await partnerRepository.GetAsync(request.Id, cancellationToken);
            return new GetPartnerResponse()
            {
                Id = partner.Id,
                Name = partner.Name,
                BlobUrl = partner.BlobUrl
            };
        }
    }
}
