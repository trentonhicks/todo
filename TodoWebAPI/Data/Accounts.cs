using System;
using System.Collections.Generic;

namespace TodoWebAPI.Data
{
    public partial class Accounts
    {
        public Accounts()
        {
            Lists = new HashSet<TodoLists>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public byte[] Picture { get; set; }
        public string Password { get; set; }

        public virtual ICollection<TodoLists> Lists { get; set; }
    }
}
