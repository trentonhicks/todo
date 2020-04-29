using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.UserStories.CreateItem
{
    public class CreateItem : IRequest<TodoListItem>
    {
        public int AccountId { get; set; }
        public int ListId { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
