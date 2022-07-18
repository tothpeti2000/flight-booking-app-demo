namespace FlyTonight.Application.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string userId);
    }
}
