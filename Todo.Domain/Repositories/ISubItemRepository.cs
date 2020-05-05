using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ISubItemRepository : IRepository
    {
        void Add(SubItem subItem);
        Task<SubItem> FindByListItemId(Guid listItemId);
        Task<List<SubItem>> FindAllSubItemsByListItemIdAsync(Guid listItemId);
        Task<SubItem> FindByIdAsync(Guid subItemId);
    }
}
