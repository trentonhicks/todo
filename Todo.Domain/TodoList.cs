using System;
using System.Collections.Generic;

namespace Todo.Domain
{
    public partial class TodoList
    {
        public TodoList()
        {
            ToDos = new HashSet<TodoListItem>();
        }

        public int Id { get; set; }
        public string ListTitle { get; set; }
        public int AccountId { get; set; }
        public bool Completed { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<TodoListItem> ToDos { get; set; }
    }
}
