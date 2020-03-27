using System;
using System.Threading.Tasks;
using TodoWebAPI;

namespace TodoWebAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}