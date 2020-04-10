using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class CreateTodoListItemModel
    {
        public string Notes { get; set; }
        public string TodoName { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
