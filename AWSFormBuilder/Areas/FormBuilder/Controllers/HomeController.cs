using AWSFormBuilder.Areas.FormBuilder.Models;
using AWSFormBuilder.Areas.FormBuilder.BLL;
using AWSFormBuilder.Areas.FormBuilder.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;


namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]
    public class HomeController : Controller
    {
        BLL.FormBuilder _FormBuilder = new BLL.FormBuilder();
        List<Entities.FormBuilder> objForms = new List<Entities.FormBuilder>();
        BLL.MailTemplates _MailTemplate = new BLL.MailTemplates();
        BLL.Members _Members = new BLL.Members();
        Entities.Enquiries objEnquiries = new Entities.Enquiries();
        Entities.Members objMembers = new Entities.Members();

        List<Entities.MailTemplates> lstMailTemplate = new List<Entities.MailTemplates>();
       
        BLL.AppUsers _AppUsers = new BLL.AppUsers();
        BLL.AppInfo _AppInfo = new BLL.AppInfo();
        BLL.SendMail _sentmail = new BLL.SendMail();

        
        #region LogReport
        [Authorize]
        public ActionResult LogReport()
        {
            int _qstatus = 0;

            objForms = _FormBuilder.GetFormlist(ref _qstatus);

            ViewBag.objForms = objForms;
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult LogReportGetList(string StartDate = "", string EndDate = "", string Search = "", string SortColumn = "", string SortOrder = "", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            List<Entities.LogReport> lstLogReport = new List<Entities.LogReport>();
            try
            {
                lstLogReport = _AppInfo.GetLogReportListByVariable(StartDate, EndDate, Search.Trim(), Sort, PageNo, Items, ref Total);

            }
            catch
            {
                Total = -1;
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstLogReport = lstLogReport;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }
        [Authorize]
        public ActionResult SubLogReport(Int64 LogId = 0)
        {
            ViewBag.LogId = LogId;

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult SubLogReportGetList(Int64 LogId = 0, string StartDate = "", string EndDate = "", string Search = "", string SortColumn = "", string SortOrder = "", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            List<Entities.LogSubReport> lstSUbLogReport = new List<Entities.LogSubReport>();

            try
            {

                lstSUbLogReport = _AppUsers.SubGetLogReportListByVariable(LogId, StartDate, EndDate, Search.Trim(), Sort, PageNo, Items, ref Total);

            }
            catch
            {
                Total = -1;
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstSUbLogReport = lstSUbLogReport;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }





        #endregion

        #region Dashboard
        public class AuthorizeAttribute : ActionFilterAttribute
        {
            BLL.Users _user = new BLL.Users();
            BLL.Roles _Roles = new BLL.Roles();
            private object FormsAuthentication;
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                string userRole = null;
                int status = 0;

                // ✅ Get ClaimsPrincipal — replaces HttpCookie + FormsAuthentication.Decrypt
                var user = filterContext.HttpContext.User;

                if (user?.Identity != null && user.Identity.IsAuthenticated)
                {
                    // ✅ Read role from Claims — replaces authTicket.UserData
                    userRole = user.FindFirst(ClaimTypes.Role)?.Value;

                    // ✅ Read email from Claims — replaces authTicket.Name
                    string emailFromClaim = user.FindFirst(ClaimTypes.Email)?.Value;

                    if (!string.IsNullOrEmpty(emailFromClaim))
                    {
                        // Get user from DB
                        var objuser = _user.GetAdminUsersGetByEmail(
                            emailFromClaim, ref status);

                        // Inside AuthorizeAttribute, where objuser != null:
                        if (objuser != null)
                        {
                            filterContext.HttpContext.Session.SetString("UserName", objuser.UserName ?? "");
                            filterContext.HttpContext.Session.SetString("UserId", objuser.UserId.ToString());
                            filterContext.HttpContext.Session.SetString("UserEmail", emailFromClaim ?? ""); // ✅ ADD THIS
                            filterContext.HttpContext.Session.SetString("chapterid", objuser.ChapterId.ToString());
                            filterContext.HttpContext.Session.SetString("userrole", userRole ?? "");
                        }
                    }
                }

                // ✅ If no role — redirect to LogOn
                //    replaces new UrlHelper(filterContext.RequestContext)
                if (string.IsNullOrEmpty(userRole))
                {
                    filterContext.Result = new RedirectToActionResult(
                        "LogOn", "Account", new { area = "FormBuilder" });
                    return;
                }

                // ✅ Get all allowed roles from DB
                int roleStatus = 0;
                List<Entities.Roles> lstRoles = _Roles.GetRolesList(ref roleStatus);

                // Build flat list of allowed role names
                List<string> allowedRoles = lstRoles
                    .Select(r => r.RoleName.Trim())
                    .ToList();

                // Split user's roles (comma separated) and check
                var userRoles = userRole
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Trim())
                    .ToList();

                bool isAuthorized = userRoles.Any(r => allowedRoles.Contains(r));

                if (!isAuthorized)
                {
                    // ✅ Redirect to Unauthorized — replaces UrlHelper + RedirectResult
                    filterContext.Result = new RedirectToActionResult(
                        "Unauthorized", "Account", new { area = "FormBuilder" });
                    return;
                }

                base.OnActionExecuting(filterContext);
            }
        }



        #endregion


    }
}
