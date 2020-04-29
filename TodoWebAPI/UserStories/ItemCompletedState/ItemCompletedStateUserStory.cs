using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.ItemCompletedState
{
    public class ItemCompletedStateUserStory : AsyncRequestHandler<ItemCompletedState>
    {
        private readonly ITodoListItemRepository _listItemRepository;

        public ItemCompletedStateUserStory(ITodoListItemRepository listItemRepository )
        {
            _listItemRepository = listItemRepository;
        }
        protected override async Task Handle(ItemCompletedState request, CancellationToken cancellationToken)
        {
            var subItemCount = await _listItemRepository.GetSubItemCountAsync(request.ItemId);

            if (subItemCount > 0)
                return;
            
            var item = await _listItemRepository.FindToDoListItemByIdAsync(request.ItemId);

            if (request.Completed == true)
            {
                item.SetCompleted();
            }
            else if (request.Completed== false)
            {
                item.SetNotCompleted();
            }

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
