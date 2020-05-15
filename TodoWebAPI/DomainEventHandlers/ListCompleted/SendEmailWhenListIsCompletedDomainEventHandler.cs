using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IAccountRepository _accountRepository;
        private readonly ITodoListRepository _todoListRepository;
        private readonly IEmailService _emailService;
        private readonly IServiceBusEmail _serviceBusEmail;
        private readonly IConfiguration _config;
        private readonly DapperQuery _dapper;

        public SendEmailWhenListIsCompletedDomainEventHandler(IAccountRepository accountRepository, ITodoListRepository todoListRepository, IEmailService emailService, IServiceBusEmail serviceBusEmail, IConfiguration config, DapperQuery dapper)
        {
            _accountRepository = accountRepository;
            _todoListRepository = todoListRepository;
            _emailService = emailService;
            _serviceBusEmail = serviceBusEmail;
            _config = config;
            _dapper = dapper;
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
                }
                //_serviceBusEmail.SendServiceBusEmail(email);
            }
        }
    }
}
