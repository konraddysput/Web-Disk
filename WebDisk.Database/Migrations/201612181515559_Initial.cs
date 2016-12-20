namespace WebDisk.Database.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blobs",
                c => new
                    {
                        BlobId = c.Guid(nullable: false),
                        Localisation = c.String(nullable: false),
                        LastBackupDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BlobId);
            
            CreateTable(
                "dbo.FieldShareInformations",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        FieldId = c.Guid(nullable: false),
                        SharedDate = c.DateTime(nullable: false),
                        SharedTime = c.Time(nullable: false, precision: 7),
                        ShareType = c.Int(nullable: false),
                        AccessType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FieldId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Fields", t => t.FieldId)
                .Index(t => t.UserId)
                .Index(t => t.FieldId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastLoginIp = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Spaces",
                c => new
                    {
                        SpaceId = c.Guid(nullable: false),
                        DefaultDirectoryId = c.Guid(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SpaceId)
                .ForeignKey("dbo.Fields", t => t.DefaultDirectoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.SpaceId)
                .Index(t => t.SpaceId)
                .Index(t => t.DefaultDirectoryId);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        FieldId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        LastBackupDate = c.DateTime(),
                        Attributes = c.Int(nullable: false),
                        DirectLink = c.String(),
                        Extension = c.String(),
                        ParentDirectoryId = c.Guid(),
                        LastModifiedById = c.Guid(),
                    })
                .PrimaryKey(t => t.FieldId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastModifiedById)
                .ForeignKey("dbo.Fields", t => t.ParentDirectoryId)
                .Index(t => t.ParentDirectoryId)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.FieldInformations",
                c => new
                    {
                        FieldId = c.Guid(nullable: false),
                        Size = c.Double(nullable: false),
                        BlobId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FieldId)
                .ForeignKey("dbo.Blobs", t => t.BlobId)
                .ForeignKey("dbo.Fields", t => t.FieldId)
                .Index(t => t.FieldId)
                .Index(t => t.BlobId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Spaces", "SpaceId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Spaces", "DefaultDirectoryId", "dbo.Fields");
            DropForeignKey("dbo.FieldShareInformations", "FieldId", "dbo.Fields");
            DropForeignKey("dbo.Fields", "ParentDirectoryId", "dbo.Fields");
            DropForeignKey("dbo.Fields", "LastModifiedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.FieldInformations", "FieldId", "dbo.Fields");
            DropForeignKey("dbo.FieldInformations", "BlobId", "dbo.Blobs");
            DropForeignKey("dbo.FieldShareInformations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FieldInformations", new[] { "BlobId" });
            DropIndex("dbo.FieldInformations", new[] { "FieldId" });
            DropIndex("dbo.Fields", new[] { "LastModifiedById" });
            DropIndex("dbo.Fields", new[] { "ParentDirectoryId" });
            DropIndex("dbo.Spaces", new[] { "DefaultDirectoryId" });
            DropIndex("dbo.Spaces", new[] { "SpaceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FieldShareInformations", new[] { "FieldId" });
            DropIndex("dbo.FieldShareInformations", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.FieldInformations");
            DropTable("dbo.Fields");
            DropTable("dbo.Spaces");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FieldShareInformations");
            DropTable("dbo.Blobs");
        }
    }
}
