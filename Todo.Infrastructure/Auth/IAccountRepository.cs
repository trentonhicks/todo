using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using Todo.Infrastructure;

namespace Todo.Infrastructure.Repositories
{
    public interface IAccountRepository : IUnitOfWork
    {
        void AddAccount(Account account);
        Task<Account> FindAccountByIdAsync(int id);
        Task<Account> FindAccountByEmailAsync(string email);
        Task<bool> DoesAccountWithAccountIdExistAsync(int accountId);
        Task RemoveAccountAsync(int accountId);
    }
}
