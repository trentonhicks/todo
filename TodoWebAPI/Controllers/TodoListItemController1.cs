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
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todos)
        {
            var todoListItemService = new TodoListItemService(_todoListRepository, _todoListItemRepository, _mediator);


            var todoItem = await todoListItemService.CreateTodoListItemAsync(listId, todos.ParentId, accountId, todos.Completed, todos.ToDoName, todos.Notes);
            
            var todo = new TodoListItemModel()
            {
                ToDoName = todos.ToDoName,
                ParentId = todos.ParentId,
                Notes = todos.Notes,
                Completed = todos.Completed,
                ListId = listId
            };

            if (!todoItem)
              return BadRequest("List doesn't exist");

            return Ok(todo);
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodoAsync(int accountId, int todoId, [FromBody] TodoListItem todo)
        {
            var service = new TodoListItemService(_todoListRepository, _todoListItemRepository, _mediator);
            await service.UpdateTodoListItemAsync(todoId, todo.Notes, todo.ToDoName, todo.Completed);
            
            return Ok($"Name = {todo.ToDoName}, Notes = {todo.Notes}, Status = {todo.Completed}");
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