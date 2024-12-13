using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Models
{
    public class CascadingDropdownViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int SelectedSubCategoryId { get; set; }
        public IEnumerable<SelectListItem> SubCategories { get; set; }
    }
}