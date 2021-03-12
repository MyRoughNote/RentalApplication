using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class VehicleRegisterView
    {
        [Required(ErrorMessage = "Registration No is Required")]
        [DisplayName("Registration Number")]
        public string RegistrationNo { get; set; }

        [Required(ErrorMessage = "Manufacture Year is Required")]
        [DisplayName("Manufacture Year")]
        public string ManufactureYear { get; set; }

        [Required(ErrorMessage = "Make/Brand is Required")]
        [DisplayName("Make / Brand")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is Required")]
        [DisplayName("Vehicle Model")]
        public string VehicleModel { get; set; }

        [Required(ErrorMessage = "Color is Required")]
        [DisplayName("Vehicle Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Fuel Type is Required")]
        [DisplayName("Fuel Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Rent / Day is Required")]
        [DisplayName("Approximate Rent / Day")]
        public decimal RentPerDay { get; set; }

        [Required(ErrorMessage = "Vehicle Owner is Required")]
        [DisplayName("Vehicle Owner")]
        public int OwnerId { get; set; }
        public string Notification { get; set; }
    }
}