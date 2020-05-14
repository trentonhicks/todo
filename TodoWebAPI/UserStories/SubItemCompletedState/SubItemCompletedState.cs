using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.SubItemCompletedState
{
    public class SubItemCompletedState : IRequest
    {
        public Guid AccountId { get; set; }
        public Guid SubItemId { get; set; }
        public bool Completed { get; set; }
    }
}
