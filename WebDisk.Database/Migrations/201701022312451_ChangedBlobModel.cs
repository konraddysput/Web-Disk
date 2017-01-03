namespace WebDisk.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBlobModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FieldInformations", "BlobId", "dbo.Blobs");
            DropIndex("dbo.FieldInformations", new[] { "BlobId" });
            RenameColumn(table: "dbo.FieldInformations", name: "FieldId", newName: "FieldInformationId");
            RenameIndex(table: "dbo.FieldInformations", name: "IX_FieldId", newName: "IX_FieldInformationId");
            AddColumn("dbo.FieldInformations", "Localisation", c => c.String(nullable: false));
            AddColumn("dbo.FieldInformations", "LastBackupDate", c => c.DateTime());
            DropColumn("dbo.FieldInformations", "BlobId");
            DropTable("dbo.Blobs");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.FieldInformations", "BlobId", c => c.Guid(nullable: false));
            DropColumn("dbo.FieldInformations", "LastBackupDate");
            DropColumn("dbo.FieldInformations", "Localisation");
            RenameIndex(table: "dbo.FieldInformations", name: "IX_FieldInformationId", newName: "IX_FieldId");
            RenameColumn(table: "dbo.FieldInformations", name: "FieldInformationId", newName: "FieldId");
            CreateIndex("dbo.FieldInformations", "BlobId");
            AddForeignKey("dbo.FieldInformations", "BlobId", "dbo.Blobs", "BlobId");
        }
    }
}
