using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Editor.Controllers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (IsUserAuthenticated(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectResult("/Store/Test");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private bool IsUserAuthenticated(HttpContextBase context)
        {
            return context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated;
        }
    }
}