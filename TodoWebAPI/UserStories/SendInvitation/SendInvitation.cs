using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.SendInvitation
{
    public class SendInvitation : IRequest
    {
        public string ListId { get; set; }
        public string Email { get; set; }
    }
}
