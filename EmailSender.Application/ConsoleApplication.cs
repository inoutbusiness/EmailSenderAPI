using EmailSender.Application.Queues.Subscribers.Interfaces;
using Serilog;

namespace EmailSender.Application
{
    public class ConsoleApplication
    {
        private readonly IEmailSenderSubscriber _emailSenderSubscriber;
        private readonly ILogger _logger;

        public ConsoleApplication(IEmailSenderSubscriber emailSenderSubscriber)
        {
            _emailSenderSubscriber = emailSenderSubscriber;
            _logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public void Run()
        {
            try
            {
                _logger.Information("Iniciando serviço de consumo de emails");

                _emailSenderSubscriber.StartConsuming();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}