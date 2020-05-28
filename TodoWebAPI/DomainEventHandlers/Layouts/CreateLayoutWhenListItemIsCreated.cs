using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.ApplicationServices
{
    public class CreateLayoutWhenListItemIsCreated : INotificationHandler<TodoListItemCreated>
    {
        private readonly ISubItemLayoutRepository _subItemLayout;

        public CreateLayoutWhenListItemIsCreated(ISubItemLayoutRepository subItemLayout)
        {
            _subItemLayout = subItemLayout;
        }

        public async Task Handle(TodoListItemCreated notification, CancellationToken cancellationToken)
        {
            var layout = new SubItemLayout { ItemId = notification.Item.Id };

            layout.Id = _subItemLayout.NextId();

            await _subItemLayout.AddLayoutAsync(layout);

            await _subItemLayout.SaveChangesAsync();
        }
    }
}