using System;
using System.Threading.Tasks;
using ToDo.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFAccountRepository : IAccountRepository
    {
        private TodoDatabaseContext _context;
        private readonly IConfiguration _config;
        public EFAccountRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);

            await _context.SaveChangesAsync();
        }

        public async Task<Account> FindAccountByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<bool> DoesAccountWithUserNameExistAsync(string userName)
        {
            return await _context.Accounts.Where(a => a.UserName == userName).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> DoesAccountWithAccountIdExistAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId) != null;
        }

        public Task RemoveAccountAsync(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}