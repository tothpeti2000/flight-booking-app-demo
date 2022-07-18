using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models.Events;
using MediatR;

namespace FlyTonight.Application.Feature.TicketOrdering
{
    public class GetOrderDetailsRequest : IRequest<GetOrderDetailsResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderDetailsResponse
    {
        public enum TicketType
        {
            Tourist,
            Premium,
            SuperPremium
        }
        
        public class Passenger
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool IsLuggage { get; set; }
            public TicketType Type { get; set; }
            public int SeatCol { get; set; }
            public int SeatRow { get; set; }
        }

        public class Flight
        {
            public Guid Id { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public int Price { get; set; }
            public bool IsReturn { get; set; }
            public DateTime DepartureTime { get; set; }
            public DateTime ArrivalTime { get; set; }
            public IList<Passenger> Passengers { get; set; }
            public string Status { get; set; }
        }

        public Guid OrderId { get; set; }
        public IList<Flight> Flights { get; set; }
    }

    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsRequest, GetOrderDetailsResponse>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IFlightRepository flightRepository;

        public GetOrderDetailsHandler(IOrderRepository orderRepository, IFlightRepository flightRepository)
        {
            this.orderRepository = orderRepository;
            this.flightRepository = flightRepository;
        }

        public async Task<GetOrderDetailsResponse> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetAsync(request.Id, cancellationToken);
            var toFlight = await flightRepository.GetAsync(order.Tickets.First(t => !t.IsReturn).Reservation.FlightId, cancellationToken);

            GetOrderDetailsResponse response = new()
            {
                OrderId = order.Id,
                Flights = new List<GetOrderDetailsResponse.Flight>()
            };

            var flight = CreateFlightWithPassengers(order, toFlight, isReturn: false);
            response.Flights.Add(flight);

            if (order.HasReturn)
            {
                var returnFlight = await flightRepository.GetAsync(order.Tickets.First(t => t.IsReturn).Reservation.FlightId, cancellationToken);
                flight = CreateFlightWithPassengers(order, returnFlight, isReturn: true);
                response.Flights.Add(flight);
            }

            return response;
        }

        private static GetOrderDetailsResponse.Flight CreateFlightWithPassengers(Domain.Models.Order o, Domain.Models.Flight f, bool isReturn)
        {
            return new()
            {
                From = f.From.Name,
                To = f.To.Name,
                IsReturn = isReturn,
                Id = f.Id,
                DepartureTime = f.TimeOfDeparture,
                ArrivalTime = f.TimeOfDeparture.AddMinutes(f.FlightTime),
                Passengers = o.Tickets.Where(t => t.IsReturn == isReturn).Select(t =>
                {
                    return new GetOrderDetailsResponse.Passenger
                    {
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        IsLuggage = t.IsLuggage,
                        Type = (GetOrderDetailsResponse.TicketType)t.Type,
                        SeatCol = t.Reservation.ColNum,
                        SeatRow = t.Reservation.RowNum
                    };
                }).ToList(),
                Price = o.Tickets.Where(t => t.IsReturn == isReturn).Sum(t => t.Price),
                Status = f.EnvEvent is not null ? f.EnvEvent.StatusMessage : string.Empty
            };
        }
    }
}