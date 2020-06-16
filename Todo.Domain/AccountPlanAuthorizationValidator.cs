using System;
using System.Collections.Generic;

namespace Todo.Domain
{
    public class AccountPlanAuthorizationValidator
    {
        public int ListCount { get; private set; }
        public int MaxLists { get; private set; }
        public AccountPlanAuthorizationValidator(int listCount, int maxLists)
        {
            ListCount = listCount;
            MaxLists = maxLists;
        }

        public bool CanCreateList()
        {
            if (ListCount < MaxLists)
                return true;

            return false;
        }
    }
}