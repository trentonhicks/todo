using System;
using MediatR;

namespace TodoWebAPI.UserStories
{
    public class AcceptInvitaion : INotification
    {
        public Guid ListId {get; set;}
        public Guid AccountId {get; set;}
    }
}