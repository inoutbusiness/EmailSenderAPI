using EmailSender.API.DTO_s;

namespace EmailSender.API.Services.Interfaces
{
    public interface ICodeGeneratorService
    {
        string GenerateEmailCode(EmailCodeConfigDto config);
    }
}