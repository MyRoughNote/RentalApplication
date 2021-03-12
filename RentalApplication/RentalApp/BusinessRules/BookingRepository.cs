using Microsoft.Ajax.Utilities;
using RentalApp.Models.DataContext;
using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalApp.BusinessRules
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDataContext context;
        public BookingRepository(ApplicationDataContext context)
        {
            this.context = context;
        }
        public Booking AddBooking(Booking data)
        {
            if (data == null)
            {
                return null;
            }
            else
            {
                context.Bookings.Add(data);
                context.SaveChanges();
                return context.Bookings.Find(data.BookingId);
            }
        }

        public int Delete(int Id)
        {
            if (Id.Equals(null))
            {
                return -1;
            }
            else
            {
                var removeBooking = context.Bookings.Find(Id);
                removeBooking.BookingStatus = (int)Constants.BookingStatus.Canceled;
                context.Entry(removeBooking).State = EntityState.Modified;
                context.SaveChanges();
                return removeBooking.UserId;
            }
        }

        public Booking EditBooking(Booking data)
        {
            if(data == null)
            {
                return null;
            }
            else
            {
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
                return context.Bookings.Find(data.BookingId);
            }
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return context.Bookings.ToList();
        }

        public Booking GetBookingById(int Id)
        {
            return context.Bookings.Find(Id);
        }

        public IEnumerable<Booking> GetBookingsByVehicleId(int VehicleId)
        {
            return context.Bookings.Where(c => c.VehicleId == VehicleId).ToList();            
        }
    }
}