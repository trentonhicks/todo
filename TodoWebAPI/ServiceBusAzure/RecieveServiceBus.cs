using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TodoWebAPI.ServiceBusAzure
{
    public class RecieveServiceBus : IHostedService
    {
        private IQueueClient _queueClient;
        const string QueueName = "notifications";
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public RecieveServiceBus(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<RecieveServiceBus>();
            _configuration = configuration;
        }
        public async Task RecieveMessage()
        {
            _queueClient = new QueueClient(_configuration.GetSection("ConnectionStrings")["AzureServiceBus"], QueueName);

            await _queueClient.CloseAsync();
        }

        public async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var data = Encoding.UTF8.GetString(message.Body);

            _logger.LogInformation($"{Encoding.UTF8.GetString(message.Body)} | BusListenerService received item.");

            await Task.Delay(5000);
        }

        protected void ProcessError(Exception e)
        {
            _logger.LogError(e, "Error while processing queue item in BusListenerService.");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _queueClient = new QueueClient(_configuration.GetSection("ConnectionStrings")["AzureServiceBus"], QueueName);

            _logger.LogDebug($"BusListenerService starting; registering message handler.");

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = true
            };
            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"BusListenerService stopping.");
            await _queueClient.CloseAsync();
        }
    }
}
