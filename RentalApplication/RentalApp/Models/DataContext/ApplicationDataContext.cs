using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalApp.Models.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(): base("ApplicationDatabaseConnection")
        { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

    }
}