using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Flight
{
    public class DeleteFlightCommand : IRequest
    {
        public DeleteFlightCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;

        public DeleteFlightCommandHandler(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public Task<Unit> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            flightRepository.Delete(new()
            {
                Id = request.Id
            });
            return Unit.Task;
        }
    }
}
