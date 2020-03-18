using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;

namespace TodoWebAPI.Interfaces
{
    public interface IAccountCollection
    {
        Accounts GetAccount(Accounts account);
        Accounts CreateAccount(Accounts account);
        void DeleteAccounts(Accounts account);
    }
}
