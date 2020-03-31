using System;
using System.Threading.Tasks;
using TodoWebAPI;
using TodoWebAPI.Services;

namespace TodoWebAPI.Repository
{
    public interface EmailServiceInterface
    {
        Task SendEmailAsync(Email email);
    }
}