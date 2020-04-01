using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public partial class TodoList
    {
        public int Id { get; set; }
        public string ListTitle { get; set; }
        public int AccountId { get; set; }
        public bool Completed { get; private set; }

        public List<INotification> DomainEvents { get; } = new List<INotification>();
        public void SetCompleted(List<TodoListItem> items)
        {
            if (!items.All(item => item.ListId == Id))
                return;

            var itemsCompleted = items.All(item => item.Completed);

            if (Completed && !itemsCompleted)
            {
                Completed = false;
                DomainEvents.Add(new TodoListCompletedStateChanged { List = this});
            }
            else if(!Completed && itemsCompleted)
            {
                Completed = true;
                DomainEvents.Add(new TodoListCompletedStateChanged { List = this });
            }
        }
       
    }
}
