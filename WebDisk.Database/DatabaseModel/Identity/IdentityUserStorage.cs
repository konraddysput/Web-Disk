using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace WebDisk.Database.DatabaseModel.Identity
{
    public class IdentityUserStorage : UserStore<ApplicationUser, CustomRole, Guid,
    IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {

        public IdentityUserStorage(WebDiskDbContext context)
        : base(context)
        {
        }
    }
}
