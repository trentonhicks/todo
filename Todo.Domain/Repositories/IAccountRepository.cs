using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain;

namespace Todo.Domain.Repositories
{
    public interface IAccountRepository : IUnitOfWork
    {
        void AddAccount(Account account);
        Task<Account> FindAccountByIdAsync(int id);
        Task<Account> FindAccountByEmailAsync(string email);
        Task<bool> DoesAccountWithUserNameExistAsync(string userName);
        Task<bool> DoesAccountWithAccountIdExistAsync(int accountId);
        Task RemoveAccountAsync(int accountId);
    }
}
