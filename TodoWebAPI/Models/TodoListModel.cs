using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class TodoListModel
    {
        public int Id { get; set; }
        public string ListTitle { get; set; }
        public int AccountId { get; set; }
    }
}
