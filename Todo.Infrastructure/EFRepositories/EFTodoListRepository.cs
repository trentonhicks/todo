using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using Todo.Infrastructure.Guids;

namespace TodoWebAPI.Data
{
    public class EFTodoListRepository : ITodoListRepository
    {
        private readonly ISequentialIdGenerator _idGenerator;

        private TodoDatabaseContext _context { get; set; }
        public EFTodoListRepository(TodoDatabaseContext context, ISequentialIdGenerator idGenerator)
        {
            _context = context;
            _idGenerator = idGenerator;
        }
        public Task AddTodoListAsync(TodoList todoList)
        {
            _context.TodoLists.Add(todoList);
            return Task.CompletedTask;
        }

        public Task<List<TodoList>> FindTodoListsByAccountIdAsync(Guid accountId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoList> FindTodoListIdByIdAsync(Guid listId)
        {
            return await _context.TodoLists.FindAsync(listId);
        }
        public async Task RemoveTodoListAsync(Guid listId)
        {
            var list = await _context.TodoLists.FindAsync(listId);

            _context.Remove(list);
        }

        public async Task RemoveAllTodoListsFromAccountAsync(Guid accountId)
        {
            var todoLists = await _context.TodoLists.Where(t => t.AccountId == accountId).ToListAsync();

            _context.TodoLists.RemoveRange(todoLists);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Guid NextId()
        {
           return _idGenerator.NextId();
        }
    }
}