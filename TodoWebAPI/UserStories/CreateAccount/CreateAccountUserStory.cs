using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.UserStories.CreateAccount
{
    public class CreateAccountUserStory : IRequestHandler<CreateAccountModel, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountProfileImageRepository _accountProfileImageRepository;

        public CreateAccountUserStory(IAccountRepository accountRepository, IAccountProfileImageRepository accountProfileImageRepository)
        {
            _accountRepository = accountRepository;
            _accountProfileImageRepository = accountProfileImageRepository;
        }
        public async Task<Account> Handle(CreateAccountModel request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAccountByEmailAsync(request.Email);

            if (account != null)
            {
                return account;
            }

            account = new Account()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email
            };

            _accountRepository.AddAccount(account);

            await _accountRepository.SaveChangesAsync();

            return account;
        }
    }
}
