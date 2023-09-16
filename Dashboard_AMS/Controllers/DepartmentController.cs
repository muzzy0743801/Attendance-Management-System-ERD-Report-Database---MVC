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
    public class DepartmentController : Controller
    {
        [HttpGet]
        public ActionResult AddNewDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewDepartment(Department dp)
        {
            bool result = dp.Add_department();
            if(result == true)
            { return View(); }
            else
            { return View(); }
            
        }
        public static bool check = false;
        public static List<Department> Sorted_Books;
        public static Department Search_book = new Department();
        public static bool checks = false;
        public static List<Department> Sorted_Bookss;
        public static Department Search_books = new Department();


        [HttpGet]
        public ActionResult DepartmentsList()
        {
            List<Department> lst = new Department().ShowAll_department();
            if (checks)
            {
                List<Department> lsts = new List<Department>() { Search_books };
                checks = false;
                return View(lsts);
            }
            if (Sorted_Bookss != null)
            {
                return View(Sorted_Bookss);
            }            

            return View(lst);
        }
        [HttpPost]
        public ActionResult DepartmentList(Department dp)
        {
            return View();
        }
        public ActionResult New_Actions(int abc)
        {
            checks = true;
            Department b = new Department();
            b.dept_id = abc;
            Search_books = b.SearchDept();
            return RedirectToAction("DepartmentsList");
        }
        public ActionResult New_Action(int abc)
        {
            check = true;
            Department b = new Department();
            b.Branch_id = abc;
            Search_book = b.Search();
            //object a = Session["j"];
            return RedirectToAction("BranchList");
        }


        [HttpGet]
        public ActionResult AddNewBranch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewBranch(Department dp)
        {
            bool result = dp.Add_Branch();
            if (result == true)
            { return View(); }
            else
            { return View(); }
        }

        [HttpGet]
        public ActionResult BranchList()
        {
            List<Department> lst = new Department().ShowAll_Branches();
            if (check)
            {
                List<Department> lsts = new List<Department>() { Search_book };
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
        public ActionResult BranchList(Department dp)
        {
            return View();
        }

        [HttpPost]
        public ActionResult sort(string abc)
        {

            Sorted_Books = new Department().Sorting(abc);
            return RedirectToAction("BranchList");
        }

        [HttpPost]
        public ActionResult sorting(string abc)
        {

            Sorted_Bookss = new Department().Sortingdept(abc);
            return RedirectToAction("DepartmentsList");
        }

        [HttpGet]
        public ActionResult UpdateDepartment(int dept_id)
        {
            Department dpt = new Department();
            dpt.dept_id = dept_id;
            Department up_dept = dpt.SearchDept();
            return View(up_dept);
        }

        [HttpPost]
        public ActionResult UpdateDepartment(Department dept)
        {
            Department up_dept = dept;
            up_dept.Update_Dept();
            return RedirectToAction("DepartmentsList");
        }


        [HttpGet]
        public ActionResult UpdateBranch(int Branch_id)
        {
            Department dpt = new Department();
            dpt.Branch_id = Branch_id;
            Department up_branch = dpt.Search();
            return View(up_branch);
        }

        [HttpPost]
        public ActionResult UpdateBranch(Department branch)
        {
            Department up_branch = branch;
            up_branch.Update_Branch();
            return RedirectToAction("BranchList");
        }

    }
}