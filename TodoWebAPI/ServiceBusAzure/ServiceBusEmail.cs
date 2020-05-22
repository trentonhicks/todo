using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Todo.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Todo.Domain;

namespace TodoWebAPI.ServiceBusRabbitmq
{
    public class ServiceBusEmail : IServiceBusEmail
    {
        const string QueueName = "notifications";
        private readonly IConfiguration _configuration;
        private QueueClient _queueClient;

        public ServiceBusEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void SendServiceBusEmail(Email email, TodoList list)
        {
            var contributors = list.Contributors;
            var connectionString = _configuration.GetSection("ConnectionStrings")["AzureServiceBus"];
            _queueClient = new QueueClient(connectionString, QueueName);

            foreach (var contributor in contributors)
            {
                string messageBody = $"To {email.To = contributor}" +
                    $", From {email.From = _configuration.GetSection("Emails")["Notifications"]}" +
                    $", Subject {email.Subject = "ListCompleted"}" +
                    $", Body {email.Body = list.ListTitle} Is Completed";

                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                await _queueClient.SendAsync(message);
            }

            await _queueClient.CloseAsync();
        }
    }
}

