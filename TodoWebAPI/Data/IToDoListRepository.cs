using System.Threading.Tasks;

namespace TodoWebAPI.Data
{
    internal interface IToDoListRepository
    {
        Task GetListAsync(int listId);
    }
}