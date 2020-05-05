using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ISubItemLayoutRepository : IRepository
    {
        Task AddLayoutAsync(SubItemLayout layout);
        Task<SubItemLayout> FindLayoutByListItemIdAsync(Guid listItemId);
        void Update(SubItemLayout layout);
    }
}
