using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.TrashItem
{
    public class TrashItem : IRequest
    {
        public int AccountId { get; set; }
        public int ItemId { get; set; }
    }
}
