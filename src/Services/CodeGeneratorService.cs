using EmailSender.API.DTO_s;
using EmailSender.API.Services.Interfaces;
using System.Security.Cryptography;

namespace EmailSender.API.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        public string GenerateEmailCode(EmailCodeConfigDto config)
        {
            var code = string.Empty;

            for (int i = 0; i < config.NumberDigits; i++)
                code += RandomNumberGenerator.GetInt32(0, 10);

            return code;
        }
    }
}
