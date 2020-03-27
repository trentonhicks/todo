using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace Todo.Domain.Services
{
    public class TodoListItemService
    {
        private readonly ITodoListRepository _listRepository;
        private readonly ITodoListItemRepository _listItemRepository;

        public TodoListItemService(ITodoListRepository listRepository, ITodoListItemRepository todoListItemRepository )
        {
            _listRepository = listRepository;
            _listItemRepository = todoListItemRepository;
        }

        public async Task<bool> CreateTodoListAsync(int listId, int? parentId, bool completed, string todoName, string notes)
        {
            var doesListExist = await _listRepository.FindTodoListIdByIdAsync(listId);

            if (doesListExist == null)
                return false;

            var todoItem = new TodoListItem()
            {
                ListId = listId,
                ParentId = parentId,
                Completed = completed,
                ToDoName = todoName,
                Notes = notes
            };

            await _listItemRepository.AddTodoListItemAsync(todoItem);
            return true;
        }






    }
}
