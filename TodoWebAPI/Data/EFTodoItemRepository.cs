using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;
using TodoWebAPI.Repositories;

namespace TodoWebAPI.Data
{
    public class EFTodoItemRepository : IToDoItemRepository
    {
        public Task<ToDos> CreateToDoAsync(ToDos toDo)
        {
            throw new NotImplementedException();
        }

        public Task<ToDos> UpdateToDoAsync(int listId, ToDos toDo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteToDoAsync(int todo)
        {
            throw new NotImplementedException();
        }
    }
}