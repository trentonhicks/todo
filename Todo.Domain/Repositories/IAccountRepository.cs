using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace Todo.Domain.Repositories
{
    public interface IAccountRepository : IRepository
    {
        void AddAccount(Account account);
        Task<Account> FindAccountByIdAsync(Guid id);
        Task<Account> FindAccountByEmailAsync(string email);
        Task<bool> DoesAccountWithAccountIdExistAsync(Guid accountId);
        Task RemoveAccountAsync(Guid accountId);
        void UpdateContributorsAsync(Account account);
    }
}
