using FluentValidation;
using FlyTonight.Domain.Enums;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Gender).IsInEnum();
            RuleFor(u => u.BirthDate).NotNull();
            RuleFor(u => u.Phone).NotEmpty();
            RuleFor(u => u.Nationality).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
        }
    }

    public class UserUpdateCommand : IRequest
    {
        public enum UserGender
        {
            Man,
            Woman
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserGender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string Password { get; set; }
        public bool NewsletterSubscription { get; set; }
    }

    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, Unit>
    {
        private readonly UserManager<User> userManager;

        public UserUpdateCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            var valid = await userManager.CheckPasswordAsync(user, request.Password);

            if (!valid)
            {
                throw new InvalidOperationException("Wrong password");
            }

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = (Gender)request.Gender;
            user.BirthDate = request.BirthDate;
            user.PhoneNumber = request.Phone;
            user.Nationality = request.Nationality;
            user.NewsletterSubscription = request.NewsletterSubscription;

            return Unit.Value;
        }
    }
}
