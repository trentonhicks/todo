using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;

namespace TodoWebAPI.Interfaces
{
    public interface IListsCollection
    {
        Task<Lists> CreateList(Lists list);
        Task<List<Lists>> GetLists(int accountId);
        Task<string> UpdateList(int accountId, int listId, string title);
        Task DeleteList(int accountId, int listId);
    }
}
