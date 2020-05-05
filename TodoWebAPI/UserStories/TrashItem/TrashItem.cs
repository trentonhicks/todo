using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.TrashItem
{
    public class TrashItem : IRequest
    {
        public Guid AccountId { get; set; }
        public Guid ItemId { get; set; }
    }
}
