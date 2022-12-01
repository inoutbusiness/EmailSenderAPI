using EmailSender.Application.DTO_s;

namespace EmailSender.Application.Services.Interfaces
{
    public interface IEmailSenderService
    {
        void SendEmail(EmailSenderRequest request);
    }
}