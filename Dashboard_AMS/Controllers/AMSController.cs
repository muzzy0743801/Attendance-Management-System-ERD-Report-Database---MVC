using Dashboard_AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard_AMS.Controllers
{
    public class AMSController : Controller
    {
        // GET: AMS

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Authentication A)
        {
            if(A.Login() == true)
            {
                Session["EMPID"] = Authentication.empid;
                return RedirectToAction("Index", "Dashboard");
            }
            ModelState.AddModelError("Error", "Invalid Username or Password");
            return View();
        //  return RedirectToAction("Login", "AMS");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout(Authentication A)
        {
            if (A.Logout() == true)
            {
                return RedirectToAction("login", "AMS");
            }
            return View();
        }
    }
}