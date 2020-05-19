using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.SignalR;

namespace TodoWebAPI.DomainEventHandlers.ListEdited
{
    public class SendNotificationToClientWhenListNameIsEdited : INotificationHandler<ListNameUpdated>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendNotificationToClientWhenListNameIsEdited(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(ListNameUpdated notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.Users(notification.List.Contributors).SendAsync("ListNameUpdated", notification.List);
        }
    }
}
