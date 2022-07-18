using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.Authenticate
{
    public class UserGetProfileRequest : IRequest<UserGetProfileResponse>
    {
        public string Id { get; set; }
    }

    public class UserGetProfileResponse
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
        public bool NewsletterSubscription { get; set; }
    }

    public class UserGetProfileRequestHandler : IRequestHandler<UserGetProfileRequest, UserGetProfileResponse>
    {
        private readonly UserManager<User> userManager;

        public UserGetProfileRequestHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserGetProfileResponse> Handle(UserGetProfileRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            return new UserGetProfileResponse
            {
                Id = request.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (UserGetProfileResponse.UserGender)user.Gender,
                BirthDate = user.BirthDate,
                Phone = user.PhoneNumber,
                Nationality = user.Nationality,
                NewsletterSubscription = user.NewsletterSubscription
            };
        }
    }
}
