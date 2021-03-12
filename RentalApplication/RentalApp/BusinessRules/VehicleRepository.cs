using RentalApp.Models.DataContext;
using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalApp.BusinessRules
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDataContext context;
        public VehicleRepository(ApplicationDataContext context)
        {
            this.context = context;
        }
        public Vehicle AddVehicle(Vehicle data)
        {
            if (data == null)
            {
                return null;
            }
            else
            {
                context.Vehicles.Add(data);
                context.SaveChanges();
                return context.Vehicles.Find(data.VehicleId);
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
                var removeVehicle = context.Vehicles.Find(Id);
                removeVehicle.Status = (int)Constants.VehicleStatus.InActive;
                context.Entry(removeVehicle).State = EntityState.Modified;
                context.SaveChanges();
                return removeVehicle.VehicleId;
            }
        }

        public Vehicle EditVehicle(Vehicle data)
        {
            if (data == null)
            {
                return null;
            }
            else
            {
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
                return context.Vehicles.Find(data.VehicleId);
            }
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return context.Vehicles.ToList();
        }

        public Vehicle GetVehicleById(int Id)
        {
            return context.Vehicles.Find(Id);
        }

        public IEnumerable<Vehicle> GetVehiclesByOwnerId(int OwnerId)
        {
            return context.Vehicles.Where(c => c.OwnerId == OwnerId).ToList();
        }
    }
}