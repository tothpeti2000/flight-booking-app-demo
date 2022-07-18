using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Discount
{
    public class GetDiscountOffersRequest : IRequest<GetDiscountOffersResponse>
    {
    }

    public class GetDiscountOffersResponse
    {
        public class Offer
        {
            public Guid FlightId { get; set; }
            public string ImgUrlFrom { get; set; }
            public string ImgUrlTo { get; set; }
            public string FromCity { get; set; }
            public string ToCity { get; set; }
            public int Price { get; set; }
        }

        public IList<Offer> Offers { get; set; }
    }

    public class GetDiscountOffersRequestHandler : IRequestHandler<GetDiscountOffersRequest, GetDiscountOffersResponse>
    {
        private readonly IDiscountRepository discountRepository;

        public GetDiscountOffersRequestHandler(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public async Task<GetDiscountOffersResponse> Handle(GetDiscountOffersRequest request, CancellationToken cancellationToken)
        {
            var discounts = await discountRepository.GetAllAsync(cancellationToken);
            return new GetDiscountOffersResponse
            {
                Offers = discounts.SelectMany(d => d.Flights.DistinctBy(f => f.FromId).Select(df => new GetDiscountOffersResponse.Offer
                {
                    FlightId = df.Id,
                    FromCity = df.From.CityName,
                    ToCity = df.To.CityName,
                    Price = df.Price,
                    ImgUrlFrom = df.From.BlobUrl,
                    ImgUrlTo = df.To.BlobUrl
                })).ToList()
            };
        }
    }
}
