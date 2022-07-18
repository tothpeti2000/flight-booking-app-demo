namespace FlyTonight.Domain.Models.Events
{
    [EventChance(percent: 4)]
    public class ProtestingEvent : EnvEventBase
    {
        public ProtestingEvent() : base(null)
        {
        }

        public ProtestingEvent(Flight flight) : base(flight)
        {
        }

        public override string ReasonMessage => "személyzet sztrájkolása";

        public override string ActionMessage => "törlésére";

        public override string StatusMessage => "Törölt járat";

        public override string EventName => "Sztrájk";

        public override int Delay => -1;
    }
}
