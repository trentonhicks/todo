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
    public class UpdateListItemCompletedState : INotificationHandler<SubItemCreated>
    {
        private readonly ISubItemRepository _subItemRepository;
        private readonly ITodoListItemRepository _listItemRepository;

        public UpdateListItemCompletedState(ISubItemRepository subItemRepository, ITodoListItemRepository listItemRepository)
        {
            _subItemRepository = subItemRepository;
            _listItemRepository = listItemRepository;
        }
        public async Task Handle(SubItemCreated notification, CancellationToken cancellationToken)
        {
            var subItems = await _subItemRepository.FindAllSubItemsByListItemIdAsync(notification.SubItem.ListItemId.GetValueOrDefault());
            var listItem = await _listItemRepository.FindToDoListItemByIdAsync(notification.SubItem.ListItemId.GetValueOrDefault());

            listItem.SetCompleted(subItems);

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
