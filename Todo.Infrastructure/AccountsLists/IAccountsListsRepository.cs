using System;
using System.Threading.Tasks;

namespace Todo.Infrastructure
{
    public interface IAccountsListsRepository
    {
        Task<AccountsLists> FindAccountsListsByAccountIdAsync(Guid listId);
    }
}