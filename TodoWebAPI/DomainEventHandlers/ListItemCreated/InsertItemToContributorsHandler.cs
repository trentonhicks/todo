using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.SignalR;

namespace TodoWebAPI.DomainEventHandlers.ListItemCreated
{
    public class InsertItemToContributorsHandler : INotificationHandler<TodoListItemCreated>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public InsertItemToContributorsHandler(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
           return _hubContext.Clients.Users(notification.List.Contributors).SendAsync("ItemCreated", notification.List.Id, notification.Item);            
        }
    }
}
