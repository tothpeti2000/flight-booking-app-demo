using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(login => login.Email).NotEmpty().EmailAddress();
            RuleFor(login => login.Password).NotEmpty();
        }
    }

    public class UserLoginRequest : IRequest<UserLoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public string Token { get; set; }
    }

    public class UserLoginRequestHandler : IRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenGenerator tokenGenerator;

        public UserLoginRequestHandler(UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email) ?? throw new ArgumentException("User not found by the given email address");
            var result = await userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                throw new InvalidOperationException("Wrong password");
            }

            return new UserLoginResponse() { Token = tokenGenerator.GenerateToken(user.Id) };
        }
    }
}
