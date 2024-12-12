using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AdventureWorks.Helper;
using AdventureWorks.Models.Infrastructure;

namespace AdventureWorks.Models.Services
{
    public class ProductSubCategoryRepository : IProductSubCategoryRepository
    {
        private OperationDataContext _dataContext;
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["AdventureWorks2022ConnectionString2"].ToString();

        public string ConnectionStringSettings
        {
            get => connectionStringSettings; set =>
                connectionStringSettings = value;
        }
        public ProductSubCategoryRepository()
        {
            _dataContext = new OperationDataContext(ConnectionStringSettings);
        }

        public void deleteProductSubCategory(int ProductSubCategoryId)
        {
            ProductSubcategory productSubCategory = _dataContext.ProductSubcategories.Where(u => u.ProductSubcategoryID == ProductSubCategoryId).SingleOrDefault();
            _dataContext.ProductSubcategories.DeleteOnSubmit(productSubCategory);
            _dataContext.SubmitChanges();
        }



        public IEnumerable<ProductSubCategoryModel> GetProductSubCategory()
        {
            IList<ProductSubCategoryModel> productSubCategoryList = new List<ProductSubCategoryModel>();
            var query = from subcategory in _dataContext.ProductSubcategories
                        join category in _dataContext.ProductCategories on subcategory.ProductCategoryID equals category.ProductCategoryID
                        select new
                        {
                            Category = category,
                            Subcategory = subcategory
                        };
          

            var productSubCategories = query.ToList();
            foreach (var subCategoryData in productSubCategories)
            {
                productSubCategoryList.Add(new ProductSubCategoryModel()
                {
                    ProductSubcategoryID = subCategoryData.Subcategory.ProductSubcategoryID,
                    ProductCategoryID = subCategoryData.Subcategory.ProductCategoryID,
                    Name = subCategoryData.Subcategory.Name,
                    ModifiedDate = subCategoryData.Subcategory.ModifiedDate,
                    ProductCategoryName = subCategoryData.Category.Name

                });
            }
            return productSubCategoryList;
        }

        public ProductSubCategoryModel GetCategories()
        {
            var dropdownGenerator = new DropdownGenerator<ProductCategory>(
               x => x.Name, // Selector for Text
               x => x.ProductCategoryID.ToString() // Selector for Value
           );

            // Generate the SelectListItems
            var categories = dropdownGenerator.PrepareSelectList(_dataContext.ProductCategories.AsQueryable());

            ProductSubCategoryModel model = new ProductSubCategoryModel();
            model.ProductCategories = categories;
            return model;
        }

        public ProductSubCategoryModel GetProductSubCategoryByID(int ProductSubCategoryId)
        {
            var query = from u in _dataContext.ProductSubcategories
                        where
                        u.ProductSubcategoryID == ProductSubCategoryId
                        select u;
            var subCategory = query.FirstOrDefault();
            var model = new ProductSubCategoryModel()
            {
                ProductSubcategoryID = subCategory.ProductSubcategoryID,
                ProductCategoryID = subCategory.ProductCategoryID,
                Name = subCategory.Name,
                ModifiedDate = subCategory.ModifiedDate
            };
            return model;
        }

        public void InsertProductSubCategory(ProductSubCategoryModel ProductSubCategory)
        {
            var subCategoryData = new ProductSubcategory()
            {           
                ProductCategoryID = ProductSubCategory.ProductCategoryID,
                Name = ProductSubCategory.Name,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()

            };
            _dataContext.ProductSubcategories.InsertOnSubmit(subCategoryData);
            _dataContext.SubmitChanges();
        }

        public void UpdateProductSubCategory(ProductSubCategoryModel ProductSubCategory)
        {
            ProductSubcategory subCategoryData = _dataContext.ProductSubcategories.Where(u => u.ProductSubcategoryID == ProductSubCategory.ProductSubcategoryID).SingleOrDefault();
            subCategoryData.Name = ProductSubCategory.Name;
            subCategoryData.ProductCategoryID = ProductSubCategory.ProductCategoryID;
            subCategoryData.ModifiedDate = DateTime.Now;

            _dataContext.SubmitChanges();
        }
    }
}