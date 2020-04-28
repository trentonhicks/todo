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

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, CreateList createTodoList)
        {
            createTodoList.AccountId = Convert.ToInt32(User.FindFirst("urn:codefliptodo:accountid").Value);
           var todoList = await _mediator.Send(createTodoList);
            if (todoList == null)
                return BadRequest("Unable to create list :(");

            return Ok(new CreateListPresentation() { Id = todoList.Id, ListTitle = todoList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            var lists = await _dapperQuery.GetListsAsync(accountId);

            return Ok(lists);
        }

        [HttpGet("accounts/{accountId}/lists/{listId}")]

        public async Task<IActionResult> GetList(int accountId, int listId)
        {
            var list = await _dapperQuery.GetListAsync(accountId, listId);

            return Ok(list);
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int accountId, int listId, UpdateList updatedList)
        {
            updatedList.ListId = listId;

            await _mediator.Send(updatedList);

            return Ok($"List title changed to {updatedList.ListTitle}");
        }
        
        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            var deleteTodoModel = new DeleteList();

            deleteTodoModel.AccountId = accountId;
            deleteTodoModel.ListId = listId;

            await _mediator.Send(deleteTodoModel);

            return Ok("List deleted");
        }

        [HttpPut("accounts/{accountId}/lists/{listId}/layout")]
        public async Task<IActionResult> UpdateLayout(int accountId, int listId, [FromBody] ListLayout todoListLayoutModel)
        {
            todoListLayoutModel.AccountId = accountId;
            todoListLayoutModel.ListId = listId;

            await _mediator.Send(todoListLayoutModel);

            return Ok();
        }

        [HttpGet("accounts/{accountId}/lists/{listId}/layout")]
        public async Task<IActionResult> GetTodoListLayout(int listId)
        {
            var layout = await _dapperQuery.GetTodoListLayoutAsync(listId);
            return Ok(layout.Layout);
        }

    }
}
