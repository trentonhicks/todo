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

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        private readonly TodoDatabaseContext _todoDatabaseContext;
        private readonly TodoListLayoutApplicationService _todoListLayoutApplicationService;
        private readonly DapperQuery _dapperQuery;

        public TodoListController(IConfiguration config,
            IMediator mediator,
            TodoDatabaseContext todoDatabaseContext,
            TodoListLayoutApplicationService todoListLayoutApplicationService,
            DapperQuery dapperQuery)
        {
            _config = config;
            _mediator = mediator;
            _todoDatabaseContext = todoDatabaseContext;
            _todoListLayoutApplicationService = todoListLayoutApplicationService;
            _dapperQuery = dapperQuery;
        }

        [HttpPost("accounts/{AccountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, CreateListModel createTodoList)
        {
           createTodoList.AccountId = accountId;
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
        public async Task<IActionResult> UpdateList(int accountId, int listId, UpdateListModel updatedList)
        {
            updatedList.ListId = listId;

            await _mediator.Send(updatedList);

            return Ok($"List title changed to {updatedList.ListTitle}");
        }
        
        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            var deleteTodoModel = new DeleteTodoModel();

            deleteTodoModel.AccountId = accountId;
            deleteTodoModel.ListId = listId;

            await _mediator.Send(deleteTodoModel);

            return Ok("List deleted");
        }

        [HttpPut("accounts/{accountId}/lists/{listId}/layout")]
        public async Task<IActionResult> UpdateLayout(int accountId, int listId, [FromBody] TodoListLayoutModel todoListLayoutModel)
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
