using FlyTonight.DAL.Data;
using FlyTonight.Domain.Interfaces;

namespace FlyTonight.DAL.Repositories
{
    public class UnitWork : IUnitWork
    {
        private readonly FlyTonightDbContext dbContext;

        public UnitWork(FlyTonightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
            {
                var entity = ex.Entries.FirstOrDefault();
                throw new InvalidOperationException($"Invalid {entity?.Entity.GetType().Name} id: {entity?.Property("Id").OriginalValue}", ex);
            }
        }
    }
}
