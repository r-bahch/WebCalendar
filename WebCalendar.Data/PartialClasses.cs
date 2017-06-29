using System;
using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Data
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    [MetadataType(typeof(ContactMetadata))]
    public partial class Contact {

        [Display(Name ="Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
            private set { }
        }
    }
}