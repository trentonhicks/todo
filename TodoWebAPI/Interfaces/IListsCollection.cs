using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Interfaces
{
    public interface IListsCollection
    {
        Task<ListModel> CreateListAsync(ListModel list);
        Task<List<ListModel>> GetListsAsync(int accountId);
        Task<string> UpdateListAsync(int listId, string title);
        Task DeleteListAsync(int listId);
    }
}
