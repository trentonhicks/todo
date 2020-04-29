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
using TodoWebAPI.UserStories.EditItem;
using TodoWebAPI.UserStories.ItemLayout;
using TodoWebAPI.UserStories.TrashItem;
using TodoWebAPI.UserStories.ItemCompletedState;
using TodoWebAPI.Extentions;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class TodoListItemController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;

        public TodoListItemController(IConfiguration config,
            IMediator mediator)
        {
            _config = config;
            _mediator = mediator;
        }


        [HttpPost("api/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int listId, [FromBody] CreateItem todo)
        {
            todo.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            todo.ListId = listId;

            var todoItem = await _mediator.Send(todo);
            
            if (todoItem == null)
              return BadRequest("List doesn't exist");


            return Ok(new TodoListItemModel {
                Id = todoItem.Id,
                Name = todoItem.Name,
                Notes = todoItem.Notes,
                ListId = listId,
                DueDate = todoItem.DueDate
            });
        }

        [HttpGet("api/lists/{listId}/todos")]
        public async Task<IActionResult> GetAllTodoItems(int listId)
        {
            var accountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            var dapper = new DapperQuery(_config);

            var items = await dapper.GetAllTodoItemAsync(accountId, listId);

            return Ok(items);
        }

        [HttpPut("api/todos/{todoId}")]
        public async Task<IActionResult> EditTodo(int todoId, [FromBody] EditItem todo)
        {
            todo.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            todo.Id = todoId;

            await _mediator.Send(todo);

            return Ok($"Name = {todo.Name}, Notes = {todo.Notes}");
        }

        [HttpPut("api/todos/{todoId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int todoId, [FromBody] ItemCompletedState itemCompletedState)
        {
            itemCompletedState.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            itemCompletedState.ItemId = todoId;
            await _mediator.Send(itemCompletedState);

            return Ok();
        }

        [HttpPut("api/todos/{todoId}/layout")]
        public async Task<IActionResult> UpdateLayout(int todoId, [FromBody] ItemLayout  itemLayout)
        {
            itemLayout.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            itemLayout.ItemId = todoId;
            await _mediator.Send(itemLayout);

            return Ok();
        }

        [HttpDelete("api/todos/{todoId}")]
        public async Task<IActionResult> TrashItem(int todoId)
        {
            var accountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");

            var trashItem = new TrashItem
            {
                AccountId = accountId,
                ItemId = todoId
            };
            await _mediator.Send(trashItem);

            return Ok("Item has been trashed!");
        }
    }
}