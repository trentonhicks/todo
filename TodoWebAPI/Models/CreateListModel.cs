using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.Models
{
    public class CreateListModel
    {
        [Required]
        public string ListTitle { get; set; }
    }
}
