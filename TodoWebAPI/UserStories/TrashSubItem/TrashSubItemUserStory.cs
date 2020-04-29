using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.TrashSubItem
{
    public class TrashSubItemUserStory : IRequestHandler<TrashSubItem>
    {
        private readonly ISubItemRepository _subItemRepository;
        public TrashSubItemUserStory(ISubItemRepository subItemRepository)
        {
            _subItemRepository = subItemRepository;
        }
        public async Task<Unit> Handle(TrashSubItem request, CancellationToken cancellationToken)
        {
            var subItem = await _subItemRepository.FindByIdAsync(request.SubItemId);

            subItem.MoveToTrash();

            await _subItemRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
