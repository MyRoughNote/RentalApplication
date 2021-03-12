using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class UserDashboardView
    {
        public User UserDetail { get; set; }
        public IEnumerable<Booking> BookingDetail { get; set; }
    }
}