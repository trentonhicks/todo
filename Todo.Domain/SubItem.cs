using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public class SubItem : Entity
    {
        public int Id { get; set; }
        public int ListItemId { get; set; }
        public bool Completed { get; protected set; }
        public string Name { get; set; }

        public void MoveToTrash()
        {
            throw new NotImplementedException();
        }

        public void SetCompleted()
        {
            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new SubItemCompletedStateChanged { SubItem = this });
        }

        public void SetNotCompleted()
        {
            if (!Completed)
                return;

            Completed = false;
            DomainEvents.Add(new SubItemCompletedStateChanged { SubItem = this });
        }
    }
}
