using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.Models
{
    public class DeleteList : IRequest
    {
        public Guid ListId { get; set; }
        public Guid AccountId { get; set; }
    }
}
