using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TimPhongTro.Common;

namespace TimPhongTro.Areas.Admin.Controllers
{
    public class ABaseController : Controller
    {
        // GET: Admin/ABase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}