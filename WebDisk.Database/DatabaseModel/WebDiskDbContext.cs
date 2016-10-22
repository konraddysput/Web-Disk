using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using WebDisk.Database.DatabaseModel.Identity;
using System.Data.Entity;

namespace WebDisk.Database.DatabaseModel
{
    public class WebDiskDbContext : IdentityDbContext<ApplicationUser, CustomRole, Guid,
        Identity.IdentityUserLogin, Identity.IdentityUserRole, Identity.IdentityUserClaim>
    {
        public WebDiskDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Blob> Blob { get; set; }
        public DbSet<Directory> Directory { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Space> Space { get; set; }
        public DbSet<SpaceShare> SpaceShare { get; set; }

        public static WebDiskDbContext Create()
        {
            return new WebDiskDbContext();
        }
    }
}
