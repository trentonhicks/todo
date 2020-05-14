using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Infrastructure
{
    public class Account
    {
        public Account()
        {
            Contributors = new List<string>();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
        public List<string> Contributors { get; set; }
    }
}
