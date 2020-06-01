using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;
using TodoWebAPI.ApplicationServices;

namespace TodoWebAPI.DomainEventHandlers
{
    public class RemoveSubItemFromItemLayoutWhenSubItemTrashed : INotificationHandler<SubItemMovedToTrash>
    {
        private readonly ISubItemLayoutRepository _subLayout;

        public RemoveSubItemFromItemLayoutWhenSubItemTrashed(ISubItemLayoutRepository subLayout)
        {
            _subLayout = subLayout;
        }
        public async Task Handle(SubItemMovedToTrash notification, CancellationToken cancellationToken)
        {
            var subItemLayout = await _subLayout.FindLayoutByListItemIdAsync(notification.ItemId.GetValueOrDefault());

            subItemLayout.RemoveSubItemFromLayout(notification.SubItem.Id);
            _subLayout.Update(subItemLayout);
            await _subLayout.SaveChangesAsync();

        }
    }
}
