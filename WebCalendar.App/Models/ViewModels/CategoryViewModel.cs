using System.ComponentModel.DataAnnotations;

namespace WebCalendar.App.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The category name must be between {2} and {1} symbols")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "Description must be less than {1} symbols")]
        public string Description { get; set; }
    }
}