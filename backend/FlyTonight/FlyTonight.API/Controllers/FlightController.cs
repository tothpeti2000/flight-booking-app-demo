using FlyTonight.Application.Feature.Flight;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IMediator mediator;

        public FlightController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<FlightController>
        [HttpGet]
        public async Task<IEnumerable<GetFlightsFromToResponse.Flight>> GetFlightsFromTo([FromQuery] GetFlightsFromToRequest dto)
        {
            var response = await mediator.Send(dto);
            return response.Flights;
        }

        // GET api/<FlightController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetFlightResponse>> GetFlightById(Guid id)
        {
            return await mediator.Send(new GetFlightRequest(id));
        }

        // POST api/<FlightController>
        [HttpPost]
        public async Task CreateFlight([FromBody] CreateFlightCommand dto)
        {
            await mediator.Send(dto);
        }

        [HttpPost("{flightId}/discount/{discountId}")]
        public async Task SetDiscount(Guid flightId, Guid discountId)
        {
            var command = new SetDiscountCommand
            {
                FlightId = flightId,
                DiscountId = discountId
            };

            await mediator.Send(command);
        }

        [HttpPost("{flightId}/tax/{taxId}")]
        public async Task SetTax(Guid flightId, Guid taxId)
        {
            var command = new SetTaxCommand
            {
                FlightId = flightId,
                TaxId = taxId
            };

            await mediator.Send(command);
        }

        [HttpDelete("{flightId}/discount/{discountId}")]
        public async Task RemoveDiscount(Guid flightId, Guid discountId)
        {
            var command = new RemoveDiscountCommand
            {
                FlightId = flightId,
                DiscountId = discountId
            };

            await mediator.Send(command);
        }

        [HttpDelete("{flightId}/tax/{taxId}")]
        public async Task RemoveTax(Guid flightId, Guid taxId)
        {
            var command = new RemoveTaxCommand
            {
                FlightId = flightId,
                TaxId = taxId
            };

            await mediator.Send(command);
        }

        // PUT api/<FlightController>/5
        [HttpPut("{id}")]
        public async Task UpdateFlight(Guid id, [FromBody] UpdateFlightCommand dto)
        {
            dto.Id = id;
            await mediator.Send(dto);
        }

        // DELETE api/<FlightController>/5
        [HttpDelete("{id}")]
        public async Task DeleteFlight(Guid id)
        {
            await mediator.Send(new DeleteFlightCommand(id));
        }
    }
}
