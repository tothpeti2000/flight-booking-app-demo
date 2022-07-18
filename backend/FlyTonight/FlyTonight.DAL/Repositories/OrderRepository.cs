using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FlyTonightDbContext context;

        public OrderRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Order order)
        {
            context.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            context.Orders.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Orders.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAllForFlightAsync(Guid flightId, CancellationToken cancellationToken = default)
        {
            return await context.Orders
                .Where(o => o.Tickets.Any(t => t.Reservation.FlightId == flightId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userId, CancellationToken cancellationToken = default)
        {
            return await context.Orders
                .Include(o => o.Tickets)
                .ThenInclude(t => t.Reservation)
                .ThenInclude(r => r.Flight)
                .ThenInclude(f => f.From)
                .Include(o => o.Tickets)
                .ThenInclude(t => t.Reservation)
                .ThenInclude(r => r.Flight)
                .ThenInclude(f => f.To)
                .Include(o => o.Tickets)
                .ThenInclude(t => t.Reservation)
                .ThenInclude(r => r.Flight)
                .ThenInclude(f => f.EnvEvent)
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await context.Orders
                .Include(o => o.Tickets)
                .ThenInclude(t => t.Reservation)
                .ThenInclude(r => r.Flight)
                .SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return order ?? throw new EntityNotFoundException(typeof(Order), id);
        }

        public void Update(Order newOrder)
        {
            context.Orders.Update(newOrder);
        }
    }
}
