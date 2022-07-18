namespace FlyTonight.Domain.Models.Events
{
    [EventChance(percent: 3)]
    public class DeletedFlightEvent : EnvEventBase
    {
        public DeletedFlightEvent() : base(null)
        {
        }

        public DeletedFlightEvent(Flight flight) : base(flight)
        {
        }

        public override string ReasonMessage => "járat törlése";

        public override string ActionMessage => "törlésére";

        public override string StatusMessage => "Törölt járat";

        public override string EventName => "Törölt járat";

        public override int Delay => -1;
    }
}
