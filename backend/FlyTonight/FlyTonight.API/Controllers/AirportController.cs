using FlyTonight.Application.Feature.Airport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        IMediator mediator;
        public AirportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<AirportController>
        [HttpGet]
        public async Task<IEnumerable<GetAllAirportResponse.Airport>> GetAllAirports()
        {

            var response = await mediator.Send(new GetAllAirportRequest());
            return response.Airports;
        }

        // GET api/<AirportController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAirportRespone>> GetAirportById(Guid id)
        {
            return await mediator.Send(new GetAirportRequest(id));
        }

        // POST api/<AirportController>
        [HttpPost]
        public async Task CreateAirport([FromBody] CreateAirportCommand dto)
        {
            await mediator.Send(dto);
        }

        // PUT api/<AirportController>/5
        [HttpPut("{id}")]
        public async Task UpdateAirport(Guid id, [FromBody] UpdateAirportCommand dto)
        {
            dto.Id = id;
            await mediator.Send(dto);
        }

        // DELETE api/<AirportController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAirport(Guid id)
        {
            await mediator.Send(new DeleteAirportCommand(id));
        }
    }
}
