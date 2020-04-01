using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using TodoWebAPI.InMemory;
using Todo.Domain.Repositories;
using Todo.Domain;
using Todo.Domain.Services;
using Todo.Infrastructure;
using MediatR;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace TodoWebAPI.Controllers
{
    public class TodoListItemController : ControllerBase
    {
        private readonly TodoDatabaseContext _context;
        private readonly IConfiguration _config;
        private readonly ITodoListRepository _todoListRepository;
        private ITodoListItemRepository _todoListItemRepository;
        private readonly IMediator _mediator;
        private IAccountRepository _accountRepository;

        public TodoListItemController(TodoDatabaseContext context,
            IConfiguration config,
            ITodoListRepository todoListRepository,
            IAccountRepository accountRepository,
            ITodoListItemRepository todoListItemRepository,
            IMediator mediator)
        {
            _context = context;
            _config = config;
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
            _mediator = mediator;
            _accountRepository = accountRepository;
        }


        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todo)
        {
            var todoListItemService = new TodoListItemService(_todoListRepository, _todoListItemRepository, _mediator);

            var todoItem = await todoListItemService.CreateTodoListItemAsync(listId, todo.ParentId, accountId, todo.ToDoName, todo.Notes);
            
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
            var service = new TodoListItemService(_todoListRepository, _todoListItemRepository, _mediator);
            await service.UpdateTodoListItemAsync(todoId, todo.Notes, todo.ToDoName);
            
            return Ok($"Name = {todo.ToDoName}, Notes = {todo.Notes}");
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int accountId, int todoId, [FromBody] bool completed)
        {
            var service = new TodoListItemService(_todoListRepository, _todoListItemRepository, _mediator);
            await service.MarkTodoListItemAsCompletedAsync(todoId, completed);

            return Ok();
        }


        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            var service = new TodoListItemService(_todoListRepository, _todoListItemRepository, _mediator);
            await service.DeleteTodoListItem(todoId);
            return Ok("Todo list item deleted.");
        }
    }
}