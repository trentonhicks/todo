using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public SubItemController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        [HttpGet("accounts/{accountId}/lists/{listId}/todos/{todoItemId}/subitems")]
        public async Task<IActionResult> GetSubItems(int todoItemId)
        {
            var dapper = new DapperQuery(_config);

            var subItems = await dapper.GetSubItems(todoItemId);

            return Ok(subItems);
        }

        [HttpPost("accounts/{accountId}/lists/{listId}/todos/{todoItemId}/subitems")]
        public async Task<IActionResult> CreateSubItem(int accountId, int listId, int todoItemId, [FromBody] CreateSubItem createSubItem)
        {
            createSubItem.AccountId = accountId;
            createSubItem.ListId = listId;
            createSubItem.ListItemId = todoItemId;
            var subItem = await _mediator.Send(createSubItem);
            return Ok(subItem);
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