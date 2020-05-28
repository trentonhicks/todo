using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class RemoveItemFromListLayoutWhenTodoListItemIsTrashed : INotificationHandler<ItemMovedToTrash>
    {
        private readonly ITodoListLayoutRepository _todoListLayoutRepository;

        public RemoveItemFromListLayoutWhenTodoListItemIsTrashed(ITodoListLayoutRepository todoListLayoutRepository)
        {
            _todoListLayoutRepository = todoListLayoutRepository;
        }
        public async Task Handle(ItemMovedToTrash notification, CancellationToken cancellationToken)
        {
            var todoListLayout = await _todoListLayoutRepository.FindLayoutByListIdAsync(notification.ListId.GetValueOrDefault());

            todoListLayout.RemoveItemFromLayout(notification.Item.Id);
            _todoListLayoutRepository.Update(todoListLayout);
            await _todoListLayoutRepository.SaveChangesAsync();
        }
    }
}
