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
        public async Task<IActionResult> CreateTodo(int accountId, int listId, int todoId, [FromBody] CreateSubItemModel todo)
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
    }
}