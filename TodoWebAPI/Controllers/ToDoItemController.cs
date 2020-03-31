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
using TodoWebAPI.Repository;

namespace TodoWebAPI.Controllers
{
    public class ToDoItemController : ControllerBase
    {
        private readonly TodoDatabaseContext _context;
        private readonly IConfiguration _config;
        private readonly ITodoListRepository _todoListRepository;
        private ITodoListItemRepository _todoListItemRepository;
        private readonly EmailServiceInterface _email;
        private IAccountRepository _accountRepository;

        public ToDoItemController(TodoDatabaseContext context, IConfiguration config, ITodoListRepository todoListRepository, IAccountRepository accountRepository, ITodoListItemRepository todoListItemRepository)
        {
            _context = context;
            _config = config;
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
            _email = new SendGridEmailService();
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
            var service = new TodoListItemService(_todoListRepository, _todoListItemRepository);
            await service.UpdateTodoListItemAsync(todoId, todo.Notes, todo.ToDoName, todo.Completed);

            var emailService = new EmailService(_email, _accountRepository);
            var notification = _config.GetSection("Emails")["Notifications"];
            
            await emailService.CreateSendEmailFormatAsync(notification, todo, accountId);

            return Ok($"Name = {todo.ToDoName}, Notes = {todo.Notes}, Status = {todo.Completed}");
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            var service = new TodoListItemService(_todoListRepository, _todoListItemRepository);
            await service.DeleteTodoListItem(todoId);
            return Ok("Todo list item deleted.");
        }
    }
}