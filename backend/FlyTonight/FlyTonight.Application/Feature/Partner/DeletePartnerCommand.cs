using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Partner
{
    public class DeletePartnerCommand : IRequest
    {
        public DeletePartnerCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, Unit>
    {
        private readonly IPartnerRepository partnerRepository;

        public DeletePartnerCommandHandler(IPartnerRepository partnerRepository)
        {
            this.partnerRepository = partnerRepository;
        }

        public Task<Unit> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            partnerRepository.Delete(new()
            {
                Id = request.Id
            });
            return Unit.Task;
        }
    }
}
