using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Repositories;

namespace TodoWebAPI.UserStories.DeleteAccount
{
    public class DeleteAccountUserStory : IRequestHandler<DeleteAccount>
    {
        private readonly IAccountRepository _repository;

        public DeleteAccountUserStory(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            await _repository.RemoveAccountAsync(request.AccountId);
            return Unit.Value;
        }
    }
}
