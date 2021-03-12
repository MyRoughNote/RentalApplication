using Microsoft.Ajax.Utilities;
using RentalApp.BusinessRules;
using RentalApp.Models.DataModels;
using RentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RentalApp.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUserRepository userRepository;
        private readonly Validate validate;
        
        public VehicleController()
        {
            var appcontext = new Models.DataContext.ApplicationDataContext();
            vehicleRepository = new VehicleRepository(appcontext);
            userRepository = new UserRepository(appcontext);
            validate = new Validate();
        }
        // GET: Vehicle
        public ActionResult Index(int UserId)
        {
            var GetAllVehicles = vehicleRepository.GetAllVehicles();
            var userDetails = userRepository.GetById(UserId);
            ViewBag.UserId = UserId;
            ViewBag.UserRole = userRepository.GetRoleById(userDetails.RoleId);
            return View(GetAllVehicles);
        }
        
        // GET: Vehicle/Details/5
        public ActionResult Details(int VehicleId)
        {
            if (VehicleId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleDetails = vehicleRepository.GetVehicleById(VehicleId);
            if (vehicleDetails == null)
            {
                return HttpNotFound();
            }
            return View(vehicleDetails);            
        }

        // GET: Vehicle/Create
        public ActionResult AddVehicle(int UserId)
        {           
            ViewBag.OwnerId = new SelectList(GetOwnersList(), "UserId", "UserName");
            ViewBag.UserId = UserId;
            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicle(VehicleRegisterView model, int UserId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (validate.IsAlreadyExists(model.RegistrationNo))
                    {
                        model.Notification = string.Format("Registration Number '{0}' Already Exists", model.RegistrationNo);
                        ViewBag.UserId = UserId;
                        ViewBag.OwnerId = new SelectList(GetOwnersList(), "UserId", "UserName");
                        return View ("AddVehicle", model);
                    }
                    else
                    {
                        var vehicle = new Vehicle()
                        {
                            RegistrationNo = model.RegistrationNo,
                            ManufactureYear = model.ManufactureYear,
                            Make = model.Make,
                            Model = model.VehicleModel,
                            Color = model.Color,
                            Type = model.Type,
                            RentPerDay = model.RentPerDay,
                            OwnerId = model.OwnerId,
                            Status = (int) Constants.VehicleStatus.Active
                        };
                        vehicleRepository.AddVehicle(vehicle);
                        return RedirectToAction("Index", "Vehicle", new { @UserId = UserId });
                    }
                }
                return View("AddVehicle", model);                
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int VehicleId, int UserId)
        {
            if (VehicleId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = vehicleRepository.GetVehicleById(VehicleId);

            if (vehicle == null)
            {
                return HttpNotFound();
            }

            var editVehicleRegisterView = new VehicleRegisterView()
            {
                RegistrationNo = vehicle.RegistrationNo,
                ManufactureYear = vehicle.ManufactureYear,
                Make = vehicle.Make,
                VehicleModel = vehicle.Model,
                Color = vehicle.Color,
                Type= vehicle.Type,
                RentPerDay  = vehicle.RentPerDay,
                OwnerId = vehicle.OwnerId                
            };

            var userDetails = userRepository.GetById(UserId);
            ViewBag.UserId = UserId;
            ViewBag.UserRole = userRepository.GetRoleById(userDetails.RoleId);
            ViewBag.VehicleId = VehicleId;            
            ViewBag.OwnerId = new SelectList(GetOwnersList(), "UserId", "UserName", vehicle.OwnerId);            
            return View(editVehicleRegisterView);            
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehicleRegisterView model, int VehicleId, int UserId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var editVehicle = vehicleRepository.GetVehicleById(VehicleId);
                    bool IsAdmin = userRepository.GetRoleById(userRepository.GetById(UserId).RoleId) == "Admin";

                    editVehicle.RegistrationNo = model.RegistrationNo;
                    editVehicle.ManufactureYear = model.ManufactureYear;
                    editVehicle.Make = model.Make;
                    editVehicle.Model = model.VehicleModel;
                    editVehicle.Color = model.Color;
                    editVehicle.Type = model.Type;
                    editVehicle.RentPerDay = model.RentPerDay;
                    editVehicle.OwnerId = IsAdmin ? model.OwnerId : UserId;
                    editVehicle.Status = (int)Constants.VehicleStatus.Active;

                    vehicleRepository.EditVehicle(editVehicle);
                    if (IsAdmin)
                    {
                        return RedirectToAction("Index", "Vehicle", new { @UserId = UserId });
                    }
                    else
                    {
                        return RedirectToAction("VendorDashboard", "Dashboard", new { id = UserId });
                    }
                }                
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int VehicleId, int UserId)
        {
            if (VehicleId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleDetails = vehicleRepository.GetVehicleById(VehicleId);

            var userDetails = userRepository.GetById(UserId);
            ViewBag.UserId = UserId;
            ViewBag.UserRole = userRepository.GetRoleById(userDetails.RoleId);
            ViewBag.VehicleId = VehicleId;

            var deleteVehicle = new VehicleRegisterView
            {
                RegistrationNo = vehicleDetails.RegistrationNo,
                ManufactureYear = vehicleDetails.ManufactureYear,
                Make = vehicleDetails.Make,
                VehicleModel = vehicleDetails.Model,
                Color = vehicleDetails.Color,
                Type = vehicleDetails.Type,
                RentPerDay = vehicleDetails.RentPerDay,
                OwnerId = vehicleDetails.OwnerId
            };

            if (vehicleDetails == null)
            {
                return HttpNotFound();
            }
            return View(deleteVehicle);         
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVehicle(int VehicleId, int UserId)
        {
            try
            {
                if (VehicleId.Equals(null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int deletedVehicle = vehicleRepository.Delete(VehicleId);
                bool IsAdmin = userRepository.GetRoleById(userRepository.GetById(UserId).RoleId) == "Admin";
                if (deletedVehicle == -1)
                {
                    return HttpNotFound();
                }

                if (IsAdmin)
                {
                    return RedirectToAction("Index", "Vehicle", new { @UserId = UserId });
                }
                else
                {
                    return RedirectToAction("VendorDashboard", "Dashboard", new { id = UserId });
                }                
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private IEnumerable<User> GetOwnersList()
        {
            return userRepository.GetAllUsersByRole((int)Constants.Roles.Vendor);
        }
    }
}
