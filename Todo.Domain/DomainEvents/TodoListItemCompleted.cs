using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.DomainEvents
{
    public class TodoListItemCompleted : INotification
    {
        public TodoListItem ListItem { get; set; }
    }
}
