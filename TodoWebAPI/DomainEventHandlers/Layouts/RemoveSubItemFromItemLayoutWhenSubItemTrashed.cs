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
    public class RemoveSubItemFromItemLayoutWhenSubItemTrashed : INotificationHandler<SubItemMovedToTrash>
    {
        private readonly SubItemLayoutApplicationService _service;

        public RemoveSubItemFromItemLayoutWhenSubItemTrashed(SubItemLayoutApplicationService service)
        {
            _service = service;
        }
        public async Task Handle(SubItemMovedToTrash notification, CancellationToken cancellationToken)
        {
            await _service.RemoveSubItemFromLayoutAsync(notification.ItemId.GetValueOrDefault(), notification.SubItem.Id);
        }
    }
}
