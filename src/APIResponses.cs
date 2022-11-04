using EmailSender.API.DTO_s;

namespace EmailSender.API
{
    public class APIResponses
    {
        public static EmailSenderResponse EmailSenderError(string message)
            => new EmailSenderResponse()
            {
                Success = false,
                Message = message,
                Code = string.Empty
            };

        public static EmailSenderResponse ApplicationError(string message)
            => new EmailSenderResponse()
            {
                Success = false,
                Message = $"Ocorreu um erro inesperado! Erro: {message}",
                Code = string.Empty
            };
    }
}
