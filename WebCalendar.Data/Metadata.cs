// Class for data annotations of generated models. Binding is in partialClasses.cs.

using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Data
{
    public class CategoryMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The category name must be between {2} and {1} symbols")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "Description must be less than {1} symbols")]
        public string Description { get; set; }
    }
}