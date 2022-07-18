using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Tax
{
    public class GetTaxRequest : IRequest<GetTaxResponse>
    {
        public GetTaxRequest(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }

    public class GetTaxResponse
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid Id { get; set; }
    }

    public class GetTaxRequestHandler : IRequestHandler<GetTaxRequest, GetTaxResponse>
    {
        private readonly ITaxRepository taxRepository;

        public GetTaxRequestHandler(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public async Task<GetTaxResponse> Handle(GetTaxRequest request, CancellationToken cancellationToken)
        {
            var tax = await taxRepository.GetAsync(request.Id, cancellationToken);
            return new GetTaxResponse
            {
                Name = tax.Name,
                Value = tax.Value,
                Id = tax.Id
            };
        }
    }
}
