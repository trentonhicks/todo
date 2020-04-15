using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.ItemLayout
{
    public class ItemLayoutUserStory : IRequestHandler<ItemLayout>
    {
        private readonly ISubItemLayout _subItemLayout;

        public ItemLayoutUserStory(ISubItemLayout subItemLayout)
        {
            _subItemLayout = subItemLayout;
        }
        public async Task<Unit> Handle(ItemLayout request, CancellationToken cancellationToken)
        {
            var layout = await _subItemLayout.FindLayoutByListItemIdAsync(request.ItemId);

            layout.UpdateLayout(request.SubItemId, request.Position);

            _subItemLayout.Update(layout);
            await _subItemLayout.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
