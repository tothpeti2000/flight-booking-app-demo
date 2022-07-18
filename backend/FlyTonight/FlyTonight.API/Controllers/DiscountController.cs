using FlyTonight.Application.Feature.Discount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMediator mediator;

        public DiscountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<DiscountController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllDiscountResponse.Discount>>> GetAllDiscounts()
        {
            var response = await mediator.Send(new GetAllDiscountRequest());
            return response.Discounts;
        }

        [HttpGet("offers")]
        public async Task<IEnumerable<GetDiscountOffersResponse.Offer>> GetDiscountOffers([FromQuery] GetDiscountOffersRequest dto)
        {
            var response = await mediator.Send(dto);
            return response.Offers;
        }

        // GET api/<DiscountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDiscountResponse>> GetDiscountById(Guid id)
        {
            return await mediator.Send(new GetDiscountRequest(id));
        }

        // POST api/<DiscountController>
        [HttpPost]
        public async Task CreateDiscount([FromBody] CreateDiscountCommand dto)
        {
            await mediator.Send(dto);
        }

        // PUT api/<DiscountController>/5
        [HttpPut("{id}")]
        public async Task UpdateDiscount(Guid id, [FromBody] UpdateDiscountCommand dto)
        {
            dto.Id = id;
            await mediator.Send(dto);
        }

        // DELETE api/<DiscountController>/5
        [HttpDelete("{id}")]
        public async Task DeleteDiscount(Guid id)
        {
            await mediator.Send(new DeleteDiscountCommand(id));
        }
    }
}
