using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using System.Data.SqlClient;
using Dapper;
using Todo.WebAPI.ApplicationServices;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using TodoWebAPI.ApplicationServices;
using MediatR;
using TodoWebAPI.UserStories.ListLayout;
using Microsoft.AspNetCore.Authorization;
using TodoWebAPI.Extentions;
namespace TodoWebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class TodoListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DapperQuery _dapperQuery;

        public TodoListController(IMediator mediator,
            DapperQuery dapperQuery)
        {
            _mediator = mediator;
            _dapperQuery = dapperQuery;
        }

        [HttpPost("api/lists")]
        public async Task<IActionResult> CreateList(int accountId, CreateList createTodoList)
        {
            createTodoList.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
           var todoList = await _mediator.Send(createTodoList);
            if (todoList == null)
                return BadRequest("Unable to create list :(");

            return Ok(new CreateListPresentation() { Id = todoList.Id, ListTitle = todoList.ListTitle });
        }

        [HttpGet("api/lists")]
        public async Task<IActionResult> GetLists()
        {
            var lists = await _dapperQuery.GetListsAsync(User.ReadClaimAsIntValue("urn:codefliptodo:accountid"));

            return Ok(lists);
        }

        [HttpGet("api/lists/{listId}")]

        public async Task<IActionResult> GetList(int listId)
        {
            var list = await _dapperQuery.GetListAsync(User.ReadClaimAsIntValue("urn:codefliptodo:accountid"), listId);

            return Ok(list);
        }

        [HttpPut("api/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int listId, UpdateList updatedList)
        {
            updatedList.ListId = listId;

            await _mediator.Send(updatedList);

            return Ok($"List title changed to {updatedList.ListTitle}");
        }
        
        [HttpDelete("api/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int listId)
        {
            var deleteTodoModel = new DeleteList();

            deleteTodoModel.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            deleteTodoModel.ListId = listId;

            await _mediator.Send(deleteTodoModel);

            return Ok("List deleted");
        }

        [HttpPut("api/lists/{listId}/layout")]
        public async Task<IActionResult> UpdateLayout(int listId, [FromBody] ListLayout todoListLayoutModel)
        {
            todoListLayoutModel.AccountId = User.ReadClaimAsIntValue("urn:codefliptodo:accountid");
            todoListLayoutModel.ListId = listId;

            await _mediator.Send(todoListLayoutModel);

            return Ok();
        }

        [HttpGet("api/lists/{listId}/layout")]
        public async Task<IActionResult> GetTodoListLayout(int listId)
        {
            var layout = await _dapperQuery.GetTodoListLayoutAsync(listId);
            return Ok(layout.Layout);
        }

    }
}
