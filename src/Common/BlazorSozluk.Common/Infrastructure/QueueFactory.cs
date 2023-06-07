﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure
{
    public class QueueFactory
    {
        public static void SendMessage(string exchangeName
                                      ,string exchangeType
                                      ,string queueName
                                      ,object obj)
        {
            var consumer = CreateBasicConsumer();
        }
        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory=new ConnectionFactory() {HostName=SozlukConstants.RabbitMQHost};
            var connection=factory.CreateConnection();
            var channel = connection.CreateModel();
            return new EventingBasicConsumer(channel);
        }
    }
}