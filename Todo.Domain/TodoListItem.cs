using MediatR;
using System;
using System.Collections.Generic;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public partial class TodoListItem : TodoListItemBase
    {
        public override void SetCompleted()
        {
            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new TodoListItemCompleted { ListItem = this });
        }

        public override void SetNotCompleted()
        {
            if (!Completed)
                return;

            Completed = false;
            DomainEvents.Add(new TodoListItemCompleted { ListItem = this });
        }

        public SubItem CreateSubItem(string name, string notes, DateTime? dueDate)
        {
            var item = new SubItem
            {
              AccountId = this.AccountId,
              DueDate = dueDate,
              ListId = this.ListId,
              ListItemId = this.Id,
              Name = name,
              Notes = notes
            };

            DomainEvents.Add(new SubItemCreated { SubItem = item });

            return item;
        }
    }
}
