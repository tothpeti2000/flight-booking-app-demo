using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Tax
{
    public class GetAllTaxRequest : IRequest<GetAllTaxResponse> { }

    public class GetAllTaxResponse
    {
        public class Tax
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public List<Tax> Taxes { get; set; }
    }

    public class GetAllTaxRequestHandler : IRequestHandler<GetAllTaxRequest, GetAllTaxResponse>
    {
        private readonly ITaxRepository taxRepository;

        public GetAllTaxRequestHandler(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public async Task<GetAllTaxResponse> Handle(GetAllTaxRequest request, CancellationToken cancellationToken)
        {
            var taxes = await taxRepository.GetAllAsync(cancellationToken);

            return new GetAllTaxResponse()
            {
                Taxes = taxes.Select(t => new GetAllTaxResponse.Tax()
                {
                    Name = t.Name,
                    Value = t.Value,
                    Id = t.Id
                }).ToList()
            };
        }
    }
}
