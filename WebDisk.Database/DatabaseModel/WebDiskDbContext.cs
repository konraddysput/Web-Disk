﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using WebDisk.Database.DatabaseModel.Identity;
using System.Data.Entity;
using WebDisk.Database.BaseModels;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebDisk.Database.DatabaseModel
{
    public class WebDiskDbContext : IdentityDbContext<ApplicationUser, CustomRole, Guid,
        Identity.IdentityUserLogin, Identity.IdentityUserRole, Identity.IdentityUserClaim>, IWebDiskDbContext
    {
        public WebDiskDbContext()
            : base("WebDiskDb")
        {
        }

        public virtual DbSet<Blob> Blob { get; set; }
        public virtual DbSet<Directory> Directory { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<DirectoryShareInformation> DirectoryShareInformation { get; set; }
        public virtual DbSet<FileShareInformation> FileShareInformation { get; set; }



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
