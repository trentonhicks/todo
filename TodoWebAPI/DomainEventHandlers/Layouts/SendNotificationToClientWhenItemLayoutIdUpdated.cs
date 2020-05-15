using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers.Layouts
{
    public class SendNotificationToClientWhenItemLayoutIdUpdated : INotificationHandler<ItemLayoutUpdated>
    {
        private readonly IHubContext<NotificationHub> _context;
        private readonly ITodoListItemRepository _todoListItem;
        private readonly ITodoListRepository _todoList;

        public SendNotificationToClientWhenItemLayoutIdUpdated(IHubContext<NotificationHub> context, ITodoListItemRepository todoListItem, ITodoListRepository todoList)
        {
            _context = context;
            _todoListItem = todoListItem;
            _todoList = todoList;
        }
        public async Task Handle(ItemLayoutUpdated notification, CancellationToken cancellationToken)
        {
            var item = await _todoListItem.FindToDoListItemByIdAsync(notification.ItemId);
            var list = await _todoList.FindTodoListIdByIdAsync(item.ListId.GetValueOrDefault());

            await _context.Clients.Users(list.Contributors).SendAsync("ItemLayoutUpdated", notification.ItemId);
        }
    }
}
