using FlyTonight.Application.Feature.Tax;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly IMediator mediator;
        public TaxController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<TaxController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllTaxResponse.Tax>>> GetAllTaxes()
        {
            var response = await mediator.Send(new GetAllTaxRequest());
            return response.Taxes;
        }

        // GET api/<TaxController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaxResponse>> GetTaxById(Guid id)
        {
            return await mediator.Send(new GetTaxRequest(id));
        }

        // POST api/<TaxController>
        [HttpPost]
        public async Task CreateTax([FromBody] CreateTaxCommand dto)
        {
            await mediator.Send(dto);
        }

        // PUT api/<TaxController>/5
        [HttpPut("{id}")]
        public async Task UpdateTax(Guid id, [FromBody] UpdateTaxCommand dto)
        {
            dto.Id = id;
            await mediator.Send(dto);
        }

        // DELETE api/<TaxController>/5
        [HttpDelete("{id}")]
        public async Task DeleteTax(Guid id)
        {
            await mediator.Send(new DeleteTaxCommand(id));
        }
    }
}
