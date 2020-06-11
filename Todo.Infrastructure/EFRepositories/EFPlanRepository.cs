using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Todo.Infrastructure.Repositories;
using Todo.Infrastructure.Guids;
using Todo.Domain.Repositories;
using Todo.Domain;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFPlanRepository : IPlanRepository
    {
        private readonly TodoDatabaseContext _context;

        public EFPlanRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public async Task<Plan> FindPlanByName(string name)
        {
            return await _context.Plans.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}