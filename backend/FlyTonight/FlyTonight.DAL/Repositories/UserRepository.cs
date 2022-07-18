using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlyTonightDbContext context;

        public UserRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await context.Users.SingleOrDefaultAsync(d => d.Id == id.ToString(), cancellationToken);

            return user ?? throw new EntityNotFoundException(typeof(User), id);
        }

        public void Update(User newUser)
        {
            context.Users.Update(newUser);

        }
    }
}
