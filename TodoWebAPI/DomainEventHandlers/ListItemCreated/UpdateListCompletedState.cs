using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers.ListItemCreated
{
    public class UpdateListCompletedState : INotificationHandler<TodoListItemCreated>
    {
        private readonly TodoListApplicationService _todoListApplicationService;

        public UpdateListCompletedState(TodoListApplicationService todoListApplicationService)
        {
            _todoListApplicationService = todoListApplicationService;
        }
        public Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
            return _todoListApplicationService.MarkTodoListAsCompletedAsync(notification.List.Id);
        }
    }
}
