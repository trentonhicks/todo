using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories
{
    public class AcceptInvitationHandler : AsyncRequestHandler<AcceptInvitaion>
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

        protected override async Task Handle(AcceptInvitaion request, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlan.FindAccountPlanByAccountIdAsync(request.AccountId);
            var account = await _accountRepository.FindAccountByIdAsync(request.AccountId);
            var list = await _listRepository.FindTodoListIdByIdAsync(request.ListId);

            await _listRepository.AddContributorRowToAccountsListsAsync(request.ListId, request.AccountId);

            accountPlan.IncrementListCount();
            list.StoreContributor(account.Email, request.AccountId);
            await _listRepository.SaveChangesAsync();
        }
    }
}