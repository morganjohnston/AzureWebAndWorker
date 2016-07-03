using System;
using System.Runtime.Serialization.Json;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Shared.Contracts;
using Web.Configuration;

namespace Web.Infrastructure
{
    public interface IBusSender
    {
        void SendMessage<T>(T messageToSend);
        void SendMessage<T>(T messageToSend, DateTimeOffset scheduledMessageEnqueTimeOffset);
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
            var client = GetBusQueueClient(messageToSend);
            client.Send(new BrokeredMessage(messageToSend, new DataContractJsonSerializer(typeof(T))));
        }

        public void SendMessage<T>(T messageToSend, DateTimeOffset scheduledMessageEnqueTimeOffset)
        {
            var client = GetBusQueueClient(messageToSend);
            var message = new BrokeredMessage(messageToSend, new DataContractJsonSerializer(typeof(WidgetAdded)))
            {
                ScheduledEnqueueTimeUtc = scheduledMessageEnqueTimeOffset.UtcDateTime
            };
            client.Send(message);
        }

        private QueueClient GetBusQueueClient<T>(T messageToSend)
        {
            var messageTypeName = messageToSend.GetType().Name;
            EnsureQueueExists(messageTypeName);
            return QueueClient.CreateFromConnectionString(busConnectionString, messageTypeName);
        }

        private void EnsureQueueExists(string queueName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(busConnectionString);
            if (!namespaceManager.QueueExists(queueName))
            {
                var queueDescription = new QueueDescription(queueName)
                {
                    EnablePartitioning = true,
                    SupportOrdering = false,
                };
                namespaceManager.CreateQueue(queueDescription);
            }
        }
    }
}