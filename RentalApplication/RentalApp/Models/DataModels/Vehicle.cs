using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.Models.DataModels
{
    public class Vehicle
    {
        public int VehicleId { get; set; }        
        public string RegistrationNo { get; set; }
        public string ManufactureYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }        
        public string Type { get; set; }
        public decimal RentPerDay { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public int Status { get; set; }
    }
}