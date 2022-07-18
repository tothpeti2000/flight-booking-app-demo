using FluentValidation;
using FlyTonight.Domain.Enums;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationCommand>
    {
        public UserRegistrationValidator()
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

    public class UserRegistrationCommand : IRequest
    {
        public enum UserGender
        {
            Man,
            Woman
        }

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

    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Unit>
    {
        private readonly UserManager<User> userManager;

        public UserRegistrationCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            User user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = (Gender)request.Gender,
                BirthDate = request.BirthDate,
                PhoneNumber = request.Phone,
                Nationality = request.Nationality,
                NewsletterSubscription = request.NewsletterSubscription
            };

            var result = await userManager.CreateAsync(user, request.Password);
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
