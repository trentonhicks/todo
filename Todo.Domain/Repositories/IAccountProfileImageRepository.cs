using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface IAccountProfileImageRepository
    {
        Task StoreImageProfileAsync(int accountId, string profileImage);
    }
}
