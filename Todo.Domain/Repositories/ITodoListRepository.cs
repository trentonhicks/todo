using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ITodoListRepository : IRepository
    {
        Task AddTodoListAsync(TodoList list, Guid accountId);
        Task<List<TodoList>> FindTodoListsByAccountIdAsync(Guid accountId, int pageSize);
        Task<TodoList> FindTodoListIdByIdAsync(Guid listId);
        Task RemoveTodoListAsync(Guid listId);
        Task AddInvitedRowToAccountListsAsync(Guid accountId, Guid listId);
        Task AddContributorRowToAccountsListsAsync(Guid accountId, Guid listId);
        Task AddDeclinedRowToAccountsListsAsync(Guid accountId, Guid listId);
        Task AddLeftRowToAccountsListsAsync(Guid accountId, Guid listId);
        void UpdateListAsync(TodoList list);
    }
}