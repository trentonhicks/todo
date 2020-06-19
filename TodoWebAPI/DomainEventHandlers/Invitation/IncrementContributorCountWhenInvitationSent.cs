using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers.Invitation
{
    public class IncrementContributorCountWhenInvitationSent : INotificationHandler<InvitationSent>
    {
        private readonly IAccountPlanRepository _accountPlanRepository;

        public IncrementContributorCountWhenInvitationSent(IAccountPlanRepository accountPlanRepository)
        {
            _accountPlanRepository = accountPlanRepository;
        }
        public async Task Handle(InvitationSent notification, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlanRepository.FindAccountPlanByAccountIdAsync(notification.SenderAccountId);

            accountPlan.IncrementContributorCount();

            await _accountPlanRepository.SaveChangesAsync();
        }
    }
}
