using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using TodoWebAPI.ApplicationServices;
using TodoWebAPI.UserStories.ItemLayout;

namespace TodoWebAPI.DomainEventHandlers
{
    public class AddPositionWhenSubItemIsCreated : INotificationHandler<SubItemCreated>
    {
        private readonly IMediator _mediator;

        public AddPositionWhenSubItemIsCreated(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Handle(SubItemCreated notification, CancellationToken cancellationToken)
        {
            return _mediator.Send(new ItemLayout { SubItemId = notification.SubItem.Id, Position = 0, ItemId = notification.SubItem.ListItemId.GetValueOrDefault()});
        }
    }
}
