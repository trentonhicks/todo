using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.SubItemCompletedState
{
    public class SubItemCompletedStateUserStory : AsyncRequestHandler<SubItemCompletedState>
    {
        private readonly ISubItemRepository _subItems;

        public SubItemCompletedStateUserStory(ISubItemRepository subItems)
        {
            _subItems = subItems;
        }
        protected override async Task Handle(SubItemCompletedState request, CancellationToken cancellationToken)
        {
            var subItem = await _subItems.FindByIdAsync(request.SubItemId);

            if (request.Completed)
                subItem.SetCompleted();

            else
                subItem.SetNotCompleted();

            await _subItems.SaveChangesAsync();
        }
    }
}
