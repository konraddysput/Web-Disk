namespace WebDisk.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSpaceNameAndType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Directories", "SpaceId", "dbo.Spaces");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Spaces", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SpaceShares", "SpaceId", "dbo.Spaces");
            DropForeignKey("dbo.SpaceShares", "SharedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "BlobStartId", "dbo.Blobs");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            AddColumn("dbo.Spaces", "Name", c => c.String());
            AddForeignKey("dbo.Directories", "SpaceId", "dbo.Spaces", "SpaceId");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Spaces", "OwnerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SpaceShares", "SpaceId", "dbo.Spaces", "SpaceId");
            AddForeignKey("dbo.SpaceShares", "SharedUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Files", "OwnerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Files", "BlobStartId", "dbo.Blobs", "BlobId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Files", "BlobStartId", "dbo.Blobs");
            DropForeignKey("dbo.Files", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SpaceShares", "SharedUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SpaceShares", "SpaceId", "dbo.Spaces");
            DropForeignKey("dbo.Spaces", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Directories", "SpaceId", "dbo.Spaces");
            DropColumn("dbo.Spaces", "Name");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Files", "BlobStartId", "dbo.Blobs", "BlobId", cascadeDelete: true);
            AddForeignKey("dbo.Files", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SpaceShares", "SharedUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SpaceShares", "SpaceId", "dbo.Spaces", "SpaceId", cascadeDelete: true);
            AddForeignKey("dbo.Spaces", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Directories", "SpaceId", "dbo.Spaces", "SpaceId", cascadeDelete: true);
        }
    }
}
