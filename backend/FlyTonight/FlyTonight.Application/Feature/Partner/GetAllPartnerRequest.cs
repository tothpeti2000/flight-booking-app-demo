using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Partner
{
    public class GetAllPartnerRequest : IRequest<GetAllPartnerResponse>
    {
    }

    public class GetAllPartnerResponse
    {
        public class Partner
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string BlobUrl { get; set; }
        }

        public List<Partner> Partners { get; set; }
    }

    public class GetAllPartnerRequestHandler : IRequestHandler<GetAllPartnerRequest, GetAllPartnerResponse>
    {
        private readonly IPartnerRepository partnerRepository;

        public GetAllPartnerRequestHandler(IPartnerRepository partnerRepository)
        {
            this.partnerRepository = partnerRepository;
        }

        public async Task<GetAllPartnerResponse> Handle(GetAllPartnerRequest request, CancellationToken cancellationToken)
        {
            var partners = await partnerRepository.GetAllAsync(cancellationToken);
            return new GetAllPartnerResponse
            {
                Partners = partners.Select(d => new GetAllPartnerResponse.Partner()
                {
                    Name = d.Name,
                    Id = d.Id,
                    BlobUrl = d.BlobUrl
                }).ToList()
            };
        }
    }
}
