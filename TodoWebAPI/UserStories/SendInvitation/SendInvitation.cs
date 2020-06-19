using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebAPI.UserStories.SendInvitation
{
    public class SendInvitation : IRequest<bool>
    {
        public Guid SenderAccountId { get; set; }
        public string SenderEmail { get; set; }
        public string Email { get; set; }
        public Guid ListId { get; set; }
    }
}
