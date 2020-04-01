using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsApplicationService _accountsApplicationService;
        private readonly TodoDatabaseContext _todoDatabaseContext;
        private readonly IConfiguration _config;
        public AccountsController(IConfiguration config, AccountsApplicationService accountsApplicationService, TodoDatabaseContext todoDatabaseContext)
        {
            _config = config;
            _accountsApplicationService = accountsApplicationService;
            _todoDatabaseContext = todoDatabaseContext;
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {

            var account = await _accountsApplicationService.CreateAccountAsync(
                model.FullName,
                model.UserName,
                model.Password,
                model.Email,
                model.Picture);
            
            if (account == null)
                return BadRequest("Username already Exists.");

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok(account);
        }

        [HttpGet("accounts/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            using (var connection = new SqlConnection( _config.GetSection("ConnectionStrings")["Development"] ))
            {
                await connection.OpenAsync();

                var account = await connection.QueryAsync<AccountPresentation>("SELECT * From Accounts Where ID = @accountId", new { accountId = accountId });

                return Ok(account);
            }
        }

        [HttpDelete("accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId)
        {
            await _accountsApplicationService.DeleteAccountAsync(accountId);

            await _todoDatabaseContext.SaveChangesAsync();

            return Ok("account deleted!");
        }
    }
}
