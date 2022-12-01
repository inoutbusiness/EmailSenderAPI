using RabbitMQ.Client;

namespace EmailSender.Application.Queues.Subscribers.Interfaces
{
    public interface ISubscriberBase
    {
        IConnection? Connection { get; set; }
        IModel? Channel { get; set; }

        void StartConsuming();
    }
}