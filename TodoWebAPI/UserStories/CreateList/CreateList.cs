using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.Models
{
    public class CreateList : IRequest<TodoList>
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [FromBody]
        public string ListTitle { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email Required")]
        [FromBody]
        public string Email { get; set; }

        [FromRoute]
        public Guid AccountId { get; set; }
    }
}
