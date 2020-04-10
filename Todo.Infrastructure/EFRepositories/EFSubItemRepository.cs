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
    public class EFSubItemRepository : ISubItemRepository
    {
        private readonly TodoDatabaseContext _context;

        public EFSubItemRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public void Add(SubItem subItem)
        {
            _context.SubItems.Add(subItem);
        }

        public async Task<List<SubItem>> FindAllSubItemsByListItemIdAsync(int listItemId)
        {
            return await _context.SubItems.Where(x => x.ListItemId == listItemId).ToListAsync();
        }

        public async Task<SubItem> FindByIdAsync(int subItemId)
        {
            return await _context.SubItems.FindAsync(subItemId);
        }

        public async Task<SubItem> FindByListItemId(int listItemId)
        {
            return await _context.SubItems.FindAsync(listItemId);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
