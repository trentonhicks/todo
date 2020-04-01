using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class CreateToDoModel
    {
        public int? ParentId { get; set; }
        public string Notes { get; set; }
        public string ToDoName { get; set; }
    }
}
