namespace FlyTonight.Domain.Models.Events
{
    public abstract class EnvEventBase
    {
        public Guid Id { get; set; }
        public abstract string ReasonMessage { get; }
        public abstract string ActionMessage { get; }
        public abstract string StatusMessage { get; }
        public abstract string EventName { get; }
        public abstract int Delay { get; }
        public string DelayText => Delay < 0 ? "törölve" : Delay.ToString();

        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }

        protected EnvEventBase(Flight flight)
        {
            Flight = flight;
        }
    }
}
