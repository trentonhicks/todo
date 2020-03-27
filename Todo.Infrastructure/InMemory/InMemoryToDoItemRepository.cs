using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.InMemory
{
    public class InMemoryToDoItemRepository : ITodoListItemRepository
    {
        public InMemoryToDoItemRepository()
        {
            _todo = new List<TodoListItem>();
            _todo.Add(new TodoListItem() { Id = 1, Completed = false, ListId = 1, Notes = "dude", ToDoName = "Clean" });
        }
        private List<TodoListItem> _todo;

        public Task AddTodoListItemAsync(TodoListItem todo)
        {
            throw new NotImplementedException();
        }

        public Task<TodoListItem> UpdateToDoListItemAsync(int listId, TodoListItem todo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTodoListItemAsync(int todo)
        {
            throw new NotImplementedException();
        }


        //public Task<TodoListItem> CreateToDoAsync(TodoListItem toDo)
        //{
        //    toDo.Id = 1;
        //    _todo.Add(toDo);
        //    return Task.FromResult(toDo);
        //}

        //public Task<TodoListItem> UpdateToDoAsync(int todoId, TodoListItem todo)
        //{
        //    var foo = new To()
        //    {
        //        Id = todoId,
        //        Notes = todo.Notes,
        //        Completed = todo.Completed,
        //        ToDoName = todo.ToDoName
        //    };

        //    return Task.FromResult(foo);
        //}

        //public async Task DeleteToDoAsync(int todoId)
        //{
        //    var todo = _todo.Find(x => x.Id == todoId);

        //    _todo.Remove(todo);
        //}

        //public Task<TodoItemModel> CreateToDoAsync(TodoItemModel toDo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
