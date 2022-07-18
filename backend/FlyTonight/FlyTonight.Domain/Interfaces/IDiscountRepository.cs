using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IDiscountRepository
    {
        public void Add(Discount discount);
        public Task<Discount> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Discount>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(Discount newDiscount);
        public void Delete(Discount discount);
    }
}
