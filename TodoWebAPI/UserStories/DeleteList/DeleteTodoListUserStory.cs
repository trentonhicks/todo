using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.UserStories
{
    public class DeleteTodoListUserStory : AsyncRequestHandler<DeleteList>
    {
        private readonly ITodoListItemRepository _todoListItem;
        private readonly ITodoListRepository _listRepository;
        private readonly IAccountPlanRepository _accountPlan;

        public DeleteTodoListUserStory(ITodoListItemRepository todoListItem, ITodoListRepository listRepository, IAccountPlanRepository accountPlan)
        {
            _todoListItem = todoListItem;
            _listRepository = listRepository;
            _accountPlan = accountPlan;
        }
        protected override async Task Handle(DeleteList request, CancellationToken cancellationToken)
        {
            await _listRepository.RemoveTodoListAsync(request.ListId);

            var accountPlan = await _accountPlan.FindAccountPlanByAccountIdAsync(request.AccountId);
            
            accountPlan.DecrementListCount();

            await _listRepository.SaveChangesAsync();
        }
    }
}
