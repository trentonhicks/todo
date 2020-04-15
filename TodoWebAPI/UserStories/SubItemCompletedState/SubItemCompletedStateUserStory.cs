using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.SubItemCompletedState
{
    public class SubItemCompletedStateUserStory : IRequestHandler<SubItemCompletedState>
    {
        private readonly ISubItemRepository _subItems;

        public SubItemCompletedStateUserStory(ISubItemRepository subItems)
        {
            _subItems = subItems;
        }
        public async Task<Unit> Handle(SubItemCompletedState request, CancellationToken cancellationToken)
        {
            var subItem = await _subItems.FindByIdAsync(request.SubItemId);

            if (request.Completed)
                subItem.SetCompleted();

            else
                subItem.SetNotCompleted();

            await _subItems.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
