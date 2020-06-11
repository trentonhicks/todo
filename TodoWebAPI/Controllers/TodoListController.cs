using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using MediatR;
using TodoWebAPI.UserStories.ListLayout;
using Microsoft.AspNetCore.Authorization;
using TodoWebAPI.Extentions;
using TodoWebAPI.UserStories.SendInvitation;
using Todo.Domain;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class TodoListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DapperQuery _dapperQuery;

        public TodoListController(
            IMediator mediator,
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
            var accountContributors = await _dapperQuery.GetContributorsAsync(User.ReadClaimAsGuidValue("urn:codefliptodo:accountid"));
            var todoListsPresentation = new TodoListsPresentation(lists, accountContributors);

            return Ok(todoListsPresentation);
        }

        [HttpGet("api/lists/{listId}")]
        public async Task<IActionResult> GetList(Guid listId)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                return Ok(list);
            }

            return Forbid();
        }

        [HttpPut("api/lists/{listId}")]
        public async Task<IActionResult> UpdateList(Guid listId, UpdateList updatedList)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            updatedList.ListId = listId;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                await _mediator.Send(updatedList);
                return Ok();
            }

            return Forbid();
        }
        
        [HttpDelete("api/lists/{listId}")]
        public async Task<IActionResult> DeleteList(Guid listId)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            var deleteTodoModel = new DeleteList();

            deleteTodoModel.Email = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            deleteTodoModel.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            deleteTodoModel.ListId = listId;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                await _mediator.Send(deleteTodoModel);
                return Ok();
            }

            return Forbid();
        }

        [HttpPut("api/lists/{listId}/layout")]
        public async Task<IActionResult> UpdateLayout(Guid listId, [FromBody] ListLayout todoListLayoutModel)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            todoListLayoutModel.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            todoListLayoutModel.ListId = listId;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                await _mediator.Send(todoListLayoutModel);
                return Ok();
            }

            return Forbid();
        }

        [HttpGet("api/lists/{listId}/layout")]
        public async Task<IActionResult> GetTodoListLayout(Guid listId)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                var layout = await _dapperQuery.GetTodoListLayoutAsync(listId);
                return Ok(layout.Layout);
            }

            return Forbid();
        }

        [HttpPost("api/lists/{listId}/email")]
        public async Task<IActionResult> SendInvitaion(string listId, [FromBody] SendInvitation send)
        {
            send.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            await _mediator.Send(send);

            return Ok();
        }

    }
}
