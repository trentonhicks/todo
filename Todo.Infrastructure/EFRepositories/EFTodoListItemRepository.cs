﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using TodoWebAPI.InMemory;

namespace TodoWebAPI.Data
{
    public class EFTodoListItemRepository : ITodoListItemRepository
    {
        private readonly TodoDatabaseContext _context;
        public EFTodoListItemRepository(TodoDatabaseContext context)
        {
            _context = context;
        }
       
        public async Task AddTodoListItemAsync(TodoListItem todo)
        {
            _context.TodoListItems.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTodoListItemAsync(int todo)
        {
           var todoListItem =  await _context.TodoListItems.FindAsync(todo);
            _context.Remove(todoListItem);
        }

        public async Task RemoveAllTodoListItemsFromAccountAsync(int accountId)
        {
            var todoListItems = await _context.TodoListItems.Where(t => t.AccountId == accountId).ToListAsync();

            _context.TodoListItems.RemoveRange(todoListItems);
            await _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TodoListItem> FindToDoListItemByIdAsync(int todoListItemId)
        {
            return await _context.TodoListItems.FindAsync(todoListItemId);
        }
    }
}