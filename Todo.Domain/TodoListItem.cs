using System;
using System.Collections.Generic;

namespace Todo.Domain
{
    public partial class TodoListItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; set; }
        public string ToDoName { get; set; }
        public int ListId { get; set; }

        public virtual TodoList List { get; set; }
    }
}
