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

namespace TodoWebAPI.DomainEventHandlers
{
    public class SendEmailUpdatedItemDomainEventHandler : INotificationHandler<TodoListItemUpdated>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public SendEmailUpdatedItemDomainEventHandler(IAccountRepository accountRepository, IEmailService emailService, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _config = config;
        }
        public async Task Handle(TodoListItemUpdated notification, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAccountByIdAsync(notification.Item.AccountId);
            var email = new Email()
            {
                To = account.Email,
                From = _config.GetSection("Emails")["Notifications"],
                Subject = $"Updated: {notification.Item.ToDoName}",
                Body = $"Item {notification.Item.ToDoName} was updated to: {(notification.Item.Completed ? "Completed" : "Incomplete")}"
            };
            await _emailService.SendEmailAsync(email);
        }
    }
}
