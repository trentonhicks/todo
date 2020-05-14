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
        public async Task CreateTodoListLayoutAsync(Guid listId)
        {
            var layout = new TodoListLayout { ListId = listId };

            layout.Id = _todoListLayoutRepository.NextId();

            await _todoListLayoutRepository.AddLayoutAsync(layout);

            await _todoListLayoutRepository.SaveChangesAsync();
        }
       
        public async Task DeleteLayoutAsync(Guid todoListItemId, Guid listId)
        {
            var todoListLayout = await _todoListLayoutRepository.FindLayoutByListIdAsync(listId);

            todoListLayout.RemoveItemFromLayout(todoListItemId);
            _todoListLayoutRepository.Update(todoListLayout);
            await _todoListLayoutRepository.SaveChangesAsync();
        }
    }
}
