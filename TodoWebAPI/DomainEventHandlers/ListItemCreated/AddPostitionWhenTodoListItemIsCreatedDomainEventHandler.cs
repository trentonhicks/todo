using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.Models;
using TodoWebAPI.UserStories.ListLayout;

namespace TodoWebAPI.DomainEventHandlers
{
    public class AddPostitionWhenTodoListItemIsCreatedDomainEventHandler : INotificationHandler<TodoListItemCreated>
    {
        private readonly IMediator _mediator;
        public AddPostitionWhenTodoListItemIsCreatedDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
            return _mediator.Send(new ListLayout { ItemId = notification.Item.Id, Position = 0, ListId = notification.List.Id });
        }
    }
}
