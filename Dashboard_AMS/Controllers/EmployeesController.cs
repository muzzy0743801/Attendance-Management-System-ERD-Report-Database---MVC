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
    public class EmployeesController : Controller
    {
        static int ID;

        [HttpGet]
        public ActionResult AddEmployeedetails()
        {
            ViewBag.roles = new Employees().ShowAll_Roles();
            ViewBag.jobtitle = new Jobs().ShowAll_Jobs();
            ViewBag.dept = new Department().ShowAll_department();
            ViewBag.branches = new Department().ShowAll_Branches();
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployeedetails(Employees Emp)
        {
            if (Emp.Add_user() == true)
            {
                Emp.AddEmployeeData();
                return RedirectToAction("EmployeesDetail", "Employees");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EmployeesDetail()
        {
            List<Employees> lst = new Employees().ShowAll_Employees();
            if (check)
            {
                List<Employees> lsts = new List<Employees>() { Search_book };
                check = false;
                return View(lsts);
            }
            if (Sorted_Books != null)
            {
                return View(Sorted_Books);
            }

            return View(lst);
        }

        [HttpPost]
        public ActionResult EmployeesDetail(Employees Emp)
        {
            return View();
        }


        public static bool check = false;

        public ActionResult New_Action(int abc)
        {
            check = true;
            Employees b = new Employees();
            b.empID = abc;
            Search_book = b.Search();
            //object a = Session["j"];
            return RedirectToAction("EmployeesDetail");
        }

        public static List<Employees> Sorted_Books;
        [HttpPost]
        public ActionResult sort(string abc)
        {
            
            Sorted_Books = new Employees().Sorting(abc);
            return RedirectToAction("EmployeesDetail");
        }
        public static Employees Search_book = new Employees();


        [HttpGet]
        public ActionResult UpdateEmployee(int empid)
        {
            ID = empid;
            ViewBag.roles = new Employees().ShowAll_Roles();
            ViewBag.jobtitle = new Jobs().ShowAll_Jobs();
            ViewBag.dept = new Department().ShowAll_department();
            ViewBag.branches = new Department().ShowAll_Branches();
            Employees emp = new Employees().Search(empid);
            return View(emp);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employees emp)
        {
            emp.UpdateEmployeeData(ID);
            return RedirectToAction("EmployeesDetail");
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int empid)
        {
            Employees emp = new Employees();
            emp.empID = empid;
            emp.Delete_User();
            return RedirectToAction("EmployeesDetail");
        }
    }
}