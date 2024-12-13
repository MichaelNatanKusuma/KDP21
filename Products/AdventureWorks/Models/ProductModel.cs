using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Models
{
    public class ProductModel
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int totalQuantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID{get ; set ;}
        [Required(ErrorMessage = "Name is required")]
        public string Name{get ; set ;}
        public int ProductCategoryID { get; set; }
        [Required(ErrorMessage = "ProductNumber is required")]
        public string ProductNumber{get ; set ;}
        [Required(ErrorMessage = "MakeFlag is required")]
        public bool MakeFlag{get ; set ;}
        [Required(ErrorMessage = "FinishedGoodsFlag is required")]
        public IEnumerable<SelectListItem> ProductCategories { get; set; }
        public IEnumerable<SelectListItem> ProductSubCategories { get; set; }
        public bool FinishedGoodsFlag{get ; set ;}
        public string Color{get ; set ;}
        [Required(ErrorMessage = "SafetyStockLevel is required")]
        public short SafetyStockLevel{get ; set ;}
        [Required(ErrorMessage = "ReorderPoint is required")]
        public short ReorderPoint{get ; set ;}
        [Required(ErrorMessage = "StandardCost is required")]
        public decimal StandardCost{get ; set ;}
        [Required(ErrorMessage = "ListPrice is required")]
        public decimal ListPrice{get ; set ;}
       
        public string Size{get ; set ;}
       
        public string SizeUnitMeasureCode{get ; set ;}
       
        public string WeightUnitMeasureCode{get ; set ;}
        
        public System.Nullable<decimal> Weight{get ; set ;}
        [Required(ErrorMessage = "DaysToManufacture is required")]
        public int DaysToManufacture{get ; set ;}
      
        public string ProductLine{get ; set ;}
        [RegularExpression("^[HML]$", ErrorMessage = "ProductLine must be either 'H', 'M' or 'L'.")]
        public string Class{get ; set ;}
        
        public string Style{get ; set ;}
        
        public System.Nullable<int> ProductSubcategoryID{get ; set ;}
        
        public System.Nullable<int> ProductModelID{get ; set ;}
        [Required(ErrorMessage = "SellStartDate is required")]
        public System.DateTime SellStartDate{get ; set ;}
       
        public System.Nullable<System.DateTime> SellEndDate{get ; set ;}
     
        public System.Nullable<System.DateTime> DiscontinuedDate{get ; set ;}     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Guid rowguid{get ; set ;}     
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy HH:mm:ss}", ApplyFormatInEditMode =true)]
        public System.DateTime ModifiedDate{get ; set ;}
        
    }
}