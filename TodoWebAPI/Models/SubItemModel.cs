using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class SubItemModel
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public string ToDoName { get; set; }
        public int ListId { get; set; }
        public int ListItemId { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
