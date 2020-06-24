using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories
{
    public class RemoveSelfFromListHandler : AsyncRequestHandler<RemoveSelfFromList>
    {
        private readonly ITodoListRepository _listRepository;
        private readonly IAccountPlanRepository _accountPlanRepository;
        private readonly IAccountRepository _accountRepository;

        public RemoveSelfFromListHandler(ITodoListRepository listRepository, IAccountPlanRepository accountPlanRepository, IAccountRepository accountRepository)
        {
            _listRepository = listRepository;
            _accountPlanRepository = accountPlanRepository;
            _accountRepository = accountRepository;
        }

        protected override async Task Handle(RemoveSelfFromList request, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlanRepository.FindAccountPlanByAccountIdAsync(request.AccountId);
            var list = await _listRepository.FindTodoListIdByIdAsync(request.ListId);
            var account = await _accountRepository.FindAccountByIdAsync(request.AccountId);

            list.Contributors.Remove(account.Email);

            _listRepository.UpdateListAsync(list);

            await _listRepository.AddLeftRowToAccountsListsAsync(request.AccountId, request.ListId);

            accountPlan.DecrementListCount();

            await _listRepository.SaveChangesAsync();
        }
    }
}