using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.SubItemCompletedState
{
    public class SubItemCompletedState : IRequest
    {
        public int AccountId { get; set; }
        public int SubItemId { get; set; }
        public bool Completed { get; set; }
    }
}
