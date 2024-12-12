using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Models.Infrastructure
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategoryModel> GetProductCategory();
        ProductCategoryModel GetProductCategoryByID(int ProductCategoryId);
        void InsertProductCategory(ProductCategoryModel ProductCategory);
        void deleteProductCategory(int ProductCategoryId);
        void UpdateProductCategory(ProductCategoryModel ProductCategory);
    }
}
