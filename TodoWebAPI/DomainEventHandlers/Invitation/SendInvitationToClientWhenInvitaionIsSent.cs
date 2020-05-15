using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using TodoWebAPI.SignalR;

namespace TodoWebAPI.DomainEventHandlers.Invitation
{
    public class SendInvitationToClientWhenInvitaionIsSent : INotificationHandler<InvitationSent>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SendInvitationToClientWhenInvitaionIsSent(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(InvitationSent notification, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.User(notification.Email).SendAsync("InvitationSent", notification.List);
        }
    }
}
