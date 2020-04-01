using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using System.Data.SqlClient;
using Dapper;
using Todo.WebAPI.ApplicationServices;

namespace TodoWebAPI.Controllers
{
    public class TodoListController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly TodoListApplicationService _todoListApplicationService;

        public TodoListController(IConfiguration config,  TodoListApplicationService todoListApplicationService)
        {
            _config = config;
            _todoListApplicationService = todoListApplicationService;
        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel createTodoList)
        {
            var todoList = await _todoListApplicationService.CreateTodoListAsync(accountId, createTodoList.ListTitle);

            if (todoList == null)
                return BadRequest("Unable to create list :(");

            return Ok(new CreateListPresentation() { Id = todoList.Id, ListTitle = todoList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            using (var connection = new SqlConnection( _config.GetSection("ConnectionStrings")["Development"] ))
            {
                await connection.OpenAsync();

                var todoLists = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId", new { accountId = accountId });

                return Ok(todoLists);
            }
        }

        [HttpGet("accounts/{accountId}/lists/{listId}")]

        public async Task<IActionResult> GetList(int accountId, int listId)
        {
            using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
            {
                await connection.OpenAsync();

                var todoList = await connection.QueryAsync<TodoListModel>("SELECT * From TodoLists Where AccountID = @accountId AND ID = @listId", new { accountId = accountId, listId = listId });

                return Ok(todoList);
            }
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int accountId, int listId, [FromBody] UpdateListModel updatedList)
        {
            await _todoListApplicationService.RenameTodoListAsync(listId, updatedList.ListTitle);

            return Ok($"List title changed to {updatedList.ListTitle}");
        }

        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            await _todoListApplicationService.DeleteTodoList(listId);

            return Ok("List deleted");
        }

    }
}
