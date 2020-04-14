using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ITodoListItemRepository : IUnitOfWork
    {
        Task AddTodoListItemAsync(TodoListItem todo);
        Task<TodoListItem> FindToDoListItemByIdAsync(int todoListItemId);
        void RemoveTodoListItemAsync(TodoListItem item);
        Task RemoveAllTodoListItemsFromAccountAsync(int listId);
        Task<List<TodoListItem>> FindAllTodoListItemsByListIdAsync(int listId);
        Task<int> GetSubItemCountAsync(int listItemId);
    }
}