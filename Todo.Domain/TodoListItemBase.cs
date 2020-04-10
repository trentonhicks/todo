using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain
{
    public abstract class TodoListItemBase : Entity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; protected set; }
        public string Name { get; set; }
        public int ListId { get; set; }
        public DateTime? DueDate { get; set; }

        public abstract void SetCompleted();
        public abstract void SetNotCompleted();
    }
}
