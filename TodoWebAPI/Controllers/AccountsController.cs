using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly TodoDatabaseContext _context;
        private readonly IConfiguration _config;
        private IAccountProfileImageRepository _profileImageRepository;
        private IAccountRepository _accountRepository;

        public AccountsController(TodoDatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _accountRepository = new InMemoryAccountRepository();
            _profileImageRepository = new AccountProfileImageRepository(_config.GetConnectionString("Development"));
        }

        [HttpPost("accounts")]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            var service = new AccountService(_accountRepository, _profileImageRepository);

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
            //var account = await _account.GetAccountAsync(accountId);
            //if(account == null)
            //{
            //    return BadRequest("Account doesn't exist");
            //}
            //var accountPresentation = new AccountPresentation()
            //{
            //    Id = account.Id,
            //    FullName = account.FullName,
            //    UserName = account.UserName,
            //    Picture = account.Picture,
            //    Email = account.Email
            //};

            //return Ok(accountPresentation);

            return Ok();
        }

        [HttpDelete("accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId)
        {
            //    if (await _account.GetAccountAsync(accountId) == null)
            //    {
            //        return NotFound("Account already doesn't exist.");
            //    }
            //    await _account.DeleteAccountsAsync(accountId);
            //    return Ok("Acccount Deleted");
            //}

            return Ok();
        }
    }
}
