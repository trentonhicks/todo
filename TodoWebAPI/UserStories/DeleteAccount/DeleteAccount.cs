using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace TodoWebAPI.UserStories.DeleteAccount
{
    public class DeleteAccount : IRequest
    {
        public Guid AccountId { get; set; }
    }
}
