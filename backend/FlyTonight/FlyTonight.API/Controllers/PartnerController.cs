using FlyTonight.Application.Feature.Partner;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IMediator mediator;

        public PartnerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<PartnerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllPartnerResponse.Partner>>> GetAllPartners()
        {
            var response = await mediator.Send(new GetAllPartnerRequest());
            return response.Partners;
        }

        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPartnerResponse>> GetPartnerById(Guid id)
        {
            return await mediator.Send(new GetPartnerRequest(id));
        }

        // POST api/<PartnerController>
        [HttpPost]
        public async Task CreatePartner([FromBody] CreatePartnerCommand dto)
        {
            await mediator.Send(dto);
        }

        // PUT api/<PartnerController>/5
        [HttpPut("{id}")]
        public async Task UpdatePartner(Guid id, [FromBody] UpdatePartnerCommand dto)
        {
            dto.Id = id;
            await mediator.Send(dto);
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        public async Task DeletePartner(Guid id)
        {
            await mediator.Send(new DeletePartnerCommand(id));
        }
    }
}
