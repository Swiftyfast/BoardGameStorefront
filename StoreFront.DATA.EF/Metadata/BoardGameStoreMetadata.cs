using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.DATA.EF//.Metadata
{
    #region Category Metadata
    public class CategroyMetaData
    {
        //public int CategoryID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* Category name must be 50 characters or less.")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategroyMetaData))]
    public partial class Category { }
    #endregion

    #region Department Metadata
    public class DepartmentMetaData
    {
        //public int DepartmentID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Department name must be 20 characters or less.")]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department {}
    #endregion

    #region Employee Metadata
    public class EmployeeMetaData
    {
        //public int EmployeeID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* First Name name must be 20 characters or less.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Last Name name must be 20 characters or less.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "* Title name must be 20 characters or less.")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string Title { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[N/A]")]
        public Nullable<System.DateTime> StartDate { get; set; }
        
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Direct Report To")]
        public Nullable<int> DirectReportTo { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        public Nullable<int> DepartmentID { get; set; }
    }

    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee { }
    #endregion

    #region InventoryStatu Metadata
    public class InventoryStatuMetaData
    {
        //public int InventoryID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(15, ErrorMessage = "* Inventory name must be 15 characters or less.")]
        [Display(Name = "Inventory Status")]
        public string InventoryName { get; set; }
    }

    [MetadataType(typeof(InventoryStatuMetaData))]
    public partial class InventoryStatu { }
    #endregion

    #region Product Metadata
    public class ProductMetaData
    {
        //public int ProductID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* The value must be 50 characters or less.")]
        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Category")]
        public Nullable<int> CategoryID { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "* The value ust be a valid number, 0 or larger")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[N/A]")]
        public Nullable<decimal> Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "* The value ust be a valid number, 0 or larger")]
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[N/A]")]
        [Display(Name = "Units Sold")]
        public Nullable<int> UnitsSold { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Inventory Status")]
        public Nullable<int> InventoryStatus { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        public Nullable<int> PublisherID { get; set; }

        [Display(Name = "Image")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string ImagePath { get; set; }
    }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product { }
    #endregion

    #region Publisher Metadata
    public class PublisherMetaData
    {
        //public int PublisherID { get; set; }

        [Required(ErrorMessage = "*Required")]
        [StringLength(50, ErrorMessage = "* Publisher name must be 50 characters or less.")]
        public string PublisherName { get; set; }

        [StringLength(20, ErrorMessage = "* City must be 20 characters or less.")]
        [DisplayFormat(NullDisplayText = "City is null")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* State must be 2 characters or less.")]
        [DisplayFormat(NullDisplayText = "State is null")]
        public string State { get; set; }

        [StringLength(20, ErrorMessage = "* Country must be 20 characters or less.")]
        [DisplayFormat(NullDisplayText = "Country is null")]
        public string Country { get; set; }
    }

    [MetadataType(typeof(PublisherMetaData))]
    public partial class Publisher { }
    #endregion
}
