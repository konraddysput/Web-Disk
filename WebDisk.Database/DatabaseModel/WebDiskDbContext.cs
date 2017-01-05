using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebDisk.Database.BaseModels;
using WebDisk.Database.DatabaseModel.Identity;

namespace WebDisk.Database.DatabaseModel
{
    public class WebDiskDbContext : IdentityDbContext<ApplicationUser, CustomRole, Guid,
        Identity.IdentityUserLogin, Identity.IdentityUserRole, Identity.IdentityUserClaim>, IWebDiskDbContext
    {
        public WebDiskDbContext()
            : base("WebDiskDb")
        {}
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<FieldShareInformation> FieldShareInformation { get; set; }
        public virtual FieldInformation FieldInformation { get; set; }




        public static WebDiskDbContext Create()
        {
            return new WebDiskDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}
