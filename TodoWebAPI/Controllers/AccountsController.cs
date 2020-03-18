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
using TodoWebAPI.Interfaces;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        private ContextService _contextService;
        private IAccountCollection _account = new InMemoryAccount();
        private IListsCollection _lists = new InMemoryListsCollection();

        public AccountsController(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _contextService = new ContextService(_context, _config);
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel accountToCreate)
        {
            
            if(accountToCreate.UserName == null)
            {
                return BadRequest("Username required");
            }
            else if(accountToCreate.Password == null)
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

            return Ok(accounts);

            //if (account.Picture != null)
            //{
            //    var image = new ImageHandler(connectionString: _config.GetConnectionString("Development"));

            //    image.StoreImageProfile(account);
            //}

        }

        [HttpGet("accounts/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            

            var account = _account.GetAccountAsync(accountId);

            if(account == null)
            {
                return BadRequest("Profile Doesn't exist");
            }
            //var accountPicture = "";

            //if (account == null)
            //{
            //    return NotFound("Profile doesn't exist. :(");
            //}
            //else if (account.Picture != null)
            //{
            //    accountPicture = Convert.ToBase64String(account.Picture);
            //}

            //var profilePresentation = new AccountPresentation()
            //{
            //    Id = account.Id,
            //    FullName = account.FullName,
            //    UserName = account.UserName,
            //    Picture = account.Picture,
            //};

            return Ok(account);
        }

        [HttpDelete("accounts/{accountId}")]
        public IActionResult DeleteAccount(int accountId)
        {
             _account.DeleteAccountsAsync(accountId);
            return Ok("Acccount Deleted");
        }

        [HttpPost("accounts/{accountId}/lists")]
        public async Task<IActionResult> CreateList(int accountId, [FromBody] CreateListModel listToCreate)
        {
            var list = new ListModel()
            {
                AccountId = accountId,
                ListTitle = listToCreate.ListTitle
            };

            var createdList = await _lists.CreateListAsync(list);

            return Ok(new CreateListPresentation() { Id = createdList.Id, ListTitle = createdList.ListTitle });

            /* MOVE TO Entity Framework Implementation of IListsCollection
            if (_contextService.AccountExists(accountId))
            {
                if (title == "")
                {
                    title = "Untitled List";
                }

                var list = new Lists()
                {
                    AccountId = accountId,
                    ListTitle = title
                };

                _context.Lists.Add(list);
                _context.SaveChanges();
                return Ok(new { list.Id, list.ListTitle });
            }
            return NotFound("Account doesn't exist.");*/
        }

        [HttpGet("accounts/{accountId}/lists")]
        public IActionResult GetLists(int accountId)
        {
            var todoPreviewNum = Convert.ToInt32(_config.GetSection("Lists")["TodoPreviewNum"]);

            if(_contextService.AccountExists(accountId))
            {
                var lists = _context.Lists
                    .Where(l => l.AccountId == accountId)
                    .ToList();

                var listPresentation = new List<ListPresentation>();

                foreach(var list in lists)
                {
                    var todos = _context.ToDos.Where(t => t.ListId == list.Id).Take(todoPreviewNum).ToList();
                    list.ToDos = todos;
                    listPresentation.Add(new ListPresentation(list));
                }

                return Ok(listPresentation);
            }
            return NotFound("Account doesn't exist.");
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public IActionResult UpdateList(int accountId, int listId, [FromBody] string title)
        {
            var list = _context.Lists.Find(listId);

            if (list != null)
            {
                if (list.AccountId != accountId)
                {
                    return BadRequest("List doesn't belong to user.");
                }

                if (title == "")
                {
                    title = "Untitled List";
                }
                list.ListTitle = title;

                _context.Lists.Update(list);
                _context.SaveChanges();
                return Ok(list.ListTitle);
            }
            return NotFound("List doesn't exist.");
        }

        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public IActionResult DeleteList(int accountId, int listId)
        {
            var list = _context.Lists.Find(listId);

            if (list != null)
            {
                if (list.AccountId != accountId)
                {
                    return BadRequest("List doesn't belong to user.");
                }

                _contextService.RemoveList(list);

                return Ok("List deleted");
            }
            return NotFound("List doesn't exist.");
        }

        [HttpPost("accounts/{accountId}/lists/{listId}/todos")]
        public IActionResult CreateTodo(int accountId, int listId, [FromBody] ToDos todos)
        {
            var list = _context.Lists.Find(listId);

            todos.ListId = listId; 

            if (list != null)
            {
                if (list.AccountId != accountId)
                {
                    return BadRequest("List belongs to another account");
                }
                _context.ToDos.Add(todos);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound("List doesn't exist.");
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public IActionResult EditTodo(int accountId, int todoId)
        {
            return NotFound();
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public IActionResult DeleteTodo(int accountId, int todoId)
        {
            var todo = _context.ToDos.Find(todoId);
            if (_contextService.AccountExists(accountId))
            {
                if (todo != null)
                {
                    _context.ToDos.Remove(todo);
                    _context.SaveChanges();
                    return Ok("ToDo list removed.");
                }
                else
                {
                    return NotFound("ToDo is already empty");
                }
            }
            return NotFound("Account doesn't exist.");
        }
    }
}
