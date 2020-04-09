using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class CreateToDoModel
    {
        public string Notes { get; set; }
        public string ToDoName { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
