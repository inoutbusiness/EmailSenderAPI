using EmailSender.Application;
using EmailSender.Application.Queues.Subscribers;
using EmailSender.Application.Queues.Subscribers.Interfaces;
using EmailSender.Application.Services;
using EmailSender.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static ManualResetEvent _quitEvent = new ManualResetEvent(false);

    private static void Main(string[] args)
    {
        Console.CancelKeyPress += (sender, eArgs) =>
        {
            _quitEvent.Set();
            eArgs.Cancel = true;
        };

        var services = new ServiceCollection();

        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddTransient<ICodeGeneratorService, CodeGeneratorService>();
        services.AddTransient<IEmailSenderSubscriber, EmailSenderSubscriber>();
        services.AddTransient<ConsoleApplication>();

        services.BuildServiceProvider()
                .GetService<ConsoleApplication>()
                .Run();

        _quitEvent.WaitOne();
    }
}