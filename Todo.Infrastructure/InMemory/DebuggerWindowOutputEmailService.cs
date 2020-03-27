using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TodoWebAPI.Services;

namespace TodoWebAPI.InMemory
{
    public class DebuggerWindowOutputEmailService : IEmailService
    {
        public Task SendEmailAsync(Email email)
        {
            var timestamp = DateTime.Now;
            Debug.WriteLine($"Time Sent: {timestamp}\nTo: {email.To}\nFrom: {email.From}\nSubject: {email.Subject}\nContent: {email.Body}");
            return Task.CompletedTask;
        }
    }
}