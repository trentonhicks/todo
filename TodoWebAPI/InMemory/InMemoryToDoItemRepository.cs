using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.InMemory
{
    public class InMemoryToDoItemRepository : IToDoItemRepository
    {
        public InMemoryToDoItemRepository()
        {
            _todo = new List<ToDos>();
            _todo.Add(new ToDos() { Id = 1, Completed = false, ListId = 1, Notes = "dude", ToDoName = "Clean" });
        }
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

        public async Task DeleteToDoAsync(int todoId)
        {
            var todo = _todo.Find(x => x.Id == todoId);

            _todo.Remove(todo);
        }

        public Task<TodoItemModel> CreateToDoAsync(TodoItemModel toDo)
        {
            throw new NotImplementedException();
        }
    }
}
