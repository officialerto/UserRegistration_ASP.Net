using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration_MVC.Models;

namespace UserRegistration_MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            User usermodel = new User();

            return View(usermodel);
        }

        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            using (DbModels db = new DbModels())
            {
                if (db.Users.Any(x => x.Username == userModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exist.";
                    return View("AddOrEdit", userModel);
                }

                db.Users.Add(userModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("AddOrEdit", new User());
        }
    }
}