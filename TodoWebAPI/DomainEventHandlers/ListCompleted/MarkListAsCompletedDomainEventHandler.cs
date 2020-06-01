using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers
{
    public class MarkListAsCompletedDomainEventHandler : INotificationHandler<TodoListItemCompletedStateChanged>
    {
        private readonly ITodoListItemRepository _itemRepository;
        private readonly ITodoListRepository _listRepository;

        public MarkListAsCompletedDomainEventHandler(ITodoListItemRepository itemRepository, ITodoListRepository listRepository)
        {
            _itemRepository = itemRepository;
            _listRepository = listRepository;
        }
        public async Task Handle(TodoListItemCompletedStateChanged notification, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.FindAllTodoListItemsByListIdAsync(notification.Item.ListId.GetValueOrDefault());
            var list = await _listRepository.FindTodoListIdByIdAsync(notification.Item.ListId.GetValueOrDefault());

            list.SetCompleted(items);

            await _listRepository.SaveChangesAsync();
        }
    }
}
