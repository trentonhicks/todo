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
using TodoWebAPI.UserStories.SendInvitation;

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
        public async Task<IActionResult> CreateList(Guid accountId, CreateList createTodoList)
        {
            createTodoList.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
           var todoList = await _mediator.Send(createTodoList);
            if (todoList == null)
                return BadRequest("Unable to create list :(");

            return Ok(new CreateListPresentation() { Id = todoList.Id, ListTitle = todoList.ListTitle });
        }

        [HttpGet("api/lists")]
        public async Task<IActionResult> GetLists()
        {
            var lists = await _dapperQuery.GetListsAsync(User.ReadClaimAsGuidValue("urn:codefliptodo:accountid"));
            var accounts = await _dapperQuery.GetContributors(User.ReadClaimAsGuidValue("urn:codefliptodo:accountid"));
            var todoListsPresentation = new TodoListsPresentation(lists, accounts);

            return Ok(todoListsPresentation);
        }

        [HttpGet("api/lists/{listId}")]

        public async Task<IActionResult> GetList(Guid listId)
        {
            var list = await _dapperQuery.GetListAsync(User.ReadClaimAsGuidValue("urn:codefliptodo:accountid"), listId);

            return Ok(list);
        }

        [HttpPut("api/lists/{listId}")]
        public async Task<IActionResult> UpdateList(Guid listId, UpdateList updatedList)
        {
            updatedList.ListId = listId;

            await _mediator.Send(updatedList);

            return Ok($"List title changed to {updatedList.ListTitle}");
        }
        
        [HttpDelete("api/lists/{listId}")]
        public async Task<IActionResult> DeleteList(Guid listId)
        {
            var deleteTodoModel = new DeleteList();

            deleteTodoModel.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            deleteTodoModel.ListId = listId;

            await _mediator.Send(deleteTodoModel);

            return Ok("List deleted");
        }

        [HttpPut("api/lists/{listId}/layout")]
        public async Task<IActionResult> UpdateLayout(Guid listId, [FromBody] ListLayout todoListLayoutModel)
        {
            todoListLayoutModel.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            todoListLayoutModel.ListId = listId;

            await _mediator.Send(todoListLayoutModel);

            return Ok();
        }

        [HttpGet("api/lists/{listId}/layout")]
        public async Task<IActionResult> GetTodoListLayout(Guid listId)
        {
            var layout = await _dapperQuery.GetTodoListLayoutAsync(listId);
            return Ok(layout.Layout);
        }

        [HttpPost("api/lists/{listId}/email")]

        public async Task<IActionResult> SendInvitaion(string listId, [FromBody] SendInvitation send)
        {
            await _mediator.Send(send);

            return Ok();
        }

    }
}
