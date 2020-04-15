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
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.UserStories.CreateItem;
using MediatR;

namespace TodoWebAPI.Controllers
{
    public class TodoListItemController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly TodoListItemApplicationService _todoListItemApplicationService;
        private readonly TodoDatabaseContext _todoDatabaseContext;
        private readonly SubItemLayoutApplicationService _subItemLayoutApplicationService;
        private readonly IMediator _mediator;

        public TodoListItemController(IConfiguration config,
            TodoListItemApplicationService todoListItemApplicationService,
            TodoDatabaseContext todoDatabaseContext,
            SubItemLayoutApplicationService subItemLayoutApplicationService,
            IMediator mediator)
        {
            _config = config;
            _todoListItemApplicationService = todoListItemApplicationService;
            _todoDatabaseContext = todoDatabaseContext;
            _subItemLayoutApplicationService = subItemLayoutApplicationService;
            _mediator = mediator;
        }


        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateItem todo)
        {
            todo.AccountId = accountId;
            todo.ListId = listId;

            var todoItem = await _mediator.Send(todo);
            
            if (todoItem == null)
              return BadRequest("List doesn't exist");


            return Ok(new {
                Id = todoItem.Id,
                ToDoName = todoItem.Name,
                Notes = todoItem.Notes,
                ListId = listId,
                DueDate = todoItem.DueDate
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
            await _todoListItemApplicationService.UpdateTodoListItemAsync(todoId, todo.Notes, todo.ToDoName, todo.DueDate);

            await _todoDatabaseContext.SaveChangesAsync();
            
            return Ok($"Name = {todo.ToDoName}, Notes = {todo.Notes}");
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int accountId, int todoId, [FromBody] bool completed)
        {
            await _todoListItemApplicationService.MarkTodoListItemAsCompletedAsync(todoId, completed);

            return Ok();
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}/layout")]
        public async Task<IActionResult> UpdateLayout(int accountId, int todoId, [FromBody] TodoListItemLayoutModel todoListItemLayoutModel)
        {
            await _subItemLayoutApplicationService.UpdateLayoutAsync(todoListItemLayoutModel.SubItemId, todoListItemLayoutModel.Position, todoId);

            return Ok();
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> TrashItem(int accountId, int todoId)
        {
            await _todoListItemApplicationService.TrashItemAsync(todoId);

            return Ok("Item has been trashed!");
        }
    }
}