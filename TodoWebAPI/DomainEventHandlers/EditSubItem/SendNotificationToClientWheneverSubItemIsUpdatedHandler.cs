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


namespace TodoWebAPI.DomainEventHandlers
{
    public class SendNotificationToClientWheneverSubItemIsUpdatedHandler : INotificationHandler<EditSubItem>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ITodoListItemRepository _todoListItem;
        private readonly ITodoListRepository _todoList;

        public SendNotificationToClientWheneverSubItemIsUpdatedHandler(IHubContext<NotificationHub> hubContext, ITodoListItemRepository todoListItem, ITodoListRepository todoList)
        {
            _hubContext = hubContext;
            _todoListItem = todoListItem;
            _todoList = todoList;
        }
        public async Task Handle(EditSubItem notification, CancellationToken cancellationToken)
        {
            var item = await _todoListItem.FindToDoListItemByIdAsync(notification.SubItem.ListItemId.GetValueOrDefault());
            var list = await _todoList.FindTodoListIdByIdAsync(item.ListId.GetValueOrDefault());

            await _hubContext.Clients.Users(list.Contributors).SendAsync("SubItemUpdated", notification.SubItem);
        }
    }
}
