using Microsoft.Ajax.Utilities;
using RentalApp.Models.DataContext;
using RentalApp.Models.DataModels;
using RentalApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalApp.BusinessRules
{
    public class Validate
    {
        private readonly IUserRepository userContext;
        private readonly IVehicleRepository vehicleContext;

        public Validate()
        {
            var appContext = new ApplicationDataContext();
            this.userContext = new UserRepository(appContext);
            this.vehicleContext = new VehicleRepository(appContext);
        }

        public Validate(ApplicationDataContext context)
        {
            this.userContext = new UserRepository(context);
        }

        public User UserLogin(LoginView model)
        {            
            if (model == null || model.LoginName == null || model.Password == null)
            {
                return null;
            }
            else
            {
                var userDetails = userContext.IsValidUser(model.LoginName, model.Password);                    
                return userDetails;
            }            
        }

        public string GetRoleById(int RoleId)
        {
            return userContext.GetRoleById(RoleId);
        }

        public bool IsAlreadyExists(string UserName, int RoleType)
        {
            var user = userContext.GetAllUsersByRole(RoleType)
                    .Where(c => c.UserName.Trim().ToLower() == UserName.Trim().ToLower()).ToList();

            return user.Count() == 0 ? false : true;
        }
        
        public bool IsAlreadyExists(string RegistrationNumber)
        {
            var vehicle = vehicleContext.GetAllVehicles()
                .Where(c => c.RegistrationNo.Trim().ToLower() == RegistrationNumber.Trim().ToLower()).ToList();

            return vehicle.Count() == 0 ? false : true;
        }
    }
}