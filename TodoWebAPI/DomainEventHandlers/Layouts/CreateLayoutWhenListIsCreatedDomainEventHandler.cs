using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class CreateLayoutWhenListIsCreatedDomainEventHandler : INotificationHandler<TodoListCreated>
    {
        private readonly ITodoListLayoutRepository _todoListLayoutRepository;

        public CreateLayoutWhenListIsCreatedDomainEventHandler(ITodoListLayoutRepository todoListLayoutRepository)
        {
            _todoListLayoutRepository = todoListLayoutRepository;
        }
        public async Task Handle(TodoListCreated notification, CancellationToken cancellationToken)
        {
            var layout = new TodoListLayout { ListId = notification.List.Id};

            layout.Id = _todoListLayoutRepository.NextId();

            await _todoListLayoutRepository.AddLayoutAsync(layout);

            await _todoListLayoutRepository.SaveChangesAsync();
        }
    }
}
