using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;
using Todo.Infrastructure;

namespace TodoWebAPI.UserStories
{
    public class RemoveSelfFromListHandler : AsyncRequestHandler<RemoveSelfFromList>
    {
        private readonly ITodoListRepository _listRepository;
        private readonly IAccountPlanRepository _accountPlanRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountsListsRepository _accountsListsRepository;

        public RemoveSelfFromListHandler(
            ITodoListRepository listRepository,
            IAccountPlanRepository accountPlanRepository,
            IAccountRepository accountRepository,
            IAccountsListsRepository accountsListsRepository)
        {
            _listRepository = listRepository;
            _accountPlanRepository = accountPlanRepository;
            _accountRepository = accountRepository;
            _accountsListsRepository = accountsListsRepository;
        }

        protected override async Task Handle(RemoveSelfFromList request, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlanRepository.FindAccountPlanByAccountIdAsync(request.AccountId);
            var list = await _listRepository.FindTodoListIdByIdAsync(request.ListId);
            var account = await _accountRepository.FindAccountByIdAsync(request.AccountId);

            list.Contributors.Remove(account.Email);

            _listRepository.UpdateListAsync(list);

            var accountsLists = await _accountsListsRepository.FindAccountsListsContributorByAccountIdAsync(request.AccountId, request.ListId);
            accountsLists.MakeLeft();

            accountPlan.DecrementListCount();

            await _listRepository.SaveChangesAsync();
        }
    }
}