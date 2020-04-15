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

        public async Task CreateTodoListItemLayoutAsync(int itemId)
        {
            var layout = new SubItemLayout { ItemId = itemId };

            await _subLayout.AddLayoutAsync(layout);

            await _subLayout.SaveChangesAsync();
        }
    }
}
