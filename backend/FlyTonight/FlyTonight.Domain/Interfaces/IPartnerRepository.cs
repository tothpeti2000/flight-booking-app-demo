using FlyTonight.Domain.Models;

namespace FlyTonight.Domain.Interfaces
{
    public interface IPartnerRepository
    {
        public void Add(Partner partner);
        public Task<Partner> GetAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Partner>> GetAllAsync(CancellationToken cancellationToken = default);
        public void Update(Partner newPartner);
        public void Delete(Partner partner);
    }
}
