using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Tax
{
    public class DeleteTaxCommand : IRequest
    {
        public DeleteTaxCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }

    public class DeleteTaxCommandHandler : IRequestHandler<DeleteTaxCommand, Unit>
    {
        private readonly ITaxRepository taxRepository;

        public DeleteTaxCommandHandler(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
        }

        public Task<Unit> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
        {
            taxRepository.Delete(new()
            {
                Id = request.Id
            });
            return Unit.Task;
        }
    }
}
