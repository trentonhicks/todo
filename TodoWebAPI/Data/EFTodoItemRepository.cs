using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using TodoWebAPI.Repositories;

namespace TodoWebAPI.Data
{
    public class EFTodoItemRepository : IToDoItemRepository
    {
        private readonly ToDoContext _context;
        public EFTodoItemRepository(ToDoContext context)
        {
            _context = context;
        }

        public async Task<TodoItemModel> CreateToDoAsync(TodoItemModel toDo)
        {
            var toDoItem = new ToDos()
            {
                Id = toDo.Id,
                ToDoName = toDo.ToDoName,
                ParentId = toDo.ParentId,
                Notes = toDo.Notes,
                Completed = toDo.Completed,
                ListId = toDo.ListId
            };
            _context.ToDos.Add(toDoItem);
            await _context.SaveChangesAsync();

            toDo.Id = toDoItem.Id;

            return toDo;
        }

        public Task<ToDos> UpdateToDoAsync(int listId, ToDos toDo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteToDoAsync(int todo)
        {
            throw new NotImplementedException();
        }
    }
}