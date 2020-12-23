using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IntroToMVC.DATA.DOMAIN
{
    public class CategoryDomainModel
    {
        [Key]
        //Designate the property as a Primary Key, and says taht this property is auto assigned and incremented
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(15, ErrorMessage = "Maximum 15 characters")]
        public string CategoryName { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public string Description { get; set; }

        //no attributes needed, this property will not be displayed
        public object picture { get; set; }
    }
}
