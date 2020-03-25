using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;
using TodoWebAPI.Presentation;

namespace TodoWebAPI.InMemory
{
    public class InMemoryTodoListRepository : ITodoListRepository
    {
        private List<TodoListModel> _lists;
        private List<ToDos> _todos;
        public InMemoryTodoListRepository()
        {
            _lists = new List<TodoListModel>();
            _lists.Add(new TodoListModel() { Id = 1, AccountId = 1, ListTitle = "List 1" });
            _lists.Add(new TodoListModel() { Id = 2, AccountId = 1, ListTitle = "List 2" });
            _lists.Add(new TodoListModel() { Id = 3, AccountId = 2, ListTitle = "List 3" });
            _lists.Add(new TodoListModel() { Id = 4, AccountId = 1, ListTitle = "List 4" });
            _lists.Add(new TodoListModel() { Id = 5, AccountId = 2, ListTitle = "List 5" });
            _lists.Add(new TodoListModel() { Id = 6, AccountId = 1, ListTitle = "List 6" });
            _lists.Add(new TodoListModel() { Id = 7, AccountId = 1, ListTitle = "List 7" });

            _todos = new List<ToDos>();
            _todos.Add(new ToDos() { Id = 1, Completed = false, ListId = 1, ToDoName = "First Todo" });
            _todos.Add(new ToDos() { Id = 2, Completed = false, ListId = 1, ToDoName = "Second Todo" });
            _todos.Add(new ToDos() { Id = 3, Completed = false, ListId = 1, ToDoName = "Third Todo" });
        }
        
        public Task<TodoListModel> CreateListAsync(TodoListModel list)
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

        public Task<List<ListPresentation>> GetListsAsync(int accountId, int pageSize)
        {
            var todoLists = _lists.FindAll(x => x.AccountId == accountId);
            var listPresentation = new List<ListPresentation>();

            foreach(var todoList in todoLists)
            {
                var todos = _todos.FindAll(x => x.ListId == todoList.Id).Take(pageSize).ToList();
                listPresentation.Add(new ListPresentation(todoList, todos));
            }
            return Task.FromResult(listPresentation);
        }

        public Task<string> UpdateListAsync(int listId, string title)
        {
            var listIndex = _lists.FindIndex(x => x.Id == listId);
            _lists[listIndex].ListTitle = title;

            return Task.FromResult(title);
        }

        public Task DeleteTodosAsync(int listId)
        {
            _todos.RemoveAll(todo => todo.ListId == listId);

            return Task.CompletedTask;
        }

        public Task<TodoListModel> GetListAsync(TodoListModel list)
        {
          return Task.FromResult(_lists.Find(x => x.Id == list.Id));
        }

        public Task<string> UpdateListAsync(TodoListModel list)
        {
            var listIndex = _lists.FindIndex(x => x.Id == list.Id);
            _lists[listIndex].ListTitle = list.ListTitle;

            return Task.FromResult(list.ListTitle);
        }

        public Task<TodoListModel> GetListAsync(int listId)
        {
            var list = _lists.Find(x => x.Id == listId);
            return Task.FromResult(list);
        }
    }
}
