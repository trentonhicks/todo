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
        public InMemoryListsCollection()
        {
            _accounts = new List<AccountModel>();
            _accounts.Add(new AccountModel() { Id = 1, FullName = "Trenton Hicks", Password = "myPassword123!", UserName = "trentonhicks" });
            _accounts.Add(new AccountModel() { Id = 2, FullName = "John Doe", Password = "myPassword123!", UserName = "johndoe" });
            _lists = new List<ListModel>();
            _lists.Add(new ListModel() { Id = 1, AccountId = 1, ListTitle = "List 1" });
            _lists.Add(new ListModel() { Id = 2, AccountId = 1, ListTitle = "List 2" });
            _lists.Add(new ListModel() { Id = 3, AccountId = 2, ListTitle = "List 3" });
            _lists.Add(new ListModel() { Id = 4, AccountId = 1, ListTitle = "List 4" });
            _lists.Add(new ListModel() { Id = 5, AccountId = 2, ListTitle = "List 5" });
            _lists.Add(new ListModel() { Id = 6, AccountId = 1, ListTitle = "List 6" });
            _lists.Add(new ListModel() { Id = 7, AccountId = 1, ListTitle = "List 7" });
        }
        private List<ListModel> _lists;
        private List<AccountModel> _accounts;

        public Task<ListModel> CreateListAsync(ListModel list)
        {
            list.Id = 1;
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
