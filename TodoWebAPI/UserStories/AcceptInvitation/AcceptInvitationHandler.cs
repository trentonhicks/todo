using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories
{
    public class AcceptInvitationHandler : INotificationHandler<AcceptInvitaion>
    {
        private readonly ITodoListRepository _listRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountPlanRepository _accountPlan;

        public AcceptInvitationHandler(ITodoListRepository listRepository, IAccountRepository accountRepository, IAccountPlanRepository accountPlan)
        {
            _listRepository = listRepository;
            _accountRepository = accountRepository;
            _accountPlan = accountPlan;
        }
        public async Task Handle(AcceptInvitaion notification, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlan.FindAccountPlanByAccountIdAsync(notification.AccountId);

            await _listRepository.AddContributorRowToAccountsListsAsync(notification.ListId, notification.AccountId);

            accountPlan.IncrementListCount();

            await _listRepository.SaveChangesAsync();
        }
    }
}