using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TodoWebAPI.Extentions
{
    public static class OAuthExtentions
    {
        public static int ReadClaimAsIntValue(this ClaimsPrincipal user, string type)
        {
            return Convert.ToInt32(user.FindFirst(type).Value);
        }
    }
}
