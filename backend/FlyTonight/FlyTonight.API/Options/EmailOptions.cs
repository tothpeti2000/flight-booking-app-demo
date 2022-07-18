namespace FlyTonight.API.Options
{
    public class EmailOptions
    {
        public const string Email = "Email";

        public string SmtpHost { get; set; } = String.Empty;
        public int SmtpPort { get; set; } = 587;
        public string FromEmail { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
