namespace FlyTonight.DAL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, Guid id)
            : base($"No {entityType.Name} was found in database with id {id}") { }
    }
}
