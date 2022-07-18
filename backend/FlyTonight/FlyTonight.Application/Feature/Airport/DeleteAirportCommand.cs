using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Airport
{
    public class DeleteAirportCommand : IRequest
    {
        public DeleteAirportCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand, Unit>
    {
        private readonly IAirportRepository airportRepository;

        public DeleteAirportCommandHandler(IAirportRepository airportRepository)
        {
            this.airportRepository = airportRepository;
        }

        public Task<Unit> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
        {
            airportRepository.Delete(new()
            {
                Id = request.Id
            });
            return Unit.Task;
        }
    }
}
