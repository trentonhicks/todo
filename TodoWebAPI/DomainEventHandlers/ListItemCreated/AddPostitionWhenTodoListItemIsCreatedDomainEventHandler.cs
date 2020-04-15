﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.Models;

namespace TodoWebAPI.DomainEventHandlers
{
    public class AddPostitionWhenTodoListItemIsCreatedDomainEventHandler : INotificationHandler<TodoListItemCreated>
    {
        private readonly TodoListLayoutApplicationService _todoListLayoutApplicationService;
        private readonly IMediator _mediator;
        public AddPostitionWhenTodoListItemIsCreatedDomainEventHandler(TodoListLayoutApplicationService todoListLayoutApplicationService,
            IMediator mediator)
        {
            _mediator = mediator;
            _todoListLayoutApplicationService = todoListLayoutApplicationService;
        }
        public Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
            return _mediator.Send(new TodoListLayoutModel { ItemId = notification.Item.Id, Position = 0, ListId = notification.List.Id });
        }
    }
}