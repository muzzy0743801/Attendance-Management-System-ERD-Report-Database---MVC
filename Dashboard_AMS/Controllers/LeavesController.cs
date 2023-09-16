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
    public class LeavesController : Controller
    {
        
        [HttpGet]
        public ActionResult LeavesHistory()
        {
            List<Leaves> lst = new Leaves().Show_history();
            
            return View(lst);
        }
        [HttpPost]
        public ActionResult LeavesHistory(Leaves lv)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ApplyLeave()
        {
            ViewBag.leavetype = new Leaves().Show_LeaveType();
            return View();
        }
        [HttpPost]
        public ActionResult ApplyLeave(Leaves lv)
        {
            lv.Apply_leave();
            return RedirectToAction("LeavesHistory","Leaves");
        }
        //public static Leaves Search_book = new Leaves();
        //public static List<Leaves> Sorted_Books;
        //public static bool check = false;

        [HttpGet]
        public ActionResult ShowAll_Leaves()
        {
            List<Leaves> lst = new Leaves().ShowAll_Leaves();
            
            //if (check)
            //{
            //    List<Leaves> lsts = new List<Leaves>() { };
            //    check = false;
            //    return View(lsts);
            //}
            return View(lst);
        }

        public ActionResult sort(string abc)
        {

           // Sorted_Books = new Employees().Sorting(abc);
            return RedirectToAction("EmployeesDetail");
        }




        [HttpPost]
        public ActionResult ShowAll_Leaves(Leaves lv)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ApproveLeave(int leave_ID)
        {
            Leaves ap_leave = new Leaves();
            ap_leave.leave_ID = leave_ID;
            ap_leave.ApproveLeave();
            return RedirectToAction("ShowAll_Leaves");
        }

        [HttpGet]
        public ActionResult RejectLeave(int leave_ID)
        {
            Leaves ap_leave = new Leaves();
            ap_leave.leave_ID = leave_ID;
            ap_leave.RejectLeave();
            return RedirectToAction("ShowAll_Leaves");
        }


        [HttpGet]
        public ActionResult CancelLeave(int leave_ID)
        {
            Leaves ap_leave = new Leaves();
            ap_leave.leave_ID = leave_ID;
            ap_leave.CancelLeave();
            return RedirectToAction("LeavesHistory");
        }

        //[HttpPost]
        //public ActionResult New_Action(int abc)
        //{
        //    check = true;
        //    Leaves b = new Leaves();
        //    b.empID = abc; b.Searchs();
        //    List<Leaves> lst = b.Searchs();
        //    //object a = Session["j"];
        //    return View("ShowAll_Leaves",lst);
        //}
    }
}