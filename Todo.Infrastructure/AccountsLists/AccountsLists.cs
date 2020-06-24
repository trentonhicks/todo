using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Todo.Domain;

namespace Todo.Infrastructure
{
    public abstract class AccountsLists : Entity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid ListId { get; set; }
        public byte Role { get; protected set; }
        public bool UserIsOwner(Guid accountId) => AccountId == accountId && Role == Roles.Owner;
        private protected AccountsLists() { }
    }

    public class RoleInvited : AccountsLists
    {
        public void Invited()
        {
            base.Role = Roles.Invited;
        }
        public void MakeContributor()
        {
            base.Role = Roles.Contributer;
        }
        public void MakeDeclined()
        {
            base.Role = Roles.Declined;
        }
    }

    public class RoleOwner : AccountsLists
    {
        public void Owned()
        {
            base.Role = Roles.Owner;
        }
    }

    public class RoleContributor : AccountsLists
    {
        public void Contributed()
        {
            base.Role = Roles.Contributer;
        }
        public void MakeLeft()
        {
            base.Role = Roles.Left;
        }
    }

    public class RoleDecline : AccountsLists
    {
        public void Decline()
        {
            base.Role = Roles.Declined;
        }
    }
    public class RoleLeft : AccountsLists
    {
        public void Left()
        {
            base.Role = Roles.Left;
        }
    }
}
