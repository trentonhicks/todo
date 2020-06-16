using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using Todo.Domain.DomainEvents;

namespace TodoWebAPI.UserStories.SendInvitation
{
    public class SendInvitationUserStory : AsyncRequestHandler<SendInvitation>
    {
        private readonly ITodoListRepository _todoList;
        private readonly IAccountRepository _account;
        private readonly IAccountRepository _accountRepository;

        public SendInvitationUserStory(ITodoListRepository todoList, IAccountRepository account, IAccountRepository accountRepository)
        {
            _todoList = todoList;
            _account = account;
            _accountRepository = accountRepository;
        }
        protected override async Task Handle(SendInvitation request, CancellationToken cancellationToken)
        {
            var account = await _account.FindAccountByEmailAsync(request.Email);
            var listId = Guid.Parse(request.ListId);
            var accountId = account.Id;

            await _todoList.AddRowToAccountListsAsync(accountId, listId);

            var list = await _todoList.FindTodoListIdByIdAsync(listId);

            list.StoreColaborator(request.Email, request.AccountId);

            await _todoList.SaveChangesAsync();
        }
    }
}
