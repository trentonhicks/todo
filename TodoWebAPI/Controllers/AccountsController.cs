using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Data;
using TodoWebAPI.InMemory;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        private AccountProfileImageRepository _image;
        private IAccountRepository _account;
        private ITodoListRepository _lists;
        private EFTodoItemRepository _todo;
        //private IToDoItemRepository _todoMemory = new InMemoryToDoItemRepository();


        public AccountsController(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _account = new EFAccountRepsitory(_config, _context);
            _image = new AccountProfileImageRepository(_config.GetConnectionString("Development"));
            _lists = new EFTodoListRepository(_context);
            _todo = new EFTodoItemRepository(_context);

        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel accountToCreate)
        {
            if (accountToCreate.Password == null)
            {
                return BadRequest("Password required");
            }

            var account = new AccountModel()
            {
                FullName = accountToCreate.FullName,
                UserName = accountToCreate.UserName,
                Password = accountToCreate.Password,
                Picture = accountToCreate.Picture
            };
            var accounts = await _account.CreateAccountAsync(account);
            if (accounts == null)
            {
                return BadRequest("Username already Exists.");
            }

            if(accounts.Picture != null)
            {
                await _image.StoreImageProfileAsync(accounts);
            }

            return Ok(accounts);

        }

        [HttpGet("accounts/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            var account = await _account.GetAccountAsync(accountId);
            if(account == null)
            {
                return BadRequest("Account doesn't exist");
            }
            var accountPresentation = new AccountPresentation()
            {
                Id = account.Id,
                FullName = account.FullName,
                UserName = account.UserName,
                Picture = account.Picture
            };

            return Ok(accountPresentation);
        }

        [HttpDelete("accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId)
        {
            if (_account.GetAccountAsync(accountId) == null)
            {
                return NotFound("Account already doesn't exist.");
            }
            await _account.DeleteAccountsAsync(accountId);
            return Ok("Acccount Deleted");
        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel listToCreate)
        {
            var list = new TodoListModel()
            {
                AccountId = accountId,
                ListTitle = listToCreate.ListTitle
            };

            if(await _account.GetAccountAsync(accountId) == null)
            {
                return BadRequest("Account doesn't exist.");
            }

            var createdList = await _lists.CreateListAsync(list);

            return Ok(new CreateListPresentation() { Id = createdList.Id, ListTitle = createdList.ListTitle });
        }

        [HttpGet("accounts/{accountId}/lists")]
        public async Task<IActionResult> GetLists(int accountId)
        {
            if(await _account.GetAccountAsync(accountId) == null)
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

            if(todoList != null)
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

            if(todoList == null)
            {
                return BadRequest("User doesn't have access to this list.");
            }
           
            await _lists.DeleteListAsync(listId);
            return Ok();
        }

        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public async Task<IActionResult> CreateTodo(int accountId, int listId, [FromBody] CreateToDoModel todos)
        {
            var todo = new TodoItemModel()
            {
                ToDoName = todos.ToDoName,
                ParentId = todos.ParentId,
                Notes = todos.Notes,
                Completed = todos.Completed,
                ListId = listId
            };
            var toDoItem = await _todo.CreateToDoAsync(todo);

            return Ok(toDoItem);
        }

        //var list = _context.Lists.Find(listId);

        //todos.ListId = listId; 

        //if (list != null)
        //{
        //    if (list.AccountId != accountId)
        //    {
        //        return BadRequest("List belongs to another account");
        //    }
        //    if (todos.ParentId != null)
        //    {
        //        return BadRequest();
        //    }
        //    _context.ToDos.Add(todos);
        //    _context.SaveChanges();
        //    return Ok();

        //return NotFound("List doesn't exist.");

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> EditTodo(int accountId, int todoId, [FromBody] ToDos todo)
        {

            var foo = await _todo.UpdateToDoAsync(todoId, todo);

            return Ok(foo);


            //var todo = _context.ToDos.Find(todoId);
            //if (_contextService.AccountExists(accountId))
            //{
            //    if (_contextService.ToDoExists(todoId))
            //    {
            //        var toDoName = todo.ToDoName;
            //        var toDoNote = todo.Notes;
            //        var toDoState = todo.Completed;
            //        if (toDoName != null)
            //        {
            //            _context.ToDos.Update(todo);
            //            _context.SaveChanges();
            //            return Ok();
            //        }
            //    }

            //    //logic to update the title
            //    //logic to update the note
            //    //logic to update the state
            //}
            //return NotFound();
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public async Task<IActionResult> DeleteTodo(int accountId, int todoId)
        {
            await _todo.DeleteToDoAsync(todoId);

            return Ok();

            //var todo = _context.ToDos.Find(todoId);
            //if (_contextService.AccountExists(accountId))
            //{
            //    if (todo != null)
            //    {
            //        _context.ToDos.Remove(todo);
            //        _context.SaveChanges();
            //        return Ok("ToDo list removed.");
            //    }
            //    else
            //    {
            //        return NotFound("ToDo is already empty");
            //    }
            //}
            //return NotFound("Account doesn't exist.");
        }
    }
}
