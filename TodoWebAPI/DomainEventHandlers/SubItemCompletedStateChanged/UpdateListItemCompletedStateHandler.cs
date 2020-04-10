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
    public class UpdateListItemCompletedStateHandler : INotificationHandler<SubItemCompletedStateChanged>
    {
        private readonly TodoListItemApplicationService _service;

        public UpdateListItemCompletedStateHandler(TodoListItemApplicationService service)
        {
            _service = service;
        }
        public Task Handle(SubItemCompletedStateChanged notification, CancellationToken cancellationToken)
        {
            return _service.MarkTodoListItemAsCompletedAsync(notification.SubItem.ListItemId);
        }
    }
}
