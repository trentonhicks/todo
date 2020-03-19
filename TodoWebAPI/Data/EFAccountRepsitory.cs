using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

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

            _context.Accounts.Add(a);
            _context.SaveChanges();

            account.Id = a.Id;
            if (account.Picture != null)
            {

                var image = new ImageHandler(connectionString: _config.GetConnectionString("Development"));

                image.StoreImageProfile(account);

            }
            return Task.FromResult(account);
        }

        public void DeleteAccountsAsync(int accountId)
        {
            var getAccount = _context.Accounts.Find(accountId);
            var listId = _context.Lists.Where(x => x.AccountId == getAccount.Id).Select(x => x.Id).FirstOrDefault();
            var getList = _context.Lists.Find(listId);
            if (getList == null)
            {
                _context.Accounts.Remove(getAccount);
                _context.SaveChanges();
            }
            else
            {
                _contextService.RemoveList(getList);
                _context.Lists.Remove(getList);
                _context.Accounts.Remove(getAccount);
                _context.SaveChanges();
            }
        }

        public Task<AccountModel> GetAccountAsync(int accountId)
        {
            ExceptionHandler.BoolCheck(_contextService.AccountExists(accountId), "Account doesn't exist.");

            var account = _context.Accounts.Find(accountId);

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

            return Task.FromResult(accountModel);

        }
    }
}
