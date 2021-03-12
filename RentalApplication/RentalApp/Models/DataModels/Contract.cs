using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.Models.DataModels
{
    public class Contract
    {
        public int ContractId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int AgreedLeaseDays { get; set; }
        public int AgreedLeaseAmount { get; set; }
        public string CreatedBy { get; set; }
        public int ContractStatus { get; set; }
    }
}