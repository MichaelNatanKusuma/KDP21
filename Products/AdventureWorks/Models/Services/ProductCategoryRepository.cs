using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AdventureWorks.Models.Infrastructure;

namespace AdventureWorks.Models.Services
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private OperationDataContext _dataContext;
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["AdventureWorks2022ConnectionString2"].ToString();

        public string ConnectionStringSettings
        {
            get => connectionStringSettings; set =>
                connectionStringSettings = value;
        }
        public ProductCategoryRepository()
        {
            _dataContext = new OperationDataContext(ConnectionStringSettings);
        }
        public void deleteProductCategory(int ProductCategoryId)
        {
            ProductCategory productCategory = _dataContext.ProductCategories.Where(u => u.ProductCategoryID == ProductCategoryId).SingleOrDefault();
            _dataContext.ProductCategories.DeleteOnSubmit(productCategory);
            _dataContext.SubmitChanges();
        }

        public IEnumerable<ProductCategoryModel> GetProductCategory()
        {
            IList<ProductCategoryModel> productCategoryList = new List<ProductCategoryModel>();
            var query = from category in _dataContext.ProductCategories
                        select category;
                        
            var productCategories = query.ToList();
            foreach (var categoryData in productCategories)
            {
                productCategoryList.Add(new ProductCategoryModel()
                {
                    ProductCategoryID = categoryData.ProductCategoryID,
                    Name = categoryData.Name,
                    ModifiedDate = categoryData.ModifiedDate
                    
                });
            }
            return productCategoryList;
        }

        public ProductCategoryModel GetProductCategoryByID(int ProductCategoryId)
        {
            var query = from u in _dataContext.ProductCategories
                        where
                        u.ProductCategoryID == ProductCategoryId
                        select u;
            var categories = query.FirstOrDefault();
            var model = new ProductCategoryModel()
            {
                ProductCategoryID = categories.ProductCategoryID,
                Name = categories.Name,
                ModifiedDate = categories.ModifiedDate          
            };
            return model;
        }

        public void InsertProductCategory(ProductCategoryModel ProductCategory)
        {
            var categoryData = new ProductCategory()
            {
                Name = ProductCategory.Name,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()


            };
            _dataContext.ProductCategories.InsertOnSubmit(categoryData);
            _dataContext.SubmitChanges();
        }

        public void UpdateProductCategory(ProductCategoryModel ProductCategory)
        {
            ProductCategory categoryData = _dataContext.ProductCategories.Where(u => u.ProductCategoryID == ProductCategory.ProductCategoryID).SingleOrDefault();
            categoryData.Name = ProductCategory.Name;
            categoryData.ModifiedDate = DateTime.Now;
           
            _dataContext.SubmitChanges();
        }
    }
}