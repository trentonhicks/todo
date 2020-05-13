using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain;

namespace Todo.Domain
{
    public class InvitationSent : INotification
    {
        public TodoList List { get; set; }
        public string Email { get; set; }
        public Guid AccountId { get; set; }
    }
}
