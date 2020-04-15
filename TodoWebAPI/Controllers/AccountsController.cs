using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using TodoWebAPI.UserStories.DeleteAccount;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TodoDatabaseContext _todoDatabaseContext;
        private readonly IConfiguration _config;
        public AccountsController(IConfiguration config,
            IMediator mediator, TodoDatabaseContext todoDatabaseContext)
        {
            _config = config;
            _mediator = mediator;
            _todoDatabaseContext = todoDatabaseContext;
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            var account = await _mediator.Send(model);
            if (account == null)
                return BadRequest("Username already Exists.");


            return Ok(account);
        }

        [HttpGet("accounts/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
           var dapper = new DapperQuery(_config);

           var account = await dapper.GetAccountAsync(accountId);

           return Ok(account);
        }

        [HttpDelete("accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId)
        {
            var deleteAccount = new DeleteAccount
            {
                AccountId = accountId
            };

            await _mediator.Send(deleteAccount);

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok("account deleted!");
        }
    }
}
