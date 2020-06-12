using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public class Account : Entity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
        public int PlanId { get; set; }
    }
}
