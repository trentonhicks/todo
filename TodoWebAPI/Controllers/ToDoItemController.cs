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
        private readonly ITodoListRepository _todoListRepository;
        private ITodoListItemRepository _todoListItemRepository;
        private readonly IEmailService _email;
        private IAccountRepository _accountRepository;

        public ToDoItemController(TodoDatabaseContext context, IConfiguration config, ITodoListRepository todoListRepository, IAccountRepository accountRepository, ITodoListItemRepository todoListItemRepository)
        {
            _context = context;
            _config = config;
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
            _email = new DebuggerWindowOutputEmailService();
            _accountRepository = accountRepository;
        }


        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todos)
        {
            var todoListItemService = new TodoListItemService(_todoListRepository, _todoListItemRepository);

            var todo = new TodoItemModel()
            {
                ToDoName = todos.ToDoName,
                ParentId = todos.ParentId,
                Notes = todos.Notes,
                Completed = todos.Completed,
                ListId = listId
            };

            var todoItem = await todoListItemService.CreateTodoListItemAsync(listId, todos.ParentId, accountId, todos.Completed, todos.ToDoName, todos.Notes);

            if (!todoItem)
              return BadRequest("List doesn't exist");

            return Ok(todoItem);
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodoAsync(int accountId, int todoId, [FromBody] TodoListItem todo)
        {
            var foo = await _todoListItemRepository.UpdateToDoListItemAsync(todoId, todo);
            var account = await _accountRepository.FindAccountByIdAsync(accountId);

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
            await _todoListItemRepository.RemoveTodoListItemAsync(todoId);
            return Ok();
        }
    }
}