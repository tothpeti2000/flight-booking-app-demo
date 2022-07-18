using FlyTonight.Domain.Extensions;
using FlyTonight.Domain.Models.Events;

namespace FlyTonight.Domain.Models
{
    public class Flight
    {
        const double MINUTE_TO_HOUR_CONVERSION_RATIO = 60;
        const int KM_PRICE = 50;
        const int TAKEOFF_TIME = 30;
        const int LANDING_TIME = 30;

        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Airport From { get; set; }
        public Guid ToId { get; set; }
        public Airport To { get; set; }
        public Guid AirplaneId { get; set; }
        public Plane Airplane { get; set; }
        public Guid DebuffId { get; set; }
        public EnvEventBase EnvEvent { get; set; }

        public DateTime TimeOfDeparture { get; set; }
        public DateTime ActualTimeOfDeparture => EnvEvent is DrunkEvent ? TimeOfDeparture.AddMinutes(30) : TimeOfDeparture;
        public DateTime ActualArrivalTime => ActualTimeOfDeparture.AddMinutes(FlightTime);
        public int FlightTime => CalculateFlightTimeWithDebuff();
        public int BaseFlightTime => (int)((TravelDistance / Airplane.MetricCruiseSpeed) * MINUTE_TO_HOUR_CONVERSION_RATIO + Airplane.TakeOffAndLandingTimeMinutes);
        public int FlightDelay => FlightTime - BaseFlightTime;
        public int TravelDistance => From.DistanceFrom(To);
        public int KmPrice => KM_PRICE;
        public float BookedSeatsToPlaneRatio => (float)Reservations.Count() / Airplane.SeatCount;
        public int Price => (int)((TravelDistance * KM_PRICE) * (1 - Discounts.Sum(d => d.Value)));

        public List<Discount> Discounts { get; set; }
        public List<Tax> Taxes { get; set; }
        public List<SeatReservation> Reservations { get; set; }

        private int CalculateFlightTimeWithDebuff()
        {
            return EnvEvent switch
            {
                RainEvent rain => CalculateRainDelay(rain),
                DrunkEvent drunk => BaseFlightTime + drunk.Delay,
                _ => BaseFlightTime,
            };
        }

        private int CalculateRainDelay(RainEvent rain)
        {
            var result = (double)BaseFlightTime;
            var arrivalTime = TimeOfDeparture.AddMinutes(result);
            var rainEnd = rain.Start + rain.Duration;

            result *= rain.AverageFlightTimeIncrementRatio;

            if (rainEnd >= arrivalTime - TimeSpan.FromMinutes(LANDING_TIME))
            {
                result += rain.AirportDelay;
            }

            if (rain.Start <= TimeOfDeparture.AddMinutes(TAKEOFF_TIME))
            {
                result += rain.AirportDelay;
            }

            return (int)result;
        }
    }
}
