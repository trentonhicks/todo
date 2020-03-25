using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Notes { get; set; }
        public bool Completed { get; set; }
        public string ToDoName { get; set; }
        public int ListId { get; set; }
    }
}
