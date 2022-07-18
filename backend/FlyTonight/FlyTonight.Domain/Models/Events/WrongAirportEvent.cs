namespace FlyTonight.Domain.Models.Events
{
    [EventChance(percent: 4)]
    public class WrongAirportEvent : EnvEventBase
    {
        public WrongAirportEvent() : base(null)
        {
        }

        public WrongAirportEvent(Flight flight) : base(flight)
        {
        }

        public override string ReasonMessage => "járat elterelése";

        public override string ActionMessage => "törlésére";

        public override string StatusMessage => "Törölt járat";

        public override string EventName => "Terelt járat";

        public override int Delay => -1;
    }
}
