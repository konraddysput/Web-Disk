using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using WebDisk.Database.DatabaseModel.Identity;

namespace WebDisk.Database.DatabaseModel
{
    public class WebDiskDbContext : IdentityDbContext<ApplicationUser,CustomRole,Guid,
        Identity.IdentityUserLogin, Identity.IdentityUserRole, Identity.IdentityUserClaim>
    {
        public WebDiskDbContext()
            : base("DefaultConnection")
        {
        }

        public static WebDiskDbContext Create()
        {
            return new WebDiskDbContext();
        }
    }
}
