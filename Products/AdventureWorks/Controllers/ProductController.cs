using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorks.Models;
using AdventureWorks.Models.Infrastructure;
using AdventureWorks.Models.Services;

namespace AdventureWorks.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        // GET: Product

        public ProductController() : this(new ProductRepository())
        {

        }
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            //var products = _repository.GetProducts();
            return View();
            
        }
        public JsonResult GetProduct()
        {
            var data = _repository.GetProducts();

            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertProduct(product);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again," +
                    "and if the problem persists see your system administrator");
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel model = _repository.GetProductByID(id);
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel product)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _repository.UpdateProduct(product);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.");
                
            }
            return View(product);
        }     

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _repository.deleteProduct(id);
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
