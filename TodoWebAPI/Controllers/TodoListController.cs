using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using System.Data.SqlClient;
using Dapper;
using Todo.WebAPI.ApplicationServices;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly TodoListApplicationService _todoListApplicationService;
        private readonly TodoDatabaseContext _todoDatabaseContext;
        private readonly TodoListLayoutApplicationService _todoListLayoutApplicationService;
        private readonly DapperQuery _dapperQuery;

        public TodoListController(IConfiguration config,
            TodoListApplicationService todoListApplicationService,
            TodoDatabaseContext todoDatabaseContext,
            TodoListLayoutApplicationService todoListLayoutApplicationService,
            DapperQuery dapperQuery)
        {
            _config = config;
            _todoListApplicationService = todoListApplicationService;
            _todoDatabaseContext = todoDatabaseContext;
            _todoListLayoutApplicationService = todoListLayoutApplicationService;
            _dapperQuery = dapperQuery;
        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel createTodoList)
        {
            var todoList = await _todoListApplicationService.CreateTodoListAsync(accountId, createTodoList.ListTitle);

            if (todoList == null)
                return BadRequest("Unable to create list :(");

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok(new CreateListPresentation() { Id = todoList.Id, ListTitle = todoList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            var lists = await _dapperQuery.GetListsAsync(accountId);

            return Ok(lists);
        }

        [HttpGet("accounts/{accountId}/lists/{listId}")]

        public async Task<IActionResult> GetList(int accountId, int listId)
        {
            var list = await _dapperQuery.GetListAsync(accountId, listId);

            return Ok(list);
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int accountId, int listId, [FromBody] UpdateListModel updatedList)
        {
            await _todoListApplicationService.RenameTodoListAsync(listId, updatedList.ListTitle);

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok($"List title changed to {updatedList.ListTitle}");
        }
        
        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            await _todoListApplicationService.DeleteTodoList(listId);

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok("List deleted");
        }

        [HttpPut("accounts/{accountId}/lists/{listId}/layout")]
        public async Task<IActionResult> UpdateLayout(int accountId, int listId, [FromBody] TodoListLayoutModel todoListLayoutModel)
        {
            await _todoListLayoutApplicationService.UpdateLayoutAsync(todoListLayoutModel.ItemId, todoListLayoutModel.Position, listId);

            return Ok();
        }

        [HttpGet("accounts/{accountId}/lists/{listId}/layout")]
        public async Task<IActionResult> GetTodoListLayout(int listId)
        {
            var layout = await _dapperQuery.GetTodoListLayoutAsync(listId);
            return Ok(layout.Layout);
        }

    }
}
