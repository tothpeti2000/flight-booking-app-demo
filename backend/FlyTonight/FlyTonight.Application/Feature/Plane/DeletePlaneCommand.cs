using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Plane
{
    public class DeletePlaneCommand : IRequest
    {
        public DeletePlaneCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeletePlaneCommandHandler : IRequestHandler<DeletePlaneCommand, Unit>
    {
        private readonly IPlaneRepository planeRepository;

        public DeletePlaneCommandHandler(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }

        public Task<Unit> Handle(DeletePlaneCommand request, CancellationToken cancellationToken)
        {
            planeRepository.Delete(new()
            {
                Id = request.Id
            });
            return Unit.Task;
        }
    }
}
