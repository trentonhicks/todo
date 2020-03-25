using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Repositories
{
    public interface IToDoItemRepository
    {
        Task<TodoItemModel> CreateToDoAsync(TodoItemModel toDo);
        Task<ToDos> UpdateToDoAsync(int listId, ToDos toDo);
        Task DeleteToDoAsync(int todo);
    }
}
