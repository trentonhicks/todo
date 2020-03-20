using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repositories
{
    public interface IAccountRepository
    {
        Task <AccountModel>GetAccountAsync(int accountId);
        Task <AccountModel>CreateAccountAsync(AccountModel account);
        async Task DeleteAccountsAsync(int accountId);
    }
}
