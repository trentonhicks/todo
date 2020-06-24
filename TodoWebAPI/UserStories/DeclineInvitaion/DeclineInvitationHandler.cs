using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories
{
    public class DeclineInvitationHandler : INotificationHandler<DeclineInvitation>
    {
        private readonly ITodoListRepository _listRepository;

        public DeclineInvitationHandler(ITodoListRepository listRepository)
        {
            _listRepository = listRepository;
        }
        public async Task Handle(DeclineInvitation notification, CancellationToken cancellationToken)
        {
            await _listRepository.AddDeclinedRowToAccountsListsAsync(notification.AccountId, notification.ListId);
            await _listRepository.SaveChangesAsync();
        }
    }
}