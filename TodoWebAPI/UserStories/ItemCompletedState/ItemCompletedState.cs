using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.ItemCompletedState
{
    public class ItemCompletedState : IRequest
    {
        public Guid AccountId { get; set; }
        public Guid ItemId { get; set; }
        public bool Completed { get; set; }
    }
}
