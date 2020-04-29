using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.TrashItem
{
    public class TrashItemUserStory : AsyncRequestHandler<TrashItem>
    {
        private readonly ITodoListItemRepository _listItemRepository;

        public TrashItemUserStory(ITodoListItemRepository listItemRepository)
        {
            _listItemRepository = listItemRepository;
        }
        protected override async Task Handle(TrashItem request, CancellationToken cancellationToken)
        {
            var item = await _listItemRepository.FindToDoListItemByIdAsync(request.ItemId);

            item.MoveToTrash();

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
