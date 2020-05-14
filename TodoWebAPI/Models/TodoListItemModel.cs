using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class TodoListItemModel
    {
        public Guid Id { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; }
        public Guid ListId { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
    }
}