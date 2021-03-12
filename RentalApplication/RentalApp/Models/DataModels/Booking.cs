using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.Models.DataModels
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingFrom { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NoOfDays { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int BookingStatus { get; set; }

    }
}