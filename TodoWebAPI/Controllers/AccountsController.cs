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
           
            if(usernameExists)
            {
                return BadRequest("Username needed.");
            }
            else if(account.Password == null)
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

        [HttpGet("accounts/{id}")]
        public IActionResult GetAccount()
        {
            return NotFound();
        }
    }
}
