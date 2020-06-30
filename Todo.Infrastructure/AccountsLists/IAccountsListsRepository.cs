using System;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure
{
    public interface IAccountsListsRepository : IRepository
    {
        Task AddAccountsListsInvitedAsync(Guid inviteeId, Guid listId);
        Task<AccountsLists> FindAccountsListsByAccountIdAsync(Guid accountId, Guid listId);
        Task<RoleInvited> FindAccountsListsInvitedByAccountIdAsync(Guid accountId, Guid listId);
        Task<RoleDecline> FindAccountsListsDeclinedByAccountIdAsync(Guid accountId, Guid listId);
        Task<RoleContributor> FindAccountsListsContributorByAccountIdAsync(Guid accountId, Guid listId);
        Task<RoleLeft> FindAccountsListsLeftByAccountIdAsync(Guid accountId, Guid listId);
    }
}