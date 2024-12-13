using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Models
{
    public class ProductSubCategoryModel
    {
        
        public int ProductSubcategoryID {get ; set ;}
        [Required(ErrorMessage = "ProductCategoryID is required")]
        public int ProductCategoryID {get ; set ;}
        public string ProductCategoryName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name {get ; set ;}
       
        public System.Guid rowguid {get ; set ;}

   
        public System.DateTime ModifiedDate {get ; set ;}
        
        public EntitySet<Product> Products { get ; set ;}

        
        public EntityRef<ProductCategory> ProductCategory {get ; set ;}

        public IEnumerable<SelectListItem> ProductCategories { get; set; }
    }
}