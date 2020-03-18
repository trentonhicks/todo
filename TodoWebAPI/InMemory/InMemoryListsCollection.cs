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
            _lists = new List<ListModel>();
            _lists.Add(new ListModel() { Id = 1, AccountId = 1, ListTitle = "List 1" });
            _lists.Add(new ListModel() { Id = 2, AccountId = 1, ListTitle = "List 2" });
            _lists.Add(new ListModel() { Id = 3, AccountId = 2, ListTitle = "List 3" });
            _lists.Add(new ListModel() { Id = 4, AccountId = 1, ListTitle = "List 4" });
            _lists.Add(new ListModel() { Id = 5, AccountId = 2, ListTitle = "List 5" });
            _lists.Add(new ListModel() { Id = 6, AccountId = 1, ListTitle = "List 6" });
            _lists.Add(new ListModel() { Id = 7, AccountId = 1, ListTitle = "List 7" });

            _todos = new List<ToDos>();
            _todos.Add(new ToDos() { Id = 1, Completed = false, ListId = 1, ToDoName = "First Todo" });
            _todos.Add(new ToDos() { Id = 3, Completed = false, ListId = 1, ToDoName = "First Todo" });
            _todos.Add(new ToDos() { Id = 2, Completed = false, ListId = 1, ToDoName = "First Todo" });
        }
        private List<ListModel> _lists;
        private List<ToDos> _todos;

        public Task<ListModel> CreateListAsync(ListModel list)
        {
            _lists.Add(list);
            return Task.FromResult(list);
        }

        public async Task DeleteListAsync(int listId)
        {
            var list = _lists.Find(x => x.Id == listId);

            await DeleteTodosAsync(listId);

            _lists.Remove(list);
        }

        public Task<List<ListModel>> GetListsAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateListAsync(int listId, string title)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTodosAsync(int listId)
        {
            _todos.RemoveAll(todo => todo.ListId == listId);

            return Task.CompletedTask;
        }
    }
}
