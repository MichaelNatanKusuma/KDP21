using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToSQLMvcApplication.Models;
using PagedList;


namespace LinqToSQLMvcApplication.Controllers
{
    public class PublisherController : Controller
    {
        private DataClasses1DataContext context;

        // Constructor to initialize context
        public PublisherController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB_KDP21ConnectionString"].ToString();
            context = new DataClasses1DataContext(connectionString);
        }

        public ActionResult Index()
        {
            IList<PublisherModel> publisherList = new List<PublisherModel>();
            var query = from publisher in context.Publishers
                        select publisher;
            var publishers = query.ToList();
            foreach (var publisherData in publishers)
            {
                publisherList.Add(new PublisherModel()
                {
                    Id = publisherData.Id,
                    Name = publisherData.Name,
                    Year = int.Parse(publisherData.Year)
                });
            }
            return View(publisherList);
        }
        public ActionResult Create()
        {
            PublisherModel model = new PublisherModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(PublisherModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate Year (should be between 0 and the current year)
                if (model.Year > DateTime.Now.Year || model.Year < 0)
                {
                    ModelState.AddModelError("Year", "Year must be between 0 and the current year.");
                    return View(model);
                }

                // Check if the publisher name already exists (case-insensitive)
                bool publisherExists = context.Publishers.Any(p => p.Name.ToLower() == model.Name.ToLower());
                if (publisherExists)
                {
                    ModelState.AddModelError("Name", "A publisher with this name already exists.");
                    return View(model);
                }

                try
                {
                    // Create a new publisher
                    Publisher publisher = new Publisher()
                    {
                        Name = model.Name,
                        Year = model.Year.ToString()
                    };

                    context.Publishers.InsertOnSubmit(publisher);
                    context.SubmitChanges();

                    // After successful creation, redirect to the Index action
                    return RedirectToAction("Index");
                }
                catch
                {
                    // Handle any database errors
                    ModelState.AddModelError("", "An error occurred while saving the publisher. Please try again.");
                    return View(model);
                }
            }
            else
            {
                // If the model state is invalid, return the view with validation errors
                return View(model);
            }
        }


        public ActionResult Edit(int id)
        {
            // Directly retrieve the Publisher using its Id without using 'Where'
            Publisher publisher = context.Publishers.SingleOrDefault(p => p.Id == id);

            if (publisher == null)
            {
                return HttpNotFound();
            }

            // Create a PublisherModel instance from the fetched Publisher data
            PublisherModel model = new PublisherModel()
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Year = int.Parse(publisher.Year)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PublisherModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Year > DateTime.Now.Year || model.Year < 0)
                {
                    ModelState.AddModelError("Year", "Year must be between 0 until present.");
                    return View(model);
                }

                try
                {
                    Publisher publisher = context.Publishers.SingleOrDefault(p => p.Id == model.Id);
                    if (publisher == null)
                    {
                        return HttpNotFound();
                    }

                    publisher.Name = model.Name;
                    publisher.Year = model.Year.ToString();
                    context.SubmitChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            // Retrieve the Publisher by Id
            Publisher publisher = context.Publishers.SingleOrDefault(p => p.Id == id);

            // If the publisher is not found, return a 404 error
            if (publisher == null)
            {
                return HttpNotFound();
            }

            // Create a PublisherModel from the Publisher data to show in the delete confirmation view
            PublisherModel model = new PublisherModel()
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Year = int.Parse(publisher.Year)
            };

            // Return the model to the Delete view
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Retrieve the Publisher by Id
            Publisher publisher = context.Publishers.SingleOrDefault(p => p.Id == id);

            // If the publisher is not found, return a 404 error
            if (publisher == null)
            {
                return HttpNotFound();
            }

            // Delete the Publisher from the context
            context.Publishers.DeleteOnSubmit(publisher);

            // Submit the changes to the database to delete the record
            context.SubmitChanges();

            // Redirect to the Index view after deletion
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            // Directly retrieve the Publisher by Id
            Publisher publisher = context.Publishers.SingleOrDefault(p => p.Id == id);

            // If the publisher is not found, return a 404 error
            if (publisher == null)
            {
                return HttpNotFound();
            }

            // Create a PublisherModel from the Publisher data
            PublisherModel model = new PublisherModel()
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Year = int.Parse(publisher.Year)
            };

            // Return the model to the Details view
            return View(model);
        }



        // GET: Publisher - Handles displaying the list with sorting, searching, and paging.
        // This is the GET method for Index without parameters
 


        // This is the GET method for Index with parameters
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // Set up sorting parameters
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";

            // Handle the search string, reset pagination on new search
            if (searchString != null)
            {
                page = 1; // Reset to page 1 when a new search is performed
            }
            else
            {
                searchString = currentFilter; // Keep the existing search string
            }

            ViewBag.CurrentFilter = searchString;

            // Fetch publishers from the context
            var publishers = from p in context.Publishers
                             select p;

            // Apply search filter if search string is not empty
            if (!String.IsNullOrEmpty(searchString))
            {
                publishers = publishers.Where(p => p.Name.Contains(searchString)
                                                  || p.Year.Contains(searchString));
            }

            // Apply sorting based on the selected sort order
            switch (sortOrder)
            {
                case "name_desc":
                    publishers = publishers.OrderByDescending(p => p.Name);
                    break;
                case "Year":
                    publishers = publishers.OrderBy(p => p.Year);
                    break;
                case "year_desc":
                    publishers = publishers.OrderByDescending(p => p.Year);
                    break;
                default:  // Name ascending
                    publishers = publishers.OrderBy(p => p.Name);
                    break;
            }

            // Convert Publisher to PublisherModel for the view
            var publisherModels = publishers.Select(p => new PublisherModel
            {
                Id = p.Id,
                Name = p.Name,
                Year = int.Parse(p.Year)
            }).ToList();

            // Implement pagination with a page size of 3
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            // Create a paged list of PublisherModel
            var pagedPublisherModels = publisherModels.ToPagedList(pageNumber, pageSize);

            // Return the view with paginated PublisherModel data
            return View(pagedPublisherModels);
        }


    }

}


    
