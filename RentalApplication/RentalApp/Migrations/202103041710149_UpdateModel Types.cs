namespace RentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vehicles", "ManufactureYear", c => c.String());
            DropColumn("dbo.Bookings", "TotalAmount");
            DropColumn("dbo.Bookings", "BookedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookedBy", c => c.String());
            AddColumn("dbo.Bookings", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Vehicles", "ManufactureYear", c => c.Int(nullable: false));
            DropColumn("dbo.Bookings", "BookingFrom");
        }
    }
}
