using AWSFormBuilder.Areas.FormBuilder.Models;
using AWSFormBuilder.Areas.FormBuilder.BLL;
using AWSFormBuilder.Areas.FormBuilder.DAL;
using Microsoft.AspNetCore.Authentication;
using AWSFormBuilder.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    //monday ofter- mahi//

    [Area("FormBuilder")]
    public class AccountController : Controller
    {
       AWSFormBuilder.Areas.FormBuilder.BLL.Users _user = new AWSFormBuilder.Areas.FormBuilder.BLL.Users();
       AWSFormBuilder.Areas.FormBuilder.BLL.Members _Members = new AWSFormBuilder.Areas.FormBuilder.BLL.Members();
        Entities.Chapters objChapters = new Entities.Chapters();
        BLL.Chapters _Chapters = new BLL.Chapters();
        BLL.AppInfo _AppInfo = new BLL.AppInfo();
        BLL.SendMail _sendmail = new BLL.SendMail();

        public ActionResult LogOn(string str = "")
        {

            if (str == "session")
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Session Expired.</div>";
            }
            if (str == "noaccess")
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Admin need to provide role access.</div>";
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogOn(LogOnModel model, string returnUrl)
        {
            try
            {
                int _status = 0;
                var objUser = _user.GetAdminUsersGetByEmail(model.Email, ref _status);

                if (objUser != null && objUser.UserId != 0 && objUser.RoleName != "EndUser")
                {
                    if (objUser.IsApproved)
                    {
                        string pwd = BLL.Password.UnEncryptPassword(objUser.Password);

                        if (model.Password.Trim() == pwd)
                        {

                            if (objUser.StartDate != DateTime.MinValue && objUser.StartDate.Date > DateTime.Now.Date)
                            {
                                return RedirectToAction("Subscription", new { email = objUser.Email });
                            }

                            if (objUser.EndDate != DateTime.MinValue && objUser.EndDate.Date < DateTime.Now.Date)
                            {
                                return RedirectToAction("Subscription", new { email = objUser.Email });
                            }

                            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, objUser.UserName),
                        new Claim(ClaimTypes.Email, objUser.Email),
                        new Claim("UserId", objUser.UserId.ToString()),
                        new Claim("ChapterId", objUser.ChapterId.ToString()),
                        new Claim(ClaimTypes.Role, objUser.RoleName)
                    };

                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);

                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = model.RememberMe,
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                            };

                            // ✅ Sign in user (replaces FormsAuthentication + Cookie)
                            await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                principal,
                                authProperties);

                            string UserRole = objUser.RoleName;

                            // ✅ Role-based redirects (same as your logic)
                            if (UserRole.Contains("SubAdmin"))
                                return RedirectToAction("Index", "Form", new { mid = 74 });

                            else if (UserRole.Contains("SuperAdmin") || UserRole.Contains("DeveloperAdmin"))
                                return RedirectToAction("Index", "Form", new { mid = 74 });

                            else if (UserRole.Contains("ChapterAdmin"))
                                return RedirectToAction("Index", "Form", new { mid = 74 });

                            else
                                return RedirectToAction("Index", "Form", new { mid = 74 });
                        }
                        else
                        {
                            TempData["messageType"] = "warning";
                            TempData["message"] = "Username or Password is incorrect.";
                        }
                    }
                    else
                    {
                        TempData["messageType"] = "warning";
                        TempData["message"] = "Your status has been deactivated. Please contact admin.";
                    }
                }
                else
                {
                    TempData["messageType"] = "warning";
                    TempData["message"] = "Username or Password is incorrect.";
                }
            }
            catch
            {
                TempData["messageType"] = "warning";
                TempData["message"] = "Failed transaction.";
            }

            return View();
        }
        [HttpGet]
        public ActionResult Subscription(string email)
        {
            int _status = 0;
            var objUser = !string.IsNullOrWhiteSpace(email)
                ? _user.GetAdminUsersGetByEmail(email, ref _status)
                : null;

            // unknown email / direct hit without email -> back to login
            if (objUser == null || objUser.UserId == 0)
            {
                return RedirectToAction("LogOn", "Account");
            }

            ViewBag.UserName = objUser.UserName;
            ViewBag.UserEmail = objUser.Email;

            if (objUser.StartDate != DateTime.MinValue && objUser.StartDate.Date > DateTime.Now.Date)
            {
                ViewBag.SubscriptionDate = objUser.StartDate.ToString("dd MMM yyyy");
                ViewBag.Message = "Your subscription is not yet active. Please contact admin.";
            }
            else if (objUser.EndDate != DateTime.MinValue && objUser.EndDate.Date < DateTime.Now.Date)
            {
                ViewBag.SubscriptionDate = objUser.EndDate.ToString("dd MMM yyyy");
                ViewBag.Message = "Your subscription has ended. Please contact admin.";
            }
            else
            {
                // subscription is valid - nothing to show on this page
                return RedirectToAction("LogOn", "Account");
            }

            return View();
        }
        public async Task<IActionResult> LogOff()
        {
            // ✅ Sign out user (removes auth cookie)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // ✅ Optional: clear session if you are using it
            HttpContext.Session.Clear();

            return RedirectToAction("LogOn", "Account");
        }

        #region Profile

        public ActionResult ChangePassword()
        {
            int _qstatus = 0;
            Entities.Users _objuser = new Entities.Users();
            try
            {
                _objuser = _user.GetUserByUserName(HttpContext.User.Identity.Name.ToString(), ref _qstatus);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.objuser = _objuser;
            return View();
        }

        [Authorize]
        [HttpPost]
        [Areas.FormBuilder.Models.SessionClass.SessionExpireFilter]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                string UserEmail = HttpContext.Session.GetString("UserEmail");
                if (UserEmail != "")
                {
                    int _qstatus = 0;
                    string UserEmails = HttpContext.Session.GetString("UserEmail");

                    string oldpass = _user.GetPassword(UserEmails, ref _qstatus);

                    if (_qstatus == 1)
                    {
                        //if (AWSFormBuilder.BLL.Password.VerifyHash(model.OldPassword.Trim(), "SHA512", oldpass) == true)
                        //{
                        //string UserEmail = HttpContext.Session.GetString("UserEmail");
                        string UserEmaill = HttpContext.Session.GetString("UserEmail");

                        string _password = model.NewPassword;
                        string encryptedPassword = BLL.Password.EncryptPassword(_password);
                        //string newpass =AWSFormBuilder.Areas.FormBuilder.BLL.Password.ComputeHash(model.NewPassword, "SHA512", null);
                        Int64 _pstatus = _user.ChangePassword(UserEmaill, encryptedPassword);

                        if (_pstatus == 1)
                        {
                            TempData["messageType"] = "success";
                            TempData["message"] = "Password Changed Successfully.";
                        }
                        else
                        {
                            TempData["messageType"] = "warning";
                            TempData["message"] = "The current password is incorrect or the new password is invalid.";

                        }
                        //}
                        //else
                        //{
                        //    TempData["messageType"] = "warning";
                        //    TempData["message"] = "The current password is incorrect or the new password is invalid.";
                        //}
                    }
                    else
                    {
                        TempData["messageType"] = "warning";
                        TempData["message"] = "The current password is incorrect or the new password is invalid.";
                    }
                }
                return RedirectToAction("Profile", "Account");
            }
            catch
            {

                TempData["messageType"] = "warning";
                TempData["message"] = "Failed transaction.";

                return RedirectToAction("Profile", "Account");
            }

        }
        [Authorize]
        public ActionResult Profile()
        {
            try
            {
                string UserEmail = HttpContext.Session.GetString("UserEmail");

                int _qstatus = 0;


                Entities.Users _objuser = _user.GetUserByUserName(UserEmail, ref _qstatus);
                Entities.Users objUser = _user.GetAdminUsersGetByEmail(_objuser.Email, ref _qstatus);
                string pwd = BLL.Password.UnEncryptPassword(objUser.Password);
                if (_qstatus == 1)
                {
                    ViewBag.objuser = _objuser;
                    ViewBag.pwd = pwd;
                }
                else
                {

                    TempData["messageType"] = "warning";
                    TempData["message"] = "Failed transaction.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {

                TempData["messageType"] = "warning";
                TempData["message"] = "Failed transaction.";
            }

            return View();
        }

        [HttpPost]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfilePic(IFormFile file, long UserId)
        {
            try
            {
                int status = 0;
                var objappinfo = _AppInfo.GetAppInfoDetails(ref status);

                if (file != null && file.Length > 0)
                {
                    // ✅ Generate unique file name
                    string fileExtension = Path.GetExtension(file.FileName);
                    string imageName = Guid.NewGuid().ToString() + fileExtension;

                    // ✅ Save path
                    string folderPath = Path.Combine(objappinfo.UploadPath, "UserProfileImages");
                    string filePath = Path.Combine(folderPath, imageName);

                    // ✅ Ensure directory exists
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // ✅ Save file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // ✅ Update DB
                    long _status = _user.UpdateUserProfileImage(UserId, ref imageName);

                    if (_status == 1)
                    {
                        TempData["messageType"] = "success";
                        TempData["message"] = "Changed Your Profile Picture Successfully.";
                    }
                    else
                    {
                        System.IO.File.Delete(filePath);

                        TempData["messageType"] = "danger";
                        TempData["message"] = "Failed uploading image.";
                    }
                }
            }
            catch
            {
                TempData["messageType"] = "danger";
                TempData["message"] = "Failed transaction.";
            }

            return RedirectToAction("Profile", "Account");
        }
        #endregion

        public ActionResult ForgotPassword()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            BLL.SendMail _sentmail = new BLL.SendMail();

            try
            {
                int _status = 0;
                Entities.AppInfo objappinfo = _AppInfo.GetAppInfoDetails(ref _status);
                Entities.Users _objuser = new Entities.Users();
                _objuser = _user.GetUserByEmail(model.Email, ref _status);

                if (_objuser.UserId == 0 && _objuser.Email != model.Email)
                {
                    ViewBag.Message = "<div class=\"alert alert-danger alert-dismissable\">User Name (or) Email is not valid.</div>";
                }
                //string _password = BLL.Password.GetUniqueKey(8);
                //string _passwordhash = BLL.Password.ComputeHash(_password, "SHA512", null);

                string _password = BLL.Password.GetUniqueKey(8);
                string encryptedPassword = BLL.Password.EncryptPassword(_password);


                Int64 _pstatus = _user.ChangePassword(_objuser.UserId, encryptedPassword);
                if (_pstatus == 1)
                {
                    StringBuilder body = new StringBuilder();
                    body.Append("<p>Dear " + _objuser.UserName + ", <br /><br />You have requested password retrieve. Please find the login details below. <br />");
                    body.Append("<br />Password: " + _password + " <br /><br /><a href=\"" + objappinfo.AdminSiteUrl + "\">Click here to Login.</a><br /><br />");
                    body.Append("Thank You,<br />Admin</p>");
                    _sentmail.SendMailSendinbrevo(model.Email, "Password Details From Admin Team", body.ToString());

                    //_sendmail.SendMailSendinbrevo(model.Email, "Password Details From Admin Team", body.ToString());

                    TempData["messageType"] = "success";
                    TempData["message"] = "Password Details Sent to Email Id Registered.";



                }
                else
                {

                    TempData["messageType"] = "warning";
                    TempData["message"] = "The email address you entered does not exist. Please check and try again.";

                }

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            return View();
        }
        public ActionResult UserValidate(string name, string id = "")
        {
            try
            {
                int _status = 0;
                Entities.AppInfo objappinfo = _AppInfo.GetAppInfoDetails(ref _status);
                Entities.Users objuser = _user.GetUserByEmail(name, ref _status);
                if (objuser.UserName != null)
                {
                    if (_status != -1 && objuser != null)
                    {
                        if (id == "reactivate")
                        {
                            Guid guid = FormBuilder.BLL.Common.generateGUID();
                            objuser.IsActivated = false;
                            objuser.RegistrationGUID = guid;
                            Int64 _guidStatus = _user.UpdateRegistrationGUID(objuser.UserId, "false", guid);
                            if (_guidStatus != -1)
                            {
                                string reactivateUrl = objappinfo.BaseUrl + "FormBuilder/Account/UserValidate?name=" + objuser.Email + "&id=" + guid.ToString();
                                string reactivateContent =
                                    "<p style=\"margin:0 0 12px 0;\">Dear <strong>" + objuser.UserName + "</strong>,</p>" +
                                    "<p style=\"margin:0 0 12px 0;\">Your request for reactivation has been accepted. Please click the button below to activate your account.</p>" +
                                    BuildEmailButton(reactivateUrl, "Activate Account");
                                string reactivateBody = BuildEmailTemplate(objappinfo.SiteName, "Account Reactivation", reactivateContent);
                                _sendmail.SendMailSendinbrevo(objuser.Email, "Account Reactivation Link - " + objappinfo.SiteName, reactivateBody);

                                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Activation details sent to mail.</div>";
                            }
                            else
                            {
                                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed reactivating account.</div>";
                            }
                        }
                        else
                        {
                            if (!objuser.IsActivated && objuser.RegistrationGUID != Guid.Empty)
                            {
                                if (objuser.RegistrationGUID.ToString() == id)
                                {
                                    string _password = BLL.Password.GetUniqueKey(8);
                                    string encryptedPassword = BLL.Password.EncryptPassword(_password);
                                    //string _hashpassword = BLL.Password.ComputeHash(_password, "SHA512", null);

                                    Int64 _passStatus = _user.ChangePassword(name, encryptedPassword);

                                    if (_passStatus != -1)
                                    {
                                        Int64 _guidStatus = _user.UpdateRegistrationGUID(objuser.UserId, "true", Guid.Empty);

                                        string loginUrl = objappinfo.BaseUrl + "FormBuilder/Account/Logon";
                                        string activatedContent =
                                            "<p style=\"margin:0 0 12px 0;\">Dear <strong>" + objuser.UserName + "</strong>,</p>" +
                                            "<p style=\"margin:0 0 6px 0;\">Your account is activated and your password has been reset. Please find your login details below.</p>" +
                                            BuildCredentialsBox(objuser.Email, _password) +
                                            BuildEmailButton(loginUrl, "Login Now") +
                                            "<p style=\"margin:0 0 10px 0;font-size:13px;line-height:20px;color:#b45309;background-color:#fffbeb;border:1px solid #fde68a;border-radius:6px;padding:10px 14px;\">For security, please change your password after logging in.</p>";
                                        string activatedBody = BuildEmailTemplate(objappinfo.SiteName, "Account Activated", activatedContent);
                                        _sendmail.SendMailSendinbrevo(objuser.Email, "Activation Details From Admin Team", activatedBody);

                                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Hi " + objuser.UserName + ", <br/> Your account is activated successfully. Further details are posted to mail registered.</div>";
                                    }
                                }
                                else
                                {
                                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Activation link is not valid. Please try <a href=\"UserValidate?name=" + objuser.UserName + "&id=reactivate\">reactivating</a> account.</div>";
                                }
                            }
                            else
                            {
                                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">User is already activated. <br/>Please click here to <a class=\"red-t\" href='Admin/Account/LogOn'>login</a></div>";
                            }
                        }
                    }
                    else
                    {
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Invalid activation link. Please try again from mail.</div>";
                    }
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Your account has been removed. So please contact to admin.</div>";
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        //public ActionResult UserValidate(string name, string id = "")
        //{
        //    try
        //    {
        //        int _status = 0;
        //        Entities.AppInfo objappinfo = _AppInfo.GetAppInfoDetails(ref _status);
        //        Entities.Users objuser = _user.GetUserByEmail(name, ref _status);
        //        if (objuser.UserName != null)
        //        {
        //            if (_status != -1 && objuser != null)
        //            {
        //                if (id == "reactivate")
        //                {
        //                    Guid guid =AWSFormBuilder.Areas.FormBuilder.BLL.Common.generateGUID();
        //                    objuser.IsActivated = false;
        //                    objuser.RegistrationGUID = guid;
        //                    Int64 _guidStatus = _user.UpdateRegistrationGUID(objuser.UserId, "false", guid);
        //                    if (_guidStatus != -1)
        //                    {
        //                        StringBuilder body = new StringBuilder();
        //                        body.Append("<p>Dear " + objuser.UserName + ", <br /><br />Request for reactivation is accepted, please find the activation link <a href=\"" + objappinfo.BaseUrl + "Admin/Account/UserValidate?name=" + objuser.Email + "&id=" + guid.ToString() + "\">here</a>. <br />");
        //                        body.Append("Thank You,<br />Admin</p>");
        //                        //_sendmail.SendMailSendinbrevo(objuser.Email, "Account Reactivation Link - " + objappinfo.SiteName, body.ToString());

        //                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Activation details sent to mail.</div>";
        //                    }
        //                    else
        //                    {
        //                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed reactivating account.</div>";
        //                    }
        //                }
        //                else
        //                {
        //                    if (!objuser.IsActivated && objuser.RegistrationGUID != Guid.Empty)
        //                    {
        //                        if (objuser.RegistrationGUID.ToString() == id)
        //                        {
        //                            string _password = BLL.Password.GetUniqueKey(8);
        //                            string encryptedPassword = BLL.Password.EncryptPassword(_password);
        //                            //string _hashpassword = BLL.Password.ComputeHash(_password, "SHA512", null);


        //                            Int64 _passStatus = _user.ChangePassword(name, encryptedPassword);
        //                            if (_passStatus != -1)
        //                            {
        //                                Int64 _guidStatus = _user.UpdateRegistrationGUID(objuser.UserId, "true", Guid.Empty);

        //                                StringBuilder body = new StringBuilder();
        //                                body.Append("<p>Dear " + objuser.UserName + ", <br /><br />Your account is activated and password is reset, please find the details below. <br />");
        //                                body.Append("<br />Email: " + objuser.Email + "<br />Password: " + _password + " <br /><br /><a href=\"" + objappinfo.BaseUrl + "Admin/Account/Logon\">Click here to Login.</a><br /><br />");
        //                                body.Append("Thank You,<br />Admin</p>");
        //                                _sendmail.SendMailSendinbrevo(objuser.Email, "Activation Details From Admin Team", body.ToString());
        //                                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Hi " + objuser.UserName + ", <br/> Your account is activated successfully. Further details are posted to mail registered.</div>";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Activation link is not valid. Please try <a href=\"UserValidate?name=" + objuser.UserName + "&id=reactivate\">reactivating</a> account.</div>";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">User is already activated. <br/>Please click here to <a class=\"red-t\" href='Admin/Account/LogOn'>login</a></div>";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Invalid activation link. Please try again from mail.</div>";
        //            }
        //        }
        //        else
        //        {
        //            TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Your account has been removed. So please contact to admin.</div>";
        //        }
        //    }
        //    catch 
        //    {
        //        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //    }
        //    return View();
        //}

        //[Authorize]
        [HttpPost]
        public JsonResult CheckProfileEmailAvailability(Int64 MemberId, string Email)
        {
            try
            {
                int _status = 0;

                Entities.Members objMembers = _Members.GetMemberFullDetailsByEmail(Email, ref _status);

                bool data = (objMembers.MemberId == MemberId || objMembers.MemberId == 0 ? true : false);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>" });
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

        private string BuildCredentialsBox(string email, string password)
        {
            return $@"
<table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""margin:20px 0;background-color:#f1f5f9;border:1px solid #dbe3ee;border-left:4px solid #2563eb;border-radius:6px;"">
  <tr>
    <td style=""padding:18px 20px;"">
      <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
        <tr>
          <td style=""padding:6px 0;font-size:12px;color:#6b7280;text-transform:uppercase;letter-spacing:0.5px;"">Email</td>
        </tr>
        <tr>
          <td style=""padding:0 0 14px 0;font-size:15px;color:#111827;font-weight:600;word-break:break-all;"">
            <a href=""mailto:{email}"" style=""color:#111827;text-decoration:none;"">{email}</a>
          </td>
        </tr>
        <tr>
          <td style=""padding:6px 0;font-size:12px;color:#6b7280;text-transform:uppercase;letter-spacing:0.5px;"">Password</td>
        </tr>
        <tr>
          <td style=""padding:0;font-size:16px;color:#111827;font-weight:700;font-family:Consolas,'Courier New',monospace;letter-spacing:1px;"">{password}</td>
        </tr>
      </table>
    </td>
  </tr>
</table>";
        }
        #region CAPTCHA

        #region CAPTCHA

        // ✅ GET - Returns Captcha Image
        public IActionResult ShowCaptchaImage()
        {
            string code = GetRandomText();

            // ✅ Core Session - SetString instead of Session["key"]
            HttpContext.Session.SetString("captchastring", code);

            using (Bitmap bitmap = new Bitmap(200, 60, PixelFormat.Format32bppArgb))
            using (Graphics g = Graphics.FromImage(bitmap))
            using (MemoryStream ms = new MemoryStream())
            {
                using (Pen pen = new Pen(Color.Yellow))
                using (SolidBrush blue = new SolidBrush(Color.CornflowerBlue))
                using (SolidBrush black = new SolidBrush(Color.Black))
                {
                    Rectangle rect = new Rectangle(0, 0, 200, 60);
                    g.FillRectangle(blue, rect);
                    g.DrawRectangle(pen, rect);

                    int counter = 0;
                    foreach (var c in code)
                    {
                        using (Font font = new Font("Tahoma",
                            15 + _rand.Next(5, 15), FontStyle.Italic))
                        {
                            g.DrawString(c.ToString(), font, black,
                                new PointF(10 + counter, 10));
                        }
                        counter += 28;
                    }
                    DrawRandomLines(g);
                }

                bitmap.Save(ms, ImageFormat.Gif);
                return File(ms.ToArray(), "image/gif");
            }
        }

        // ✅ POST - Get Captcha string from Session
        [HttpPost]
        public JsonResult GetCaptcha()
        {
            try
            {
                // ✅ Core Session - GetString instead of Session["key"]
                string str = HttpContext.Session.GetString("captchastring");

                if (string.IsNullOrEmpty(str))
                    return Json(new
                    {
                        ok = false,
                        message = "<div class=\"alert alert-danger " +
                                  "alert-dismissable\">Session expired.</div>"
                    });

                return Json(new { ok = true, data = str, message = "" });
            }
            catch
            {
                return Json(new
                {
                    ok = false,
                    message = "<div class=\"alert alert-danger " +
                              "alert-dismissable\">Failed transaction.</div>"
                });
            }
        }

        // ✅ Helper - Random lines noise
        private static Random _rand = new Random();

        private void DrawRandomLines(Graphics g)
        {
            using (Pen yellowPen = new Pen(Color.Yellow, 1))
            {
                for (int i = 0; i < 20; i++)
                {
                    g.DrawLines(yellowPen, GetRandomPoints());
                }
            }
        }

        private Point[] GetRandomPoints()
        {
            return new Point[]
            {
        new Point(_rand.Next(0, 200), _rand.Next(0, 60)),
        new Point(_rand.Next(0, 200), _rand.Next(0, 60))
            };
        }

        private string GetRandomText()
        {
            const string chars =
                "0123456789ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            char[] result = new char[6];
            for (int i = 0; i < 6; i++)
                result[i] = chars[_rand.Next(chars.Length)];
            return new string(result);
        }

        #endregion


        [HttpPost]
        public async Task<IActionResult> AdminAuth(LogOnModel model, string returnUrl)
        {
            try
            {
                int _status = 0;

                // Get app info and user details
                Entities.AppInfo objappinfo = _AppInfo.GetAppInfoDetails(ref _status);
                Entities.Users objUser = _user.GetAdminUsersGetByEmail(model.Email, ref _status);

                // Validate user exists
                if (objUser == null || objUser.UserId == 0)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                          "Username or Password is incorrect.</div>";
                    return View("LogOn");
                }

                // Get chapter info
                int qstatus = 0;
                Int64 ChapterId = objUser.ChapterId;
                string cname = "";

                if (ChapterId != 0)
                {
                    objChapters = _Chapters.GetChaptersById(ChapterId, ref qstatus);
                    cname = objChapters?.ChapterName ?? "";
                }

                if (ChapterId == 0)
                    ChapterId = 1;

                // Check role — EndUser not allowed
                if (objUser.RoleName == "EndUser")
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                          "Username or Password is incorrect.</div>";
                    return View("LogOn");
                }

                // Check if approved
                if (!objUser.IsApproved)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                          "Your status has been deactivated. Please contact admin.</div>";
                    return View("LogOn");
                }

                // Check email matches
                if (model.Email != objUser.Email)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                          "Username or Password is incorrect.</div>";
                    return View("LogOn");
                }

                // ✅ Set Session values (replaces old Session["key"] = value)
                HttpContext.Session.SetString("username", objUser.UserName);
                HttpContext.Session.SetString("userrole", objUser.RoleName);
                HttpContext.Session.SetString("chapterid", objUser.ChapterId.ToString());
                HttpContext.Session.SetString("ChapterName", cname);

                // ✅ Create Claims (replaces FormsAuthenticationTicket)
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,           objUser.UserName),
            new Claim(ClaimTypes.Email,          objUser.Email),
            new Claim(ClaimTypes.Role,           objUser.RoleName),
            new Claim("UserId",                  objUser.UserId.ToString()),
            new Claim("ChapterId",               objUser.ChapterId.ToString()),
            new Claim("ChapterName",             cname)
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                };

                // ✅ Sign In (replaces FormsAuthentication.Encrypt + HttpCookie)
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // ✅ Role-based redirect
                string UserRole = objUser.RoleName;

                if (UserRole.Contains("SubAdmin"))
                    return RedirectToAction("Registrations", "EventRegistrations",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("SuperAdmin"))
                    return RedirectToAction("Index", "MenuItems",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("ChapterAdmin"))
                    return RedirectToAction("Index", "MenuItems",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("SiteAdmin"))
                    return RedirectToAction("Index", "MenuItems",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Volunteers"))
                    return RedirectToAction("Index", "Volunteers",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Events"))
                    return RedirectToAction("Index", "Events",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Enquiry"))
                    return RedirectToAction("Index", "Enquiry",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("PhotoGallery"))
                    return RedirectToAction("Index", "PhotoGallery",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("VideoGallery"))
                    return RedirectToAction("Index", "VideoGallery",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Committees"))
                    return RedirectToAction("Index", "Committees",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Youth"))
                    return RedirectToAction("Index", "Youth",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Members"))
                    return RedirectToAction("Index", "Members",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Sponsors"))
                    return RedirectToAction("Index", "Sponsors",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("ThemeBanners"))
                    return RedirectToAction("Index", "WebsiteBanners",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Chapters"))
                    return RedirectToAction("Index", "Chapters",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Services"))
                    return RedirectToAction("Index", "Services",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Donations"))
                    return RedirectToAction("Index", "Donations",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Sponsorships"))
                    return RedirectToAction("Index", "Sponsorships",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("PatrikaRegistrations"))
                    return RedirectToAction("Index", "PatrikaRegistrations",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("News"))
                    return RedirectToAction("Index", "News",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Treasurer"))
                    return RedirectToAction("ServiceDonors", "ServiceDonations",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("Helpline"))
                    return RedirectToAction("Index", "Services",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else if (UserRole.Contains("ProgramsManager"))
                    return RedirectToAction("Index", "Events",
                        new { area = "Admin", ChapterId = objUser.ChapterId });

                else
                {
                    // No role access
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                          "Admin needs to provide role access.</div>";

                    // ✅ Redirect using configured BaseUrl
                    string url = objappinfo.BaseUrl + "Admin/Account/LogOn?str=noaccess";
                    return Redirect(url);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                      "Failed transaction.</div>";
            }

            return View("LogOn");
        }
        #endregion
        public ActionResult Unauthorized()
        {
            return View();
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

        public class CustomAuthorizeAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var user = context.HttpContext.User;

                // ✅ Check authentication (instead of cookie manually)
                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToActionResult(
                        "Login", "Account", new { area = "FormBuilder" });
                }

                base.OnActionExecuting(context);
            }
        }
    }
}
