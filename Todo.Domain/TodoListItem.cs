using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public class TodoListItem : TodoListItemBase
    {
        public void SetCompleted(List<SubItem> items)
        {
            if (!items.All(item => item.ListItemId == Id))
                return;

            var itemsCompleted = items.All(item => item.Completed);

            if (Completed && !itemsCompleted)
            {
                Completed = false;
                DomainEvents.Add(new TodoListItemCompletedStateChanged { Item = this });
            }
            else if (!Completed && itemsCompleted)
            {
                Completed = true;
                DomainEvents.Add(new TodoListItemCompletedStateChanged { Item = this });
            }
        }

        public override void SetCompleted()
        {
            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new TodoListItemCompletedStateChanged { Item = this });
        }

        public override void SetNotCompleted()
        {
            if (!Completed)
                return;

            Completed = false;
            DomainEvents.Add(new TodoListItemCompletedStateChanged { Item = this });
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
