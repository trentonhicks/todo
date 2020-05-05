using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class TodoListModel
    {
        public Guid Id { get; set; }
        public string ListTitle { get; set; }
        public Guid AccountId { get; set; }
        public bool Completed { get; set; }
    }
}
