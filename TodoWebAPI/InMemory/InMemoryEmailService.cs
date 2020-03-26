using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TodoWebAPI.Services;

namespace TodoWebAPI.InMemory
{
    public class InMemoryEmailService : IEmailService
    {
        public Task SendEmail(Email email)
        {
            var timestamp = DateTime.Now;
            Debug.WriteLine($"Time Sent: {timestamp}\n To: {email.To}\n From: {email.From}\n Subject: {email.Subject}\n Content: {email.Body}");
            return Task.CompletedTask;
        }
    }
}