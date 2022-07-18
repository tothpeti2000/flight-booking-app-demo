using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IUserRepository
    {
        public void Add(User user);
        public Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(User newUser);
        public void Delete(User user);
    }
}
