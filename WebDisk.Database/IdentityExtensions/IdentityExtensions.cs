using System;
using System.Security.Principal;

namespace WebDisk.Database.IdentityExtensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            return Guid.Parse(Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(identity));
        }
    }
}
