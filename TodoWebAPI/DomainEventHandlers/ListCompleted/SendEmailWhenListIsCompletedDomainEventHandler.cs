using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Email;
using Todo.Infrastructure.Repositories;
using TodoWebAPI.ServiceBusRabbitmq;

namespace TodoWebAPI.DomainEventHandlers
{
    public class SendEmailWhenListIsCompletedDomainEventHandler : INotificationHandler<TodoListCompletedStateChanged>
    {
        private readonly IServiceBusEmail _serviceBusEmail;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly DapperQuery _dapper;

        public SendEmailWhenListIsCompletedDomainEventHandler(IEmailService emailService, IConfiguration config, DapperQuery dapper, IServiceBusEmail serviceBusEmail)
        {
            _emailService = emailService;
            _config = config;
            _dapper = dapper;
            _serviceBusEmail = serviceBusEmail;
        }
        public async Task Handle(TodoListCompletedStateChanged notification, CancellationToken cancellationToken)
        {
            var list = notification.List;

            var emails = await _dapper.GetEmailsFromAccountsByListIdAsync(list.Id);

            if (list.Completed)
            {
                foreach (var userEmail in emails)
                {
                    var email = new Email()
                    {
                        To = userEmail,
                        From = _config.GetSection("Emails")["Notifications"],
                        Subject = $"You finished a list!",
                        Body = $"List {list.ListTitle} is finished! Nice work!"
                    };
                    _serviceBusEmail.SendServiceBusEmail(email, list);
                }
            }
        }
    }
}
