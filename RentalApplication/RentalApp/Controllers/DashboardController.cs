using RentalApp.BusinessRules;
using RentalApp.Models.DataModels;
using RentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IVehicleRepository vehicleRepository;
        public DashboardController()
        {
            var appContext = new Models.DataContext.ApplicationDataContext();
            userRepository = new UserRepository(appContext);
            vehicleRepository = new VehicleRepository(appContext);
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserDashboard(int Id)
        {            
            var userDetails = userRepository.GetById(Id);
            ViewBag.UserName = userDetails.UserName;
            ViewBag.Name = userDetails.Name;
            ViewBag.UserId = userDetails.UserId;

            var userDashboardView = new UserDashboardView
            {
                UserDetail = userDetails,
                BookingDetail = userRepository.GetUserBookings(Id)                
            };
            
            return View(userDashboardView);
        }

        public ActionResult VendorDashboard(int Id)
        {            
            var vendorDetails = userRepository.GetById(Id);
            ViewBag.UserName = vendorDetails.UserName;
            ViewBag.Name = vendorDetails.Name;
            ViewBag.UserId = vendorDetails.UserId;

            var vendorDashboardView = new VendorDashboardView
            {
                VendorDetail = vendorDetails,
                VendorOwnedVehicles = vehicleRepository.GetVehiclesByOwnerId(Id)
            };
            return View(vendorDashboardView);
        }

        public ActionResult AdminDashboard(int Id)
        {            
            var adminDetails = userRepository.GetById(Id);
            ViewBag.UserName = adminDetails.UserName;
            ViewBag.Name = adminDetails.Name;
            ViewBag.UserId = adminDetails.UserId;

            return View();
        }
    }
}