using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class LoginView
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage ="Please enter UserName")]
        public string LoginName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password, ErrorMessage = "Please enter Proper Password")]
        public string Password  { get; set; }

        public string Notification { get; set; }
    }
}