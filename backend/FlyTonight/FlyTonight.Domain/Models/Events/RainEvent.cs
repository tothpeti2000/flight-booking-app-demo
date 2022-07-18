namespace FlyTonight.Domain.Models.Events
{
    [EventChance(percent: 10)]
    public class RainEvent : EnvEventBase
    {
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public int AirportDelay => 30;
        public double AverageFlightTimeIncrementRatio => 1.1;

        public RainEvent() : base(null)
        {
        }

        public RainEvent(Flight flight) : base(flight)
        {
            Start = GenerateRainStart(flight);
            Duration = GenerateRainLength();
        }

        private TimeSpan GenerateRainLength()
        {
            int number = (int)(new Random().NextDouble() * 100.0);

            return MapNumberToTime(number);
        }

        private TimeSpan MapNumberToTime(int n) => n switch
        {
            { } when (0 <= n && n < 33) => TimeSpan.FromMinutes(30),
            { } when (33 <= n && n < 66) => TimeSpan.FromMinutes(60),
            { } when (66 <= n && n < 100) => TimeSpan.FromMinutes(90),
            _ => TimeSpan.FromMinutes(30)
        };

        private DateTime GenerateRainStart(Flight flight)
        {
            var minutesOffset = (int)(flight.FlightTime * new Random().NextDouble());

            // legalább 15 percel az indulás előtt és legfeljebb 15 percel a leszállás előtt elkezd esni az eső
            return flight.TimeOfDeparture - TimeSpan.FromMinutes(15) + TimeSpan.FromMinutes(minutesOffset);
        }

        public override string ReasonMessage => "eső";

        public override string ActionMessage => "megnövekedett menetidőre";

        public override string StatusMessage => "Késésre kell számítani";

        public override string EventName => "Eső";

        public override int Delay => Flight.FlightDelay;
    }
}
