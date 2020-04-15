using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.ApplicationServices
{
    public class SubItemApplicationService
    {
        private readonly ISubItemRepository _subItems;

        public SubItemApplicationService(ISubItemRepository subItems)
        {
            _subItems = subItems;
        }
       
        public async Task ChangeCompletedStateAsync(int subItemId, bool completed)
        {
            var subItem = await _subItems.FindByIdAsync(subItemId);

            if (completed)
                subItem.SetCompleted();

            else
                subItem.SetNotCompleted();

            await _subItems.SaveChangesAsync();
        }
    }
}
