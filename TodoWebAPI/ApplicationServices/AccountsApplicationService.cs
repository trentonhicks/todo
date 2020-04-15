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

        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepository.RemoveAccountAsync(accountId);
        }
    }
}