
namespace EmailSender.API.DTO_s
{
    public class EmailSenderRequest
    {
        public string Subject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public EmailCodeConfigDto CodeConfig { get; set; }
        public EmailAuthenticateInfoDto AuthenticateInfo { get; set; }
    }
}
