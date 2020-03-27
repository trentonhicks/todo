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
        private readonly ITodoListRepository _todoListRepository;
        private readonly IAccountRepository _accountRepository;

        public ToDoListController(TodoDatabaseContext context, IConfiguration config, ITodoListRepository todoListRepository, IAccountRepository accountRepository)
        {
            _context = context;
            _config = config;
            _todoListRepository = todoListRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel todoList)
        {
            var todoListService = new TodoListService(_todoListRepository, _accountRepository);

            var todoListCreated = await todoListService.CreateTodoListAsync(accountId, todoList.ListTitle);

            if (!todoListCreated)
                return BadRequest("Account doesn't exist :(");

            return Ok(new CreateListPresentation() { Id = accountId, ListTitle = todoList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            if (await _accountRepository.FindAccountByIdAsync(accountId) == null)
            {
                return BadRequest("Account doesn't exist.");
            }

            var todoPreviewNum = Convert.ToInt32(_config.GetSection("Lists")["TodoPreviewNum"]);
            var lists = await _todoListRepository.FindTodoListsByAccountIdAsync(accountId, todoPreviewNum);

            return Ok(lists);
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int accountId, int listId, [FromBody] UpdateListModel updatedList)
        {
            var service = new TodoListService(_todoListRepository, _accountRepository);

            await service.RenameTodoListAsync(listId, updatedList.ListTitle);

            return Ok($"List title changed to {updatedList.ListTitle}");
        }

        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            await _todoListRepository.RemoveTodoListAsync(listId);

            return Ok("List deleted");
        }

    }
}
