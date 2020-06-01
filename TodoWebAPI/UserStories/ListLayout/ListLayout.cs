using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.UserStories.ListLayout
{
    public class ListLayout : IRequest
    {
        public Guid ItemId { get; set; }
        public int Position { get; set; }
        public Guid ListId { get; set; }
        public Guid AccountId { get; set; }
        public string Email { get; set; }
    }
}
