using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
