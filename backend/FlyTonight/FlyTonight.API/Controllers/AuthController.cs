using FlyTonight.Application.Feature.Authenticate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task RegisterUser([FromBody] UserRegistrationCommand dto)
        {
            await mediator.Send(dto);
        }

        [HttpPost("login")]
        public async Task<UserLoginResponse> LoginUser([FromBody] UserLoginRequest dto)
        {
            return await mediator.Send(dto);
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task EditProfile([FromBody] UserUpdateCommand dto)
        {
            dto.Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await mediator.Send(dto);
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<UserChangePasswordResponse> ChangePassword([FromBody] UserChangePasswordCommand dto)
        {
            dto.Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await mediator.Send(dto);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<UserGetProfileResponse> GetUserProfile()
        {
            var dto = new UserGetProfileRequest() { Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value };
            return await mediator.Send(dto);
        }

        [HttpPost("recover")]
        public async Task RequestPasswordReset([FromBody] UserPasswordRecoverCommand dto)
        {
            await mediator.Send(dto);
        }

        [HttpPost("reset")]
        public async Task Resetpassword(UserPasswordResetCommand dto)
        {
            await mediator.Send(dto);
        }
    }
}
