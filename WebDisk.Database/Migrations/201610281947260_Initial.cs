namespace WebDisk.Database.Migrations
{
    using System;
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
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.BlobId);
            
            CreateTable(
                "dbo.Directories",
                c => new
                    {
                        DirectoryId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                        LastModifiedById = c.Guid(),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        LastBackupDate = c.DateTime(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DirectoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastModifiedById)
                .ForeignKey("dbo.Spaces", t => t.SpaceId, cascadeDelete: true)
                .Index(t => t.SpaceId)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Spaces",
                c => new
                    {
                        SpaceId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastLoginIp = c.String(),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SpaceId)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.SpaceShares",
                c => new
                    {
                        ShareId = c.Guid(nullable: false),
                        SharedDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                        SharedUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ShareId)
                .ForeignKey("dbo.AspNetUsers", t => t.SharedUserId, cascadeDelete: false)
                .ForeignKey("dbo.Spaces", t => t.SpaceId, cascadeDelete: false)
                .Index(t => t.SpaceId)
                .Index(t => t.SharedUserId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        Extension = c.String(nullable: false),
                        BlobStartId = c.Guid(nullable: false),
                        Size = c.Double(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        LastBackupDate = c.DateTime(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.Blobs", t => t.BlobStartId, cascadeDelete: true)
                .Index(t => t.BlobStartId)
                .Index(t => t.OwnerId);
            
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
            DropForeignKey("dbo.Files", "BlobStartId", "dbo.Blobs");
            DropForeignKey("dbo.Files", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Directories", "SpaceId", "dbo.Spaces");
            DropForeignKey("dbo.SpaceShares", "SpaceId", "dbo.Spaces");
            DropForeignKey("dbo.SpaceShares", "SharedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Spaces", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Directories", "LastModifiedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Files", new[] { "OwnerId" });
            DropIndex("dbo.Files", new[] { "BlobStartId" });
            DropIndex("dbo.SpaceShares", new[] { "SharedUserId" });
            DropIndex("dbo.SpaceShares", new[] { "SpaceId" });
            DropIndex("dbo.Spaces", new[] { "OwnerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Directories", new[] { "LastModifiedById" });
            DropIndex("dbo.Directories", new[] { "SpaceId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Files");
            DropTable("dbo.SpaceShares");
            DropTable("dbo.Spaces");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Directories");
            DropTable("dbo.Blobs");
        }
    }
}
