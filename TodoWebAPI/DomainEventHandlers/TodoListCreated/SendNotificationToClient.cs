using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class NotificationHub : Hub
    {

    }
    public class SendNotificationToClient : INotificationHandler<TodoListCreated>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendNotificationToClient(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(TodoListCreated notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("RefreshList");
        }
    }
}
