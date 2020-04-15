using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class MarkListAsCompletedDomainEventHandler : INotificationHandler<TodoListItemCompletedStateChanged>
    {
        private readonly TodoListApplicationService _todoListApplicationService;

        public MarkListAsCompletedDomainEventHandler(TodoListApplicationService todoListService)
        {
            _todoListApplicationService = todoListService;
        }
        public Task Handle(TodoListItemCompletedStateChanged notification, CancellationToken cancellationToken)
        {
            return _todoListApplicationService.MarkTodoListAsCompletedAsync(notification.Item.ListId.GetValueOrDefault());
        }
    }
}
