using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IPlaneRepository
    {
        public void Add(Plane plane);
        public Task<Plane> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Plane>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(Plane newPlane);
        public void Delete(Plane plane);
    }
}
