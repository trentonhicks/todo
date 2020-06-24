using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;
using Todo.Infrastructure;

namespace TodoWebAPI.UserStories
{
    public class AcceptInvitationHandler : AsyncRequestHandler<AcceptInvitaion>
    {
        private readonly ITodoListRepository _listRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountPlanRepository _accountPlan;
        private readonly IAccountsListsRepository _accountsListsRepository;

        public AcceptInvitationHandler(
            ITodoListRepository listRepository,
            IAccountRepository accountRepository,
            IAccountPlanRepository accountPlan,
            IAccountsListsRepository accountsListsRepository)
        {
            _listRepository = listRepository;
            _accountRepository = accountRepository;
            _accountPlan = accountPlan;
            _accountsListsRepository = accountsListsRepository;
        }

        protected override async Task Handle(AcceptInvitaion request, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlan.FindAccountPlanByAccountIdAsync(request.AccountId);
            var account = await _accountRepository.FindAccountByIdAsync(request.AccountId);
            var list = await _listRepository.FindTodoListIdByIdAsync(request.ListId);

            var accountsLists = await _accountsListsRepository.FindAccountsListsInvitedByAccountIdAsync(request.AccountId, request.ListId);
            accountsLists.MakeContributor();

            accountPlan.IncrementListCount();
            list.StoreContributor(account.Email, request.AccountId);
            await _listRepository.SaveChangesAsync();
        }
    }
}