using FlyTonight.Domain.Enums;
using FlyTonight.Domain.Extensions;

namespace FlyTonight.Domain.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int Price { get; set; }
        public bool IsLuggage { get; set; }
        public bool IsReturn { get; set; }
        public TicketType Type { get; set; }
        public Order Order { get; set; }
        public SeatReservation Reservation { get; set; }

        public void SetTicketPrice()
        {
            var flight = Reservation.Flight;
            int distance = flight.From.DistanceFrom(flight.To);

            int price = CalculateBasePrice(distance);

            price = ModifyPriceBasedOnClass(price);

            int checkInPrice = CalculateCheckInPrice(distance);

            price = ModifyPriceBasedOnDepartureTime(price);

            int discount = CalculateDiscount(price);
            int tax = CalculateTax();

            Price = price + checkInPrice + tax - discount;
        }

        private int CalculateBasePrice(int flightDistance)
        {
            return flightDistance * Reservation.Flight.KmPrice;
        }

        private int ModifyPriceBasedOnClass(int basePrice) => Type switch
        {
            TicketType.Tourist => basePrice,
            TicketType.Premium => (int)(basePrice * 1.3f),
            TicketType.SuperPremium => (int)(basePrice * 1.55f),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), $"Not expected TicketType value: {Type}")
        };

        private int CalculateCheckInPrice(int flightDistance)
        {
            if (IsLuggage && Type != TicketType.SuperPremium && flightDistance <= 1400)
            {
                return flightDistance * 5;
            }

            return 0;
        }

        private int ModifyPriceBasedOnDepartureTime(int basePrice)
        {
            var timeToDepart = Reservation.Flight.TimeOfDeparture - DateTime.Now;

            if (timeToDepart.Days <= 5 && Reservation.Flight.BookedSeatsToPlaneRatio < 0.5f)
            {
                return (int)(basePrice * 0.6f);
            }
            else if (timeToDepart.TotalDays > 60)
            {
                return (int)(basePrice * 0.6f);
            }
            else if (timeToDepart.TotalDays > 28 && timeToDepart.TotalDays < 42)
            {
                return (int)(basePrice * 0.8f);
            }
            else
            {
                return basePrice;
            }
        }

        private int CalculateDiscount(int basePrice)
        {
            double discountTemp = Reservation.Flight.Discounts.Sum(d => d.Value);
            return (int)(basePrice * discountTemp);
        }

        private int CalculateTax()
        {
            return Reservation.Flight.Taxes.Sum(t => t.Value);
        }
    }
}