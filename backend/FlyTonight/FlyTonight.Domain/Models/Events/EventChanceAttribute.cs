namespace FlyTonight.Domain.Models.Events
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventChanceAttribute : Attribute
    {
        private int percent;

        public int Percent
        {
            get { return percent; }
        }

        public EventChanceAttribute(int percent)
        {
            this.percent = percent;
        }
    }
}
