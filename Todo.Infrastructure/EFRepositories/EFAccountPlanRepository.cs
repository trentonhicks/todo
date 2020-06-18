using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Todo.Infrastructure.Guids;
using Todo.Domain.Repositories;
using Todo.Domain;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFAccountPlanRepository : IAccountPlanRepository
    {
        private readonly TodoDatabaseContext _context;

        public EFAccountPlanRepository(TodoDatabaseContext context)
        {
            _context = context;
        }

        public Task AddAccountPlanAsync(AccountPlan accountPlan)
        {
            _context.AccountsPlans.Add(accountPlan);
            
            return Task.CompletedTask;
        }

        public async Task<AccountPlan> FindAccountPlanByAccountIdAsync(Guid accountId)
        {
            return await _context.AccountsPlans.Where(x => x.AccountId == accountId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}