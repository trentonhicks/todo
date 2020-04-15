using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;

namespace TodoWebAPI.ApplicationServices
{
    public class CreateLayoutWhenListItemIsCreated : INotificationHandler<TodoListItemCreated>
    {
        private readonly SubItemLayoutApplicationService _service;

        public CreateLayoutWhenListItemIsCreated(SubItemLayoutApplicationService service)
        {
            _service = service;
        }
       
        public Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
            return _service.CreateTodoListItemLayoutAsync(notification.Item.Id);
        }
    }
}