using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.Models;
using TodoWebAPI.UserStories.CreateSubItem;
using TodoWebAPI.UserStories.SubItemCompletedState;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class SubItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubItemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("accounts/{accountId}/lists/{listId}/todos/{todoId}/subitems")]
        public async Task<IActionResult> CreateSubItem(int accountId, int listId, int todoId, [FromBody] CreateSubItem createSubItem)
        {
            createSubItem.AccountId = accountId;
            createSubItem.ListId = listId;
            createSubItem.ItemId = todoId;
            var subItem = await _mediator.Send(createSubItem);
            return Ok(new SubItemModel()
            {
                Id = subItem.Id,
                ToDoName = subItem.Name,
                Notes = subItem.Notes,
                ListId = listId,
                ListItemId = todoId
            });
        }

        [HttpPut("accounts/{accountId}/subitems/{subitemId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int accountId, int subItemId, [FromBody] bool completed)
        {
            var subItemCompleted = new SubItemCompletedState
            {
                AccountId = accountId,
                SubItemId = subItemId,
                Completed = completed
            };
            await _mediator.Send(subItemCompleted);
            return Ok();
        }
    }
}