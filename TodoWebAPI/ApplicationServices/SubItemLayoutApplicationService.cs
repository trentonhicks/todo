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

        public async Task CreateTodoListItemLayoutAsync(Guid itemId)
        {
            var layout = new SubItemLayout { ItemId = itemId };

            layout.Id = _subLayout.NextId();

            await _subLayout.AddLayoutAsync(layout);

            await _subLayout.SaveChangesAsync();
        }
    }
}
