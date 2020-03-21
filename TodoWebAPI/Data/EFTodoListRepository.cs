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
        private ToDoContext _context { get; set; }
        private TodoListService _todoListService { get; set; }
        public EFTodoListRepository(ToDoContext context, TodoListService todoListService)
        {
            _context = context;
            _todoListService = todoListService;
        }

        public async Task<TodoListModel> CreateListAsync(TodoListModel list)
        {
            var todoList = new TodoLists()
            {
                Id = list.Id,
                AccountId = list.AccountId,
                ListTitle = list.ListTitle
            };
            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();

            list.Id = todoList.Id;

            return list;
        }

        public async Task DeleteListAsync(int listId)
        {
            var todoList = await _context.TodoLists.FindAsync(listId);
            await _todoListService.RemoveListAsync(todoList);
        }

        public async Task<List<ListPresentation>> GetListsAsync(int accountId, int pageSize)
        {
            var todoLists = await _context.TodoLists.Where(l => l.AccountId == accountId).ToListAsync();
            var listPresentation = new List<ListPresentation>();

            foreach(var todoList in todoLists)
            {
                var todoListPresentation = new TodoListModel() {
                    Id = todoList.Id,
                    ListTitle = todoList.ListTitle,
                    AccountId = todoList.AccountId
                };
                var todos = await _context.ToDos.Where(t => t.ListId == todoList.Id).Take(pageSize).ToListAsync();
                listPresentation.Add(new ListPresentation(todoListPresentation, todos));
            }
            return listPresentation;
        }

        public async Task<string> UpdateListAsync(TodoListModel list)
        {
            var todoList = await _context.TodoLists.FindAsync(list.Id);
            _context.TodoLists.Update(todoList);
            await _context.SaveChangesAsync();

            return list.ListTitle;
        }

        public async Task<TodoListModel> GetListAsync(TodoListModel list)
        {
            var todoList = await _context.TodoLists
                .Where(x => x.Id == list.Id && x.AccountId == list.AccountId)
                .FirstOrDefaultAsync();

            return todoList != null ? list : null;
        }

    }
}