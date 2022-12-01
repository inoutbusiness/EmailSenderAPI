using EmailSender.Application.DTO_s;
using EmailSender.Application.Exceptions;
using EmailSender.Application.Exceptions.Enums;
using EmailSender.Application.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace EmailSender.Application.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public void SendEmail(EmailSenderRequest request)
        {
            try
            {
                var emailMessage = BuildEmailMessageWithCode(request);
                SendEmailAsyncBySmtpClient(emailMessage, request);
            }
            catch (EmailSenderException emailSenderEx)
            {
                throw new EmailSenderException(emailSenderEx.Message);
            }
            catch (Exception ex)
            {
                throw new EmailSenderException(ex.Message);
            }
        }

        private MimeMessage BuildEmailMessageWithCode(EmailSenderRequest request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(request.EmailFrom));
                email.To.Add(MailboxAddress.Parse(request.EmailTo));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = HtmlEmailBuilder(request.EmailTo, request.RecoveryToken) };

                return email;
            }
            catch (EmailSenderException ex)
            {
                throw new EmailSenderException(eEmailSenderExceptionType.Building, ex.Message);
            }
        }

        private void SendEmailAsyncBySmtpClient(MimeMessage emailMessage, EmailSenderRequest request)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(request.AuthenticateInfo.EmailAuth, request.AuthenticateInfo.PasswordAuth);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
            catch (EmailSenderException ex)
            {
                throw new EmailSenderException(eEmailSenderExceptionType.Sending, ex.Message);
            }
        }

        private string HtmlEmailBuilder(string email, string generateCode)
        {
            return new StringBuilder()
                .Append($"<h2>Olá {email},</h2><br>")
                .AppendLine("<p>Esqueceu sua senha? Sem problemas, vamos redefinir <img src=\"https://html-online.com/editor/tiny4_9_11/plugins/emoticons/img/smiley-wink.gif\" alt=\"wink\" /></p>")
                .AppendLine($"<p><strong>Segue código para redefinição da sua senha:</strong> <strong><span style=\"color: #ff0000;\">{generateCode}</span></strong></p><br>")
                .AppendLine("<p><span style=\"background - color: #999999;\">Caso a redefinição de senha não tenha sido solicitada, desconsidere este email!</span></p>")
                .AppendLine("<p><span style=\"background - color: #999999;\">Email gerado automáticamente, por favor, não responda.</span></p>").ToString();
        }
    }
}