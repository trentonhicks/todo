using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class AddPostitionWhenTodoListItemIsCreatedDomainEventHandler : INotificationHandler<TodoListItemCreated>
    {
        private readonly TodoListLayoutApplicationService _todoListLayoutApplicationService;

        public AddPostitionWhenTodoListItemIsCreatedDomainEventHandler(TodoListLayoutApplicationService todoListLayoutApplicationService)
        {
            _todoListLayoutApplicationService = todoListLayoutApplicationService;
        }
        public Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
            return _todoListLayoutApplicationService.UpdateLayoutAsync(notification.Item.Id, todoListItemPosition: 0, notification.List.Id);
        }
    }
}
