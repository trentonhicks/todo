using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Data;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;


namespace TodoWebAPI.Controllers
{
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        private IAccountRepository _account;
        private ITodoListRepository _lists;


        public ToDoListController(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _account = new EFAccountRepsitory(_config, _context);
            _lists = new EFTodoListRepository(_context);

        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel listToCreate)
        {
            var list = new TodoListModel()
            {
                AccountId = accountId,
                ListTitle = listToCreate.ListTitle
            };

            if (await _account.GetAccountAsync(accountId) == null)
            {
                return BadRequest("Account doesn't exist.");
            }

            var createdList = await _lists.CreateListAsync(list);

            return Ok(new CreateListPresentation() { Id = createdList.Id, ListTitle = createdList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            if (await _account.GetAccountAsync(accountId) == null)
            {
                return BadRequest("Account doesn't exist.");
            }

            var todoPreviewNum = Convert.ToInt32(_config.GetSection("Lists")["TodoPreviewNum"]);
            var lists = await _lists.GetListsAsync(accountId, todoPreviewNum);

            return Ok(lists);
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> UpdateList(int accountId, int listId, [FromBody] UpdateListModel updatedList)
        {
            var todoListModel = new TodoListModel() { Id = listId, AccountId = accountId, ListTitle = updatedList.ListTitle };
            var todoList = await _lists.GetListAsync(todoListModel);

            if (todoList != null)
            {
                var updatedTitle = await _lists.UpdateListAsync(todoListModel);
                return Ok("List updated successfully.");
            }

            return BadRequest("User doesn't have access to this list.");
        }

        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public async Task<IActionResult> DeleteList(int accountId, int listId)
        {
            var todoListModel = new TodoListModel() { Id = listId, AccountId = accountId };
            var todoList = await _lists.GetListAsync(todoListModel);

            if (todoList == null)
            {
                return BadRequest("User doesn't have access to this list.");
            }

            await _lists.DeleteListAsync(listId);
            return Ok();
        }

    }
}
