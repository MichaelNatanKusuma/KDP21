using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorks.Models;
using AdventureWorks.Models.Infrastructure;
using AdventureWorks.Models.Services;

namespace AdventureWorks.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryRepository _repository;

        public ProductCategoryController() : this(new ProductCategoryRepository())
        {

        }
        public ProductCategoryController(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            var categories = _repository.GetProductCategory();
            return View(categories);
        }

        public JsonResult GetCategory()
        {
            var data = _repository.GetProductCategory();

            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductCategory/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                ProductCategoryModel model = _repository.GetProductCategoryByID(id);
                return View(model);
            }
            catch (Exception)
            {
                //return View("Error");
                throw new Exception("Test error");
            }
        }

        // GET: ProductCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        [HttpPost]
        public ActionResult Create(ProductCategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertProductCategory(category);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again," +
                    "and if the problem persists see your system administrator");

            }
            return View(category);
        }

        // GET: ProductCategory/Edit/5
        public ActionResult Edit(int id)
        {
            ProductCategoryModel model = _repository.GetProductCategoryByID(id);
            return View(model);
        }

        // POST: ProductCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductCategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateProductCategory(category);
                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.");
            }
            return View(category);
        }

        // GET: ProductCategory/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ProductCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _repository.deleteProductCategory(id);
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
