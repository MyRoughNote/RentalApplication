using RentalApp.BusinessRules;
using RentalApp.Models.DataContext;
using RentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly Validate validate;        
        public LoginController()
        {
            validate = new Validate();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginView model)
        {
            if (ModelState.IsValid)
            {
                var userDetail = validate.UserLogin(model);

                if (userDetail == null)
                {
                    model.Notification = @"User does not Exists!";
                    return View("Index", model);
                }
                else if(validate.GetRoleById(userDetail.RoleId) == "User")
                {
                    return RedirectToAction("UserDashboard", "Dashboard", new { id = userDetail.UserId });
                }
                else if (validate.GetRoleById(userDetail.RoleId) == "Vendor")
                {
                    return RedirectToAction("VendorDashboard","Dashboard", new { id = userDetail.UserId });
                }
                else if (validate.GetRoleById(userDetail.RoleId) == "Admin")
                {
                    return RedirectToAction("AdminDashboard", "Dashboard", new { id = userDetail.UserId });
                }
                Session["UserId"] = userDetail.UserId;
            }
            return RedirectToAction("Index");
        }
        public ActionResult SignOut()
        {
            Session.Abandon();
            return View("Index");
        }
    }
}