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
        public Guid AccountId { get; set; }
        public Guid ListId { get; set; }
        public Guid ListItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
