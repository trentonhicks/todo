using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.TrashSubItem
{
    public class TrashSubItem : IRequest
    {
        public Guid AccountId { get; set; }
        public Guid SubItemId { get; set; }
    }
}
