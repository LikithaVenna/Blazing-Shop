using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blazing_Shop.ViewModels
{
    public class NewAppointmentViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int ?Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string PersonName { get; set; }



        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }



        [Required(ErrorMessage = "Please provide your PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid phone number")]
        public long PhoneNumber { get; set; }



        public bool IsConfirmed { get; set; }



        public int ?PId { get; set; }

        public NewAppointmentViewModel()
        {
            Id = 0;
        }
        public NewAppointmentViewModel(Appointment appointment)
        {
            Id = appointment.Id;
            PersonName = appointment.PersonName;
            Email = appointment.Email;
            Date = appointment.Date;
            PhoneNumber = appointment.PhoneNumber;
            IsConfirmed = appointment.IsConfirmed;
            PId = appointment.PId;
        }

    }
}