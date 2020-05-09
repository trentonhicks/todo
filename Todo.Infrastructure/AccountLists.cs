using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public class AccountLists : Entity
    {
        [Key, Column(Order = 0)]
        public Guid AccountId { get; set; }
        [Key, Column(Order = 1)]
        public Guid ListId { get; set; }
        public byte Role { get; set; }
    }
}
