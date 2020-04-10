using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public class SubItem : TodoListItemBase
    {
        public int ListItemId { get; set; }
        public override void SetCompleted()
        {
            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new SubItemCompletedStateChanged { SubItem = this });
        }

        public override void SetNotCompleted()
        {
            if (!Completed)
                return;

            Completed = false;
            DomainEvents.Add(new SubItemCompletedStateChanged { SubItem = this });
        }
    }
}
