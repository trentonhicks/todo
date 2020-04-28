using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.DomainEvents;

namespace Todo.Domain
{
    public class SubItem : Entity
    {
        public int Id { get; set; }
        public int? ListItemId { get; set; }
        public bool Completed { get; protected set; }
        public string Name { get; set; }

        public void SetCompleted()
        {
            CheckIfSubItemIsTrashed();

            if (Completed)
                return;

            Completed = true;
            DomainEvents.Add(new SubItemCompletedStateChanged { SubItem = this });
        }

        public void SetNotCompleted()
        {
            CheckIfSubItemIsTrashed();

            if (!Completed)
                return;

            Completed = false;
            DomainEvents.Add(new SubItemCompletedStateChanged { SubItem = this });
        }

        public void MoveToTrash()
        {
            var itemId = this.ListItemId;
            this.ListItemId = null;

            DomainEvents.Add(new SubItemMovedToTrash { ItemId = itemId, SubItem = this });
        }

        private void CheckIfSubItemIsTrashed()
        {
            if (this.ListItemId == null)
                throw new InvalidOperationException("Item is in the trash!");
        }
    }
}
