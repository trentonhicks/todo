using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers
{
    public class UpdateListItemCompletedStateHandler : INotificationHandler<SubItemCompletedStateChanged>
    {
        private readonly ISubItemRepository _subItemRepository;
        private readonly ITodoListItemRepository _listItemRepository;

        public UpdateListItemCompletedStateHandler(ISubItemRepository subItemRepository, ITodoListItemRepository listItemRepository)
        {
            _subItemRepository = subItemRepository;
            _listItemRepository = listItemRepository;
        }
        public async Task Handle(SubItemCompletedStateChanged notification, CancellationToken cancellationToken)
        {
            var subItems = await _subItemRepository.FindAllSubItemsByListItemIdAsync(notification.SubItem.ListItemId.GetValueOrDefault());
            var listItem = await _listItemRepository.FindToDoListItemByIdAsync(notification.SubItem.ListItemId.GetValueOrDefault());

            listItem.SetCompleted(subItems);

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
