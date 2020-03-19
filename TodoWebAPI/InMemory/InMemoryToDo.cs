using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.InMemory
{
    public class InMemoryToDo : IToDoRepository
    {
        public InMemoryToDo()
        {
            _accounts = new List<AccountModel>();
            _accounts.Add(new AccountModel() { Id = 1, FullName = "Parker", UserName = "parker", Picture = "", Password = "1234" });
            _list = new List<ListModel>();
            _list.Add(new ListModel() { Id = 1, AccountId = 1, ListTitle = "New List" });

            _todo = new List<ToDos>();
            _todo.Add(new ToDos() { Id = 1, Completed = false, ListId = 1, Notes = "", ToDoName = "yes" });
        }
        private List<AccountModel> _accounts;
        private List<ListModel> _list;
        private List<ToDos> _todo;

        public Task<ToDos> CreateToDoAsync(ToDos toDo)
        {
            toDo.Id = 1;
            _todo.Add(toDo);
            return Task.FromResult(toDo);
        }
        public Task<ToDos> UpdateToDoAsync(int todoId, ToDos todo)
        {
            var foo = new ToDos()
            {
                Id = todoId,
                Notes = todo.Notes,
                Completed = todo.Completed,
                ToDoName = todo.ToDoName
            };

            return Task.FromResult(foo);
        }
        //public Task<string> UpdateListAsync(int listId, string title)
        //{
        //    var listIndex = _lists.FindIndex(x => x.Id == listId);
        //    _lists[listIndex].ListTitle = title;

        //    return Task.FromResult(title);
        //}


        public async Task DeleteToDoAsync(int todoId)
        {
            var todo = _todo.Find(x => x.Id == todoId);

            _todo.Remove(todo);
        }
    }
}
