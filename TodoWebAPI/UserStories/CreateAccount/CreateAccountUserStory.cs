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
            var doesAccountExist = await _accountRepository.DoesAccountWithUserNameExistAsync(request.UserName);

            if (doesAccountExist)
                return null;

            var account = new Account()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email
            };

            _accountRepository.AddAccount(account);

            if (!string.IsNullOrEmpty(request.Picture))
                await _accountProfileImageRepository.StoreImageProfileAsync(account.Id, request.Picture);

            await _accountRepository.SaveChangesAsync();

            return account;
        }
    }
}
