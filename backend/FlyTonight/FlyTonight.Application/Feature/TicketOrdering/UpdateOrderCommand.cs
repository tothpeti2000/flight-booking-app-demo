using FluentValidation;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using MediatR;
using System.Linq;

namespace FlyTonight.Application.Feature.TicketOrdering
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(o => o.FlightId).NotEmpty();
            RuleForEach(o => o.Tickets).SetValidator(new UpdateOrderTickerValidator());
        }
    }

    public class UpdateOrderTickerValidator : AbstractValidator<UpdateOrderCommand.Ticket>
    {
        public UpdateOrderTickerValidator()
        {
            RuleFor(t => t.FirstName).NotEmpty();
            RuleFor(t => t.LastName).NotEmpty();
            RuleFor(t => t.Type).IsInEnum();
            RuleFor(t => t.ColNum).GreaterThan(0);
            RuleFor(t => t.RowNum).GreaterThan(0);
        }
    }

    public class UpdateOrderCommand : IRequest
    {
        public enum TicketType
        {
            Tourist,
            Premium,
            SuperPremium
        }

        public class Ticket
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public TicketType Type { get; set; }
            public bool IsLuggage { get; set; }
            public int ColNum { get; set; }
            public int RowNum { get; set; }
        }

        public Guid OrderId { get; set; }
        public Guid FlightId { get; set; }
        public bool IsReturn { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }

    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;
        private readonly IOrderRepository orderRepository;

        public UpdateOrderHandler(IFlightRepository flightRepository, IOrderRepository orderRepository)
        {
            this.flightRepository = flightRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetAsync(request.OrderId, cancellationToken);
            var flight = await flightRepository.GetAsync(request.FlightId, cancellationToken);

            // Remove tickets we want to update, toFlight or returnFlight tickets
            order.Tickets.RemoveAll(t => t.IsReturn == request.IsReturn);

            //update and add new tickets
            var newTickets = request.Tickets.Select(ticketDto =>
            {
                flight.Airplane.ValidateSeat(ticketDto.ColNum, ticketDto.RowNum);

                Ticket ticket = new Ticket
                {
                    DateOfPurchase = DateTime.Now,
                    FirstName = ticketDto.FirstName,
                    LastName = ticketDto.LastName,
                    IsLuggage = ticketDto.IsLuggage,
                    IsReturn = request.IsReturn,
                    Type = (Domain.Enums.TicketType)ticketDto.Type,
                    Reservation = new SeatReservation
                    {
                        FlightId = request.FlightId,
                        Flight = flight,
                        ColNum = ticketDto.ColNum,
                        RowNum = ticketDto.RowNum,
                    }
                };
                ticket.SetTicketPrice();

                return ticket;
            });

            order.Tickets.AddRange(newTickets);

            return Unit.Value;
        }
    }
}
