using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyTonight.DAL.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly FlyTonightDbContext context;

        public DiscountRepository(FlyTonightDbContext context)
        {
            this.context = context;
        }

        public void Add(Discount discount)
        {
            context.Discounts.Add(discount);
        }

        public void Delete(Discount discount)
        {
            context.Discounts.Remove(discount);
        }

        public async Task<IEnumerable<Discount>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Discounts
                .Include(d => d.Flights)
                .ThenInclude(f => f.From)
                .Include(d => d.Flights)
                .ThenInclude(f => f.To)
                .Include(d => d.Flights)
                .ThenInclude(f => f.Airplane)
                .Include(d => d.Flights)
                .ThenInclude(f => f.Discounts)
                .ToListAsync(cancellationToken);
        }

        public async Task<Discount> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var discount = await context.Discounts.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

            return discount ?? throw new EntityNotFoundException(typeof(Discount), id);
        }

        public void Update(Discount newDiscount)
        {
            context.Discounts.Update(newDiscount);
        }
    }
}
