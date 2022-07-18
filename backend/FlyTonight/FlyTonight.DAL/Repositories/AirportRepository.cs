using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly FlyTonightDbContext context;

        public AirportRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Airport airport)
        {
            context.Airports.Add(airport);
        }

        public void Delete(Airport airport)
        {
            context.Airports.Remove(airport);
        }

        public async Task<IEnumerable<Airport>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Airports.ToListAsync(cancellationToken);
        }

        public async Task<Airport> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var airport = await context.Airports.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return airport ?? throw new EntityNotFoundException(typeof(Airport), id);
        }

        public void Update(Airport newAirport)
        {
            context.Airports.Update(newAirport);
        }
    }
}
