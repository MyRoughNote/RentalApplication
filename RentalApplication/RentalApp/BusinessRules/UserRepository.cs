using RentalApp.Models.DataContext;
using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalApp.BusinessRules
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDataContext context;
        public UserRepository(ApplicationDataContext context)
        {
            this.context = context;
        }
        public User Add(User data)
        {
            if(data == null)
            {
                return null;
            }
            else
            {
                context.Users.Add(data);
                context.SaveChanges();
                return context.Users.Find(data.UserId);
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
                var removeUser = context.Users.Find(Id);
                removeUser.UserStatus = (int)Constants.UserStatus.InActive;
                context.Entry(removeUser).State = EntityState.Modified;
                context.SaveChanges();
                return removeUser.UserId;
            }
        }

        public User Edit(User data)
        {
            if (data == null)
            {
                return null;
            }
            else
            {
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
                return context.Users.Find(data.UserId);
            }
        }

        public IEnumerable<Booking> GetUserBookings(int Id)
        {
            return context.Bookings
                .Where(c => c.UserId == Id).ToList();
        }

        public User GetById(int Id)
        {
            return context.Users.Find(Id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users.ToList();
        }
        public IEnumerable<User> GetAllUsersByRole(int RoleId)
        {
            return context.Users
                .Where(c => c.RoleId == RoleId).ToList();
        }
        public User IsValidUser(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return null;
            }
            else
            {
                var userDetails = context
                    .Users.Where(c => c.UserName == userName && c.Password == password && c.UserStatus == 1)
                    .FirstOrDefault();
                
                return userDetails;
            }
        }
        public string GetRoleById(int Id)
        {
            return context.Roles
                .Where(r => r.RoleId == Id)
                .Select(r => r.UserRole)
                .SingleOrDefault();
        }
    }
}