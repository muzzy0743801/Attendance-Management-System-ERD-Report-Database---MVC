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
    public class JobsController : Controller
    {

        [HttpGet]
        public ActionResult AddNewJob()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewJob(Jobs jb)
        {
            bool result = jb.Add_Job();
            if (result == true)
            {
                return View();
            }
            else
            {
                return View();
            }
        }
        public static bool check = false;
        public static List<Jobs> Sorted_Books;
        public static Jobs Search_book = new Jobs();

        [HttpGet]
        public ActionResult JobDetails()
        {
            List<Jobs> lst = new Jobs().ShowAll_Jobs();
            if (check)
            {
                List<Jobs> lsts = new List<Jobs>() { Search_book };
                check = false;
                return View(lsts);
            }
            if (Sorted_Books != null)
            {
                return View(Sorted_Books);
            }

            return View(lst);
           
        }

        public ActionResult New_Action(int abc)
        {
            check = true;
            Jobs b = new Jobs();
            b.job_id = abc;
            Search_book = b.SearchJob();
            //object a = Session["j"];
            return RedirectToAction("JobDetails");
        }

        
        [HttpPost]
        public ActionResult sort(string abc)
        {

            Sorted_Books = new Jobs().Sort(abc);
            return RedirectToAction("JobDetails");
        }



        [HttpPost]
        public ActionResult JobDetails(Jobs jb)
        {
            return View();
        }


        [HttpGet]
        public ActionResult UpdateJob(int job_id)
        {
            Jobs job = new Jobs();
            job.job_id = job_id;
            Jobs up_job = job.SearchJob();
            return View(up_job);
        }

        [HttpPost]
        public ActionResult UpdateJob(Jobs jobs)
        {
            Jobs up_job = jobs;
            up_job.Update_Job();
            return RedirectToAction("JobDetails");
        }

    }
}