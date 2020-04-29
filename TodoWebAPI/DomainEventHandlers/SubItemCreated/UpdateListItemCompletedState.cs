using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.WebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class UpdateListItemCompletedState : INotificationHandler<SubItemCreated>
    {
        private readonly TodoListItemApplicationService _service;

        public UpdateListItemCompletedState(TodoListItemApplicationService service)
        {
            _service = service;
        }
        public Task Handle(SubItemCreated notification, CancellationToken cancellationToken)
        {
           return _service.MarkTodoListItemAsCompletedAsync(notification.SubItem.ListItemId.GetValueOrDefault());
        }
    }
}
