using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.ApplicationServices
{
    public class TodoListApplicationService
    {
        private readonly ITodoListItemRepository _itemRepository;
        private readonly ITodoListRepository _listRepository;
        public TodoListApplicationService(ITodoListItemRepository itemRepository, ITodoListRepository listRepository)
        {
            _itemRepository = itemRepository;
            _listRepository = listRepository;
        }
        public async Task MarkTodoListAsCompletedAsync(Guid listId)
        {
            var items = await _itemRepository.FindAllTodoListItemsByListIdAsync(listId);
            var list = await _listRepository.FindTodoListIdByIdAsync(listId);

            list.SetCompleted(items);

            await _listRepository.SaveChangesAsync();
        }
    }
}
