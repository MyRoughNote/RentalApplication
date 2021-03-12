using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.BusinessRules
{
    interface IUserRepository
    {
        User Add(User data);
        int Delete(int Id);        
        User Edit(User data);
        User GetById(int Id);
        IEnumerable<User> GetAllUsers();
        User IsValidUser(string userName, string password);
        IEnumerable<Booking> GetUserBookings(int Id);
        string GetRoleById(int RoleId);
        IEnumerable<User> GetAllUsersByRole(int RoleId);
    }
}
