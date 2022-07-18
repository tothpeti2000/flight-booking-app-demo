
namespace FlyTonight.Domain.Models
{
    public class Discount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string BlobUrl { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
