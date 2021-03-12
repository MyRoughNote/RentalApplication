using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class VendorDashboardView
    {
        public User VendorDetail { get; set; }
        public IEnumerable<Vehicle> VendorOwnedVehicles { get; set; }
    }
}