using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace AdventureWorks.Models
{
    public class ProductCategoryModel
    {
        [Required(ErrorMessage = "ProductCategoryID is required")]
        public int ProductCategoryID{get ; set ;}

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Category Name")]
        public string Name{get ; set ;}
   


        public System.Guid rowguid{get ; set ;}
        [Required(ErrorMessage = "ModifiedDate is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public System.DateTime ModifiedDate{get ; set ;}    
    }
}