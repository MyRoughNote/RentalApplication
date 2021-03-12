using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.BusinessRules
{
    public static class Constants
    {
        public enum Roles
        {
            User = 1,
            Vendor = 2,
            Admin = 3
        }

        public enum BookingStatus
        {
            Booked = 1,
            Canceled = 2,
            Deleted = 3
        }

        public enum VehicleStatus
        {
            Active = 1,
            InActive = 2,
            Deleted = 3
        }

        public enum UserStatus
        {
            Active = 1,
            InActive = 2,
            Deleted = 3
        }
    }
}