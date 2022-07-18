using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly FlyTonightDbContext context;

        public TaxRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Tax tax)
        {
            context.Taxes.Add(tax);
        }

        public void Delete(Tax tax)
        {
            context.Taxes.Remove(tax);
        }

        public async Task<IEnumerable<Tax>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Taxes.ToListAsync(cancellationToken);
        }

        public async Task<Tax> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tax = await context.Taxes.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return tax ?? throw new EntityNotFoundException(typeof(Tax), id);
        }

        public void Update(Tax newTax)
        {
            context.Taxes.Update(newTax);
        }
    }
}
