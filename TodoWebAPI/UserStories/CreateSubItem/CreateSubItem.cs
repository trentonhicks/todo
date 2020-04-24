using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.UserStories.CreateSubItem
{
    public class CreateSubItem : IRequest<SubItem>
    {
        public int AccountId { get; set; }
        public int ListId { get; set; }
        public int ListItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
