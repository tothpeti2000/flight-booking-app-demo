
namespace FlyTonight.Domain.Models
{
    public class Tax
    {
        public Guid Id { get; set; }
        public string Name  { get; set; }
        public int Value { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
