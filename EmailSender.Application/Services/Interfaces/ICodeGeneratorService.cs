using EmailSender.Application.DTO_s;

namespace EmailSender.Application.Services.Interfaces
{
    public interface ICodeGeneratorService
    {
        string GenerateEmailCode(EmailCodeConfigDto config);
    }
}