using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserPasswordRecoverValidator : AbstractValidator<UserPasswordRecoverCommand>
    {
        public UserPasswordRecoverValidator()
        {
            RuleFor(login => login.Email).NotEmpty().EmailAddress();
        }
    }

    public class UserPasswordRecoverCommand : IRequest
    {
        public string Email { get; set; }
    }

    public class UserPasswordResetRequestHandler : IRequestHandler<UserPasswordRecoverCommand, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;

        public UserPasswordResetRequestHandler(UserManager<User> userManager, IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }

        public async Task<Unit> Handle(UserPasswordRecoverCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            await emailService.ResetPasswordEmail(user.Email, user.Id, token);

            return Unit.Value;
        }
    }
}
