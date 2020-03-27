using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using TodoWebAPI.InMemory;

namespace TodoWebAPI.Data
{
    public class EFTodoItemRepository : ITodoListItemRepository
    {
        private readonly TodoDatabaseContext _context;
        public EFTodoItemRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
       
        public Task AddTodoListItemAsync(TodoListItem todo)
        {
            throw new NotImplementedException();
        }

        public Task<TodoListItem> UpdateToDoListItemAsync(int listId, TodoListItem todo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTodoListItemAsync(int todo)
        {
            throw new NotImplementedException();
        }
    }
}