using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.TrashSubItem
{
    public class TrashSubItem : IRequest
    {
        public int AccountId { get; set; }
        public int SubItemId { get; set; }
    }
}
