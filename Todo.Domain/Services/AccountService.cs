using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace ToDo.Domain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountProfileImageRepository _profileImageRepository;
        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoListItemRepository _todoListItemRepository;

        public AccountService(IAccountRepository accountRepository, IAccountProfileImageRepository profileImageRepository, ITodoListRepository todoListRepository, ITodoListItemRepository todoListItemRepository)
        {
            _accountRepository = accountRepository;
            _profileImageRepository = profileImageRepository;
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
        }

        public async Task<Account> CreateAccountAsync(string fullName, string userName, string password, string email, string profileImage)
        {
            var doesAccountExist = await _accountRepository.DoesAccountWithUserNameExistAsync(userName);

            if (doesAccountExist)
                return null;

            var account = new Account()
            {
                FullName = fullName,
                UserName = userName,
                Password = password,
                Email = email
            };

            await _accountRepository.AddAccountAsync(account);

            if (!string.IsNullOrEmpty(profileImage))
                await _profileImageRepository.StoreImageProfileAsync(account.Id, profileImage);

            return account;
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepository.RemoveAccountAsync(accountId);
        }
    }
}