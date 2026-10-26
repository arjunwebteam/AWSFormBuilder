using Grpc.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]

    public class MenuItemsController : Controller
    {
        BLL.MenuItems _MenuItems = new BLL.MenuItems();
        BLL.Chapters _Chapters = new BLL.Chapters();
        BLL.PageDetails _PageDetails = new BLL.PageDetails();
        BLL.AdminMenuItems _adminmenu = new BLL.AdminMenuItems();
        BLL.Users _users = new BLL.Users();

        [Authorize]
       public IActionResult Index(long ChapterId = 0, bool IsFooterBar = false, bool IsMenuBar = false, bool IsQuickLinks = false, long mid = 0)
       {
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
        int CategoryLevel = 0;

        try
        {
            lstMenuItems = _MenuItems.GetMenuItemsByLevel(CategoryLevel, ref status);
            lstPageDetails = _PageDetails.GetPageDetailsList(ref status);
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
        ViewBag.IsFooterBar = IsFooterBar;
        ViewBag.IsMenuBar = IsMenuBar;
        ViewBag.IsQuickLinks = IsQuickLinks;

        ViewBag.lstChapters = lstChapters;
        ViewBag.lstPageDetails = lstPageDetails;
        ViewBag.lstMenuItems = lstMenuItems;
        ViewBag.Mid = mid;

        return View();
    }
        
        public IActionResult MenuItemsList(Int64 ChapterId = 0,  bool IsFooterBar = false, bool IsMenuBar = false, bool IsQuickLinks = false, bool IsEdit = false, bool IsView = false, bool IsDelete = false, bool IsExport = false, bool IsAdd = false, Int64 Mid = 0)
        {
            try
            {
                int status = 0;
                List<Entities.MenuItems> lstMenuItems2 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems3 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems4 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems = _MenuItems.GetMenuItemsAll(ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ChapterId, IsFooterBar, IsMenuBar, IsQuickLinks ,ref status);
                if (status == 1)
                {
                    ViewBag.lstMenuItems = lstMenuItems;
                    ViewBag.lstMenuItems2 = lstMenuItems2;
                    ViewBag.lstMenuItems3 = lstMenuItems3;
                    ViewBag.lstMenuItems4 = lstMenuItems4;
                    ViewBag.total = lstMenuItems.Count;
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.ChapterId = ChapterId;
            ViewBag.IsEdit = IsEdit;
            ViewBag.IsView = IsView;
            ViewBag.IsDelete = IsDelete;
            ViewBag.IsExport = IsExport;
            ViewBag.IsAdd = IsAdd;
            ViewBag.Mid = Mid;
            ViewBag.IsFooterBar = IsFooterBar;
            ViewBag.IsMenuBar = IsMenuBar;
            ViewBag.IsQuickLinks = IsQuickLinks;
            return View();
        }

        [HttpPost]
        public JsonResult MenuItemsStatus(Int64 MenuItemId)
        {
           

            try
            {
                Int64 _status = _MenuItems.UpdateMenuItemsStatus(MenuItemId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Status Updated Successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed updating user status"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = ex.Message
                });
            }
        }

        public ActionResult CreateMenuItems(Int64 ChapterId = 0, bool IsFooterBar = false, bool IsMenuBar = false, bool IsQuickLinks = false, Int64 mid = 0)
        {
            try
            {
                List<Entities.MenuItems> lstMenuItems2 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems3 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems4 = new List<Entities.MenuItems>();
                int status = 0;
                List<Entities.Chapters> lstChapters = _Chapters.GetChaptersList(ref status);
                List<Entities.MenuItems> lstMenuItems = _MenuItems.GetMenuItemsDD(ChapterId, IsFooterBar, IsMenuBar, IsQuickLinks, ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ref status);
                if (status == 1)
                {
                
                    ViewBag.lstChapters = lstChapters; 
                    ViewBag.lstMenuItems = lstMenuItems;
                    ViewBag.lstMenuItems2 = lstMenuItems2;
                    ViewBag.lstMenuItems3 = lstMenuItems3;
                    ViewBag.lstMenuItems4 = lstMenuItems4;
                 
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.ChapterId = ChapterId;
            ViewBag.mid = mid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMenuItems(Entities.MenuItems objMenuItems, Int64 mid = 0)
        {
            try
            { 
                Int64 _status = 0;
                objMenuItems.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objMenuItems.InsertedBy = HttpContext.User.Identity.Name.ToString();
                objMenuItems.UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                objMenuItems.InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                objMenuItems.IsActive = true;
                _status = _MenuItems.InsertMenuItems(objMenuItems);
                if (_status == 1)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Record Inserted Successfully";
                }
                else if (_status == 2)
                {
                    TempData["messageType"] = "success"; // Assuming "info" for update
                    TempData["message"] = "Changes has been Updated Successfully";
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data Error";
                }
            }
            catch (Exception EX)
            {
                TempData["messageType"] = "error";
                TempData["message"] = EX.Message;
            }
            return RedirectToAction("Index", "MenuItems", new { mid = mid });
        }

        public ActionResult EditMenuItems(Int64 MenuItemId, bool IsFooterBar = false, bool IsMenuBar = false, bool IsQuickLinks = false, Int64 mid = 0,Int64 ChapterId=0)
        {
            try
            {
                int _status = 0;
                int _list = 0;  
                Entities.MenuItems objMenuItems = _MenuItems.GetMenuItemsById(MenuItemId, ref _status);
                List<Entities.MenuItems> lstMenuItems2 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems3 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems4 = new List<Entities.MenuItems>();
                List<Entities.Chapters> lstChapters = _Chapters.GetChaptersList(ref _status);
                List<Entities.MenuItems> lstMenuItems = _MenuItems.GetMenuItemsDD(objMenuItems.ChapterId, IsFooterBar, IsMenuBar, IsQuickLinks, ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ref _list);
                if (_list == 1)
                {
                    ViewBag.lstMenuItems = lstMenuItems;
                    ViewBag.lstMenuItems2 = lstMenuItems2;
                    ViewBag.lstMenuItems3 = lstMenuItems3;
                    ViewBag.lstMenuItems4 = lstMenuItems4;
                }
                if (_status == 1)
                {
                    ViewBag.lstChapters = lstChapters;
                    ViewBag.objMenuItems = objMenuItems;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "MenuItems", new { mid = mid });
                }
                ViewBag.mid = mid;
                ViewBag.ChapterId = ChapterId;
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "MenuItems", new { mid = mid });
            }
        }

        [HttpPost]
        //public ActionResult EditMenuItems(Entities.MenuItems objMenuItems, Int64 mid = 0)
        //{
        //    try
        //    {
        //        Int64 _status = 0;
        //        objMenuItems.IsActive = Convert.ToBoolean(Microsoft.Extensions.Configuration.ConfigurationManager.AppSettings["masterstatus"]);

        //        _status = _MenuItems.InsertMenuItems(objMenuItems);
        //        if (_status == 1)
        //        {
        //            TempData["messageType"] = "success";
        //            TempData["message"] = "Record Inserted Successfully";
        //        }
        //        else if (_status == 2)
        //        {
        //            TempData["messageType"] = "warning"; // Assuming "info" for update
        //            TempData["message"] = "Changes has been Updated Successfully";
        //        }
        //        else
        //        {
        //            TempData["messageType"] = "error";
        //            TempData["message"] = "Data Error";
        //        }
        //    }
        //    catch (Exception EX)
        //    {
        //        TempData["messageType"] = "error";
        //        TempData["message"] = EX.Message;
        //    }
        //    return RedirectToAction("Index", "MenuItems", new { mid = mid });
        //}

        public ActionResult ViewMenuItems(Int64 MenuItemId, Int64 mid = 0)
        {
            try
            {
                int _status = 0; 
                List<Entities.Chapters> lstChapters = _Chapters.GetChaptersList(ref _status);
                Entities.MenuItems objMenuItems = _MenuItems.GetMenuItemsById(MenuItemId, ref _status);
                if (_status == 1)
                {
                    ViewBag.lstChapters = lstChapters;
                    ViewBag.objMenuItems = objMenuItems;
                    ViewBag.mid = mid;
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data Error";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "MenuItems", new { mid = mid });
            }
           
        }

        [HttpPost]
        public JsonResult MenuItemsDelete(Int64 MenuItemId)
        {
           

            try
            {
                Int64 _status = _MenuItems.DeleteMenuItems(MenuItemId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Record Deleted Successfully"
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
        public JsonResult MenuItemsOrderNo(int Position, Int64 MenuItemId)
        {
            string str = "";
            try
            {
                Int64 _status = _MenuItems.UpdateMenuItemsOrderNo(Position, MenuItemId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Updated Order No Successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed Updating Order No"
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."
                });
            }
        }

        public ActionResult MenuItemsByInstitute(Int64 ChapterId = 0 , bool IsFooterBar = false, bool IsMenuBar = false, bool IsQuickLinks = false)
        {
            string str = "";
            string message = "";

            try
            {
                List<Entities.MenuItems> lstMenuItems2 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems3 = new List<Entities.MenuItems>();
                List<Entities.MenuItems> lstMenuItems4 = new List<Entities.MenuItems>();
                int status = 0; 
                List<Entities.MenuItems> lstMenuItems = _MenuItems.GetMenuItemsDD(ChapterId, IsFooterBar, IsMenuBar, IsQuickLinks, ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ref status);
                if (status == 1)
                {  
                    return Json(new { ok = true, data = lstMenuItems, data2=lstMenuItems2, data3 = lstMenuItems3, data4 = lstMenuItems4 });
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

        //public void logreport(string error)
        //{

        //    string pageName = Path.GetFileName(Request.Path);
        //    string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        //    string filepath = Server.MapPath("~/Content/logfiles/" + filename);
        //    if (System.IO.File.Exists(filepath))
        //    {
        //        using (StreamWriter stwriter = new StreamWriter(filepath, true))
        //        {
        //            stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
        //            stwriter.WriteLine("Page :" + pageName);
        //            stwriter.WriteLine(error);
        //            stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
        //        }
        //    }
        //    else
        //    {
        //        StreamWriter stwriter = System.IO.File.CreateText(filepath);
        //        stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
        //        stwriter.WriteLine("Page :" + pageName);
        //        stwriter.WriteLine(error);
        //        stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
        //        stwriter.Close();
        //    }
        //}
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

        //public class AuthorizeAttribute : ActionFilterAttribute
        //{
        //    private readonly BLL.Users _user;
        //    private readonly BLL.Roles _roles;
        //    private readonly IDataProtector _protector;

        //    public AuthorizeAttribute(
        //        BLL.Users user,
        //        BLL.Roles roles,
        //        IDataProtectionProvider provider)
        //    {
        //        _user = user;
        //        _roles = roles;
        //        _protector = provider.CreateProtector("UserCookieProtector");
        //    }

        //    public void OnAuthorization(AuthorizationFilterContext context)
        //    {
        //        string userRole = null;
        //        string emailFromTicket = null;
        //        int status = 0;

        //        var authCookie = context.HttpContext.Request.Cookies["UserCookie"];

        //        if (!string.IsNullOrEmpty(authCookie))
        //        {
        //            try
        //            {
        //                // Decrypt cookie
        //                string decryptedValue = _protector.Unprotect(authCookie);

        //                // Expected format: "email|role"
        //                var parts = decryptedValue.Split('|');

        //                emailFromTicket = parts[0];
        //                userRole = parts.Length > 1 ? parts[1] : null;

        //                var objuser = _user.GetAdminUsersGetByEmail(emailFromTicket, ref status);

        //                if (objuser != null)
        //                {
        //                    context.HttpContext.Response.Cookies.Append("UserName", objuser.UserName);
        //                    context.HttpContext.Response.Cookies.Append("UserId", objuser.UserId.ToString());
        //                    context.HttpContext.Response.Cookies.Append("chapterid", objuser.ChapterId.ToString());
        //                    context.HttpContext.Response.Cookies.Append("UserRole", userRole ?? "");
        //                }
        //            }
        //            catch
        //            {
        //                // Invalid cookie / tampered
        //                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
        //                return;
        //            }
        //        }

        //        // No role → not logged in
        //        if (string.IsNullOrEmpty(userRole))
        //        {
        //            context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
        //            return;
        //        }

        //        // Get allowed roles
        //        int Status = 0;
        //        var lstRoles = _roles.GetRolesList(ref Status);

        //        var roles = lstRoles.Select(r => r.RoleName.Trim()).ToList();

        //        var userRoles = userRole.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                                .Select(r => r.Trim());

        //        bool isAuthorized = userRoles.Any(role => roles.Contains(role));

        //        if (!isAuthorized)
        //        {
        //            context.Result = new RedirectToActionResult("Unauthorized", "Account", new { area = "Admin" });
        //        }
        //    }
        //}
    }
}
