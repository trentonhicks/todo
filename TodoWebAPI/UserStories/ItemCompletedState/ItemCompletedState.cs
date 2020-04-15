using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.ListCompletedState.cs
{
    public class ItemCompletedState : IRequest
    {
        public int AccountId { get; set; }
        public int ItemId { get; set; }
        public bool Completed { get; set; }
    }
}
