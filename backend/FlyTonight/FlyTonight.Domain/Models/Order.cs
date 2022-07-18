
namespace FlyTonight.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int Price => Tickets.Sum(t => t.Price);
        public bool HasReturn => Tickets.Any(t => t.IsReturn);
        public List<Ticket> Tickets { get; set; }
    }
}
