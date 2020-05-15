using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;
using TodoWebAPI.SignalR;

namespace TodoWebAPI.DomainEventHandlers.ListItemCompleted
{
    public class SendNotificationToClientWhenItemCompletedHandler : INotificationHandler<TodoListItemCompletedStateChanged>
    {
        private readonly Microsoft.AspNetCore.SignalR.IHubContext<NotificationHub> _hubContext;
        private readonly ITodoListRepository _todoListRepository;

        public SendNotificationToClientWhenItemCompletedHandler(IHubContext<NotificationHub> hubContext, ITodoListRepository todoListRepository)
        {
            _hubContext = hubContext;
            _todoListRepository = todoListRepository;
        }
        public async Task Handle(TodoListItemCompletedStateChanged notification, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.FindTodoListIdByIdAsync(notification.Item.ListId.GetValueOrDefault());

            await _hubContext.Clients.Users(todoList.Contributors).SendAsync("ItemCompleted", notification.Item);
        }
    }
}
