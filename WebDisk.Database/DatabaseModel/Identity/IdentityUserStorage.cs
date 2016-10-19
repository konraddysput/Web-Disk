using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
