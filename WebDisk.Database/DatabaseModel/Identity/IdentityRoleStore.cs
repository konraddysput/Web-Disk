using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
