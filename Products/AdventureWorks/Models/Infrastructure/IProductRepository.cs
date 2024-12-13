using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Models.Infrastructure
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetProducts();
        ProductModel GetProductByID(int ProductId);
        void InsertProduct(ProductModel Product);
        void deleteProduct(int ProductId);
        void UpdateProduct(ProductModel Product);
        ProductModel GetCategories();
        List<ProductSubCategoryModel> GetProductSubcategories(int productCategoryId);
    }
}
