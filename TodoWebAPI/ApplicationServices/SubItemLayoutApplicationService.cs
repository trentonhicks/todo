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
        private readonly ISubItemLayout _subLayout;

        public SubItemLayoutApplicationService(ISubItemLayout subLayout)
        {
            _subLayout = subLayout;
        }

        public async Task CreateTodoListLayoutAsync(int itemId)
        {
            var layout = new SubItemLayout { ItemId = itemId };

            await _subLayout.AddLayoutAsync(layout);

            await _subLayout.SaveChangesAsync();
        }
        public async Task UpdateLayoutAsync(int subItemId, int subItemPosition, int itemId)
        {
            var layout = await _subLayout.FindLayoutByListItemIdAsync(itemId);

            layout.UpdateLayout(subItemId, subItemPosition);

            _subLayout.Update(layout);
            await _subLayout.SaveChangesAsync();

        }   
    }
}
