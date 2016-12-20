using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace WebDisk.Database.DatabaseModel.Identity
{
    public class CustomRole : IdentityRole<Guid, IdentityUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name)
        {
            Name = name;
        }

    }
}
