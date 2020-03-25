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

        public EFAccountRepsitory(IConfiguration config, ToDoContext context)
        {
            _context = context;
            _config = config;
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
            var getList = await _context.TodoLists.FindAsync(listId);
            if(getList == null)
            {
                _context.Accounts.Remove(getAccount);
                await _context.SaveChangesAsync();
            }
            else
            {
                 await RemoveListAsync(getList);
                _context.Accounts.Remove(getAccount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AccountModel> GetAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account != null)
            {
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
        public async Task RemoveListAsync(TodoLists list)
        {
            var todos = await _context.ToDos.Where(t => t.ListId == list.Id).ToListAsync();

            foreach (var todo in todos)
            {
                _context.ToDos.Remove(todo);
            }

            _context.TodoLists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}
