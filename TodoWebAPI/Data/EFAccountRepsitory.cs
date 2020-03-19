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
        private ContextService _contextService;

        public EFAccountRepsitory(IConfiguration config, ToDoContext context)
        {
            _context = context;
            _config = config;
            _contextService = new ContextService(_context, _config);
        }
        public Task<AccountModel> CreateAccountAsync(AccountModel account)
        {
            var a = new Accounts();

            a.FullName = account.FullName;
            a.UserName = account.UserName;
            a.Password = account.Password;

            _context.Accounts.AddAsync(a);
            _context.SaveChangesAsync();

            account.Id = a.Id;
            if (account.Picture != null)
            {

                var image = new AccountProfileImageRepository(connectionString: _config.GetConnectionString("Development"));

                image.StoreImageProfile(account);

            }
            return Task.FromResult(account);
        }

        public async Task DeleteAccountsAsync(int accountId)
        {
            var getAccount = await _context.Accounts.FindAsync(accountId);
            var listId = _context.TodoLists.Where(x => x.AccountId == getAccount.Id).Select(x => x.Id).FirstOrDefault();
            var getList = _context.TodoLists.Find(listId);
            if (_contextService.ListExists(listId))
            {
                _context.Accounts.Remove(getAccount);
                await _context.SaveChangesAsync();
            }
            else
            {
                _contextService.RemoveList(getList);
                _context.TodoLists.Remove(getList);
                _context.Accounts.Remove(getAccount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AccountModel> GetAccountAsync(int accountId)
        {
            if (_contextService.AccountExists(accountId))
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
