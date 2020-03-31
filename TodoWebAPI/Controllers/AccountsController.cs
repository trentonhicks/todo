using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using ToDo.Domain.Services;
using TodoWebAPI.Data;
using TodoWebAPI.InMemory;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoListItemRepository _todoListItemRepository;
        private readonly IAccountProfileImageRepository _profileImageRepository;
        private readonly IConfiguration _config;
        private readonly IAccountRepository _accountRepository;
        public AccountsController(IConfiguration config, IAccountRepository accountRepository, IAccountProfileImageRepository accountProfileImageRepository, ITodoListRepository todoListRepository, ITodoListItemRepository todoListItemRepository)
        {
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
            _config = config;
            _accountRepository = accountRepository;
            _profileImageRepository = accountProfileImageRepository;
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            var service = new AccountService(_accountRepository, _profileImageRepository, _todoListRepository, _todoListItemRepository);

            var account = await service.CreateAccountAsync(
                model.FullName,
                model.UserName,
                model.Password,
                model.Email,
                model.Picture);

            if (account == null)
                return BadRequest("Username already Exists.");

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
            var service = new AccountService(_accountRepository, _profileImageRepository, _todoListRepository, _todoListItemRepository);

            await service.DeleteAccountAsync(accountId);

            return Ok("account deleted!");
        }
    }
}
