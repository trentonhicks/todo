using System;
using System.Collections.Generic;

namespace TodoWebAPI.Data
{
    public partial class ToDos
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; set; }
        public string ToDoName { get; set; }
        public int ListId { get; set; }

        public virtual TodoLists List { get; set; }
    }
}
