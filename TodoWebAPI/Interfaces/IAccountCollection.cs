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
        Task GetAccount(int accountId);
        Task CreateAccount(CreateAccountModel account);
        void DeleteAccounts(int accountId);
    }
}
