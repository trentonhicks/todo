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
    public class SendInvitationToClientWhenSubItemIsCreated : INotificationHandler<SubItemCreated>
    {
        private readonly IHubContext<NotificationHub> _hub;
        private readonly ITodoListItemRepository _todoListItem;
        private readonly ITodoListRepository _todoList;

        public SendInvitationToClientWhenSubItemIsCreated(IHubContext<NotificationHub> hub, ITodoListItemRepository todoListItem, ITodoListRepository todoList)
        {
            _hub = hub;
            _todoListItem = todoListItem;
            _todoList = todoList;
        }
        public async Task Handle(SubItemCreated notification, CancellationToken cancellationToken)
        {
            var item = await _todoListItem.FindToDoListItemByIdAsync(notification.SubItem.ListItemId.GetValueOrDefault());
            var list = await _todoList.FindTodoListIdByIdAsync(item.ListId.GetValueOrDefault());

            await _hub.Clients.Users(list.Contributors).SendAsync("SubItemCreated", notification.SubItem);
        }
    }
}
