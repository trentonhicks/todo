using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure.EFRepositories
{
    public class EFSubItemLayout : ISubItemLayout
    {
        private readonly TodoDatabaseContext _context;

        public EFSubItemLayout(TodoDatabaseContext context)
        {
            _context = context;
        }
        public Task AddLayoutAsync(SubItemLayout layout)
        {
            _context.SubItemLayouts.Add(layout);
            return Task.CompletedTask;
        }

        public async Task<SubItemLayout> FindLayoutByListItemIdAsync(int listItemId)
        {
            return await _context.SubItemLayouts.Where(x => x.ItemId == listItemId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(SubItemLayout layout)
        {
            _context.Entry(layout).State = EntityState.Modified;
        }
    }
}
