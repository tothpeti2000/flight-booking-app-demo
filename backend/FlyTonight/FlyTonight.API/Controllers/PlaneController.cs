using FlyTonight.Application.Feature.Plane;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaneController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlaneController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<PlaneController>
        [HttpGet]
        public async Task<IEnumerable<GetAllPlaneResponse.Plane>> GetAllPlanes()
        {
            var response = await mediator.Send(new GetAllPlaneRequest());
            return response.Planes;
        }

        // GET api/<PlaneController>/5
        [HttpGet("{id}")]
        public async Task<GetPlaneResponse> GetPlaneById(Guid id)
        {
            return await mediator.Send(new GetPlaneRequest(id));
        }

        // POST api/<PlaneController>
        [HttpPost]
        public async Task CreatePlane([FromBody] CreatePlaneCommand dto)
        {
            await mediator.Send(dto);
        }

        // PUT api/<PlaneController>/5
        [HttpPut("{id}")]
        public async Task UpdatePlane(Guid id, [FromBody] UpdatePlaneCommand dto)
        {
            dto.Id = id;
            await mediator.Send(dto);
        }

        // DELETE api/<PlaneController>/5
        [HttpDelete("{id}")]
        public async Task DeletePlane(Guid id)
        {
            await mediator.Send(new DeletePlaneCommand(id));
        }
    }
}
