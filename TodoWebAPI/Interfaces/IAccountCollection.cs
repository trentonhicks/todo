using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Interfaces
{
    public interface IAccountCollection
    {
        Task <AccountModel>GetAccountAsync(int accountId);
        Task <AccountModel>CreateAccountAsync(AccountModel account);
        void DeleteAccountsAsync(int accountId);
    }
}
