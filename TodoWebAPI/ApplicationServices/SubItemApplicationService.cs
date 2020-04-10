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
        private readonly ITodoListItemRepository _listItems;
        private readonly ISubItemRepository _subItems;

        public SubItemApplicationService(ITodoListItemRepository listItems, ISubItemRepository subItems)
        {
            _listItems = listItems;
            _subItems = subItems;
        }
        public async Task<SubItem> CreateSubItemAsync(int listItemId, string todoName, string notes, DateTime? dueDate)
        {
            var item = await _listItems.FindToDoListItemByIdAsync(listItemId);
            var subItem = item.CreateSubItem(todoName, notes, dueDate);

            _subItems.Add(subItem);

            await _subItems.SaveChangesAsync();

            return subItem;
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
