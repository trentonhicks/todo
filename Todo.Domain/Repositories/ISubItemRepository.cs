using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ISubItemRepository : IUnitOfWork
    {
        void Add(SubItem subItem);
        Task<SubItem> FindByListItemId(int listItemId);
        Task<List<SubItem>> FindAllSubItemsByListItemIdAsync(int listItemId);
        Task<SubItem> FindByIdAsync(int subItemId);
    }
}
