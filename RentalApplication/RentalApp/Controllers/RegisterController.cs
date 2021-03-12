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
    public class RegisterController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly Validate validate;
        public RegisterController()
        {
            userRepository = new UserRepository(new Models.DataContext.ApplicationDataContext());
            validate = new Validate();
        }
        // GET: Register
        public ActionResult ViewVendors(int UserId)
        {
            var GetAllVendors = userRepository.GetAllUsersByRole((int) Constants.Roles.Vendor);
            var userDetails = userRepository.GetById(UserId);            
            ViewBag.UserRole = userRepository.GetRoleById(userDetails.RoleId);
            ViewBag.UserId = UserId;

            List<VendorRegisterView> vendors = new List<VendorRegisterView>();

            foreach (var item in GetAllVendors)
            {
                vendors.Add(new VendorRegisterView()
                {
                    VendorId = item.UserId,
                    VendorName = item.Name,
                    LoginName = item.UserName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    VendorStatus = item.UserStatus
                });
            }
            return View(vendors);
        }

        public ActionResult ViewUsers(int UserId)
        {
            ViewBag.UserId = UserId;
            var GetAllUsers = userRepository.GetAllUsersByRole((int) Constants.Roles.User);
            return View(GetAllUsers);
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserRegisterView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (validate.IsAlreadyExists(model.UserName, (int)Constants.Roles.User))
                    {
                        model.Notification = string.Format("UserName '{0}' Already Exists", model.UserName);
                        return View("RegisterUser", model);
                    }
                    else
                    {
                        var user = new User()
                        {
                            UserName = model.UserName,
                            Password = model.ConfirmPassword,
                            Name = model.FullName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            UserStatus= (int)Constants.UserStatus.Active,
                            RoleId = (int)Constants.Roles.User
                        };

                        var addedUser = userRepository.Add(user);
                        return RedirectToAction("UserDashboard", "Dashboard", new { id = addedUser.UserId });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return View("RegisterUser",model);
        }

        
        public ActionResult EditUserDetail(int UserId) 
        {
            if (UserId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userDetails = userRepository.GetById(UserId);
            
            if (userDetails == null)
            {
                return HttpNotFound();
            }

            var editUser = new UserRegisterView()
            {
                UserName= userDetails.UserName,
                FullName = userDetails.Name,
                Email = userDetails.Email,
                PhoneNumber= userDetails.PhoneNumber,
                Password = userDetails.Password                
            };

            ViewBag.UserId = UserId;
            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserDetail(UserRegisterView model, int UserId)
        {
            if (ModelState.IsValid)
            {
                var editUser = userRepository.GetById(UserId);
                                
                editUser.Password = model.ConfirmPassword;
                editUser.Name = model.FullName;
                editUser.Email = model.Email;
                editUser.PhoneNumber = model.PhoneNumber;
                editUser.UserStatus = (int)Constants.UserStatus.Active;
                editUser.RoleId = (int)Constants.Roles.User;                

                userRepository.Edit(editUser);
                return RedirectToAction("UserDashboard", "Dashboard", new { id = UserId });
            }
            return View("EditUserDetail", model);
        }
        public ActionResult RegisterVendor(int UserId)
        {
            if (!UserId.Equals(-1))
            {
                var userDetails = userRepository.GetById(UserId);
                ViewBag.UserRole = userRepository.GetRoleById(userDetails.RoleId);
                ViewBag.UserId = UserId;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterVendor(VendorRegisterView model, int UserId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (validate.IsAlreadyExists(model.LoginName, (int)Constants.Roles.Vendor))
                    {
                        model.Notification = string.Format("Login Name '{0}' Already Exists", model.LoginName);
                        return View("RegisterVendor", model);
                    }
                    else
                    {
                        var user = new User()
                        {
                            UserName = model.LoginName,
                            Password = model.ConfirmPassword,
                            Name = model.VendorName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            UserStatus = (int) Constants.VehicleStatus.Active,
                            RoleId = (int)Constants.Roles.Vendor
                        };

                        var addedUser = userRepository.Add(user);
                        if (UserId == -1)
                        {
                            return RedirectToAction("VendorDashboard", "Dashboard", new { id = addedUser.UserId });
                        }
                        else
                        {
                            return RedirectToAction("ViewVendors", "Register", new { @UserId = UserId });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }            
            return View("RegisterVendor");
        }

        public ActionResult EditVendorDetail(int UserId)
        {
            if (UserId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userDetails = userRepository.GetById(UserId);

            if (userDetails == null)
            {
                return HttpNotFound();
            }

            var editVendor = new VendorRegisterView()
            {
                VendorName = userDetails.Name,
                LoginName = userDetails.UserName,
                Email = userDetails.Email,
                PhoneNumber = userDetails.PhoneNumber,
                Password = userDetails.Password
            };

            ViewBag.UserId = UserId;
            return View(editVendor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVendorDetail(VendorRegisterView model, int UserId)
        {
            if (ModelState.IsValid)
            {
                var editVendor = userRepository.GetById(UserId);

                editVendor.Password = model.ConfirmPassword;
                editVendor.Name = model.VendorName;
                editVendor.Email = model.Email;
                editVendor.PhoneNumber = model.PhoneNumber;
                editVendor.UserStatus = (int)Constants.VehicleStatus.Active;
                editVendor.RoleId = (int)Constants.Roles.Vendor;

                userRepository.Edit(editVendor);
                return RedirectToAction("VendorDashboard", "Dashboard", new { id = UserId });
            }
            return View("EditVendorDetail", model);
        }

        public ActionResult Delete(int VendorId, int UserId)
        {
            if (VendorId.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userDetails = userRepository.GetById(VendorId);            

            var viewDeleteModel = new VendorRegisterView
            {
                 VendorName = userDetails.Name,
                 LoginName = userDetails.UserName,
                 Email = userDetails.Email,
                 PhoneNumber = userDetails.PhoneNumber
            };

            if (userDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = UserId;
            return View(viewDeleteModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVendor(int VendorId, int UserId)
        {
            try
            {
                if (VendorId.Equals(null))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int deletedUserId = userRepository.Delete(VendorId);
                if (deletedUserId == -1)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("ViewVendors", "Register", new { @UserId = UserId });
            }
            catch
            {
                return View();
            }
        }
    }
}