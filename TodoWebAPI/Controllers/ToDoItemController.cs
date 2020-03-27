using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using TodoWebAPI.Services;
using TodoWebAPI.InMemory;
using Todo.Domain.Repositories;
using Todo.Domain;
using Todo.Domain.Services;
using Todo.Infrastructure;

namespace TodoWebAPI.Controllers
{
    public class ToDoItemController : ControllerBase
    {
        private readonly TodoDatabaseContext _context;
        private readonly IConfiguration _config;
        private ITodoListItemRepository _todo;
        private readonly IEmailService _email;
        private IAccountRepository _account;
        private TodoListItemService _todoListItemService;

        public ToDoItemController(TodoDatabaseContext context, IConfiguration config, TodoListItemService todoListItemService, ITodoListItemRepository todoListItemRepository)
        {
            _context = context;
            _config = config;
            _todo = todoListItemRepository;
            _email = new DebuggerWindowOutputEmailService();
            _account = new InMemoryAccountRepository();
            _todoListItemService = todoListItemService;
        }


        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todos)
        {
            var todo = new TodoItemModel()
            {
                ToDoName = todos.ToDoName,
                ParentId = todos.ParentId,
                Notes = todos.Notes,
                Completed = todos.Completed,
                ListId = listId
            };

            var todoItem = await _todoListItemService.CreateTodoListAsync(listId, todos.ParentId, todos.Completed, todos.ToDoName, todos.Notes);

            if (!todoItem)
              return BadRequest("List doesn't exist");

            return Ok(todoItem);
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodoAsync(int accountId, int todoId, [FromBody] TodoListItem todo)
        {
            var foo = await _todo.UpdateToDoListItemAsync(todoId, todo);
            var account = await _account.FindAccountByIdAsync(accountId);

            var email = new Email()
            {
                To = account.Email,
                From = _config.GetSection("Emails")["Notifications"],
                Subject = $"Updated: {todo.ToDoName}",
                Body = $"Item {todo.ToDoName} was updated to: {(todo.Completed ? "Completed" : "Incomplete")}"
            };
            await _email.SendEmailAsync(email);
            return Ok(foo);
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            await _todo.RemoveTodoListItemAsync(todoId);
            return Ok();
        }
    }
}