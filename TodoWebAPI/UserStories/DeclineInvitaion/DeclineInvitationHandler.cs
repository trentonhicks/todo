using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories
{
    public class DeclineInvitationHandler : AsyncRequestHandler<DeclineInvitation>
    {
        private readonly ITodoListRepository _listRepository;

        public DeclineInvitationHandler(ITodoListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        protected override async Task Handle(DeclineInvitation request, CancellationToken cancellationToken)
        {
            await _listRepository.AddDeclinedRowToAccountsListsAsync(request.AccountId, request.ListId);
            await _listRepository.SaveChangesAsync();
        }
    }
}