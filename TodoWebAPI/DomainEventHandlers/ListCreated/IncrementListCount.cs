using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace TodoWebAPI.DomainEventHandlers
{
    public class IncrementListCount : INotificationHandler<TodoListCreated>
    {
        private readonly IAccountPlanRepository _accountPlanRepository;

        public IncrementListCount(IAccountPlanRepository accountPlanRepository)
        {
            _accountPlanRepository = accountPlanRepository;
        }
        public async Task Handle(TodoListCreated notification, CancellationToken cancellationToken)
        {
            
        }
    }
}
