using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.ApplicationServices
{
    public class SubItemLayoutApplicationService
    {
        private readonly ISubItemLayoutRepository _subLayout;

        public SubItemLayoutApplicationService(ISubItemLayoutRepository subLayout)
        {
            _subLayout = subLayout;
        }

        public async Task RemoveSubItemFromLayoutAsync(Guid todoListItemId, Guid subItemId)
        {
            var subItemLayout = await _subLayout.FindLayoutByListItemIdAsync(todoListItemId);

            subItemLayout.RemoveSubItemFromLayout(subItemId);
            _subLayout.Update(subItemLayout);
            await _subLayout.SaveChangesAsync();

        }
    }
}
