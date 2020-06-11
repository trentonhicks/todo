using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public class Account : Entity
    {
        public Account()
        {
            Contributors = new List<string>();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
        public List<string> Contributors { get; private set; }
        public int PlanId { get; set; }

        public void AddContributor(string email)
        {
            if (email == null)
                return;
            Contributors.Add(email);
        }
    }
}
