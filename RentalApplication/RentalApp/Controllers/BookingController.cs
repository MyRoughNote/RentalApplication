using RentalApp.BusinessRules;
using RentalApp.Models.DataModels;
using RentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RentalApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository bookRepository;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUserRepository userRepository;
        public BookingController()
        {
            var appcontext = new Models.DataContext.ApplicationDataContext();
            bookRepository = new BookingRepository(appcontext);
            vehicleRepository = new VehicleRepository(appcontext);
            userRepository = new UserRepository(appcontext);
        }
        // GET: Booking
        public ActionResult Index(int UserId)
        {
            var allBookingDetails = (from booking in bookRepository.GetAllBookings()
                                     join vehicle in vehicleRepository.GetAllVehicles()
                                     on booking.VehicleId equals vehicle.VehicleId
                                     join user in userRepository.GetAllUsers()
                                     on booking.UserId equals user.UserId
                                     select new Booking()
                                     {
                                         BookingId = booking.BookingId,
                                         BookingDate = booking.BookingDate,
                                         BookingFrom = booking.BookingFrom,
                                         VehicleId = vehicle.VehicleId,
                                         Vehicle = vehicle,
                                         UserId = user.UserId,
                                         User = user,
                                         NoOfDays = booking.NoOfDays,
                                         AdvanceAmount = booking.AdvanceAmount,
                                         TotalAmount= booking.TotalAmount,
                                         BookingStatus= booking.BookingStatus
                                     }).ToList();
            ViewBag.UserId = UserId;
            return View(allBookingDetails);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int BookingId)
        {
            if (BookingId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookingDetails = bookRepository.GetBookingById(BookingId);           
            if(bookingDetails == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetails);
        }

        // GET: Booking/Create
        public ActionResult BookVehicle(int UserId)
        {            
            ViewBag.VehicleList = new SelectList(GetAllVehicles(), "VehicleId", "Model", "Select");
            ViewBag.UserId = UserId;
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookVehicle(BookingView model, int UserId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    decimal vehicleRentPerDay = GetVehicleById(model.VehicleId).RentPerDay;
                    var bookingDetail = new Booking() 
                    { 
                         BookingDate = DateTime.Today,
                         BookingFrom = model.BookingFrom,
                         VehicleId = model.VehicleId,
                         UserId = UserId,
                         NoOfDays= model.NoOfDays,
                         AdvanceAmount = model.AdvanceAmount,
                         TotalAmount = (model.NoOfDays * vehicleRentPerDay),
                         BookingStatus = (int) Constants.BookingStatus.Booked 
                    };
                    bookRepository.AddBooking(bookingDetail);
                    if(userRepository.GetRoleById(userRepository.GetById(UserId).RoleId) == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Dashboard", new { id = UserId });
                    }
                    else
                    {
                        return RedirectToAction("UserDashboard", "Dashboard", new { id = UserId });
                    }
                }
                return RedirectToAction("BookVehicle");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int BookingId)
        {
            if (BookingId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }             
            
            var booking = bookRepository.GetBookingById(BookingId);
            
            ViewBag.VehicleList = new SelectList(GetAllVehicles(), "VehicleId", "Model", booking.VehicleId);
            ViewBag.BookingId = booking.BookingId;
            ViewBag.UserId = booking.UserId;
            var editBookingView = new BookingView()
            {
                BookingFrom = booking.BookingFrom,
                AdvanceAmount = booking.AdvanceAmount,
                NoOfDays = booking.NoOfDays,
            };

            if (booking == null)
            {
                return HttpNotFound();
            }
            
            return View(editBookingView);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingView model, int UserId, int BookingId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    decimal vehicleRentPerDay = GetVehicleById(model.VehicleId).RentPerDay;
                    var editBooking = bookRepository.GetBookingById(BookingId);
                    editBooking.BookingDate = DateTime.Today;
                    editBooking.BookingFrom = model.BookingFrom;
                    editBooking.VehicleId = model.VehicleId;
                    editBooking.UserId = UserId;
                    editBooking.NoOfDays = model.NoOfDays;
                    editBooking.AdvanceAmount = model.AdvanceAmount;
                    editBooking.TotalAmount = (model.NoOfDays* vehicleRentPerDay);
                    editBooking.BookingStatus = (int)Constants.BookingStatus.Booked;

                    bookRepository.EditBooking(editBooking);
                    if (userRepository.GetRoleById(userRepository.GetById(UserId).RoleId) == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Dashboard", new { id = UserId });
                    }
                    else
                    {
                        return RedirectToAction("UserDashboard", "Dashboard", new { id = UserId });
                    }
                }
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }        
               
        public ActionResult Delete(int BookingId)
        {
            if (BookingId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookingDetails = bookRepository.GetBookingById(BookingId);

            ViewBag.BookedVehicle = GetVehicleById(bookingDetails.VehicleId).Model;        

            var viewModel = new BookingView
            {
                  BookingFrom =  bookingDetails.BookingFrom,
                  VehicleId = bookingDetails.VehicleId,
                  NoOfDays = bookingDetails.NoOfDays,
                  AdvanceAmount = bookingDetails.AdvanceAmount                  
            };

            if (bookingDetails == null)
            {
                return HttpNotFound();
            }
            return View(bookingDetails);
        }
        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBooking(int BookingId)
        {
            try
            {
                if (BookingId.Equals(null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int BookedUserId = bookRepository.Delete(BookingId);
                if (BookedUserId == -1)
                {
                    return HttpNotFound();                    
                }
                return RedirectToAction("UserDashboard", "Dashboard", new { @Id = BookedUserId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewBookings(int VehicleId, int UserId)
        {
            if (VehicleId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookingDetails = (from booking in bookRepository.GetAllBookings()
                                  join vehicle in vehicleRepository.GetAllVehicles()
                                  on booking.VehicleId equals vehicle.VehicleId
                                  join user in userRepository.GetAllUsers()
                                  on booking.UserId equals user.UserId
                                  select new Booking()
                                  {
                                      BookingId = booking.BookingId,
                                      BookingDate = booking.BookingDate,
                                      BookingFrom = booking.BookingFrom,
                                      VehicleId = vehicle.VehicleId,
                                      Vehicle = vehicle,
                                      UserId = user.UserId,
                                      User = user,
                                      NoOfDays= booking.NoOfDays,
                                      AdvanceAmount = booking.AdvanceAmount,
                                      TotalAmount = booking.TotalAmount,
                                      BookingStatus = booking.BookingStatus
                                  }).Where(c => c.VehicleId == VehicleId).ToList();                          

            if (bookingDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = UserId;
            return View(bookingDetails);
        }
        
        [NonAction]
        private Vehicle GetVehicleById(int Id)
        {
            return vehicleRepository.GetVehicleById(Id);
        }
        [NonAction]
        private IEnumerable<Vehicle> GetAllVehicles()
        {
            return vehicleRepository.GetAllVehicles().ToList();
        }
    }
}
