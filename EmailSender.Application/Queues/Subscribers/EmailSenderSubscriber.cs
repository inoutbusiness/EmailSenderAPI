using EmailSender.Application.DTO_s;
using EmailSender.Application.Queues.Subscribers.Interfaces;
using EmailSender.Application.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System.Text;

namespace EmailSender.Application.Queues.Subscribers
{
    public class EmailSenderSubscriber : IEmailSenderSubscriber
    {
        public IConnection? Connection { get; set; }
        public IModel? Channel { get; set; }

        private const string BROKER_NAME = "email_sender";
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger _logger;

        public EmailSenderSubscriber(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
            _logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();

            Channel.QueueDeclare(queue: BROKER_NAME,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += Consumer_Received;

            Channel.BasicConsume(queue: BROKER_NAME,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            ProcessEvent(message);
        }

        private void ProcessEvent(string message)
        {
            var request = JsonConvert.DeserializeObject<EmailSenderRequest>(message);

            if (request != null)
            {
                _emailSenderService.SendEmail(request);
                _logger.Information($"Processando mensagem: {message}");
            }
        }
    }
}