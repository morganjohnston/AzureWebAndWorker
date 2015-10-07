using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusConnectionString = Web.Configuration.ServiceBusConnectionString;

namespace Web.Infrastructure
{
    public interface IBusSender
    {
        void SendMessage<T>(T messageToSend);
    }

    public class BusSender : IBusSender
    {
        private readonly string busConnectionString;

        public BusSender(ServiceBusConnectionString busConnectionString)
        {
            this.busConnectionString = busConnectionString;
        }

        public void SendMessage<T>(T messageToSend)
        {
            var messageTypeName = messageToSend.GetType().Name;
            EnsureQueueExists(messageTypeName);
            var client = QueueClient.CreateFromConnectionString(busConnectionString, messageTypeName);
            client.Send(new BrokeredMessage(messageToSend));
        }

        private void EnsureQueueExists(string queueName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(busConnectionString);
            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(queueName);
            }
        }
    }
}