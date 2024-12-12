using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using AdventureWorks.Models.Infrastructure;

namespace AdventureWorks.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private OperationDataContext _dataContext;
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["AdventureWorks2022ConnectionString2"].ToString();

        public string ConnectionStringSettings
        {
            get => connectionStringSettings; set =>
                connectionStringSettings = value;
        }

        public ProductRepository()
        {
            _dataContext = new OperationDataContext(ConnectionStringSettings);
        }
        public void deleteProduct(int ProductId)
        {
            Product product = _dataContext.Products.Where(u => u.ProductID == ProductId).SingleOrDefault();
            _dataContext.Products.DeleteOnSubmit(product);
            _dataContext.SubmitChanges();
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            IList<ProductModel> productList = new List<ProductModel>();
            var query = from product in _dataContext.Products
                        join subCategory in _dataContext.ProductSubcategories on product.ProductSubcategoryID equals subCategory.ProductSubcategoryID
                        join category in _dataContext.ProductCategories on subCategory.ProductCategoryID equals category.ProductCategoryID
                        select new
                        {
                            Product = product,
                            ProductSubcategory = subCategory,
                            ProductCategory = category
                        };
            var products = query.ToList();
            foreach (var productData in products)
            {
                productList.Add(new ProductModel()
                {
                    ProductID = productData.Product.ProductID,
                    CategoryName = productData.ProductCategory.Name,
                    SubCategoryName = productData.ProductSubcategory.Name,
                    Name = productData.Product.Name,
                    Class = productData.Product.Class,
                    Color = productData.Product.Color,
                    DaysToManufacture = productData.Product.DaysToManufacture,
                    DiscontinuedDate = productData.Product.DiscontinuedDate,
                    FinishedGoodsFlag = productData.Product.FinishedGoodsFlag,
                    ListPrice = productData.Product.ListPrice,
                    MakeFlag = productData.Product.MakeFlag,
                    ModifiedDate = productData.Product.ModifiedDate,
                    ProductLine = productData.Product.ProductLine,
                    ProductModelID = productData.Product.ProductModelID,
                    ProductNumber = productData.Product.ProductNumber,
                    ProductSubcategoryID = productData.Product.ProductSubcategoryID,
                    ReorderPoint = productData.Product.ReorderPoint,
                    rowguid = productData.Product.rowguid,
                    SafetyStockLevel = productData.Product.SafetyStockLevel,
                    SellEndDate = productData.Product.SellEndDate,
                    SellStartDate = productData.Product.SellStartDate,
                    Size = productData.Product.Size,
                    SizeUnitMeasureCode = productData.Product.SizeUnitMeasureCode,
                    StandardCost = productData.Product.StandardCost,
                    Style = productData.Product.Style,
                    Weight = productData.Product.Weight,
                    WeightUnitMeasureCode = productData.Product.WeightUnitMeasureCode
                });
            }
            return productList;
        }

        public ProductModel GetProductByID(int ProductId)
        {
            var query = from u in _dataContext.Products
                        where
                        u.ProductID == ProductId
                        select u;
            var productData = query.FirstOrDefault();
            var categoryAndSubCategory = from subCategory in _dataContext.ProductSubcategories
                           join category in _dataContext.ProductCategories on subCategory.ProductCategoryID equals category.ProductCategoryID
                           where subCategory.ProductSubcategoryID == productData.ProductSubcategoryID
                           select new
                           {                           
                               ProductSubcategory = subCategory,
                               ProductCategory = category
                           };
            //var categoryAndSubData = categoryAndSubCategory.FirstOrDefault();
            var model = new ProductModel()
            {
                ProductID = productData.ProductID,
                //CategoryName = category,
                Name = productData.Name,
                Class = productData.Class,
                Color = productData.Color,
                DaysToManufacture = productData.DaysToManufacture,
                DiscontinuedDate = productData.DiscontinuedDate,
                FinishedGoodsFlag = productData.FinishedGoodsFlag,
                ListPrice = productData.ListPrice,
                MakeFlag = productData.MakeFlag,
                ModifiedDate = productData.ModifiedDate,
                ProductLine = productData.ProductLine,
                ProductModelID = productData.ProductModelID,
                ProductNumber = productData.ProductNumber,
                ProductSubcategoryID = productData.ProductSubcategoryID,
                ReorderPoint = productData.ReorderPoint,
                rowguid = productData.rowguid,
                SafetyStockLevel = productData.SafetyStockLevel,
                SellEndDate = productData.SellEndDate,
                SellStartDate = productData.SellStartDate,
                Size = productData.Size,
                SizeUnitMeasureCode = productData.SizeUnitMeasureCode,
                StandardCost = productData.StandardCost,
                Style = productData.Style,
                Weight = productData.Weight,
                WeightUnitMeasureCode = productData.WeightUnitMeasureCode
            };
            return model;
        }

        public void InsertProduct(ProductModel productData)
        {
            var productInsert = new Product()
            {
                ProductID = productData.ProductID,
                Name = productData.Name,
                Class = productData.Class,
                Color = productData.Color,
                DaysToManufacture = productData.DaysToManufacture,
                DiscontinuedDate = productData.DiscontinuedDate,
                FinishedGoodsFlag = productData.FinishedGoodsFlag,
                ListPrice = productData.ListPrice,
                MakeFlag = productData.MakeFlag,
                ModifiedDate = DateTime.Now,
                ProductLine = productData.ProductLine,
                ProductModelID = productData.ProductModelID,
                ProductNumber = productData.ProductNumber,
                ProductSubcategoryID = productData.ProductSubcategoryID,
                ReorderPoint = productData.ReorderPoint,               
                SafetyStockLevel = productData.SafetyStockLevel,
                SellEndDate = productData.SellEndDate,
                SellStartDate = productData.SellStartDate,
                Size = productData.Size,
                SizeUnitMeasureCode = productData.SizeUnitMeasureCode,
                StandardCost = productData.StandardCost,
                Style = productData.Style,
                Weight = productData.Weight,
                WeightUnitMeasureCode = productData.WeightUnitMeasureCode,
                rowguid = Guid.NewGuid()
            };
            _dataContext.Products.InsertOnSubmit(productInsert);
            _dataContext.SubmitChanges();
        }

        public void UpdateProduct(ProductModel productData)
        {
            Product productInserted = _dataContext.Products.Where(u => u.ProductID == productData.ProductID).SingleOrDefault();
            productInserted.ProductID = productData.ProductID;
            productInserted.Name = productData.Name;
            productInserted.Class = productData.Class;
            productInserted.Color = productData.Color;
            productInserted.DaysToManufacture = productData.DaysToManufacture;
            productInserted.DiscontinuedDate = productData.DiscontinuedDate;
            productInserted.FinishedGoodsFlag = productData.FinishedGoodsFlag;
            productInserted.ListPrice = productData.ListPrice;
            productInserted.MakeFlag = productData.MakeFlag;
            productInserted.ModifiedDate = DateTime.Now;
            productInserted.ProductLine = productData.ProductLine;
            productInserted.ProductModelID = productData.ProductModelID;
            productInserted.ProductNumber = productData.ProductNumber;
            productInserted.ProductSubcategoryID = productData.ProductSubcategoryID;
            productInserted.ReorderPoint = productData.ReorderPoint;        
            productInserted.SafetyStockLevel = productData.SafetyStockLevel;
            productInserted.SellEndDate = productData.SellEndDate;
            productInserted.SellStartDate = productData.SellStartDate;
            productInserted.Size = productData.Size;
            productInserted.SizeUnitMeasureCode = productData.SizeUnitMeasureCode;
            productInserted.StandardCost = productData.StandardCost;
            productInserted.Style = productData.Style;
            productInserted.Weight = productData.Weight;
            productInserted.WeightUnitMeasureCode = productData.WeightUnitMeasureCode;
            _dataContext.SubmitChanges();
        }
    }
}