using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ITodoListLayoutRepository : IRepository
    {
        Task AddLayoutAsync(TodoListLayout layout);
        Task<TodoListLayout> FindLayoutByListIdAsync(Guid listId);
        void Update(TodoListLayout todoListLayout);
    }
}
