using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class RemoveItemFromListLayoutWhenTodoListItemIsTrashed : INotificationHandler<ItemMovedToTrash>
    {
        private readonly TodoListLayoutApplicationService _service;

        public RemoveItemFromListLayoutWhenTodoListItemIsTrashed(TodoListLayoutApplicationService service)
        {
            _service = service;
        }
        public Task Handle(ItemMovedToTrash notification, CancellationToken cancellationToken)
        {
            return _service.DeleteLayoutAsync(notification.Item.Id, notification.ListId.GetValueOrDefault());
        }
    }
}
