using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;

namespace TodoWebAPI.InfrastructureEventHandlers
{
    public class StoreCollaboratorInListHanlder : INotificationHandler<InvitationSent>
    {
        private readonly ITodoListRepository _todoList;

        public StoreCollaboratorInListHanlder(ITodoListRepository todoList)
        {
            _todoList = todoList;
        }
        public async Task Handle(InvitationSent notification, CancellationToken cancellationToken)
        {
            notification.List.AddCollaborator(notification.Email);

            _todoList.UpdateListAsync(notification.List);

            await _todoList.SaveChangesAsync();
        }
    }
}
