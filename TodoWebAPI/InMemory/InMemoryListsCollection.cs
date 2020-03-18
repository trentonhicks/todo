using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Interfaces;
using TodoWebAPI.Models;

namespace TodoWebAPI.InMemory
{
    public class InMemoryListsCollection : IListsCollection
    {
        private List<ListModel> _lists = new List<ListModel>();

        public Task<ListModel> CreateListAsync(ListModel list)
        {
            _lists.Add(list);
            return Task.FromResult(list);
        }

        public Task DeleteList(int accountId, int listId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ListModel>> GetListsAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateList(int accountId, int listId, string title)
        {
            throw new NotImplementedException();
        }
    }
}
