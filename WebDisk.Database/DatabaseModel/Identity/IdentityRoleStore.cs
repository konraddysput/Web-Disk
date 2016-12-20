using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace WebDisk.Database.DatabaseModel.Identity
{
    public class IdentityRoleStore : RoleStore<CustomRole, Guid, IdentityUserRole>
    {
        public IdentityRoleStore(WebDiskDbContext context)
        : base(context)
        {
        }
    }
}
