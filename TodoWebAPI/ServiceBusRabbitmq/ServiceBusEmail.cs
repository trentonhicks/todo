using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Todo.Infrastructure.Email;

namespace TodoWebAPI.ServiceBusRabbitmq
{
    public class ServiceBusEmail : IServiceBusEmail
    {
        public void SendServiceBusEmail(Email email)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                byte[] messagebuffer = Encoding.Default.GetBytes(JsonConvert.SerializeObject(email));

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: messagebuffer);
            }
        }
    }
}
