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
    public class CreateLayoutWhenListIsCreatedDomainEventHandler : INotificationHandler<TodoListCreated>
    {
        private readonly TodoListLayoutApplicationService _todoListLayoutApplicationService;
        public CreateLayoutWhenListIsCreatedDomainEventHandler(TodoListLayoutApplicationService todoListLayoutApplicationService)
        {
            _todoListLayoutApplicationService = todoListLayoutApplicationService;
        }
        public async Task Handle(TodoListCreated notification, CancellationToken cancellationToken)
        {
            await _todoListLayoutApplicationService.CreateTodoListLayoutAsync(notification.List.Id);
        }
    }
}
