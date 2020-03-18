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
        Task GetAccount(int accountId);
        Task CreateAccount(CreateAccountModel account);
        void DeleteAccounts(int accountId);
    }
}
