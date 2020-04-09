using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.ApplicationServices
{
    public class TodoListLayoutApplicationService
    {
        private readonly ITodoListLayoutRepository _todoListLayoutRepository;
        public TodoListLayoutApplicationService(ITodoListLayoutRepository todoListLayoutRepository)
        {
            _todoListLayoutRepository = todoListLayoutRepository;
        }
        public async Task CreateTodoListLayoutAsync(int listId)
        {
            var layout = new TodoListLayout { ListId = listId };

            await _todoListLayoutRepository.AddLayoutAsync(layout);

            await _todoListLayoutRepository.SaveChangesAsync();
        }
        public async Task UpdateLayoutAsync(int todoListItemId, int todoListItemPosition, int listId)
        {
            var todoListLayout = await _todoListLayoutRepository.FindLayoutByListIdAsync(listId);

            todoListLayout.UpdateLayout(todoListItemId, todoListItemPosition);

            _todoListLayoutRepository.Update(todoListLayout);
            await _todoListLayoutRepository.SaveChangesAsync();
        }
    }
}
