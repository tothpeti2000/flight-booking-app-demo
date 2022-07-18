using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlyTonight.Application.Feature.TicketOrdering
{
    public class PlaceOrderValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderValidator()
        {
            RuleFor(o => o.ToFlight).NotNull().SetValidator(new PlaceOrderFlightValidator());
            RuleFor(o => o.ReturnFlight).SetValidator(new PlaceOrderFlightValidator());
        }
    }

    public class PlaceOrderFlightValidator : AbstractValidator<PlaceOrderCommand.Flight>
    {
        public PlaceOrderFlightValidator()
        {
            RuleFor(f => f.FlightId).NotEmpty();
            RuleForEach(f => f.Tickets).SetValidator(new PlaceOrderTickerValidator());
        }
    }

    public class PlaceOrderTickerValidator : AbstractValidator<PlaceOrderCommand.Flight.Ticket>
    {
        public PlaceOrderTickerValidator()
        {
            RuleFor(t => t.FirstName).NotEmpty();
            RuleFor(t => t.LastName).NotEmpty();
            RuleFor(t => t.Type).IsInEnum();
            RuleFor(t => t.ColNum).GreaterThan(0);
            RuleFor(t => t.RowNum).GreaterThan(0);
        }
    }

    public class PlaceOrderCommand : IRequest<PlaceOrderResponse>
    {
        public class Flight
        {
            public class Ticket
            {
                public enum TicketType
                {
                    Tourist,
                    Premium,
                    SuperPremium
                };

                public string FirstName { get; set; }
                public string LastName { get; set; }
                public TicketType Type { get; set; }
                public bool IsLuggage { get; set; }
                public int ColNum { get; set; }
                public int RowNum { get; set; }
            }

            public Guid FlightId { get; set; }
            public bool IsReturn { get; set; }
            public IList<Ticket> Tickets { get; set; }
        }

        public string UserId { get; set; }
        public Flight ToFlight { get; set; }
        public Flight ReturnFlight { get; set; }
    }
    public class PlaceOrderResponse 
    {
        public Guid OrderId { get; set; }
    }

    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PlaceOrderResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly IFlightRepository flightRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IEmailService emailService;

        public PlaceOrderCommandHandler(UserManager<User> userManager, IFlightRepository flightRepository, IOrderRepository orderRepository, IEmailService emailService)
        {
            this.userManager = userManager;
            this.flightRepository = flightRepository;
            this.orderRepository = orderRepository;
            this.emailService = emailService;
        }

        public async Task<PlaceOrderResponse> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            Order order = new Order
            {
                DateOfPurchase = DateTime.Now,
                User = user,
                Tickets = await CreateTicketsFromFlightDto(request.ToFlight, cancellationToken)
            };

            if (request.ReturnFlight != null)
            {
                order.Tickets.AddRange(await CreateTicketsFromFlightDto(request.ReturnFlight, cancellationToken));
            }

            orderRepository.Add(order);

            var flight = await flightRepository.GetAsync(request.ToFlight.FlightId, cancellationToken);
            await emailService.SendTicketConfirmationEmail(user.Email, order.Id.ToString(), flight);

            return new()
            {
                OrderId = order.Id
            };
        }

        private async Task<List<Ticket>> CreateTicketsFromFlightDto(PlaceOrderCommand.Flight toFlight, CancellationToken cancellationToken)
        {
            var flight = await flightRepository.GetAsync(toFlight.FlightId, cancellationToken);

            return toFlight.Tickets.Select(ticketDto =>
            {
                flight.Airplane.ValidateSeat(ticketDto.ColNum, ticketDto.RowNum);

                var ticket = new Ticket
                {
                    DateOfPurchase = DateTime.Now,
                    FirstName = ticketDto.FirstName,
                    LastName = ticketDto.LastName,
                    IsLuggage = ticketDto.IsLuggage,
                    IsReturn = toFlight.IsReturn,
                    Type = (Domain.Enums.TicketType)ticketDto.Type,
                    Reservation = new SeatReservation
                    {
                        FlightId = toFlight.FlightId,
                        Flight = flight,
                        ColNum = ticketDto.ColNum,
                        RowNum = ticketDto.RowNum,
                    },
                };
                ticket.SetTicketPrice();

                return ticket;

            }).ToList();
        }
    }
}