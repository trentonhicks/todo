using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI
{
    public class TodoListService
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        public TodoListService(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<bool> AccountExistsAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId) == null ? false : true;
        }

        public async Task<bool> ListExistsAsync(int listId)
        {
            return await _context.TodoLists.FindAsync(listId) == null ? false : true;
        }

        public async Task RemoveListAsync(TodoLists list)
        {
            var todos = await _context.ToDos.Where(t => t.ListId == list.Id).ToListAsync();

            foreach (var todo in todos)
            {
                _context.ToDos.Remove(todo);
            }

            _context.TodoLists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}
