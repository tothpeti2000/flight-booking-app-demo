using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class PlaneRepository : IPlaneRepository
    {
        private readonly FlyTonightDbContext context;

        public PlaneRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Plane plane)
        {
            context.Planes.Add(plane);
        }

        public void Delete(Plane plane)
        {
            context.Planes.Remove(plane);
        }

        public async Task<IEnumerable<Plane>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Planes.ToListAsync(cancellationToken);
        }

        public async Task<Plane> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var plane = await context.Planes
                .Include(p => p.Flights)
                .ThenInclude(f => f.Reservations)
                .Include(p => p.Flights)
                .ThenInclude(f => f.From)
                .Include(p => p.Flights)
                .ThenInclude(f => f.To)
                .Include(p => p.Flights)
                .ThenInclude(f => f.EnvEvent)
                .SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return plane ?? throw new EntityNotFoundException(typeof(Plane), id);
        }

        public void Update(Plane newPlane)
        {
            context.Planes.Update(newPlane);
        }
    }
}
