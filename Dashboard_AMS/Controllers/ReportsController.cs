using Dashboard_AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Dashboard_AMS.Controllers
{
    /// <summary>
    [Session]
    /// </summary>
    public class ReportsController : Controller
    {

        [HttpGet]
        public ActionResult EmployeesReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeesReport(Reporting Rpt)
        {
            return View();
        }

        [HttpGet]
        public ActionResult SalaryReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalaryReport(Reporting Rpt)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AttendanceReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AttendanceReport(Reporting Rpt)
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/AMS_Report/Attendance_Report.rdlc");


            ReportDataSource rd = new ReportDataSource("Attendance_DS", Rpt.ShowAllAttendance());
            lr.DataSources.Add(rd);
            byte[] render = lr.Render("PDF");
            return File(render, "application/pdf");
        }

        [HttpGet]
        public ActionResult LeavesReport()
        {
            return View();
        }



        [HttpPost]
        public ActionResult LeavesReport(Reporting rp)
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/AMS_Report/leave.rdlc");


            ReportDataSource rd = new ReportDataSource("DataSet11", rp.ShowAllLeaves());
            lr.DataSources.Add(rd);
            byte[] render = lr.Render("PDF");
            return File(render, "application/pdf");



        }


        
    }
}