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


        public AccountsController(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _account = new InMemoryAccountRepository();
            _image = new AccountProfileImageRepository(_config.GetConnectionString("Development"));
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
                Email = accountToCreate.Email,
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
                Picture = account.Picture,
                Email = account.Email
            };

            return Ok(accountPresentation);
        }

        [HttpDelete("accounts/{accountId}")]
        public async Task<IActionResult> DeleteAccountAsync(int accountId)
        {
            if (await _account.GetAccountAsync(accountId) == null)
            {
                return NotFound("Account already doesn't exist.");
            }
            await _account.DeleteAccountsAsync(accountId);
            return Ok("Acccount Deleted");
        }

       
      
    }
}
