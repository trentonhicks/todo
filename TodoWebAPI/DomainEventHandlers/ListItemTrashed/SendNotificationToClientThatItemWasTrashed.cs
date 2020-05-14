using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;

namespace TodoWebAPI.DomainEventHandlers.ListItemTrashed
{
    public class SendNotificationToClientThatItemWasTrashed : INotificationHandler<ItemMovedToTrash>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendNotificationToClientThatItemWasTrashed(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(ItemMovedToTrash notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("ItemDeleted");
        }
    }
}
