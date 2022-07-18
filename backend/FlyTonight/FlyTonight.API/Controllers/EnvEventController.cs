using FlyTonight.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvEventController : ControllerBase
    {
        private readonly IMediator mediator;

        public EnvEventController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("generate/events")]
        public async Task TriggerEventGeneration([FromBody] EnvEventCommand dto)
        {
            await mediator.Send(dto);
        }

        [HttpPost("generate/sheet")]
        public async Task TriggerSpreadheetGeneration([FromBody] EventSpreadsheetCommand dto)
        {
            await mediator.Send(dto);
        }
    }
}
