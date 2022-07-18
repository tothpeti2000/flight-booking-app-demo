
namespace FlyTonight.Domain.Models
{
    public class Plane
    {
        const double MACH_TO_KMH_CONVERSION_RATIO = 1234.8;
        const double TAKE_OFF_AND_LANDING_TIME_MINUTES = 60;

        public Guid Id { get; set; }
        public string Registration { get; set; }
        public string Type { get; set; }
        public double FuselageLength { get; set; }
        public double Wingspan { get; set; }
        public int SeatCount => SeatColCount * SeatRowCount;
        public int SeatColCount { get; set; }
        public int SeatRowCount { get; set; }
        public int FlightDistance { get; set; }
        public int CruiseHeight { get; set; }
        public double CruiseSpeed { get; set; }
        public double MetricCruiseSpeed => CruiseSpeed * MACH_TO_KMH_CONVERSION_RATIO;
        public List<Flight> Flights { get; set; }
        public double TakeOffAndLandingTimeMinutes => TAKE_OFF_AND_LANDING_TIME_MINUTES;

        public void ValidateSeat(int colNum, int rowNum)
        {
            if (colNum <= 0 || rowNum <= 0 || SeatColCount < colNum || SeatRowCount < rowNum)
            {
                throw new ArgumentException("Invalid seat reservation.");
            }
        }
    }
}
