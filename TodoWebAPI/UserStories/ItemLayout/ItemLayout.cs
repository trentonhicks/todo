using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.ItemLayout
{
    public class ItemLayout : IRequest
    {
        public Guid AccountId { get; set; }
        public Guid ItemId { get; set; }
        public Guid SubItemId { get; set; }
        public int Position { get; set; }
    }
}
