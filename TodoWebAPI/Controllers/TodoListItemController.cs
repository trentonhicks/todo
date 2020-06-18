using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;
using TodoWebAPI.UserStories.CreateItem;
using MediatR;
using TodoWebAPI.UserStories.EditItem;
using TodoWebAPI.UserStories.ItemLayout;
using TodoWebAPI.UserStories.TrashItem;
using TodoWebAPI.UserStories.ItemCompletedState;
using TodoWebAPI.Extentions;
using Microsoft.AspNetCore.Authorization;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class TodoListItemController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        private readonly DapperQuery _dapperQuery;
        private readonly IAccountPlanRepository _accountPlanRepository;
        private readonly IPlanRepository _planRepository;

        public TodoListItemController(
            IConfiguration config,
            IMediator mediator,
            DapperQuery dapperQuery,
            IAccountPlanRepository accountPlanRepository,
            IPlanRepository planRepository)
        {
            _config = config;
            _mediator = mediator;
            _dapperQuery = dapperQuery;
            _accountPlanRepository = accountPlanRepository;
            _planRepository = planRepository;
        }


        [HttpPost("api/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(Guid listId, [FromBody] CreateItem todo)
        {
            var accountPlan = await _accountPlanRepository.FindAccountPlanByAccountIdAsync(User.ReadClaimAsGuidValue("urn:codefliptodo:accountid"));
            var plan = await _planRepository.FindPlanByIdAsync(accountPlan.PlanId);

            var accountPlanAuthorization = new AccountPlanAuthorizationValidator(accountPlan, plan);

            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            
            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorization = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorization.IsUserAuthorized())
            {
                todo.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
                todo.ListId = listId;

                if(todo.DueDate.HasValue && !accountPlanAuthorization.CanAddDueDate())
                    return BadRequest();

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

            return Forbid();
        }

        [HttpGet("api/lists/{listId}/todos")]
        public async Task<IActionResult> GetAllTodoItems(Guid listId)
        {
            var accountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            
            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorization = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorization.IsUserAuthorized())
            {
                var items = await _dapperQuery.GetAllTodoItemsAsync(listId);
                return Ok(items);
            }

            return Forbid();
        }

        [HttpPut("api/lists/{listId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodo(Guid listId, Guid todoId, [FromBody] EditItem todo)
        {
            var accountPlan = await _accountPlanRepository.FindAccountPlanByAccountIdAsync(User.ReadClaimAsGuidValue("urn:codefliptodo:accountid"));
            var plan = await _planRepository.FindPlanByIdAsync(accountPlan.PlanId);
            var accountPlanAuthorization = new AccountPlanAuthorizationValidator(accountPlan, plan);

            todo.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            todo.Id = todoId;

            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                if(todo.DueDate.HasValue && !accountPlanAuthorization.CanAddDueDate())
                    return BadRequest();

                await _mediator.Send(todo);
                return Ok();
            }

            return Forbid();
        }

        [HttpPut("api/lists/{listId}/todos/{todoId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(Guid listId, Guid todoId, [FromBody] ItemCompletedState itemCompletedState)
        {
            itemCompletedState.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
            itemCompletedState.ItemId = todoId;

            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                await _mediator.Send(itemCompletedState);
                return Ok();
            }

            return Forbid();
        }

        [HttpGet("api/lists/{listId}/todos/{todoItemId}/layout")]
        public async Task<IActionResult> GetLayout(Guid listId, Guid todoItemId)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            
            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                var layout = await _dapperQuery.GetTodoItemLayoutAsync(todoItemId);
                return Ok(layout);
            }

            return Forbid();
        }

        [HttpPut("api/lists/{listId}/todos/{todoId}/layout")]
        public async Task<IActionResult> UpdateLayout(Guid listId, Guid todoId, [FromBody] ItemLayout itemLayout)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            
            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                itemLayout.AccountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");
                itemLayout.ItemId = todoId;
                await _mediator.Send(itemLayout);

                return Ok();
            }

            return Forbid();
        }

        [HttpDelete("api/lists/{listId}/todos/{todoId}")]
        public async Task<IActionResult> TrashItem(Guid listId, Guid todoId)
        {
            var userEmail = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            
            var list = await _dapperQuery.GetListAsync(listId);

            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, userEmail);

            if(todoListAuthorizationValidator.IsUserAuthorized())
            {
                var accountId = User.ReadClaimAsGuidValue("urn:codefliptodo:accountid");

                var trashItem = new TrashItem
                {
                    AccountId = accountId,
                    ItemId = todoId
                };
                await _mediator.Send(trashItem);

                return Ok();
            }

            return Forbid();
        }
    }
}