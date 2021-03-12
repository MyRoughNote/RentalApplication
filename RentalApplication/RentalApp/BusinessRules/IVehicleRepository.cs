using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.BusinessRules
{
    interface IVehicleRepository
    {
        Vehicle AddVehicle(Vehicle data);
        int Delete(int Id);
        Vehicle EditVehicle(Vehicle data);
        Vehicle GetVehicleById(int Id);
        IEnumerable<Vehicle> GetAllVehicles();
        IEnumerable<Vehicle> GetVehiclesByOwnerId(int OwnerId);
    }
}
