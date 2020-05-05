using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ITodoListItemRepository : IRepository
    {
        Task AddTodoListItemAsync(TodoListItem todo);
        Task<TodoListItem> FindToDoListItemByIdAsync(Guid todoListItemId);
        void RemoveTodoListItemAsync(TodoListItem item);
        Task RemoveAllTodoListItemsFromAccountAsync(Guid listId);
        Task<List<TodoListItem>> FindAllTodoListItemsByListIdAsync(Guid listId);
        Task<int> GetSubItemCountAsync(Guid listItemId);
    }
}