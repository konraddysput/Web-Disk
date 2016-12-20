namespace WebDisk.Database.Migrations
{
    using DatabaseModel;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebDisk.Database.DatabaseModel.WebDiskDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebDiskDbContext context)
        {
            var passwordHash = new PasswordHasher();
            var realPassword = "test!@#ZXC";
            string password = passwordHash.HashPassword(realPassword);
            var temporaryId = Guid.NewGuid();
            var userToCreate = new ApplicationUser
            {
                Id = temporaryId,
                UserName = "konrad.dysput",
                Email = "konrad.dysput@gmail.com",
                Space = new Space()
                {
                    Directory = new Field()
                    {
                        Name = "root",
                        Type = DatabaseModel.Types.FieldType.Directory,
                        LastModifiedDate = DateTime.Now,
                        FieldId = temporaryId
                    },
                    IsEnabled = true,
                    DefaultDirectoryId = temporaryId,
                    SpaceId = temporaryId
                },
                PasswordHash = password,
                PhoneNumber = "798740944",
                EmailConfirmed = true
            };
            context.Users.AddOrUpdate(u => u.UserName, userToCreate);


            if (!context.Users.Any(n => n.UserName == "konrad.dysput"))
            {
                var userStore = new UserStore<ApplicationUser,
                    DatabaseModel.Identity.CustomRole,
                    Guid,
                    DatabaseModel.Identity.IdentityUserLogin,
                    DatabaseModel.Identity.IdentityUserRole,
                    DatabaseModel.Identity.IdentityUserClaim>(context);
                var userManager = new UserManager<ApplicationUser, Guid>(userStore);
                var result = userManager.Create(userToCreate, realPassword);
                //var context
            }
        }
    }
}
