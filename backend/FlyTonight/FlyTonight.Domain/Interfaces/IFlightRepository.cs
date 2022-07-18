using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IFlightRepository
    {
        public Task<IEnumerable<Flight>> GetFlightsAsync(string from, string to, DateTime departureTime, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Flight>> GetAllByDate(DateTime date, CancellationToken cancellationToken = default);
        public Task<List<Flight>> GetFlightsBetweenDates(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        public void Add(Flight flight);
        public Task<Flight> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Flight>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(Flight newFlight);
        public void Delete(Flight flight);
    }
}
