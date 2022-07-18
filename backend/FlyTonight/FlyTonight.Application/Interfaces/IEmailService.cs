using FlyTonight.Domain.Models;
using FlyTonight.Domain.Models.Events;

namespace FlyTonight.Application.Interfaces
{
    public interface IEmailService
    {
        public Task SendTicketConfirmationEmail(string email, string orderId, Flight flight);
        public Task ResetPasswordEmail(string email, string userId, string resetToken);
        public Task SendDelayEventEmail(string email, string flightId, EnvEventBase envType);
        public Task SendEmailWithExcelAttachment(string email, string subject, string mailBody, Stream attachment);
    }
}
