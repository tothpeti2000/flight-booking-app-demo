namespace FlyTonight.Domain.Models.Events
{
    [EventChance(percent: 5)]
    public class DrunkEvent : EnvEventBase
    {
        public DrunkEvent() : base(null)
        {
        }

        public DrunkEvent(Flight flight) : base(flight)
        {
        }

        public override string ReasonMessage => "utasok késése";

        public override string ActionMessage => "későbbi indulására";

        public override string StatusMessage => "Járat indulása késik";

        public override string EventName => "Késő utas";

        public override int Delay => 30;
    }
}
