namespace RentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contracts", "Owner_UserId", "dbo.Users");
            DropIndex("dbo.Contracts", new[] { "Owner_UserId" });
            RenameColumn(table: "dbo.Contracts", name: "Owner_UserId", newName: "UserId");
            AlterColumn("dbo.Contracts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts", "UserId");
            AddForeignKey("dbo.Contracts", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            DropColumn("dbo.Contracts", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contracts", "OwnerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Contracts", "UserId", "dbo.Users");
            DropIndex("dbo.Contracts", new[] { "UserId" });
            AlterColumn("dbo.Contracts", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Contracts", name: "UserId", newName: "Owner_UserId");
            CreateIndex("dbo.Contracts", "Owner_UserId");
            AddForeignKey("dbo.Contracts", "Owner_UserId", "dbo.Users", "UserId");
        }
    }
}
