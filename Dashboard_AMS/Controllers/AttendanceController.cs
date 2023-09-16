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
    public class AttendanceController : Controller
    {
        


        [HttpGet]
        public ActionResult AttendanceHistory()
        {
            List<Attendance> lst = new Attendance().Show_Attendance();
            
            return View(lst);
        }

        [HttpPost]
        public ActionResult AttendanceHistory(Attendance Atd)
        {
            return View();
        }
        
    }
}