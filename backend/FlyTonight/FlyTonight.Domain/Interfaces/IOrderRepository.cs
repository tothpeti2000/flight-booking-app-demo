using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public void Add(Order order);
        public Task<Order> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order>> GetUserOrders(string userId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order>> GetAllForFlightAsync(Guid flightId, CancellationToken cancellationToken = default);
        public void Update(Order newOrder);
        public void Delete(Order order);
    }
}
