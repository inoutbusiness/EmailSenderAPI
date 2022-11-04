using EmailSender.API.Exceptions.Enums;

namespace EmailSender.API.Exceptions
{
    public class EmailSenderException : Exception
    {
        public EmailSenderException() { }

        public EmailSenderException(string message)
            : base(message) { }

        public EmailSenderException(eEmailSenderExceptionType type, string message)
            : base($"Não foi possível concluir a operação, Tipo do erro: {nameof(type)}. Erro: {message}") { }

        public EmailSenderException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
