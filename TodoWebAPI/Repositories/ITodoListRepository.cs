using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.Repositories
{
    public interface ITodoListRepository
    {
        Task<TodoListModel> CreateListAsync(TodoListModel list);
        Task<List<ListPresentation>> GetListsAsync(int accountId, int pageSize);
        Task<TodoListModel> GetListAsync(TodoListModel list);
        Task<string> UpdateListAsync(TodoListModel list);
        Task DeleteListAsync(int listId);
    }
}
