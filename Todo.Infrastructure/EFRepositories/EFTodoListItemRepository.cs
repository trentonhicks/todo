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
    public class EFTodoListItemRepository : ITodoListItemRepository
    {
        private readonly TodoDatabaseContext _context;
        public EFTodoListItemRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
       
        public async Task AddTodoListItemAsync(TodoListItem todo)
        {
            _context.TodoListItems.Add(todo);
            await _context.SaveChangesAsync();
        }

        public Task<TodoListItem> UpdateToDoListItemAsync(int listId, TodoListItem todo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTodoListItemAsync(int todo)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllTodoListItemsFromAccountAsync(int accountId)
        {
            var todoListItems = await _context.TodoListItems.Where(t => t.AccountId == accountId).ToListAsync();

            _context.TodoListItems.RemoveRange(todoListItems);
            await _context.SaveChangesAsync();
        }
    }
}