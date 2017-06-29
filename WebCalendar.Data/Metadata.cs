// Class for data annotations of generated models. Binding is in partialClasses.cs.

using System;
using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Data
{
    public class CategoryMetadata
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The category name must be between {2} and {1} symbols")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "Description must be less than {1} symbols")]
        public string Description { get; set; }
    }

    public class ContactMetadata
    {
        //TODO: check at least one of first name and last name is entered
        [Display(Name = "First Name")]
        [MaxLength(100, ErrorMessage = "The first name must be at most {1} characters long")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(100, ErrorMessage = "The last name must be at most {1} characters long")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.DateTime)]

        public DateTime? BirthDate { get; set; }

        [Display(Name = "Phone number")]
        [RegularExpression("([+]?[0-9]*)", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [MaxLength(100, ErrorMessage = "The address must be at most {1} characters long")]
        public string Address { get; set; }

        [Display(Name = "Aditional Info")]
        [MaxLength(200, ErrorMessage = "The aditional info must be at most {1} characters long")]
        public string AditionalInfo { get; set; }
    }
}