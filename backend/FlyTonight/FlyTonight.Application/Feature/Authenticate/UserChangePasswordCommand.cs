using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordCommand>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(login => login.NewPassword).NotEqual(login => login.Password);
        }
    }

    public class UserChangePasswordCommand : IRequest<UserChangePasswordResponse>
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }

    public class UserChangePasswordResponse
    {
        public string Token { get; set; }
    }

    public class UserChangePasswordHandler : IRequestHandler<UserChangePasswordCommand, UserChangePasswordResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenGenerator tokenGenerator;

        public UserChangePasswordHandler(UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<UserChangePasswordResponse> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            var result = await userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

            if (!result.Succeeded)
            {
                string errors = string.Empty;
                result.Errors.ToList().ForEach(e => errors += $"{e.Description} ");
                throw new InvalidOperationException(errors);
            }

            return new UserChangePasswordResponse() { Token = tokenGenerator.GenerateToken(request.Id) };
        }
    }
}
