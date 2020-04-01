using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;
using System.Data.SqlClient;
using Dapper;
using Todo.WebAPI.ApplicationServices;

namespace TodoWebAPI.Controllers
{
    public class TodoListItemController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly TodoListItemApplicationService _todoListItemApplicationService;

        public TodoListItemController(IConfiguration config, TodoListItemApplicationService todoListItemApplicationService)
        {
            _config = config;
            _todoListItemApplicationService = todoListItemApplicationService;
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
                ParentId = todoItem.ParentId,
                Notes = todoItem.Notes,
                ListId = listId
            });
        }

        [HttpGet("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> GetAllTodoItemsAsync(int accountId, int listId)
        {
            using (var connection = new SqlConnection(_config.GetSection("ConnectionStrings")["Development"]))
            {
                await connection.OpenAsync();

                var todoList = await connection.QueryAsync<TodoListItemModel>("SELECT * From TodoListItems Where AccountID = @accountId AND ListID = @listId", new { accountId = accountId, listId = listId });

                return Ok(todoList);
            }
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodoAsync(int accountId, int todoId, [FromBody] TodoListItemModel todo)
        {
            await _todoListItemApplicationService.UpdateTodoListItemAsync(todoId, todo.Notes, todo.ToDoName);
            
            return Ok($"Name = {todo.ToDoName}, Notes = {todo.Notes}");
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int accountId, int todoId, [FromBody] bool completed)
        {
            await _todoListItemApplicationService.MarkTodoListItemAsCompletedAsync(todoId, completed);

            return Ok();
        }


        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            await _todoListItemApplicationService.DeleteTodoListItem(todoId);
            return Ok("Todo list item deleted.");
        }
    }
}