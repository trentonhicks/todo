using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;
using System.Data.SqlClient;
using Dapper;
using Todo.WebAPI.ApplicationServices;
using Todo.Domain.Repositories;
using Todo.Infrastructure;

namespace TodoWebAPI.Controllers
{
    public class TodoListItemController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly TodoListItemApplicationService _todoListItemApplicationService;
        private readonly TodoDatabaseContext _todoDatabaseContext;

        public TodoListItemController(IConfiguration config, TodoListItemApplicationService todoListItemApplicationService, TodoDatabaseContext todoDatabaseContext)
        {
            _config = config;
            _todoListItemApplicationService = todoListItemApplicationService;
            _todoDatabaseContext = todoDatabaseContext;
        }


        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todo)
        {
            var todoItem = await _todoListItemApplicationService.CreateTodoListItemAsync(listId, todo.ParentId, accountId, todo.ToDoName, todo.Notes);
            
            if (todoItem == null)
              return BadRequest("List doesn't exist");


            return Ok(new TodoListItemModel()
            {
                Id = todoItem.Id,
                ToDoName = todoItem.ToDoName,
                Notes = todoItem.Notes,
                ListId = listId
            });
        }

        [HttpGet("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> GetAllTodoItemsAsync(int accountId, int listId)
        {
            var dapper = new DapperQuery(_config);

            var items = await dapper.GetAllTodoItemAsync(accountId, listId);

            return Ok(items);
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodoAsync(int accountId, int todoId, [FromBody] TodoListItemModel todo)
        {
            await _todoListItemApplicationService.UpdateTodoListItemAsync(todoId, todo.Notes, todo.ToDoName);

            await _todoDatabaseContext.SaveChangesAsync();
            
            return Ok($"Name = {todo.ToDoName}, Notes = {todo.Notes}");
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int accountId, int todoId, [FromBody] bool completed)
        {
            await _todoListItemApplicationService.MarkTodoListItemAsCompletedAsync(todoId, completed);

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            await _todoListItemApplicationService.DeleteTodoListItem(todoId);

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok("Todo list item deleted.");
        }
    }
}