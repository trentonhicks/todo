using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Interfaces;

namespace TodoWebAPI.InMemory
{
    public class ListAcountCollection : IAccountCollection
    {
        private List<Accounts> _accounts = new List<Accounts>();

        public Accounts CreateAccount(Accounts account)
        {
            _accounts.Add(account);
            return account;
        }

        public void DeleteAccounts(Accounts account)
        {
            _accounts.Remove(account);
        }
        public Accounts GetAccount(int accountId, Accounts account)
        {
            var value = _accounts.Find(x => x.Id == accountId);

            return value;
        }
    }
}
