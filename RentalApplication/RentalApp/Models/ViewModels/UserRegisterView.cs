using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class UserRegisterView
    {
        [Required(ErrorMessage = "UserName is Required.")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        [DisplayName("Password")]
        [DataType(DataType.Password, ErrorMessage = "Please enter Proper Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password, ErrorMessage = "Please re-enter Proper Password")]
        [Compare("Password", ErrorMessage = "Confirm Password should Match with Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is Required.")]
        [DisplayName("Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address is Required.")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter proper Email ID")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required.")]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter proper phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter proper phone Number")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Maximum Length for Phone is 13")]
        public string PhoneNumber { get; set; }
        public string Notification { get; set; }
    }
}