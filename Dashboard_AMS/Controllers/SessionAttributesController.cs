using Dashboard_AMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard_AMS.Controllers
{
    public class SessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            HttpContext ctx = HttpContext.Current;
            if (Authentication.ischeck == false || session["EMPID"] == null)
            { 
                filterContext.Result = new RedirectResult("~\\AMS\\Login");
                return;
            }
            
            base.OnActionExecuting(filterContext);

        }
    }
}