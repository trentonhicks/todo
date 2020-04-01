using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace Todo.WebAPI.ApplicationServices
{
    public class AccountsApplicationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountProfileImageRepository _profileImageRepository;

        public AccountsApplicationService(IAccountRepository accountRepository, IAccountProfileImageRepository profileImageRepository)
        {
            _accountRepository = accountRepository;
            _profileImageRepository = profileImageRepository;
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