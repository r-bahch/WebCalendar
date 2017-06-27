using System.ComponentModel.DataAnnotations;

namespace WebCalendar.App.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The Username field is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The username must be between {2} and {1} symbols")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White spaces are not allowed.")]
        [System.Web.Mvc.Remote("IsUsernameAvailble", "Validations", ErrorMessage = "This username is taken")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        [System.Web.Mvc.Remote("IsEmailAvailable", "Validations", ErrorMessage = "There already exists a user with that Email address")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(100, ErrorMessage = "The first name must be at most {1} characters long")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(100, ErrorMessage = "The first name must be at most {1} characters long")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
    }
}