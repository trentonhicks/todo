using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using Todo.Domain.DomainEvents;
using Todo.Domain;

namespace TodoWebAPI.UserStories.SendInvitation
{
    public class SendInvitationUserStory : IRequestHandler<SendInvitation, bool>
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountPlanRepository _accountPlanRepository;
        private readonly IPlanRepository _planRepository;

        public SendInvitationUserStory(
            ITodoListRepository todoListRepository,
            IAccountRepository accountRepository,
            IAccountPlanRepository accountPlanRepository,
            IPlanRepository planRepository)
        {
            _todoListRepository = todoListRepository;
            _accountRepository = accountRepository;
            _accountPlanRepository = accountPlanRepository;
            _planRepository = planRepository;
        }

        public async Task<bool> Handle(SendInvitation request, CancellationToken cancellationToken)
        {
            var accountPlan = await _accountPlanRepository.FindAccountPlanByAccountIdAsync(request.SenderAccountId);
            var plan = await _planRepository.FindPlanByIdAsync(accountPlan.PlanId);
            var accountPlanAuthorization = new AccountPlanAuthorizationValidator(accountPlan, plan);

            var list = await _todoListRepository.FindTodoListIdByIdAsync(request.ListId);
            var todoListAuthorizationValidator = new TodoListAuthorizationValidator(list.Contributors, request.SenderEmail);

            if (todoListAuthorizationValidator.IsUserAuthorized())
            {
                if (accountPlanAuthorization.CanAddContributor())
                {
                    try
                    {
                        var invitee = await _accountRepository.FindAccountByEmailAsync(request.Email);

                        await _todoListRepository.AddRowToAccountListsAsync(invitee.Id, request.ListId);

                        list.StoreColaborator(request.Email, request.SenderAccountId);

                        await _todoListRepository.SaveChangesAsync();

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
