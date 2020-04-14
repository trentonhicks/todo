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
    public class AddPositionWhenSubItemIsCreated : INotificationHandler<SubItemCreated>
    {
        private readonly SubItemLayoutApplicationService _service;

        public AddPositionWhenSubItemIsCreated(SubItemLayoutApplicationService service)
        {
            _service = service;
        }
        public Task Handle(SubItemCreated notification, CancellationToken cancellationToken)
        {
            return _service.UpdateLayoutAsync(notification.SubItem.Id, 0, notification.SubItem.ListItemId);
        }
    }
}
