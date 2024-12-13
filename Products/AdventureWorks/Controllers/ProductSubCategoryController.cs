using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AdventureWorks.Helper;
using AdventureWorks.Models;
using AdventureWorks.Models.Infrastructure;
using AdventureWorks.Models.Services;

namespace AdventureWorks.Controllers
{
    public class ProductSubCategoryController : Controller
    {
        private IProductSubCategoryRepository _repository;
        private OperationDataContext _dataContext;
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["AdventureWorks2022ConnectionString2"].ToString();

        public string ConnectionStringSettings
        {
            get => connectionStringSettings; set =>
                connectionStringSettings = value;
        }

        public ProductSubCategoryController() : this(new ProductSubCategoryRepository())
        {

        }
        public ProductSubCategoryController(IProductSubCategoryRepository repository)
        {
            _repository = repository;
            _dataContext = new OperationDataContext(ConnectionStringSettings);
        }
        // GET: ProductSubCategory
        public ActionResult Index()
        {
            var subCategories = _repository.GetProductSubCategory();
            return View(subCategories);
        }

        public JsonResult GetSubCategory()
        {
            var data = _repository.GetProductSubCategory();
            

            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductSubCategory/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                ProductSubCategoryModel model = _repository.GetProductSubCategoryByID(id);
                return View(model);
            }
            catch (Exception)
            {
                //return View("Error");
                throw new Exception("Test error");
            }
        }

        // GET: ProductSubCategory/Create
        public ActionResult Create()
        {
          
        
            ProductSubCategoryModel model = _repository.GetCategories();
            
            return View(model);
        }

        // POST: ProductSubCategory/Create
        [HttpPost]
        public ActionResult Create(ProductSubCategoryModel subCategory)
        {
       
            try
            {

                if (ModelState.IsValid)
                {
                                     
                    _repository.InsertProductSubCategory(subCategory);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                
                ModelState.AddModelError("", "Unable to save changes. Try again," +
                    "and if the problem persists see your system administrator");

            }
            var dropdownGenerator = new DropdownGenerator<ProductCategory>(
               x => x.Name, // Selector for Text
               x => x.ProductCategoryID.ToString() // Selector for Value
           );

            // Generate the SelectListItems
            var categories = dropdownGenerator.PrepareSelectList(_dataContext.ProductCategories.AsQueryable());

            //subCategory = _repository.GetCategories();
            subCategory.ProductCategories = categories;
            
            
            //subCategory.ProductCategories = _repository.GetCategories().ProductCategories;
            return View(subCategory);
        }

        // GET: ProductSubCategory/Edit/5
        public ActionResult Edit(int id)
        {
            ProductSubCategoryModel model = _repository.GetProductSubCategoryByID(id);
            return View(model);
        }

        // POST: ProductSubCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductSubCategoryModel subcategory)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateProductSubCategory(subcategory);
                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.");
            }
            return View(subcategory);
        }

        // GET: ProductSubCategory/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ProductSubCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _repository.deleteProductSubCategory(id);
                return RedirectToAction("Index");
                //Publisher publisher = context.Publishers.Where(x => x.Id == publishInput.Id).SingleOrDefault();
                //context.Publishers.DeleteOnSubmit(publisher);
                //context.SubmitChanges();
                //return RedirectToAction("Index");
            }
            catch
            {
                Response.StatusCode = 500;
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                   "problem persists see your system administrator.");
                return View();
            }
        }
    }
}
