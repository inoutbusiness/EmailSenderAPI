using EmailSender.API.Services;
using EmailSender.API.Services.Interfaces;
using EmailSender.Application.Queues.Subscribers;
using EmailSender.Application.Queues.Subscribers.Interfaces;

namespace EmailSender.API.IoC
{
    public class Injector
    {
        public static void InjectIocServices(IServiceCollection service)
        {
            service.AddTransient<IEmailSenderService, EmailSenderService>();
            service.AddTransient<ICodeGeneratorService, CodeGeneratorService>();
            service.AddTransient<IEmailSenderSubscriber, EmailSenderSubscriber>();
        }
    }
}
