using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface ISubItemLayout : IUnitOfWork
    {
        Task AddLayoutAsync(SubItemLayout layout);
        Task<SubItemLayout> FindLayoutByListItemIdAsync(int listItemId);
        void Update(SubItemLayout layout);
    }
}
