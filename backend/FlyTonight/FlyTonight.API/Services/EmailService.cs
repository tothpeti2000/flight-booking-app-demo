using FlyTonight.API.Options;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Models;
using FlyTonight.Domain.Models.Events;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FlyTonight.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions options;
        private readonly IConfiguration configuration;

        public EmailService(IOptionsSnapshot<EmailOptions> options, IConfiguration configuration)
        {
            this.options = options.Value;
            this.configuration = configuration;
        }

        private async Task SendEmail(string email, string subject, string mailBody)
        {
            var message = new MailMessage(options.FromEmail, email, subject, mailBody);

            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            var client = new SmtpClient(options.SmtpHost, options.SmtpPort);
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.EnableSsl = false;

            await client.SendMailAsync(message);
        }

        public async Task SendEmailWithExcelAttachment(string email, string subject, string mailBody, Stream attachment)
        {
            var message = new MailMessage(options.FromEmail, email, subject, mailBody);

            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            attachment.Position = 0;
            message.Attachments.Add(new Attachment(attachment, $"weekly_report.xlsx", "application/octet-stream"));

            var client = new SmtpClient(options.SmtpHost, options.SmtpPort);
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.EnableSsl = false;

            await client.SendMailAsync(message);
        }

        public async Task ResetPasswordEmail(string email, string userId, string resetToken)
        {
            string reseturl = $"{configuration["Frontend:BaseUrl"]}/recover?id={userId}&token={resetToken}";
            string body = $@"Ön jelszó visszaállítást kért.<br>
                          <a href=""{reseturl}"">Erre a linkre</a> kattintva adhat meg új jelszót.<br><br>
                          Üdvözlettel,<br>
                          A FlyTonight csapata";

            await SendEmail(email, "Password reset", body);
        }

        public async Task SendTicketConfirmationEmail(string email, string orderId, Flight flight)
        {
            string body = $@"Tisztelt Utasunk!<br><br>
                          Köszönjük a vásárlását, reméljük kellemes útja lesz velünk.<br>
                          A vásárolt jegy részletei:<br>
                          Honnan: {flight.From.CityName}<br>
                          Hova: {flight.To.CityName}<br>
                          Mikor: {flight.TimeOfDeparture}<br><br>
                          Köszönjük, hogy a FlyTonight társasággal utazik!<br>
                          Üdvözlettel,<br>
                          A FlyTonight csapata";

            await SendEmail(email, $"Jegy foglalás {orderId}", body);
        }

        public async Task SendDelayEventEmail(string email, string flightId, EnvEventBase envType)
        {
            string body = $@"Tisztelt Utasunk!<br><br>
                          Sajnáljuk, de az {envType.ReasonMessage} miatt a {flightId} járat {envType.ActionMessage} lehet számítani.<br><br>
                          Köszönjük a megértést<br>!
                          FlyTonight csapata";

            await SendEmail(email, $"Járat változás {flightId}", body);
        }
    }
}
