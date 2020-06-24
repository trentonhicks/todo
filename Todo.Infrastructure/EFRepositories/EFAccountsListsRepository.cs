using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Guids;
using Todo.Domain;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFAccountsListsRepository : IAccountsListsRepository
    {
        private readonly TodoDatabaseContext _context;
        public EFAccountsListsRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public async Task<AccountLists> FindAccountsListsByAccountIdAsync(Guid accountId)
        {
            return await _context.AccountLists
                .Where(a => a.AccountId == accountId)
                .FirstOrDefaultAsync();
        }
    }
}