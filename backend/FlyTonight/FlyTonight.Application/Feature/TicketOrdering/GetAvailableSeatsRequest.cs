using FluentValidation;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.TicketOrdering
{
    public class GetAvailableSeatsValidator : AbstractValidator<GetAvailableSeatsRequest>
    {
        public GetAvailableSeatsValidator()
        {
            RuleFor(f => f.PlaneId).NotEmpty();
            RuleFor(f => f.FlightId).NotEmpty();
        }
    }

    public class GetAvailableSeatsRequest : IRequest<GetAvailableSeatsResponse>
    {
        public Guid PlaneId { get; set; }
        public Guid FlightId { get; set; }
    }

    public class GetAvailableSeatsResponse
    {
        public class Seat
        {
            public int ColNum { get; set; }
            public int RowNum { get; set; }
        }

        public string PlaneType { get; set; }
        public int SeatColCount { get; set; }
        public int SeatRowCount { get; set; }
        public IList<Seat> BookedSeats { get; set; }
    }

    public class GetAvailalbeSeatsHandler : IRequestHandler<GetAvailableSeatsRequest, GetAvailableSeatsResponse>
    {
        private readonly IFlightRepository flightRepository;

        public GetAvailalbeSeatsHandler(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task<GetAvailableSeatsResponse> Handle(GetAvailableSeatsRequest request, CancellationToken cancellationToken)
        {
            var flight = await flightRepository.GetAsync(request.FlightId, cancellationToken);

            return new()
            {
                PlaneType = flight.Airplane.Type,
                SeatColCount = flight.Airplane.SeatColCount,
                SeatRowCount = flight.Airplane.SeatRowCount,
                BookedSeats = flight.Reservations.Select(s =>
                {
                    return new GetAvailableSeatsResponse.Seat
                    {
                        ColNum = s.ColNum,
                        RowNum = s.RowNum
                    };
                }).ToList()
            };
        }
    }
}