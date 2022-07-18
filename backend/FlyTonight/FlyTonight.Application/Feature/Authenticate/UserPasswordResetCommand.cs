using FluentValidation;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserPasswordResetValidator : AbstractValidator<UserPasswordResetCommand>
    {
        public UserPasswordResetValidator()
        {
            RuleFor(login => login.Id).NotEmpty();
            RuleFor(login => login.NewPassword).NotEmpty();
            RuleFor(login => login.ResetToken).NotEmpty();
        }
    }

    public class UserPasswordResetCommand : IRequest
    {
        public string Id { get; set; }
        public string NewPassword { get; set; }
        public string ResetToken { get; set; }
    }

    public class UserResetPasswordCommandHandler : IRequestHandler<UserPasswordResetCommand, Unit>
    {
        private readonly UserManager<User> userManager;

        public UserResetPasswordCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(UserPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);

            var result = await userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                result.Errors.ToList().ForEach(e => errors += $"{e.Description} ");
                throw new InvalidOperationException(errors);
            }

            return Unit.Value;
        }
    }
}
