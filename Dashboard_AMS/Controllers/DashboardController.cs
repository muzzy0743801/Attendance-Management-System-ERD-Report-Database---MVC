using Dashboard_AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard_AMS.Controllers
{
    /// <summary>
    [Session]
    /// </summary>
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.roleid = Authentication.roleid;
            ViewBag.timein = new Authentication().Check_TimeIn();
            ViewBag.timeout = new Authentication().Check_TimeOut();
            List<Employees> lst = new Employees().RecentEmployees();
            return View(lst);
        }

        [HttpPost]
        public ActionResult Index(Employees e)
        {
            bool result = e.Mark_Attendance();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Timeout(Employees e)
        {
            bool result = e.Mark_Attendance_out();
            return RedirectToAction("Index");
        }

    }
}