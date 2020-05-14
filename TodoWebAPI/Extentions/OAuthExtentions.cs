using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TodoWebAPI.Extentions
{
    public static class OAuthExtentions
    {
        public static Guid ReadClaimAsGuidValue(this ClaimsPrincipal user, string type)
        {
            return Guid.Parse(user.FindFirst(type).Value);
        }
    }
}
