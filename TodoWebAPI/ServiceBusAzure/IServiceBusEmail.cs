using System;
using Todo.Domain;
using Todo.Infrastructure.Email;

namespace TodoWebAPI.ServiceBusRabbitmq
{
    public interface IServiceBusEmail
    {
        void SendServiceBusEmail(Email email, TodoList list);
    }
}
