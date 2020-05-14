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
    public class SendNotificationToClientWhenLayoutIsChanged : INotificationHandler<ListLayoutUpdated>
    {
        private readonly IHubContext<NotificationHub> _context;
        private readonly ITodoListRepository _todoList;

        public SendNotificationToClientWhenLayoutIsChanged(IHubContext<NotificationHub> context, ITodoListRepository todoList)
        {
            _context = context;
            _todoList = todoList;
        }
        public async Task Handle(ListLayoutUpdated notification, CancellationToken cancellationToken)
        {
            var list = await _todoList.FindTodoListIdByIdAsync(notification.ListId);
            await _context.Clients.Users(list.Contributors).SendAsync("ListLayoutChanged", notification.ListId);
        }
    }
}
