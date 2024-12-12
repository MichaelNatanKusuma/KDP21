using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using UserRegistration2.Models;

namespace UserRegistration2.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private IUserRepository _repository;

        public UserController() : this(new UserRepository()){

        }
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var users = _repository.GetUsers();
            return View(users);
        }


        public ActionResult Details(int id)
        {
            try
            {
                UserModel model = _repository.GetUserByID(id);
                return View(model);
            }
            catch(Exception)
            {
                //return View("Error");
                throw new Exception("Test error");
            }
           
           
        }
        public ActionResult Edit(int id)
        {
            UserModel model = _repository.GetUserByID(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateUser(user);
                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.");
            }
            return View(user);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.";
            }
            UserModel user = _repository.GetUserByID(id);
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                UserModel  user = _repository.GetUserByID(id);
                _repository.deleteUser(id);
            }
            catch(DataException)
            {
                return RedirectToAction("Delete",
                    new System.Web.Routing.RouteValueDictionary
                    {
                        {"id", id },
                        {"saveChangesError", true }
                    });

            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertUser(user);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again," +
                    "and if the problem persists see your system administrator");

            }
            return View(user);
        }
    }
}