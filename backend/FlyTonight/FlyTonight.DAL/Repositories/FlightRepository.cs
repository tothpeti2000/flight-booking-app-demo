using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlyTonightDbContext context;

        public FlightRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Flight flight)
        {
            context.Flights.Add(flight);
        }

        public void Delete(Flight flight)
        {
            context.Flights.Remove(flight);
        }

        public async Task<IEnumerable<Flight>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Flights.ToListAsync(cancellationToken);
        }

        public async Task<List<Flight>> GetFlightsBetweenDates(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await context.Flights
                .Include(x => x.Airplane)
                .Include(x => x.From)
                .Include(x => x.To)
                .Include(x => x.Discounts)
                .Include(x => x.Taxes)
                .Include(x => x.Reservations)
                .Include(x => x.EnvEvent)
                .Where(f => f.TimeOfDeparture.Date >= startDate.Date && f.TimeOfDeparture.Date <= endDate.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Flight>> GetAllByDate(DateTime date, CancellationToken cancellationToken = default)
        {
            var flight = await context.Flights
                .Include(x => x.Airplane)
                .Include(x => x.From)
                .Include(x => x.To)
                .Include(x => x.Discounts)
                .Include(x => x.Taxes)
                .Include(x => x.Reservations)
                .Include(x => x.EnvEvent)
                .Where(d => d.TimeOfDeparture.Date == date.Date)
                .ToListAsync(cancellationToken);

            return flight;
        }

        public async Task<Flight> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var flight = await context.Flights
                .Include(x => x.Airplane)
                .Include(x => x.From)
                .Include(x => x.To)
                .Include(x => x.Discounts)
                .Include(x => x.Taxes)
                .Include(x => x.Reservations)
                .Include(x => x.EnvEvent)
                .SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return flight ?? throw new EntityNotFoundException(typeof(Flight), id);
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync(string from, string to, DateTime departureTime, CancellationToken cancellationToken = default)
        {
            return await context.Flights
                .Include(x => x.Airplane)
                .Include(x => x.From)
                .Include(x => x.To)
                .Include(x => x.Discounts)
                .Include(x => x.Taxes)
                .Include(x => x.Reservations)
                .Include(x => x.EnvEvent)
                .Where(f => f.From.Name == from && f.To.Name == to && f.TimeOfDeparture.Date == departureTime.Date)
                .ToListAsync(cancellationToken);
        }

        public void Update(Flight newFlight)
        {
            context.Flights.Update(newFlight);
        }
    }
}
