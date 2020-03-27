using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain
{
    public class Account
    {
        public Account()
        {
            Lists = new HashSet<TodoList>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public byte[] Picture { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<TodoList> Lists { get; set; }
    }
}
