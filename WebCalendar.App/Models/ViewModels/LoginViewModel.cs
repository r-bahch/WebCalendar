using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCalendar.App.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The Username field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //TODO: implement
        public bool RememberMe { get; set; }
    }
}