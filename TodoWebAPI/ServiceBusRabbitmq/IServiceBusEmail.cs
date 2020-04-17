using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Infrastructure.Email;

namespace TodoWebAPI.ServiceBusRabbitmq
{
    public interface IServiceBusEmail
    {
        void SendServiceBusEmail(Email email);
    }
}
