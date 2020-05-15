using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers.EditItem
{
    public class SendNotificationWhenItemHadBeenUpdated : INotificationHandler<ItemChanged>
    {
        private readonly ITodoListRepository _todoList;
        private readonly IHubContext<NotificationHub> _context;

        public SendNotificationWhenItemHadBeenUpdated(ITodoListRepository todoList, IHubContext<NotificationHub> context)
        {
            _todoList = todoList;
            _context = context;
        }
        public async Task Handle(ItemChanged notification, CancellationToken cancellationToken)
        {
            var list = await _todoList.FindTodoListIdByIdAsync(notification.Item.ListId.GetValueOrDefault());

            await _context.Clients.Users(list.Contributors).SendAsync("ItemUpdated", notification.Item);
        }
    }
}
