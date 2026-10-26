using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]

    public class RolesController : Controller
    {
        BLL.Roles _Roles = new BLL.Roles();
        List<Entities.Roles> lstRoles = new List<Entities.Roles>();
        BLL.Chapters _Chapters = new BLL.Chapters();
        BLL.Users _users = new BLL.Users();

        #region Roles

        [Authorize]
        public ActionResult Index(Int64 mid = 0)
        {
            int Total = 0;

            long userId = 0;

            var userIdStr = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdStr))
            {
                long.TryParse(userIdStr, out userId);
            }

            if (Request.Cookies.TryGetValue("UserRole", out string userRoleValue))
            {
                HttpContext.Session.SetString("userrole", userRoleValue);
            }
            long chapterId = 0;

            var chapterValue = HttpContext.Session.GetString("chapterid");

            if (!string.IsNullOrEmpty(chapterValue))
            {
                long.TryParse(chapterValue, out chapterId);
            }

            List<Entities.Chapters> lstChapters = new();
            List<Entities.PageDetails> lstPageDetails = new();
            List<Entities.MenuItems> lstMenuItems = new();
            Entities.UserRoles objuserroles = new();

            int status = 0;
            int _qstatus = 0;
            int CategoryLevel = 0;

            try
            {

                lstChapters = _Chapters.GetChaptersList(ref status);
                objuserroles = _users.GetRoleDetialsById(userId, mid, ref status);
                // ✅ Session (Core way)
                HttpContext.Session.SetString("IsEdit", objuserroles.IsEdit.ToString());
                HttpContext.Session.SetString("IsView", objuserroles.IsView.ToString());
                HttpContext.Session.SetString("IsDelete", objuserroles.IsDelete.ToString());
                HttpContext.Session.SetString("IsExport", objuserroles.IsExport.ToString());
                HttpContext.Session.SetString("IsAdd", objuserroles.IsAdd.ToString());

                // ✅ ViewBag (optional)
                ViewBag.IsEdit = objuserroles.IsEdit;
                ViewBag.IsView = objuserroles.IsView;
                ViewBag.IsDelete = objuserroles.IsDelete;
                ViewBag.IsExport = objuserroles.IsExport;
                ViewBag.IsAdd = objuserroles.IsAdd;
                ViewBag.mid = mid;

            }
            catch
            {
                status = -1;
            }

            // ✅ ViewBag assignments
            ViewBag.ChapterId = chapterId;

            ViewBag.lstChapters = lstChapters;
            ViewBag.lstPageDetails = lstPageDetails;
            ViewBag.lstMenuItems = lstMenuItems;
            ViewBag.Mid = mid;
            return View();

        }

        [Authorize]
        public ActionResult RolesList(string Search = "", string SortColumn = "", string SortOrder = "DESC", int PageNo = 1, int Items = 20, bool IsEdit = false, bool IsView = false, bool IsDelete = false, bool IsExport = false, bool IsAdd = false, Int64 mid = 0)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstRoles = _Roles.GetRolesListByVariable(Search, Sort, PageNo, Items, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstRoles = lstRoles;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.IsEdit = IsEdit;
            ViewBag.IsView = IsView;
            ViewBag.IsDelete = IsDelete;
            ViewBag.IsExport = IsExport;
            ViewBag.IsAdd = IsAdd;
            ViewBag.mid = mid;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddRoles(Entities.Roles objRoles, Int64 mid = 0)
        {
            string str = "";
            bool _bool = true;

            objRoles.UpdatedBy = HttpContext.User.Identity.Name.ToString();
            objRoles.UpdatedTime =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();
            objRoles.IsActive = true;

            try
            {

                Int64 _status = _Roles.InsertRoles(objRoles);
                if (_status == 1)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "Roles", new { mid = mid });
                }
                else if (_status == 2)
                {
                    TempData["messageType"] = "success"; // Assuming "info" for update
                    TempData["message"] = "Changes has been Updated Successfully";
                    return RedirectToAction("Index", "Roles", new { mid = mid });
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data Error";
                    return RedirectToAction("Index", "Roles", new { mid = mid });
                }
            }
            catch (Exception ex)
            {
                TempData["messageType"] = "error";
                TempData["message"] = ex.Message;

            }

            return RedirectToAction("Index", "Roles", new { mid = mid });

        }

        [Authorize]
        public ActionResult EditRoles(Int64 RolesId = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Roles _objRoles = _Roles.GetRolesById(RolesId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objRoles });
                }
                else
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Failed Transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [Authorize]
        public ActionResult ViewRoles(Int64 RolesId = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Roles _objRoles = _Roles.GetRolesById(RolesId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objRoles });
                }
                else
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Failed Transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }





        [Authorize]
        [HttpPost]
        public JsonResult DeleteRoles(Int64 RoleId)
        {
            string str = "";
            try
            {

                Int64 _status = _Roles.DeleteRoles(RoleId);

                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Record Deleted successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed Deleting the Record"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."

                });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult RolesStatus(Int64 RoleId)
        {
            string str = "";
            try
            {

                Int64 _status = _Roles.UpdateRolesStatus(RoleId);

                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Updated Status Successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed updating status"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."

                });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult CheckRoleNameAvailability(Int64 RoleId, string RoleName)
        {
            int status = 0;
            try
            {
                Entities.Roles objRoles = _Roles.RolesGetByRoleName(RoleName, ref status);
                bool data = (objRoles.RoleName == RoleName || objRoles.RoleName == "" ? true : false);
                return Json(new { ok = true, data = data });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed transaction.</div>" });
            }
        }

        public ActionResult RolesGetByRoleId(Int64 RoleId)
        {
            string str = "";
            string message = "";

            try
            {
                int status = 0;
                List<Entities.Roles> lstRolebind = _Roles.RolesGetByRoleId(RoleId, ref status);
                if (status == 1)
                {
                    return Json(new { ok = true, data = lstRolebind });
                }
                else
                {
                    message = "error";
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                message = "error";
                return Json(new { ok = true, data = str });
            }
        }

        #endregion

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
    }
}