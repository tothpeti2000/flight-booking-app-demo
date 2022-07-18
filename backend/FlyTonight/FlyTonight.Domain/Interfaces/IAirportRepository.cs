using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IAirportRepository
    {
        public void Add(Airport airport);
        public Task<Airport> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Airport>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(Airport newAirport);
        public void Delete(Airport airport);
    }
}
