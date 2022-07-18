using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly FlyTonightDbContext context;

        public PartnerRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Partner partner)
        {
            context.Partners.Add(partner);
        }

        public void Delete(Partner partner)
        {
            context.Partners.Remove(partner);
        }

        public async Task<IEnumerable<Partner>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Partners.ToListAsync(cancellationToken);
        }

        public async Task<Partner> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var partner = await context.Partners.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return partner ?? throw new EntityNotFoundException(typeof(Partner), id);
        }

        public void Update(Partner newPartner)
        {
            context.Partners.Update(newPartner);
        }
    }
}
