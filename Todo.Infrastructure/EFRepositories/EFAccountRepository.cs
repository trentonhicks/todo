using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFAccountRepository : IAccountRepository
    {
        private TodoDatabaseContext _context;
        public EFAccountRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
        }
        public async Task<Account> FindAccountByIdAsync(int id) => await _context.Accounts.FindAsync(id);
        public async Task<bool> DoesAccountWithAccountIdExistAsync(int accountId) => await _context.Accounts.FindAsync(accountId) != null;
        public async Task RemoveAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            _context.Accounts.Remove(account);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Account> FindAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}