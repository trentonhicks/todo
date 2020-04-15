using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.ItemLayout
{
    public class ItemLayout : IRequest
    {
        public int AccountId { get; set; }
        public int ItemId { get; set; }
        public int SubItemId { get; set; }
        public int Position { get; set; }
    }
}
