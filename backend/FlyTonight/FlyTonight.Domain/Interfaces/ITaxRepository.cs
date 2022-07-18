using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface ITaxRepository
    {
        public void Add(Tax tax);
        public Task<Tax> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Tax>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(Tax newTax);
        public void Delete(Tax tax);
    }
}
