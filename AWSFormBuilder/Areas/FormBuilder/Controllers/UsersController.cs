using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    //[Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Volunteers,ChapterAdmin,SiteAdmin,Administrator,DeveloperAdmin,")]
    [Area("FormBuilder")]
    //test
    public class UsersController : Controller
    {
        //This is Seema
        BLL.Users _user = new BLL.Users();
        Entities.Roles objRoles = new Entities.Roles();
        List<Entities.Roles> lstRoles = new List<Entities.Roles>();
        List<Entities.Roles> userRoles = new List<Entities.Roles>();
        BLL.Chapters _Chapters = new BLL.Chapters();
        BLL.Members _Members = new BLL.Members();
        BLL.SendMail _sendmail = new BLL.SendMail();
        BLL.AppInfo _appinfo = new BLL.AppInfo();
        BLL.AdminMenuItems _AdminMenuItems = new BLL.AdminMenuItems();

        [Authorize]
        public ActionResult Index(string RoleName = "")
        {
            List<Entities.Chapters> lstChapters = new List<Entities.Chapters>();
            int status = 0;
            try
            {
                lstChapters = _Chapters.GetChaptersList(ref status);
                Int32 _qstatus = 0;
                List<Entities.Roles> lstRoles = _AdminMenuItems.GetRolesAssignMenu(ref _qstatus);
                ViewBag.lstRoles = lstRoles;
            }
            catch
            {
                status = -1;
            }
            ViewBag.lstChapters = lstChapters;
            ViewBag.Title = "Admin Panel User List";
            ViewBag.RoleName = RoleName;
            return View();
        }

        [Authorize]
        public ActionResult UserList(string Search = "", Int64 UserId = 0, string RoleName = "", string RoleIds = "", string SortColumn = "", string SortOrder = "", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            List<Entities.Users> lstuser = new List<Entities.Users>();
            try
            {
                lstuser = _user.GetUserListByVariable(RoleName, UserId, RoleIds, Search, Sort, PageNo, Items, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstuser = lstuser;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateUser(Entities.Users objUsers)
        {
            try
            {
                Entities.AppInfo objappinfo = new Entities.AppInfo();
                int status = 0;
                Guid guid = AWSFormBuilder.Areas.FormBuilder.BLL.Common.generateGUID();
                objUsers.InsertedBy = HttpContext.User.Identity.Name.ToString();
                //objUsers.InsertedTime = DateTime.UtcNow;
                objUsers.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                //objUsers.UpdatedTime = DateTime.UtcNow;
                objUsers.IsApproved = false;
                objUsers.IsLockedOut = false;
                objUsers.IsActivated = false;
                objUsers.RegistrationGUID = guid;
                objUsers.DateActivated = DateTime.MinValue;
                objUsers.LastLoginDate = DateTime.MinValue;
                objUsers.LastPasswordChangedDate = DateTime.MinValue;

                objUsers.UpdatedTime = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();
                objUsers.InsertedTime = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                objappinfo = _appinfo.GetAppInfoDetails(ref status);

                if (objUsers.Designation == "ChapterAdmin")
                {
                    objUsers.RoleName = "ChapterAdmin";
                }
                else
                {
                    objUsers.RoleName = "SiteAdmin";
                }

                Int64 _status = _user.InsertUserProfile(objUsers);
                if (_status == 1)
                {
                    string activationUrl = objappinfo.BaseUrl + "FormBuilder/Account/UserValidate?name=" + objUsers.Email + "&id=" + guid.ToString();
                    string createdContent =
                        "<p style=\"margin:0 0 12px 0;\">Dear <strong>" + objUsers.UserName + "</strong>,</p>" +
                        "<p style=\"margin:0 0 12px 0;\">Your account has been created. Please click the button below to activate your account.</p>" +
                        BuildEmailButton(activationUrl, "Activate Account");
                    string createdBody = BuildEmailTemplate(objappinfo.SiteName, "Account Created", createdContent);
                    _sendmail.SendMailSendinbrevo(objUsers.Email, "Account Created - Admin Panel Admin Team", createdBody);

                    //TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Created user account with username " + objUsers.UserName + ".</div>";

                    TempData["messageType"] = "success";
                    TempData["message"] = "Created user account with username.";
                    return RedirectToAction("Index", "Users", new { RoleName = "Admin" });
                }
                else
                {
                    //TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading image.</div>";
                    TempData["messageType"] = "warning";
                    TempData["message"] = "Failed uploading image";

                    return RedirectToAction("Index", "Users", new { RoleName = "Admin" });
                }
            }
            catch (Exception EX)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";

                TempData["messageType"] = "error";
                TempData["message"] = EX.Message;

                return RedirectToAction("Index", "Users", new { RoleName = "Admin" });
            }
        }
        private string BuildEmailTemplate(string siteName, string title, string contentHtml)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
<meta charset=""UTF-8"">
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body style=""margin:0;padding:0;background-color:#f4f6f9;font-family:'Segoe UI',Arial,Helvetica,sans-serif;"">
  <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color:#f4f6f9;padding:30px 12px;"">
    <tr>
      <td align=""center"">
        <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""max-width:560px;background-color:#ffffff;border-radius:10px;overflow:hidden;box-shadow:0 2px 8px rgba(0,0,0,0.08);"">
          <tr>
            <td align=""center"" style=""background-color:#1e3a8a;padding:26px 20px;"">
              <h1 style=""margin:0;font-size:22px;line-height:28px;color:#ffffff;font-weight:600;"">{siteName}</h1>
            </td>
          </tr>
          <tr>
            <td style=""padding:32px 30px 10px 30px;color:#333333;font-size:15px;line-height:24px;"">
              <h2 style=""margin:0 0 18px 0;font-size:20px;line-height:26px;color:#1e3a8a;font-weight:600;"">{title}</h2>
              {contentHtml}
            </td>
          </tr>
          <tr>
            <td style=""padding:20px 30px 30px 30px;color:#333333;font-size:15px;line-height:24px;"">
              Thank You,<br /><strong>Admin Team</strong>
            </td>
          </tr>
          <tr>
            <td align=""center"" style=""background-color:#f9fafb;padding:16px 20px;border-top:1px solid #e5e7eb;color:#9ca3af;font-size:12px;line-height:18px;"">
              This is an automated email, please do not reply.<br />&copy; {DateTime.Now.Year} {siteName}. All rights reserved.
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
        }

        private string BuildEmailButton(string url, string text)
        {
            return $@"
<table role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" align=""center"" style=""margin:26px auto;"">
  <tr>
    <td align=""center"" bgcolor=""#2563eb"" style=""border-radius:6px;"">
      <a href=""{url}"" target=""_blank"" style=""display:inline-block;padding:14px 36px;font-size:16px;font-weight:600;color:#ffffff;text-decoration:none;border-radius:6px;background-color:#2563eb;"">{text}</a>
    </td>
  </tr>
</table>";
        }

        [HttpPost]
        //public ActionResult CreateUser(Entities.Users objUsers)
        //{
        //    try
        //    {
        //        Entities.AppInfo objappinfo = new Entities.AppInfo();
        //        int status = 0;
        //        Guid guid =AWSFormBuilder.Areas.FormBuilder.BLL.Common.generateGUID();
        //        objUsers.InsertedBy = HttpContext.User.Identity.Name.ToString();
        //        //objUsers.InsertedTime = DateTime.UtcNow;
        //        objUsers.UpdatedBy = HttpContext.User.Identity.Name.ToString();
        //        //objUsers.UpdatedTime = DateTime.UtcNow;
        //        objUsers.IsApproved = false;
        //        objUsers.IsLockedOut = false;
        //        objUsers.IsActivated = false;
        //        objUsers.RegistrationGUID = guid;
        //        objUsers.DateActivated = DateTime.MinValue;
        //        objUsers.LastLoginDate = DateTime.MinValue;
        //        objUsers.LastPasswordChangedDate = DateTime.MinValue;

        //        objUsers.UpdatedTime =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();
        //        objUsers.InsertedTime =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();


        //        objappinfo = _appinfo.GetAppInfoDetails(ref status);

        //        if (objUsers.Designation == "ChapterAdmin")
        //        {
        //           objUsers.RoleName = "ChapterAdmin";
        //        }
        //        else
        //        {
        //           objUsers.RoleName = "SiteAdmin";
        //        }

        //        Int64 _status = _user.InsertUserProfile(objUsers);
        //        if (_status == 1)
        //        {
        //            StringBuilder body = new StringBuilder();
        //            body.Append("<p>Dear " + objUsers.UserName + ", <br /><br />Your account has been created, please find the activation link <a href=\"" + objappinfo.BaseUrl + "Admin/Account/UserValidate?name=" + objUsers.Email + "&id=" + guid.ToString() + "\">here</a>. <br />");
        //            body.Append("Thank You,<br />Admin</p>");
        //            _sendmail.SendMailSendinbrevo(objUsers.Email, "Account Created - Admin Panel Admin Team", body.ToString());


        //            //TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Created user account with username " + objUsers.UserName + ".</div>";



        //            TempData["messageType"] = "success";
        //            TempData["message"] = "Created user account with username.";
        //            return RedirectToAction("Index", "Users", new { RoleName = "Admin" });
        //        }
        //        else
        //        {
        //            //TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading image.</div>";
        //            TempData["messageType"] = "warning";
        //            TempData["message"] = "Failed uploading image";



        //            return RedirectToAction("Index", "Users", new { RoleName = "Admin" });
        //        }
        //    }
        //    catch (Exception EX)
        //    {
        //        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";

        //        TempData["messageType"] = "error";
        //        TempData["message"] = EX.Message;

        //        return RedirectToAction("Index", "Users", new { RoleName = "Admin" });
        //    }
        //}


        [HttpPost]
        [Authorize]
        public ActionResult EditUser(Int64 UserId)
        {
            string str = "";
            try
            {
                Int32 _qstatus = 0;
                Entities.Users _objuser = _user.GetUserDetailsById(UserId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objuser, data1 = _objuser.lstChapterUsers });
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


        [HttpPost]
        [Authorize]
        public ActionResult UpdateUser(Entities.Users _objuser, string Type = "")
        {
            try
            {
                int status = 0;
                AWSFormBuilder.Entities.Users _objEuser = _user.GetUserByEmail(_objuser.Email, ref status);
                if (_objEuser.UserId == _objuser.UserId || _objEuser.UserId == 0)
                {
                    _objuser.UpdatedTime = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();
                    _objuser.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                    Int64 _qstatus = _user.UpdateUser(_objuser);



                    TempData["messageType"] = "success"; // Assuming "info" for update
                    TempData["message"] = "Changes has been Updated Successfully";




                    //TempData["message"] = (_qstatus == 1 ? "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>" : "<div class=\"alert alert-danger alert-dismissable\">Failed editing profile.</div>");
                }
                else
                {
                    //TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">We have a account with email.</div>";

                    TempData["messageType"] = "success";
                    TempData["message"] = "We have a account with email.";


                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";

                TempData["messageType"] = "warning";
                TempData["message"] = "Failed transaction..";

            }
            if (Type == "Profile")
            {
                return RedirectToAction("Profile", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(Entities.ChangePasswordModel model)
        {
            try
            {
                if (model.UserId != 0)
                {
                    string newpass = AWSFormBuilder.Areas.FormBuilder.BLL.Password.ComputeHash(model.NewPassword, "SHA512", null);
                    Int64 _pstatus = _user.ChangePassword(HttpContext.User.Identity.Name.ToString(), newpass);
                    if (_pstatus == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Password Changed Successfully.<div>";
                    }
                    else
                    {
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">New Password is Invalid.</div>";
                    }
                }
                return RedirectToAction("EditUser", "Users", new { UserId = model.UserId });
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("EditUser", "Users", new { UserId = model.UserId });
            }
        }


        [HttpPost]
        [Authorize]
        public JsonResult UserStatus(Int64 UserId)
        {
            string str = "";
            try
            {
                Int64 _status = _user.UpdateUserStatus(UserId);
                if (_status == 1)
                {
                    //str = "<div class=\"alert alert-success alert-dismissable\">Updated Status Successfully</div>";
                    //return Json(new { ok = true, data = str });


                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Updated Status Successfully"
                    });


                }
                else
                {
                    //str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating user status</div>";
                    //return Json(new { ok = false, data = str });


                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed updating user status"
                    });


                }
            }
            catch
            {
                //str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                //return Json(new { ok = true, data = str });

                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."
                });




            }
        }

        // ============================================================
        // ✅ ADDED — IMPERSONATE USER
        // Lets a SuperAdmin/DeveloperAdmin temporarily sign in as another
        // (lower-privileged) user to view/act on the app exactly as that
        // user would, for support/troubleshooting. The admin's own identity
        // is preserved in an "ImpersonatedByAdminEmail" claim on the new
        // session so StopImpersonating() can restore it later.
        // ============================================================
        [Authorize]
        public async Task<IActionResult> ImpersonateUser(Int64 UserId)
        {
            // Only these roles may impersonate. Same "contains" style check
            // used elsewhere in this codebase (RoleName can be a comma
            // separated list of roles on one account).
            string currentRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            bool canImpersonate = currentRole.Contains("SuperAdmin") || currentRole.Contains("DeveloperAdmin");

            if (!canImpersonate)
            {
                return RedirectToAction("Unauthorized", "Account", new { area = "FormBuilder" });
            }

            int status = 0;
            var targetUser = _user.GetUserDetailsById(UserId, ref status);

            if (targetUser == null || status != 1)
            {
                TempData["messageType"] = "warning";
                TempData["message"] = "User not found.";
                return RedirectToAction("Index", "Users");
            }

            // Safety rail: never allow impersonating another admin account —
            // impersonation is for viewing the app as an ordinary user.
            string targetRole = targetUser.RoleName ?? "";
            if (targetRole.Contains("SuperAdmin") || targetRole.Contains("DeveloperAdmin"))
            {
                TempData["messageType"] = "warning";
                TempData["message"] = "You cannot impersonate another Admin account.";
                return RedirectToAction("Index", "Users");
            }

            // Don't let an already-impersonating admin "stack" impersonation —
            // require returning to their own session first.
            string alreadyImpersonating = HttpContext.User.FindFirst("ImpersonatedByAdminEmail")?.Value;
            if (!string.IsNullOrEmpty(alreadyImpersonating))
            {
                TempData["messageType"] = "warning";
                TempData["message"] = "Please return to your Admin session before impersonating another user.";
                return RedirectToAction("Index", "Users");
            }

            string originalAdminId = HttpContext.User.FindFirst("UserId")?.Value ?? "";
            string originalAdminEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            string originalAdminName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, targetUser.UserName),
                new Claim(ClaimTypes.Email, targetUser.Email),
                new Claim("UserId", targetUser.UserId.ToString()),
                new Claim("ChapterId", targetUser.ChapterId.ToString()),
                new Claim(ClaimTypes.Role, targetUser.RoleName),

                // Impersonation trail — read by StopImpersonating() and by
                // the "Viewing as" banner partial.
                new Claim("ImpersonatedByAdminId", originalAdminId),
                new Claim("ImpersonatedByAdminEmail", originalAdminEmail),
                new Claim("ImpersonatedByAdminName", originalAdminName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });

            return RedirectToAction("Index", "Form", new { area = "FormBuilder", mid = 74 });
        }

        // ============================================================
        // ✅ ADDED — STOP IMPERSONATING
        // Reads the stashed admin identity off the current (impersonated)
        // session's claims and signs back in as that admin, dropping the
        // impersonation claims entirely.
        // ============================================================
        [Authorize]
        public async Task<IActionResult> StopImpersonating()
        {
            string originalAdminEmail = HttpContext.User.FindFirst("ImpersonatedByAdminEmail")?.Value;

            if (string.IsNullOrEmpty(originalAdminEmail))
            {
                // Not currently impersonating anyone — nothing to return to.
                return RedirectToAction("Index", "Form", new { area = "FormBuilder", mid = 74 });
            }

            int status = 0;
            var adminUser = _user.GetAdminUsersGetByEmail(originalAdminEmail, ref status);

            if (adminUser == null)
            {
                // Original admin account no longer resolvable — safest move
                // is to sign out completely rather than leave a broken session.
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("LogOn", "Account", new { area = "FormBuilder" });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, adminUser.UserName),
                new Claim(ClaimTypes.Email, adminUser.Email),
                new Claim("UserId", adminUser.UserId.ToString()),
                new Claim("ChapterId", adminUser.ChapterId.ToString()),
                new Claim(ClaimTypes.Role, adminUser.RoleName)
                // No ImpersonatedBy* claims — this is a clean admin session again.
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });

            return RedirectToAction("Index", "Users");
        }

        [Authorize]
        public ActionResult RolesAccess(Int64 UserId = 0)
        {
            Entities.Users _objuser = new Entities.Users();
            try
            {
                Int32 _qstatus = 0;
                _objuser = _user.GetUserDetailsById(UserId, ref _qstatus);
            }
            catch
            {

            }
            ViewBag.UserId = UserId;
            ViewBag.objuser = _objuser;
            return View();
        }



        public ActionResult UserAccess(Int64 UserId = 0, string keyword = "")
        {
            List<Entities.Roles> lstsubRoles = new List<Entities.Roles>();
            try
            {
                int _qstatus = 0;
                userRoles = _user.GetUserRolesListById(UserId, ref _qstatus);
                lstRoles = _user.GetUserRolesList(keyword, ref _qstatus);
                lstsubRoles = _user.UserRolesSubDropDownGetList(keyword, ref _qstatus);
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.lstRoles = lstRoles;
            ViewBag.userRoleslst = userRoles;
            ViewBag.lstsubRoles = lstsubRoles;
            return View();
        }
        public ActionResult RolesByAccess(Int64 UserId = 0)
        {
            Entities.Users _objuser = new Entities.Users();
            try
            {
                Int32 _qstatus = 0;
                _objuser = _user.GetUserDetailsById(UserId, ref _qstatus);
            }
            catch
            {

            }
            ViewBag.UserId = UserId;
            ViewBag.objuser = _objuser;
            return View();
        }


        public ActionResult RolesByAccessList(Int64 UserId = 0, string keyword = "")
        {
            List<Entities.Roles> lstsubRoles = new List<Entities.Roles>();
            try
            {
                int _qstatus = 0;
                userRoles = _user.GetUserRolesListById(UserId, ref _qstatus);
                lstRoles = _user.GetUserRolesList(keyword, ref _qstatus);
                lstsubRoles = _user.UserRolesSubDropDownGetList(keyword, ref _qstatus);
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.lstRoles = lstRoles;
            ViewBag.userRoleslst = userRoles;
            ViewBag.lstsubRoles = lstsubRoles;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateUserAccess(Entities.UserRoles _objuser)
        {
            try
            {
                Int64 _qstatus = _user.UpdateUserAccess(_objuser);


                if (_qstatus == 1)
                {
                    TempData["messageType"] = "success"; // Assuming "info" for update
                    TempData["message"] = "Changes has been Updated Successfully";
                }
                else
                {
                    TempData["messageType"] = "warning";
                    TempData["message"] = "Changes has been Updated Failed.";
                }
                //TempData["message"] = (_qstatus == 1 ? "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>" : "<div class=\"alert alert-danger alert-dismissable\">Failed editing profile.</div>");

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Users");
        }


        [HttpPost]
        public JsonResult RoleBasedAccess(Entities.UserRoles objUserRoles)
        {
            string str = "";
            try
            {
                Int64 _status = _user.UpdateRoleBasedAccess(objUserRoles);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Successfully given Role access to the User.."
                    });
                    //str = "<div class=\"alert alert-success alert-dismissable\">Successfully given Role access to the User..</div>";
                    //return Json(new { ok = true, data = str });
                }
                else
                {

                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed giving Role access to the User.."
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
        [HttpPost]
        public JsonResult UpdateRolesWiseAcces(Entities.UserRoles objUserRoles)
        {
            string str = "";
            try
            {
                Int64 _status = _user.UpdateRolesWiseAcces(objUserRoles);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Successfully given Role access to the User.."
                    });
                    //str = "<div class=\"alert alert-success alert-dismissable\">Successfully given Role access to the User..</div>";
                    //return Json(new { ok = true, data = str });
                }
                else
                {

                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed giving Role access to the User.."
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

        [HttpPost]
        public JsonResult UserDelete(Int64 UserId)
        {
            string str = "";
            try
            {
                Int64 _status = _user.DeleteUser(UserId);
                if (_status == 1)
                {
                    //str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    //return Json(new { ok = true, data = str });

                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Record Deleted successfully"
                    });



                }
                else
                {
                    //str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting user status</div>";
                    //return Json(new { ok = false, data = str });
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed Deleting the Record"
                    });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        public JsonResult CheckProfileEmailAvailability(Int64 UserId, string Email)
        {
            int status = 0;
            try
            {
                Entities.Users objEuser = _user.GetUserByEmail(Email, ref status);
                bool data = (objEuser.UserId == UserId || objEuser.UserId == 0 ? true : false);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckEmailAvailability(string Email)
        {
            int status = 0;
            try
            {
                Entities.Users objEuser = _user.GetUserByEmail(Email, ref status);
                bool data = (objEuser != null && objEuser.UserId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>" });
            }
        }

        [HttpPost]
        public JsonResult CheckUserNameAvailability(string UserName)
        {
            int status = 0;
            try
            {
                Entities.Users objUser = _user.GetUserByUserName(UserName, ref status);
                bool data = (objUser != null && objUser.UserId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>" });
            }
        }

        [HttpPost]
        public ActionResult MembersData(Int64 ChapterId)
        {
            string str = "";
            try
            {
                List<Entities.Members> lstMembers = new List<Entities.Members>();

                int _qstatus = 0;
                lstMembers = _Members.GetMembersListByChapterId(ChapterId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = lstMembers });
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


        //[Areas.Admin.Models.SessionClass.SessionExpireFilter]
        //[HttpPost]
        //public ActionResult MembersEdit(Int64 MemberId)
        //{
        //    string str = "";
        //    try
        //    {
        //        int _qstatus = 0;
        //        Entities.Members _objMembers = _Members.GetMembersFullDetailsById(MemberId, ref _qstatus);

        //        if (_qstatus == 1)
        //        {
        //            return Json(new { ok = true, data = _objMembers });
        //        }
        //        else
        //        {
        //            str = "<div class=\"alert alert-success alert-dismissable\">Failed Transaction</div>";
        //            return Json(new { ok = false, data = str });
        //        }
        //    }
        //    catch
        //    {
        //        str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //        return Json(new { ok = false, data = str });
        //    }
        //}

        [HttpPost]
        public ActionResult MembersEdit(string Email)
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                Entities.Members _objMembers = _Members.GetMemberFullDetailsByEmail(Email, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objMembers });
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

        //[Authorize]
        //[HttpPost]
        //public JsonResult GetPassword(string Password = "")
        //{
        //    string str = "";
        //    try
        //    {
        //        str = BLL.Password.UnEncryptPassword(Password);

        //        // ✅ Removed JsonRequestBehavior.AllowGet — not needed in Core
        //        return Json(new { ok = true, success = str }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        str = "<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">"
        //            + ex.Message +
        //            "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" " +
        //            "aria-label=\"Close\"></button></div>";

        //        return Json(new { ok = false, success = str });
        //    }
        //}
        [Authorize]
        [HttpPost]
        public JsonResult GetPassword(string Password = "")
        {
            string str = "";
            try
            {
                str = BLL.Password.UnEncryptPassword(Password);
                return Json(new { ok = true, success = str }); // ✅ removed JsonRequestBehavior.AllowGet
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed to decrypt password");
                str = "<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">"
                    + "Unable to retrieve password."  // ✅ generic message
                    + "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" "
                    + "aria-label=\"Close\"></button></div>";
                return Json(new { ok = false, success = str });
            }
        }
        #region RoleAccess

        [Authorize]
        public ActionResult RoleAccess(Int64 UserId = 0)
        {
            ViewBag.UserId = UserId;
            return View();
        }

        [Authorize]
        public ActionResult RoleAccessList(Int64 UserId)
        {
            try
            {
                int _qstatus = 0;
                string keyword = "";
                userRoles = _user.GetUserRolesListById(UserId, ref _qstatus);
                lstRoles = _user.GetUserRolesList(keyword, ref _qstatus);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.lstRoles = lstRoles;
            ViewBag.userRoleslst = userRoles;
            return View();
        }

        // This is Lakshman
        [Authorize]
        [HttpPost]
        public JsonResult RemoveRoleAccess(Int64 UserRoleId, Int64 ParentId)
        {
            string str = "";
            try
            {
                Int64 _status = _user.RemoveRoleAccess(UserRoleId, ParentId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Remove Role access successfully..!!"
                    });

                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed removing role access"
                    });
                    //str = "<div class=\"alert alert-danger alert-dismissable\">Failed removing role access</div>";
                    //return Json(new { ok = false, data = str });
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
