using System;
using System.Collections.Generic;

namespace TodoWebAPI.Data
{
    public partial class Lists
    {
        public Lists()
        {
            ToDos = new HashSet<ToDos>();
        }

        public int Id { get; set; }
        public string ListTitle { get; set; }
        public int AccountId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual ICollection<ToDos> ToDos { get; set; }
    }
}
