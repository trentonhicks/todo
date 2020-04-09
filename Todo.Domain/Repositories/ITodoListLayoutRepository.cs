using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ITodoListLayoutRepository : IUnitOfWork
    {
        Task AddLayoutAsync(TodoListLayout layout);
        Task<TodoListLayout> FindLayoutByListIdAsync(int listId);
        void Update(TodoListLayout todoListLayout);
    }
}
