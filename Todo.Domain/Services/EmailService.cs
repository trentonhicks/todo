using System;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using TodoWebAPI.Repository;
using TodoWebAPI.Services;

namespace Todo.Domain.Services
{
    public class EmailService
    {
        private readonly EmailServiceInterface _email;
        private readonly IAccountRepository _accountRepository;

        public EmailService(EmailServiceInterface emailServiceRepository, IAccountRepository accountRepository)
        {
            _email = emailServiceRepository;
            _accountRepository = accountRepository;
        }
        public async Task CreateSendEmailFormatAsync(string notifications, TodoListItem todoListItem, int accountId)
        {
            var account = await _accountRepository.FindAccountByIdAsync(accountId);
            var email = new Email()
            {
                To = account.Email,
                From = notifications,
                Subject = $"Updated: {todoListItem.ToDoName}",
                Body = $"Item {todoListItem.ToDoName} was updated to: {(todoListItem.Completed ? "Completed" : "Incomplete")}"
            };
            await _email.SendEmailAsync(email);
        }
    }
}