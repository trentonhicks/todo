using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using Todo.Domain.Repositories;
using TodoWebAPI.InMemory;
using Todo.Domain.Services;
using Todo.Infrastructure.EFRepositories;
using Todo.Infrastructure;

namespace TodoWebAPI.Controllers
{
    public class ToDoListController : ControllerBase
    {
        private readonly TodoDatabaseContext _context;
        private readonly IConfiguration _config;
        private IAccountRepository _account;
        private ITodoListRepository _lists;
        private TodoListService _todoListService;

        public ToDoListController(TodoDatabaseContext context, IConfiguration config, TodoListService todoListService)
        {
            _context = context;
            _config = config;
            _todoListService = todoListService;
            _account = new EFAccountRepository(_context);
            //_lists = new EFTodoListRepository(_context);

        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel todoList)
        {
            var todoListCreated = await _todoListService.CreateTodoListAsync(accountId, todoList.ListTitle);

            if (!todoListCreated)
                return BadRequest("Account doesn't exist :(");

            return Ok(new CreateListPresentation() { Id = accountId, ListTitle = todoList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            if (await _account.FindAccountByIdAsync(accountId) == null)
            {
                return BadRequest("Account doesn't exist.");
            }

            var todoPreviewNum = Convert.ToInt32(_config.GetSection("Lists")["TodoPreviewNum"]);
            var lists = await _lists.FindTodoListsByAccountIdAsync(accountId, todoPreviewNum);

            return Ok(lists);
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int accountId, int listId, [FromBody] UpdateListModel updatedList)
        {
            var todoList = await _lists.FindTodoListIdByIdAsync(listId);

            if (todoList != null)
            {
                await _lists.UpdateTodoListAsync(todoList);
                return Ok("List updated successfully.");
            }

            return BadRequest("User doesn't have access to this list.");
        }

        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            await _lists.RemoveTodoListAsync(listId);

            return Ok("List deleted");
        }

    }
}
