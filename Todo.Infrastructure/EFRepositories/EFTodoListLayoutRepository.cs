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
    public class EFTodoListLayoutRepository : ITodoListLayoutRepository
    {
        private readonly TodoDatabaseContext _context;

        public EFTodoListLayoutRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
        public Task AddLayoutAsync(TodoListLayout layout)
        {
            _context.TodoListLayouts.Add(layout);
            return Task.CompletedTask;
        }

        public async Task<TodoListLayout> FindLayoutByListIdAsync(int listId)
        {
            return await _context.TodoListLayouts.Where(x => x.ListId == listId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(TodoListLayout todoListLayout)
        {
            _context.Entry(todoListLayout).State = EntityState.Modified;
        }
    }
}
