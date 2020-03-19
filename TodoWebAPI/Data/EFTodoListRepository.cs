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
    public class EFTodoListRepository : ITodoListRepository
    {
        public EFTodoListRepository(IConfiguration config, ToDoContext context)
        {
            _config = config;
            _context = context;
        }

        private IConfiguration _config { get; set; }
        private ToDoContext _context { get; set; }

        public async Task<TodoListModel> CreateListAsync(TodoListModel list)
        {
            var todoList = new TodoLists()
            {
                Id = list.Id,
                AccountId = list.AccountId,
                ListTitle = list.ListTitle
            };
            await _context.TodoLists.AddAsync(todoList);
            await _context.SaveChangesAsync();

            return list;
        }

        public Task DeleteListAsync(int listId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ListPresentation>> GetListsAsync(int accountId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateListAsync(int listId, string title)
        {
            throw new NotImplementedException();
        }
    }
}
