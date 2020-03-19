using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.Repositories
{
    public interface IListsRepository
    {
        Task<ListModel> CreateListAsync(ListModel list);
        Task<List<ListPresentation>> GetListsAsync(int accountId, int pageSize);
        Task<string> UpdateListAsync(int listId, string title);
        Task DeleteListAsync(int listId);
    }
}
