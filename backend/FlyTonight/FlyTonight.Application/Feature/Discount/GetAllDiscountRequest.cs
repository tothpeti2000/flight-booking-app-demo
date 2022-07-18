using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Discount
{
    public class GetAllDiscountRequest : IRequest<GetAllDiscountResponse> { }

    public class GetAllDiscountResponse 
    {
        public class Discount 
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
            public string BlobUrl { get; set; }
        }

        public List<Discount> Discounts { get; set; }
    }

    public class GetAllDiscountRequestHandler : IRequestHandler<GetAllDiscountRequest, GetAllDiscountResponse>
    {
        private readonly IDiscountRepository discountrepository;

        public GetAllDiscountRequestHandler(IDiscountRepository discountrepository)
        {
            this.discountrepository = discountrepository;
        }

        public async Task<GetAllDiscountResponse> Handle(GetAllDiscountRequest request, CancellationToken cancellationToken)
        {
            var discounts = await discountrepository.GetAllAsync(cancellationToken);

            return new GetAllDiscountResponse()
            {
                Discounts = discounts.Select(d => new GetAllDiscountResponse.Discount()
                {
                    Name = d.Name,
                    Value = d.Value,
                    Id = d.Id,
                    BlobUrl = d.BlobUrl
                }).ToList()
            };
        }
    }
}
