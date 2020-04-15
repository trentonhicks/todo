using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.UserStories.CreateItem
{
    public class CreateItem : IRequest<TodoListItem>
    {
        public int AccountId { get; set; }
        public int ListId { get; set; }
        public string Notes { get; set; }
        public string TodoName { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
