using System;
using System.Collections.Generic;

namespace Todo.Domain
{
    public class AccountPlanAuthorizationValidator
    {
        public AccountPlan AccountPlan { get; private set; }
        public Plan Plan { get; private set; }
        public AccountPlanAuthorizationValidator(AccountPlan accountPlan, Plan plan)
        {
            AccountPlan = accountPlan;
            Plan = plan;
        }

        public bool CanCreateList()
        {
            if (AccountPlan.ListCount < Plan.MaxLists || Plan.MaxLists == -1)
                return true;

            return false;
        }

        public bool CanAddDueDate()
        {
            return Plan.CanAddDueDates;
        }
    }
}