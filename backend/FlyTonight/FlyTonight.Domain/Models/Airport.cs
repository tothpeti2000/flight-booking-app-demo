
namespace FlyTonight.Domain.Models
{
    public class Airport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BlobUrl { get; set; }
        public List<Flight> InboundFlights { get; set; }
        public List<Flight> OutboundFlights { get; set; }

    }
}
