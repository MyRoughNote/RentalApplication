using RentalApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalApp.Models.ViewModels
{
    public class BookingView
    {        
        [Required(ErrorMessage = "Date is Required")]
        [DisplayName("Booking Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingFrom { get; set; }

        [Required(ErrorMessage = "Vehicle is Required")]
        [DisplayName("Required Vehicle")]
        public int  VehicleId { get; set; }

        [Required(ErrorMessage = "No of Days is Required")]
        [DisplayName("Number of Days Vehicle Required")]
        [Range(1,5,ErrorMessage ="Maximum 5 Days are allowed")]
        public int NoOfDays { get; set; }

        [Required(ErrorMessage = "Advance Amount is Required")]
        [DisplayName("Booking Advance Amount")]
        [DataType(DataType.Currency, ErrorMessage ="Enter proper Amount")]
        public decimal AdvanceAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int BookingStatus { get; set; }
    }
}