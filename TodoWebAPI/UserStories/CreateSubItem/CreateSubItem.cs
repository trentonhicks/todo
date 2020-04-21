using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.UserStories.CreateSubItem
{
    public class CreateSubItem : IRequest<SubItem>
    {
        public int AccountId { get; set; }
        public int ListId { get; set; }
        public int ListItemId { get; set; }
        public string Name { get; set; }
    }
}
