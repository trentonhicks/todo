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
            CheckIfListItemIsTrashed();
            
            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new TodoListItemCompletedStateChanged { Item = this });
        }

        public override void SetNotCompleted()
        {
            CheckIfListItemIsTrashed();

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
              ToDoName = name,
              Notes = notes
            };

            DomainEvents.Add(new SubItemCreated { SubItem = item });

            return item;
        }

        public override void MoveToTrash()
        {
            var listId = this.ListId;
            this.ListId = null;

            DomainEvents.Add(new ItemMovedToTrash { ListId = listId, Item = this });
        }

        private void CheckIfListItemIsTrashed()
        {
            if(this.ListId == null)
                throw new InvalidOperationException("Item is in the trash!");
        }
    }
}
