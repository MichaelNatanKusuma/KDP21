using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserRegistration2.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error"); // Maps to Views/Shared/Error.cshtml
        }

        // Handles 404 errors
        public ActionResult NotFound()
        {
            return View("NotFound"); // You can create a separate NotFound.cshtml view for 404 errors
        }
    }
}