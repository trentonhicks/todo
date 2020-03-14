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
        public IActionResult CreateAccount()
        {
            return NotFound();
        }

        [HttpGet("accounts/{id}")]
        public IActionResult GetAccount(int id)
        {
            return NotFound();
        }

        [HttpDelete("accounts/{id}")]
        public IActionResult DeleteAccount(int id)
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
