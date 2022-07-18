
namespace FlyTonight.Domain.Models
{
    public class SeatReservation
    {
        public Guid Id { get; set; }
        public int ColNum { get; set; }
        public int RowNum { get; set; }
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
