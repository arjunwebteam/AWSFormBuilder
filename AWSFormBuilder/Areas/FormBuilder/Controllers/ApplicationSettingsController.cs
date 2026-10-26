using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;


namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]

    //new
    public class ApplicationSettingsController : Controller
    {
        BLL.AppInfo _appinfo = new BLL.AppInfo();
        BLL.Chapters _Chapters = new BLL.Chapters();

        [Authorize]
        public ActionResult Index()
        {
            int Total = 0;
            List<Entities.Chapters> lstChapters = new List<Entities.Chapters>();
            lstChapters = _Chapters.GetChaptersList(ref Total);
            try
            {
                int status = 0;
                Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status);
                if (status != 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.objAppInfo = objAppInfo;
                ViewBag.lstChapters = lstChapters;
            }
            catch
            {
                TempData["messageType"] = "error";
                TempData["message"] = "An error occurred.";
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [Authorize]
        public ActionResult UpdateAppInfos(Entities.AppInfo objAppInfo, IFormFile LayoutLogo, IFormFile faviconlogo, IFormFile Loginlogo, IFormFile MailLogo)
        {
            try
            {
                // ✅ Use AppInfo.UploadPath instead of Session["UploadPath"]
                int status1 = 0;
                BLL.AppInfo _appinfoHelper = new BLL.AppInfo();
                Entities.AppInfo objAppInfoPath = _appinfoHelper.GetAppInfoDetails(ref status1);
                string uploadPath = objAppInfoPath.UploadPath;

                objAppInfo.UpdatedBy = HttpContext.User.Identity.Name;
                objAppInfo.UpdatedTime =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                string imageurl = "";
                string imageurl1 = "";
                string imageurl2 = "";
                string imageurl3 = "NA";
                string MailLogoFile = "";
                // ✅ Save LayoutLogo
                if (LayoutLogo != null && LayoutLogo.Length > 0)
                {
                    string layoutNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "NormalImages");
                    string layoutThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "ThumbImages");

                    Directory.CreateDirectory(layoutNormalDir);
                    Directory.CreateDirectory(layoutThumbDir);

                    string layoutTempPath = Path.Combine(layoutNormalDir, LayoutLogo.FileName);
                    using (var stream = new FileStream(layoutTempPath, FileMode.Create))
                    {
                        LayoutLogo.CopyTo(stream);
                    }
                    imageurl = LayoutLogo.FileName;
                }

                // ✅ Save faviconlogo
                if (faviconlogo != null && faviconlogo.Length > 0)
                {
                    string faviconNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "NormalImages");
                    string faviconThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "ThumbImages");
                 
                    Directory.CreateDirectory(faviconNormalDir);
                    Directory.CreateDirectory(faviconThumbDir);

                    string faviconTempPath = Path.Combine(faviconNormalDir, faviconlogo.FileName);
                    using (var stream = new FileStream(faviconTempPath, FileMode.Create))
                    {
                        faviconlogo.CopyTo(stream);
                    }
                    imageurl1 = faviconlogo.FileName;
                }

                // ✅ Save Loginlogo
                if (Loginlogo != null && Loginlogo.Length > 0)
                {
                    string loginNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "NormalImages");
                    string loginThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "ThumbImages");

                    Directory.CreateDirectory(loginNormalDir);
                    Directory.CreateDirectory(loginThumbDir);

                    string loginTempPath = Path.Combine(loginNormalDir, Loginlogo.FileName);
                    using (var stream = new FileStream(loginTempPath, FileMode.Create))
                    {
                        Loginlogo.CopyTo(stream);
                    }
                    imageurl2 = Loginlogo.FileName;
                }
                if (MailLogo != null && MailLogo.Length > 0)
                {
                    MailLogoFile = Path.GetFileName(MailLogo.FileName);

                    string MailLogoNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "NormalImages");
                    string MailLogoThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "ThumbImages");

                    Directory.CreateDirectory(MailLogoNormalDir);
                    Directory.CreateDirectory(MailLogoThumbDir);

                    string MailLogoTempPath = Path.Combine(MailLogoNormalDir, MailLogoFile);
                    using (var stream = new FileStream(MailLogoTempPath, FileMode.Create))
                    {
                        MailLogo.CopyTo(stream);
                    }
                    imageurl3 = MailLogoFile;
                }
                // ✅ Update DB
                Int64 status = _appinfo.UpdateAppInfoDetails(objAppInfo, ref imageurl, ref imageurl1, ref imageurl2, ref imageurl3);

                // ✅ Copy to final paths after DB update (imageurl may change from DB)
                if (!string.IsNullOrEmpty(imageurl))
                {
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "NormalImages", imageurl);
                    string thumbPath = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "ThumbImages", imageurl);
                    string tempPath = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "NormalImages", LayoutLogo.FileName);
                    

                    if (System.IO.File.Exists(tempPath) && tempPath != normalPath)
                    {
                        System.IO.File.Copy(tempPath, normalPath, true);
                        System.IO.File.Delete(tempPath);
                    }
                    if (System.IO.File.Exists(normalPath))
                        System.IO.File.Copy(normalPath, thumbPath, true);
                }

                if (!string.IsNullOrEmpty(imageurl1))
                {
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "NormalImages", imageurl1);
                    string thumbPath = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "ThumbImages", imageurl1);
                    string tempPath = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "NormalImages", faviconlogo.FileName);

                  

                    if (System.IO.File.Exists(tempPath) && tempPath != normalPath)
                    {
                        System.IO.File.Copy(tempPath, normalPath, true);
                        System.IO.File.Delete(tempPath);
                    }
                    if (System.IO.File.Exists(normalPath))
                        System.IO.File.Copy(normalPath, thumbPath, true);
                }

                if (!string.IsNullOrEmpty(imageurl2))
                {
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "NormalImages", imageurl2);
                    string thumbPath = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "ThumbImages", imageurl2);
                    string tempPath = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "NormalImages", Loginlogo.FileName);


                    if (System.IO.File.Exists(tempPath) && tempPath != normalPath)
                    {
                        System.IO.File.Copy(tempPath, normalPath, true);
                        System.IO.File.Delete(tempPath);
                    }
                    if (System.IO.File.Exists(normalPath))
                        System.IO.File.Copy(normalPath, thumbPath, true);
                }

                if (status != -1)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Changes has been Updated Successfully";
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data Error";
                }
            }
            catch (Exception ex)
            {
                TempData["messageType"] = "error";
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("Index", "ApplicationSettings");
        }




        [Authorize]
        [HttpPost]
        [Authorize]
        public ActionResult UpdateAppInfo(Entities.AppInfo objAppInfo, IFormFile LayoutLogo, IFormFile faviconlogo, IFormFile Loginlogo,IFormFile MailLogo)
        {
            try
            {
                // ✅ Use AppInfo.UploadPath instead of Session["UploadPath"]
                int status1 = 0;
                BLL.AppInfo _appinfoHelper = new BLL.AppInfo();
                Entities.AppInfo objAppInfoPath = _appinfoHelper.GetAppInfoDetails(ref status1);
                string uploadPath = objAppInfoPath.UploadPath;

                objAppInfo.UpdatedBy = HttpContext.User.Identity.Name;
                objAppInfo.UpdatedTime =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                string imageurl = "NA";
                string imageurl1 = "NA";
                string imageurl2 = "NA";
                string imageurl3 = "NA";
                string layoutFile = "";
                string faviconFile = "";
                string loginFile = "";

                // ✅ Save LayoutLogo
                if (LayoutLogo != null && LayoutLogo.Length > 0)
                {
                    layoutFile = Path.GetFileName(LayoutLogo.FileName);

                    string layoutNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "NormalImages");
                    string layoutThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "ThumbImages");

                    Directory.CreateDirectory(layoutNormalDir);
                    Directory.CreateDirectory(layoutThumbDir);

                    string layoutTempPath = Path.Combine(layoutNormalDir, layoutFile);
                    using (var stream = new FileStream(layoutTempPath, FileMode.Create))
                    {
                        LayoutLogo.CopyTo(stream);
                    }
                    imageurl = layoutFile;
                }

                // ✅ Save faviconlogo
                if (faviconlogo != null && faviconlogo.Length > 0)
                {
                    faviconFile = Path.GetFileName(faviconlogo.FileName);
                    string faviconNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "NormalImages");
                    string faviconThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "ThumbImages");

                    Directory.CreateDirectory(faviconNormalDir);
                    Directory.CreateDirectory(faviconThumbDir);

                    string faviconTempPath = Path.Combine(faviconNormalDir, faviconFile);
                    using (var stream = new FileStream(faviconTempPath, FileMode.Create))
                    {
                        faviconlogo.CopyTo(stream);
                    }
                    imageurl1 = faviconFile;
                }


                if (MailLogo != null && MailLogo.Length > 0)
                {
                    string MailLogoFile = Path.GetFileName(MailLogo.FileName);
                    string MailLogoNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "NormalImages");
                    string MailLogoThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "ThumbImages");
                    string normalPath3 = Path.Combine(MailLogoNormalDir, MailLogoFile);

                    Directory.CreateDirectory(MailLogoNormalDir);
                    Directory.CreateDirectory(MailLogoThumbDir);

                    using (var stream = new FileStream(normalPath3, FileMode.Create))
                    {
                        MailLogo.CopyTo(stream);
                    }
                    imageurl3 = Path.GetFileName(MailLogo.FileName);
                }



                // ✅ Save Loginlogo
                if (Loginlogo != null && Loginlogo.Length > 0)
                {
                    loginFile = Path.GetFileName(Loginlogo.FileName);
                    string loginNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "NormalImages");
                    string loginThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "ThumbImages");
                    Directory.CreateDirectory(loginNormalDir);
                    Directory.CreateDirectory(loginThumbDir);

                    string loginTempPath = Path.Combine(loginNormalDir, loginFile);
                    using (var stream = new FileStream(loginTempPath, FileMode.Create))
                    {
                        Loginlogo.CopyTo(stream);
                    }
                    imageurl2 = loginFile;
                }

                // ✅ Update DB — imageurl values may change after this call
                Int64 status = _appinfo.UpdateAppInfoDetails(objAppInfo, ref imageurl, ref imageurl1, ref imageurl2, ref imageurl3);

                // ✅ Copy LayoutLogo to final path after DB update
                if (!string.IsNullOrEmpty(layoutFile) && imageurl != "NA" && imageurl != "")
                {

                    string tempPath = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "NormalImages", layoutFile);
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "NormalImages", imageurl);
                    string thumbPath = Path.Combine("Areas", "FormBuilder", "Content", "LayoutLogo", "ThumbImages", imageurl);


                    if (System.IO.File.Exists(tempPath) && tempPath != normalPath)
                    {
                        System.IO.File.Copy(tempPath, normalPath, true);
                        System.IO.File.Delete(tempPath);
                    }
                    if (System.IO.File.Exists(normalPath))
                        System.IO.File.Copy(normalPath, thumbPath, true);
                }

                // ✅ Copy faviconlogo to final path after DB update
                if (!string.IsNullOrEmpty(faviconFile) && imageurl1 != "NA" && imageurl1 != "")
                {
                    string tempPath = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "NormalImages", faviconFile);
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "NormalImages", imageurl1);
                    string thumbPath = Path.Combine("Areas", "FormBuilder", "Content", "faviconlogo", "ThumbImages", imageurl1);

                    if (System.IO.File.Exists(tempPath) && tempPath != normalPath)
                    {
                        System.IO.File.Copy(tempPath, normalPath, true);
                        System.IO.File.Delete(tempPath);
                    }
                    if (System.IO.File.Exists(normalPath))
                        System.IO.File.Copy(normalPath, thumbPath, true);
                }

                if (imageurl3 != "NA" && imageurl3 != "")
                {
                    string MailLogoNormalDir = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "NormalImages");
                    string MailLogoThumbDir = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "ThumbImages");

                    string uploadedPath3 = Path.Combine(MailLogoNormalDir, Path.GetFileName(MailLogo.FileName));
                    string finalNormalPath3 = Path.Combine(MailLogoNormalDir, imageurl3);
                    string finalThumbPath3 = Path.Combine(MailLogoThumbDir, imageurl3);

                    if (!string.Equals(uploadedPath3, finalNormalPath3, StringComparison.OrdinalIgnoreCase)
                        && System.IO.File.Exists(uploadedPath3))
                    {
                        System.IO.File.Copy(uploadedPath3, finalNormalPath3, true);
                        System.IO.File.Delete(uploadedPath3);
                    }
                    if (System.IO.File.Exists(finalNormalPath3))
                        System.IO.File.Copy(finalNormalPath3, finalThumbPath3, true);
                }



                // ✅ Copy Loginlogo to final path after DB update
                if (!string.IsNullOrEmpty(loginFile) && imageurl2 != "NA" && imageurl2 != "")
                {
                    string tempPath = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "NormalImages", loginFile);
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "NormalImages", imageurl2);
                    string thumbPath = Path.Combine("Areas", "FormBuilder", "Content", "Loginlogo", "ThumbImages", imageurl2);


                    if (System.IO.File.Exists(tempPath) && tempPath != normalPath)
                    {
                        System.IO.File.Copy(tempPath, normalPath, true);
                        System.IO.File.Delete(tempPath);
                    }
                    if (System.IO.File.Exists(normalPath))
                        System.IO.File.Copy(normalPath, thumbPath, true);
                }

                if (status != -1)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Changes have been updated successfully";
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data error";
                }
            }
            catch (Exception ex)
            {
                TempData["messageType"] = "error";
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("Index", "ApplicationSettings");
        }


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
                        "LogOn", "Account", new { area = "Admin" });
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
