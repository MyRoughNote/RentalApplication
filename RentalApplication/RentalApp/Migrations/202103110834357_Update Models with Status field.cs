namespace RentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelswithStatusfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bookings", "BookingStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "ContractStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "ContractStatus");
            DropColumn("dbo.Vehicles", "Status");
            DropColumn("dbo.Users", "UserStatus");
            DropColumn("dbo.Bookings", "BookingStatus");
            DropColumn("dbo.Bookings", "TotalAmount");
        }
    }
}
