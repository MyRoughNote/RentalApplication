using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.BusinessRules
{
    interface IBookingRepository
    {
        Booking AddBooking(Booking data);
        int Delete(int Id);
        Booking EditBooking(Booking data);
        Booking GetBookingById(int Id);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetBookingsByVehicleId(int VehicleId);
    }
}
