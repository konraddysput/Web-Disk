using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel.Identity;

namespace WebDisk.Database.DatabaseModel
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    public class ApplicationUser : IdentityUser<Guid, Identity.IdentityUserLogin, Identity.IdentityUserRole, Identity.IdentityUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
