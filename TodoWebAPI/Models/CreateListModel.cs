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
    public class CreateListModel : IRequest<TodoList>
    {
        [Required]
        [FromBody]
        public string ListTitle { get; set; }
        [FromRoute]
        public int AccountId { get; set; }
    }
}
