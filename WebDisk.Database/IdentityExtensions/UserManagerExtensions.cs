using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDisk.Database.DatabaseModel;

namespace WebDisk.Database.IdentityExtensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindById(this UserManager<ApplicationUser, Guid> userManager, Guid id)
        {
            var dbContext = new WebDiskDbContext();
            return dbContext.Users.FirstOrDefault(n => n.Id == id);
        }
    }
}
