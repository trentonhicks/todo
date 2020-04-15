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
        public int ListId { get; set; }
        public int AccountId { get; set; }
    }
}
