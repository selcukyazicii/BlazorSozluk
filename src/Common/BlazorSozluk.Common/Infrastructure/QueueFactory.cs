using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure
{
    public static class QueueFactory
    {
        public static void SendMessage(string exchangeName
                                      ,string exchangeType
                                      ,string queueName
                                      ,object obj)
        {
            CreateBasicConsumer().EnsureExchange(exchangeName, exchangeType)
                                 .EnsureQueue(queueName, queueName);
        }
        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory=new ConnectionFactory() {HostName=SozlukConstants.RabbitMQHost};
            var connection=factory.CreateConnection();
            var channel = connection.CreateModel();
            return new EventingBasicConsumer(channel);
        }
        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer,
            string exchangeName,string exchangeType = SozlukConstants.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchange:exchangeName, 
                                           type:exchangeType,
                                           durable:false,
                                           autoDelete:false);
            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                            string queueName, 
                                                            string exchangeName)
        {
            consumer.Model.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                null);

            consumer.Model.QueueBind(queueName,exchangeName,queueName);
            return consumer;
        }
    }
}
