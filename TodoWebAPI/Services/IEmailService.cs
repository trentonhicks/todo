using System;
using System.Threading.Tasks;

namespace TodoWebAPI.Service
{
    public interface IEmailService
    {
        Task SendEmail(string to, string from, string subject, string content);
    }
}