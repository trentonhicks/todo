using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Todo.Domain;
using Todo.Domain.Repositories;
using System.Threading;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFAccountRepository : IAccountRepository
    {
        private TodoDatabaseContext _context;
        public EFAccountRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            return Task.CompletedTask;
        }
        public async Task<Account> FindAccountByIdAsync(int id) => await _context.Accounts.FindAsync(id);
        public async Task<bool> DoesAccountWithUserNameExistAsync(string userName) => await _context.Accounts.Where(a => a.UserName == userName).FirstOrDefaultAsync() != null;
        public async Task<bool> DoesAccountWithAccountIdExistAsync(int accountId) => await _context.Accounts.FindAsync(accountId) != null;
        public async Task RemoveAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            _context.Accounts.Remove(account);
        }
    }
}