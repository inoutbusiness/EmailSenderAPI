using EmailSender.API.DTO_s;

namespace EmailSender.API.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task<EmailSenderResponse> SendEmailAsync(EmailSenderRequest request);
    }
}