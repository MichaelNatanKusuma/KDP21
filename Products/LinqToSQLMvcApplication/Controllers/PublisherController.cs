using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using LinqToSQLMvcApplication.Models;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace LinqToSQLMvcApplication.Controllers
{
    public class PublisherController : Controller
    {
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_KDPMVCConnectionString"].ToString();

        public string ConnectionStringSettings
        {
            get => connectionStringSettings; set =>
                connectionStringSettings = value;
        }

        private OperationDataContext context;
        public PublisherController()
        {
            context = new OperationDataContext(ConnectionStringSettings);
        }

        public ActionResult Index()
        {
            //IList<PublisherModel> publisherList = new List<PublisherModel>();
            //var query = from publisher in context.Publishers
            //            select publisher;
            //var publishers = query.ToList();
            //foreach (var publisherData in publishers)
            //{
            //    publisherList.Add(new PublisherModel()
            //    {
            //        Id = publisherData.Id,
            //        Name = publisherData.Name,
            //        Year = Convert.ToInt32(publisherData.Year)
            //    });
            //}
            return View();
        }
        public JsonResult GetData()
        {
            var query = from publisher in context.Publishers
                         select new
                         {
                             publisher.Name,
                             publisher.Year
                         };
                var data = query.ToList();

            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult SearchAndSort(string searchTerm, string sortOrder)
        //{
        //    var result = from publisher in context.Publishers
        //                 select publisher;

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        result = result.Where(p => p.Name.Contains(searchTerm));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            result = result.OrderByDescending(p => p.Name);
        //            break;
        //        case "year":
        //            result = result.OrderBy(p => p.Year);
        //            break;
        //        default:
        //            result = result.OrderBy(p => p.Name);
        //            break;
        //    }
        //    try
        //    {
        //        return View(result.ToList());
        //    }
        //    catch
        //    {
        //        throw new Exception("Test error");
        //    }
        //    //return PartialView("_ProductList", result.ToList());

        //}

        // This is the GET method for Index with parameters
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
         

            // Fetch publishers from the context
            //var publishers = from p in context.Publishers
            //                 select p;

         

            //// Convert Publisher to PublisherModel for the view
            //var publisherModels = publishers.Select(p => new PublisherModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Year = int.Parse(p.Year)
            //}).ToList();
            

            //// Return the view with paginated PublisherModel data
            return View();
        }


        public ActionResult Edit(int id)
        {
            PublisherModel model = context.Publishers.Where(x => x.Id == id).Select(x =>
                                new PublisherModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Year = Convert.ToInt32(x.Year)
                                 
                                }).SingleOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(PublisherModel publisher)
        {
            Publisher publisherData = context.Publishers.Where(u => u.Id == publisher.Id).SingleOrDefault();
            publisherData.Name = publisher.Name;
            publisherData.Year = publisher.Year.ToString();
            context.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            PublisherModel model = context.Publishers.Where(x => x.Id == id).Select(x =>
                                  new PublisherModel()
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      Year = Convert.ToInt32( x.Year)
                                
                                  }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PublisherModel publishInput)
        {
            try
            {
                Publisher publisher = context.Publishers.Where(x => x.Id == publishInput.Id).SingleOrDefault();
                context.Publishers.DeleteOnSubmit(publisher);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Response.StatusCode = 500;
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                   "problem persists see your system administrator.");
                return View();
            }
            
        }
        // GET: Publisher/Create
        public ActionResult Create()
        {
            PublisherModel model = new PublisherModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PublisherModel model)
        {
            try
            {
                Publisher publisher = new Publisher()
                {
                    Name = model.Name,
                    Year = model.Year.ToString()
                };
                if (context.Publishers.Any(p => p.Name == model.Name))
                {
                    ModelState.AddModelError("Name", "Publisher Name must be unique.");
                }
                if (ModelState.IsValid)
                {
                    var message = $"{model.Name} created successfully";
                    TempData["SuccessMessage"] = message;
                    context.Publishers.InsertOnSubmit(publisher);
                    context.SubmitChanges();
                    //return RedirectToAction("Index");
                }
                else
                {
                    var message = model.Name + " creation was unsuccessful. Please check your input and try again. ";
 
                    TempData["ErrorMessage"] = message;
                }
                //return View(model);
            }
            catch(Exception ex)
            {
                //return View(model);
                var message = model.Name + " creation was unsuccessful. Please check your input and try again. " +
            ex.Message + " " + ex.InnerException ?? string.Empty;
                TempData["ErrorMessage"] = message;
            }
            return RedirectToAction("Index");

        }

    }
}
