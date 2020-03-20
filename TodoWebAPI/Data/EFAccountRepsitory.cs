using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using Microsoft.EntityFrameworkCore;


namespace TodoWebAPI.Data
{
    public class EFAccountRepsitory : IAccountRepository
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        private TodoListService _contextService;

        public EFAccountRepsitory(IConfiguration config, ToDoContext context)
        {
            _context = context;
            _config = config;
            _contextService = new TodoListService(_context, _config);
        }
        public async Task<AccountModel> CreateAccountAsync(AccountModel account)
        {
            var usernameExists = await _context.Accounts.Where(x => x.UserName == account.UserName).FirstOrDefaultAsync() != null;
            if (usernameExists)
                return null;

                var a = new Accounts()
                {
                    Id = account.Id,
                    FullName = account.FullName,
                    UserName = account.UserName,
                    Password = account.Password
                };
                await _context.Accounts.AddAsync(a);
                await _context.SaveChangesAsync();
                account.Id = a.Id;

                return account;
        }

        public async Task DeleteAccountsAsync(int accountId)
        {
            var getAccount = await _context.Accounts.FindAsync(accountId);
            var listId = await _context.TodoLists.Where(x => x.AccountId == getAccount.Id).Select(x => x.Id).FirstOrDefaultAsync();
            var getList = _context.TodoLists.Find(listId);
            if (await _contextService.ListExistsAsync(listId) == false)
            {
                _context.Accounts.Remove(getAccount);
                await _context.SaveChangesAsync();
            }
            else
            {
                await _contextService.RemoveListAsync(getList);
                _context.TodoLists.Remove(getList);
                _context.Accounts.Remove(getAccount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AccountModel> GetAccountAsync(int accountId)
        {
            if (await _contextService.AccountExistsAsync(accountId))
            {
                var account = await _context.Accounts.FindAsync(accountId);

                var accountPicture = "";
                
                if (account.Picture != null)
                {
                    accountPicture = Convert.ToBase64String(account.Picture);
                }

                var accountModel = new AccountModel()
                {
                    Id = account.Id,
                    FullName = account.FullName,
                    UserName = account.UserName,
                    Picture = accountPicture
                };

                return accountModel;
            }
            return null;
        }
    }
}
