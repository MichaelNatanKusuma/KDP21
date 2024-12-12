using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Models.Infrastructure
{
    public interface IProductSubCategoryRepository
    {
        IEnumerable<ProductSubCategoryModel> GetProductSubCategory();
        ProductSubCategoryModel GetCategories();
        ProductSubCategoryModel GetProductSubCategoryByID(int ProductSubCategoryId);
        void InsertProductSubCategory(ProductSubCategoryModel ProductSubCategory);
        void deleteProductSubCategory(int ProductSubCategoryId);
        void UpdateProductSubCategory(ProductSubCategoryModel ProductSubCategory);
    }
}
