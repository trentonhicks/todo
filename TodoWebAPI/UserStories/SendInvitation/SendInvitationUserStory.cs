using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Repositories;

namespace TodoWebAPI.UserStories.SendInvitation
{
    public class SendInvitationUserStory : AsyncRequestHandler<SendInvitation>
    {
        private readonly DapperQuery _dapper;
        private readonly ITodoListRepository _todoList;

        public SendInvitationUserStory(DapperQuery dapper, ITodoListRepository todoList)
        {
            _dapper = dapper;
            _todoList = todoList;
        }
        protected override async Task Handle(SendInvitation request, CancellationToken cancellationToken)
        {
            var accountId = await _dapper.GetAccountIdByEmailAsync(request.Email);
            var listId = Guid.Parse(request.ListId);

            await _todoList.AddRowToAccountListsAsync(accountId, listId);

            await _todoList.SaveChangesAsync();
        }
    }
}
