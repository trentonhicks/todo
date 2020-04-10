using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class CreateSubItemModel
    {
        public string Notes { get; set; }
        public string TodoName { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
