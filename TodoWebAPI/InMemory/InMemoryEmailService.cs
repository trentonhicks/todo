using System;
using System.Threading.Tasks;
using TodoWebAPI.Service;

namespace TodoWebAPI.InMemory
{
    public class InMemoryEmailRepository : IEmailService
    {
        public Task SendEmail(string to, string from, string subject, string content)
        {
            
        }
    }
}