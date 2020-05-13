using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Infrastructure;
using Todo.Infrastructure.Repositories;

namespace TodoWebAPI.InfrastructureEventHandlers
{
    public class StoreEmailInsideAccountContributorColumnWhenInvitedHandler : INotificationHandler<InvitationSent>
    {
        private readonly IAccountRepository _accountRepository;

        public StoreEmailInsideAccountContributorColumnWhenInvitedHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task Handle(InvitationSent notification, CancellationToken cancellationToken)
        {
            var claimAccount = await _accountRepository.FindAccountByIdAsync(notification.AccountId);

            claimAccount.AddContributor(notification.Email);

            _accountRepository.UpdateContributorsAsync(claimAccount);

            var invitee = await _accountRepository.FindAccountByEmailAsync(notification.Email);

            invitee.AddContributor(claimAccount.Email);

            _accountRepository.UpdateContributorsAsync(invitee);

            await _accountRepository.SaveChangesAsync();

        }
    }
}
