namespace RentalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                        AdvanceAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookedBy = c.String(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        RegistrationNo = c.String(),
                        ManufactureYear = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Color = c.String(),
                        Type = c.String(),
                        RentPerDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Int(nullable: false),
                        Owner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Users", t => t.Owner_UserId)
                .Index(t => t.Owner_UserId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ContractId = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        AgreedLeaseDays = c.Int(nullable: false),
                        AgreedLeaseAmount = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        Owner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.Users", t => t.Owner_UserId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.Owner_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Contracts", "Owner_UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "Owner_UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Contracts", new[] { "Owner_UserId" });
            DropIndex("dbo.Contracts", new[] { "VehicleId" });
            DropIndex("dbo.Vehicles", new[] { "Owner_UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "VehicleId" });
            DropTable("dbo.Contracts");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Bookings");
        }
    }
}
