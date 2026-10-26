using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace AWSFormBuilder.Areas.FormBuilder.Models
{
    //test
    public class SessionClass
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class SessionExpireFilterAttribute : ActionFilterAttribute
        {
            public class SessionExpireFilter : ActionFilterAttribute
            {
                public override void OnActionExecuting(ActionExecutingContext context)
                {
                    var httpContext = context.HttpContext;
                    var request = httpContext.Request;
                    var user = httpContext.User;

                    // ✅ Check authentication (NO Session in Core)
                    if (!user.Identity.IsAuthenticated)
                    {
                        if (IsAjaxRequest(request))
                        {
                            // AJAX request → return special response
                            context.Result = new JsonResult("_Logon_");
                        }
                        else
                        {
                            // Normal request → redirect to login
                            context.Result = new RedirectToActionResult(
                                "LogOn", "Account", null);
                        }

                        return;
                    }

                    base.OnActionExecuting(context);
                }

                private bool IsAjaxRequest(HttpRequest request)
                {
                    return request.Headers["X-Requested-With"] == "XMLHttpRequest";
                }
            }
        }


        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
        public class LocsAuthorizeAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var httpContext = context.HttpContext;
                var request = httpContext.Request;
                var user = httpContext.User;

                // ✅ If NOT logged in
                if (!user.Identity.IsAuthenticated)
                {
                    if (IsAjaxRequest(request))
                    {
                        // AJAX → send special response
                        context.Result = new JsonResult("_Logon_");
                    }
                    else
                    {
                        // Normal request → redirect
                        context.Result = new RedirectToActionResult(
                            "TimeoutRedirect", "Home", null);
                    }

                    return;
                }

                // ✅ If logged in but not authorized
                // (this mimics your 403 logic)
                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
            }

            private bool IsAjaxRequest(HttpRequest request)
            {
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
        public class PermitAccessAttribute : Attribute, IAuthorizationFilter
        {
            public string Roles { get; set; }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;

                // ✅ Not logged in
                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToActionResult("LogOn", "Account", null);
                    return;
                }

                // ✅ If no roles specified → allow
                if (string.IsNullOrEmpty(Roles))
                    return;

                var allowedRoles = Roles.Split(',').Select(r => r.Trim());

                // ✅ Check role from claims
                bool hasAccess = allowedRoles.Any(role => user.IsInRole(role));

                if (!hasAccess)
                {
                    context.Result = new ForbidResult(); // or redirect
                }
            }
        }
    }
}