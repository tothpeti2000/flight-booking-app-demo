using FlyTonight.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Gender Gender { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Nationality { get; set; }
        
        public bool NewsletterSubscription { get; set; }
        
        public List<Order> Orders { get; set; }
    }
}
