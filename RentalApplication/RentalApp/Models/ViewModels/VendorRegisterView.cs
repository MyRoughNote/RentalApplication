using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class VendorRegisterView
    {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Company Name is Required.")]
        [DisplayName("Company Name")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Login Name is Required.")]
        [DisplayName("Login Name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        [DisplayName("Password")]
        [DataType(DataType.Password, ErrorMessage = "Please enter Proper Password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required.")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password, ErrorMessage = "Please Re-enter Proper Password.")]
        [Compare("Password", ErrorMessage = "Confirm Password should Match with Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is Required.")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter proper Email ID")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required.")]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter proper phone Number Type")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter proper phone Number")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Maximum Length for Phone is 13")]
        public string PhoneNumber { get; set; }
        public int VendorStatus { get; set; }
        public string Notification { get; set; }
    }
}