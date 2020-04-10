using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.Models;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class SubItemController : ControllerBase
    {
        private readonly SubItemApplicationService _service;

        public SubItemController(SubItemApplicationService service)
        {
            _service = service;
        }
        [HttpPost("accounts/{accountId}/lists/{listId}/todos/{todoId}/subitems")]
        public async Task<IActionResult> CreateSubItem(int accountId, int listId, int todoId, [FromBody] CreateSubItemModel todo)
        {
            var todoItem = await _service.CreateSubItemAsync(todoId, todo.TodoName, todo.Notes, todo.DueDate);

            return Ok(new SubItemModel()
            {
                Id = todoItem.Id,
                ToDoName = todoItem.Name,
                Notes = todoItem.Notes,
                ListId = listId,
                ListItemId = todoId
            });
        }

        [HttpPut("accounts/{accountId}/subitems/{subitemId}/completed")]
        public async Task<IActionResult> ToggleCompletedState(int accountId, int subItemId, [FromBody] bool completed)
        {
            await _service.ChangeCompletedStateAsync(subItemId, completed);

            return Ok();
        }
    }
}