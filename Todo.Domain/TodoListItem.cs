using MediatR;
using System;
using System.Collections.Generic;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public partial class TodoListItem
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? ParentId { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; private set; }
        public string ToDoName { get; set; }
        public int ListId { get; set; }
        public List<INotification> DomainEvents { get; } = new List<INotification>();

        public void SetCompleted()
        {
            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new TodoListItemCompleted { ListItem = this });
        }

        public void SetNotCompleted()
        {
            if (!Completed)
                return;

            Completed = false;
            DomainEvents.Add(new TodoListItemCompleted { ListItem = this });
        }
    }
}
