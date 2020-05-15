using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.SignalR;

namespace TodoWebAPI.DomainEventHandlers
{
    public class SendNotificationWhenTodoListCompletedStateChangedEventHandler: INotificationHandler<TodoListCompletedStateChanged>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendNotificationWhenTodoListCompletedStateChangedEventHandler(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(TodoListCompletedStateChanged notification, CancellationToken cancellationToken)
        {
           return _hubContext.Clients.Users(notification.List.Contributors).SendAsync("ListCompletedStateChanged", notification.List.Id, notification.List.Completed);
        }
    }
}