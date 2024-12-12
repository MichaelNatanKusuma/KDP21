using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToSQLMvcApplication.Models;
using LinqToSQLMvcApplication.Helper;
using System.Data.SqlClient;
using PagedList;

namespace LinqToSQLMvcApplication.Controllers
{
    public class BookController : Controller
    {
        private OperationDataContext context;
        public string connectionStringSettings = ConfigurationManager.ConnectionStrings["DB_KDPMVCConnectionString"].ToString();

        public string ConnectionStringSettings
        {
            get => connectionStringSettings; set =>
                connectionStringSettings = value;
        }
        public BookController()
        {
            context = new OperationDataContext(ConnectionStringSettings);
        }
        //private void PreparePublisher(BookModel model)
        //{
        //    model.Publishers = context.Publishers.AsQueryable<Publisher>().Select(x =>
        //            new SelectListItem()
        //            {
        //                Text = x.Name,
        //                Value = x.Id.ToString()
        //            });
        //}
        public ActionResult Index(string sortOrder, string searchString,string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IList<BookModel> BookList = new List<BookModel>();
            var query = from book in context.BOOKs
                        join publisher in context.Publishers
                        on book.PublisherId equals publisher.Id
                        select new BookModel
                        {
                            Id = book.Id,
                            Title = book.Title,
                            PublisherName = publisher.Name,
                            Auther = book.Auther,
                            Year = book.Year,
                            Price = book.Price
                        };
         
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Title.Contains(searchString) ||
                s.PublisherName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    query = query.OrderByDescending(s => s.Title);
                    break;
                case "Year":
                    query = query.OrderBy(s => s.Year);
                    break;
                default:
                    query = query.OrderBy(s => s.Title);
                    break;
                    
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //BookList = query.ToList();
            return View(query.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Details(int id)
        {
            BookModel model = context.BOOKs.Where(x => x.Id == id).Select(x =>
                                                new BookModel()
                                                {
                                                    Id = x.Id,
                                                    Title = x.Title,
                                                    Auther = x.Auther,
                                                    Price = x.Price,
                                                    Year = x.Year,
                                                    PublisherName = x.Publisher.Name
                                                }).SingleOrDefault();
            return View(model);
        }
        public ActionResult Create()
        {
            var dropdownGenerator = new DropdownGenerator<Publisher>(
                x => x.Name + "-" + x.Year, // Selector for Text
                x => x.Id.ToString() // Selector for Value
            );

            // Generate the SelectListItems
            var publishers = dropdownGenerator.PrepareSelectList(context.Publishers.AsQueryable());

            BookModel model = new BookModel();
            model.Publishers = publishers;
           // PreparePublisher(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(BookModel model)
        {
            try
            {
                BOOK book = new BOOK()
                {
                    Title = model.Title,
                    Auther = model.Auther,
                    Year = model.Year,
                    Price = model.Price,
                    PublisherId = model.PublisherId
                };
                context.BOOKs.InsertOnSubmit(book);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            BookModel model = context.BOOKs.Where(x => x.Id == id).Select(x =>
                                new BookModel()
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Auther = x.Auther,
                                    Price = x.Price,
                                    Year = x.Year,
                                    PublisherId = x.PublisherId
                                }).SingleOrDefault();
            //PreparePublisher(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(BookModel model)
        {
            try
            {
                BOOK book = context.BOOKs.Where(x => x.Id == model.Id).Single<BOOK>();
                book.Title = model.Title;
                book.Auther = model.Auther;
                book.Price = model.Price;
                book.Year = model.Year;
                book.PublisherId = model.PublisherId;
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            BookModel model = context.BOOKs.Where(x => x.Id == id).Select(x =>
                                  new BookModel()
                                  {
                                      Id = x.Id,
                                      Title = x.Title,
                                      Auther = x.Auther,
                                      Price = x.Price,
                                      Year = x.Year,
                                      PublisherName = x.Publisher.Name
                                  }).SingleOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(BookModel model)
        {
            try
            {
                BOOK book = context.BOOKs.Where(x => x.Id == model.Id).Single<BOOK>();
                context.BOOKs.DeleteOnSubmit(book);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}