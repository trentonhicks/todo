using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Models;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;

        public AccountsController(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("accounts")]
        public IActionResult CreateAccount(AccountModel account)
        {
            var a = new Accounts();
            var usernameExists = _context.Accounts.Where(x => x.UserName == account.UserName).FirstOrDefault() != null;

            if (usernameExists)
            {
                return BadRequest("Username needed.");
            }
            else if (account.Password == null)
            {
                return BadRequest("Password needed.");
            }

            a.FullName = account.FullName;
            a.UserName = account.UserName;
            a.Password = account.Password;

            _context.Accounts.Add(a);

            _context.SaveChanges();

            account.Id = a.Id;

            if (account.Picture != null)
            {
                //
            }

            return Ok($"{account.UserName} was created.");
        }

        [HttpGet("accounts/{accountId}")]
        public IActionResult GetAccount(int accountId)
        {
            var account = _context.Accounts.Find(accountId);
            return Ok(account);
        }

        [HttpDelete("accounts/{accountId}")]
        public IActionResult DeleteAccount(int accountId)
        {
            return NotFound();
        }

        [HttpPost("accounts/{accountId}/lists")]
        public IActionResult CreateList(int accountId)
        {
            return NotFound();
        }

        [HttpPut("accounts/{accountId}/lists/{listId}")]
        public IActionResult UpdateList(int accountId, int listId)
        {
            return NotFound();
        }

        [HttpDelete("accounts/{accountId}/lists/{listId}")]
        public IActionResult DeleteList(int accountId, int listId)
        {
            return NotFound();
        }

        [HttpPost("accounts/{accountId}/todos")]
        public IActionResult CreateTodo(int accountId)
        {
            return NotFound();
        }

        [HttpPut("accounts/{accountId}/todos/{todoId}")]
        public IActionResult EditTodo(int accountId, int todoId)
        {
            return NotFound();
        }

        [HttpDelete("accounts/{accountId}/todos/{todoId}")]
        public IActionResult DeleteTodo(int accountId, int todoId)
        {
            return NotFound();
        }
    }
}
