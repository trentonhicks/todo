using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;
using Todo.Domain.Services;

namespace TodoWebAPI.DomainEventHandlers
{
    public class MarkListAsCompletedDomainEventHandler : INotificationHandler<TodoListItemCompleted>
    {
        private readonly TodoListService _todoListService;

        public MarkListAsCompletedDomainEventHandler(TodoListService todoListService)
        {
            _todoListService = todoListService;
        }
        public Task Handle(TodoListItemCompleted notification, CancellationToken cancellationToken)
        {
            return _todoListService.MarkTodoListAsCompletedAsync(notification.ListId);
        }
    }
}
