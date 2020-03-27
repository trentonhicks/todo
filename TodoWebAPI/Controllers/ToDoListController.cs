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
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

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
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel createTodoList)
        {
            var todoListService = new TodoListService(_todoListRepository, _accountRepository);

            var todoList = await todoListService.CreateTodoListAsync(accountId, createTodoList.ListTitle);

            if (todoList == null)
                return BadRequest("Unable to create list :(");

            return Ok(new CreateListPresentation() { Id = todoList.Id, ListTitle = todoList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            using (var connection = new SqlConnection("Server=.;Database=ToDo;Integrated Security=True"))
            {
                await connection.OpenAsync();

                var todoLists = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId", new { accountId = accountId });

                return Ok(todoLists);
            }
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
