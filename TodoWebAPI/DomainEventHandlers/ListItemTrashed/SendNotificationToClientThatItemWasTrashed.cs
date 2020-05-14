using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers.ListItemTrashed
{
    public class SendNotificationToClientThatItemWasTrashed : INotificationHandler<ItemMovedToTrash>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ITodoListRepository _todoListRepository;

        public SendNotificationToClientThatItemWasTrashed(IHubContext<NotificationHub> hubContext, ITodoListRepository todoListRepository)
        {
            _hubContext = hubContext;
            _todoListRepository = todoListRepository;
        }
        public async Task Handle(ItemMovedToTrash notification, CancellationToken cancellationToken)
        {
            var todoList = await _todoListRepository.FindTodoListIdByIdAsync(notification.ListId.GetValueOrDefault());

            await _hubContext.Clients.Users(todoList.Contributors).SendAsync("ItemTrashed", notification.ListId, notification.Item);
        }
    }
}

