using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.Data
{
    public class EFAccountCollection : IAccountRepository
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        public EFAccountCollection(IConfiguration config, ToDoContext context)
        {
            _context = context;
            _config = config;
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
            throw new NotImplementedException();
        }

        public Task<AccountModel> GetAccountAsync(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
