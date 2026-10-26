using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]
    public class MailTemplatesController : Controller
    {
        BLL.MailTemplates _MailTemplate = new BLL.MailTemplates();
        List<Entities.MailTemplates> lstMailTemplate = new List<Entities.MailTemplates>();
        BLL.AdminMenuItems _adminmenu = new BLL.AdminMenuItems();
        BLL.Users _users = new BLL.Users();
        BLL.AppInfo _appinfo = new BLL.AppInfo();
        BLL.FormBuilder _FormBuilder = new BLL.FormBuilder();

        [Authorize]
        public ActionResult Index(Int64 mid = 0)
        {
            long userId = 0;
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userIdStr))
                long.TryParse(userIdStr, out userId);

            if (Request.Cookies.TryGetValue("UserRole", out string userRoleValue))
                HttpContext.Session.SetString("userrole", userRoleValue);

            long chapterId = 0;
            var chapterValue = HttpContext.Session.GetString("chapterid");
            if (!string.IsNullOrEmpty(chapterValue))
                long.TryParse(chapterValue, out chapterId);

            List<Entities.Chapters> lstChapters = new();
            List<Entities.PageDetails> lstPageDetails = new();
            List<Entities.MenuItems> lstMenuItems = new();
            Entities.UserRoles objuserroles = new();

            int status = 0;

            try
            {
                objuserroles = _users.GetRoleDetialsById(userId, mid, ref status);
                HttpContext.Session.SetString("IsEdit", objuserroles.IsEdit.ToString());
                HttpContext.Session.SetString("IsView", objuserroles.IsView.ToString());
                HttpContext.Session.SetString("IsDelete", objuserroles.IsDelete.ToString());
                HttpContext.Session.SetString("IsExport", objuserroles.IsExport.ToString());
                HttpContext.Session.SetString("IsAdd", objuserroles.IsAdd.ToString());

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

            ViewBag.lstPageDetails = lstPageDetails;
            ViewBag.lstMenuItems = lstMenuItems;
            ViewBag.Mid = mid;
            return View();
        }

        [Authorize]
        public ActionResult MailTemplatesList(string Search = "", string SortColumn = "UpdatedTime", string SortOrder = "Desc", int PageNo = 1, int Items = 25, bool IsEdit = false, bool IsView = false, bool IsDelete = false, bool IsExport = false, bool IsAdd = false, Int64 Mid = 0)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstMailTemplate = _MailTemplate.GetMailTemplatesListByVariable(Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstMailTemplate = lstMailTemplate;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.IsEdit = IsEdit;
            ViewBag.IsView = IsView;
            ViewBag.IsDelete = IsDelete;
            ViewBag.IsExport = IsExport;
            ViewBag.IsAdd = IsAdd;
            ViewBag.Mid = Mid;
            return View();
        }

        [Authorize]
        public ActionResult AddMailTemplate(Int64 mid = 0, Int64 formId = 0, string mailType = null, string embed = null)
        {
            int status = 0;
            List<Int64> preselected = formId > 0 ? new List<Int64> { formId } : new List<Int64>();
            ViewBag.lstForms = _MailTemplate.GetFormsListForDropdown(preselected, ref status);
            ViewBag.mid = mid;
            ViewBag.FormId = formId;
            ViewBag.MailType = mailType; // "Admin" or "Auto" — comes from the FormsList buttons
            ViewBag.IsEmbed = embed == "1"; // true when opened inside the FormBuilder "Mail Templates" panel

            if (formId > 0)
            {
                int formStatus = 0;
                var form = _FormBuilder.GetFormSchema(formId, ref formStatus);
                ViewBag.FormTitle = form != null ? form.Title : null;
                ViewBag.IsFormActive = form != null && form.IsActive;         // ✅ ADDED — drives the "User Mail" Active pill
                ViewBag.IsFormAdminMail = form != null && form.IsAdminMail;   // ✅ ADDED — drives the "Admin Mail" Active pill

                var fields = _FormBuilder.GetFormFieldLabels(form != null ? form.FormSchema : null);


                fields.Add("SubmissionId");
                fields.Add("SubmittedDate");

                bool hasPaymentField = _FormBuilder.FormHasPaymentField(form != null ? form.FormSchema : null);
                if (hasPaymentField)
                {
                    fields.AddRange(new[] { "PaymentStatus", "PaymentTxnId", "PaymentGateway", "PaymentAmount", "PaymentCurrency" });
                }

                ViewBag.FormFields = fields;
            }
            else
            {
                ViewBag.FormTitle = null;
                ViewBag.FormFields = new List<string>();
                ViewBag.IsFormActive = false;      // ✅ ADDED
                ViewBag.IsFormAdminMail = false;   // ✅ ADDED
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddMailTemplate(Entities.MailTemplates objMailTemplate, IFormFile Logo, List<Int64> SelectedFormIds, Int64 mid = 0, Int64 ContextFormId = 0, string embed = null)
        {
            try
            {
                objMailTemplate.SelectedFormIds = SelectedFormIds ?? new List<Int64>();

                if (Logo != null && Logo.Length > 0)
                {
                    int status1 = 0;
                    var objappinfo = _appinfo.GetAppInfoDetails(ref status1);

                    string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(Logo.FileName);

            
                    string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "Maillogo", "NormalImages", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(normalPath));

                    using (var stream = new FileStream(normalPath, FileMode.Create))
                    {
                        Logo.CopyTo(stream);
                    }

                    objMailTemplate.LogoUrl = objappinfo.BaseUrl + "Areas/FormBuilder/Content/Maillogo/NormalImages/" + fileName;
                }

                objMailTemplate.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objMailTemplate.UpdatedTime = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();
                Int64 _status = _MailTemplate.InsertMailTemplates(objMailTemplate);

                if (_status == 1)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Inserted Record Successfully";
                }
                else if (_status == 2)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Changes has been Updated Successfully";
                }
                else
                {
                    TempData["messageType"] = "warning";
                    TempData["message"] = "Failed uploading page.";
                }

                // ✅ CHANGED — stay on this form's mail-template page instead of going to the list
                if (ContextFormId > 0)
                {
                    return RedirectToAction("EditByForm", "MailTemplates", new { formId = ContextFormId, mailType = objMailTemplate.MailType, mid = mid, embed = embed });
                }
                if (embed == "1")
                {
                    return RedirectToAction("AddMailTemplate", "MailTemplates", new { mid = mid, embed = embed });
                }
                return RedirectToAction("Index", "MailTemplates", new { mid = mid });
            }
            catch (Exception ex)
            {
                TempData["messageType"] = "error";
                TempData["message"] = ex.Message;

                // ✅ CHANGED — same-page redirect on error too
                if (ContextFormId > 0)
                {
                    return RedirectToAction("EditByForm", "MailTemplates", new { formId = ContextFormId, mailType = objMailTemplate.MailType, mid = mid, embed = embed });
                }
                return RedirectToAction("AddMailTemplate", "MailTemplates", new { mid = mid, embed = embed });
            }
        }
        [Authorize]
        public ActionResult EditByForm(Int64 formId, string mailType, Int64 mid = 0, string embed = null)
        {
            int status = 0;
            var template = _MailTemplate.GetMailTemplateByFormIdAndType(formId, mailType, ref status);

            if (status == 1 && template != null && template.MailTemplateId > 0)
            {
                return RedirectToAction("EditMailTemplate", new { MailTemplateId = template.MailTemplateId, formId = formId, mid = mid, embed = embed });
            }
            return RedirectToAction("AddMailTemplate", new { formId = formId, mailType = mailType, mid = mid, embed = embed });
        }

        [Authorize]
        public ActionResult EditMailTemplate(Int64 MailTemplateId = 0, Int64 mid = 0, Int64 formId = 0, string embed = null)
        {
            try
            {
                int _qstatus = 0;
                Entities.MailTemplates objTemplates = _MailTemplate.GetMailTemplateById("", MailTemplateId, ref _qstatus);

                if (_qstatus == 1)
                {
                    int formsStatus = 0;
                    ViewBag.lstForms = _MailTemplate.GetFormsListForDropdown(objTemplates.SelectedFormIds, ref formsStatus);

                    // ✅ ADDED — context form for the heading + fields panel:
                    // prefer the formId passed in the URL, else fall back to the first linked form
                    Int64 contextFormId = formId > 0 ? formId
                        : (objTemplates.SelectedFormIds != null && objTemplates.SelectedFormIds.Count > 0 ? objTemplates.SelectedFormIds[0] : 0);

                    ViewBag.FormId = contextFormId;
                    ViewBag.MailType = objTemplates.MailType;
                    ViewBag.IsEmbed = embed == "1"; // true when opened inside the FormBuilder "Mail Templates" panel

                    if (contextFormId > 0)
                    {
                        int formStatus = 0;
                        var form = _FormBuilder.GetFormSchema(contextFormId, ref formStatus);
                        ViewBag.FormTitle = form != null ? form.Title : null;
                        ViewBag.IsFormActive = form != null && form.IsActive;         // ✅ ADDED — drives the "User Mail" Active pill
                        ViewBag.IsFormAdminMail = form != null && form.IsAdminMail;   // ✅ ADDED — drives the "Admin Mail" Active pill

                        var fields = _FormBuilder.GetFormFieldLabels(form != null ? form.FormSchema : null);
                        fields.Add("SubmissionId");
                        fields.Add("SubmittedDate");

                        bool hasPaymentField = _FormBuilder.FormHasPaymentField(form != null ? form.FormSchema : null);
                        if (hasPaymentField)
                        {
                            fields.AddRange(new[] { "PaymentStatus", "PaymentTxnId", "PaymentGateway", "PaymentAmount", "PaymentCurrency" });
                        }

                        ViewBag.FormFields = fields;
                    }
                    else
                    {
                        ViewBag.FormTitle = null;
                        ViewBag.FormFields = new List<string>();
                        ViewBag.IsFormActive = false;      // ✅ ADDED
                        ViewBag.IsFormAdminMail = false;   // ✅ ADDED
                    }

                    ViewBag.mid = mid;
                    ViewBag.objTemplates = objTemplates;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["messageType"] = "warning";
                    TempData["message"] = "Failed transaction.";
                    return View();
                }
            }
            catch
            {
                TempData["messageType"] = "error";
                TempData["message"] = "Failed transaction.";
                return View();
            }
            return View();
        }

        [Authorize]
        public ActionResult ViewMailTemplate(Int64 MailTemplateId = 0, Int64 mid = 0)
        {
            try
            {
                int _qstatus = 0;
                Entities.MailTemplates objTemplates = _MailTemplate.GetMailTemplateById("", MailTemplateId, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.mid = mid;
                    ViewBag.objTemplates = objTemplates;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "MailTemplates", new { mid = mid });
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "MailTemplates", new { mid = mid });
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult SendSampleMail(string ToEmail, string Subject, string Description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ToEmail))
                    return Json(new { ok = false, message = "Please enter an email address to send the sample to." });

                if (string.IsNullOrWhiteSpace(Subject))
                    return Json(new { ok = false, message = "Please enter a subject." });

                if (string.IsNullOrWhiteSpace(Description))
                    return Json(new { ok = false, message = "Please write the email content before sending a sample." });

                bool sent = _MailTemplate.SendSampleMail(ToEmail, Subject, Description);

                if (sent)
                    return Json(new { ok = true, message = "Sample email sent successfully." });

                return Json(new { ok = false, message = "Sample email not sent. Please check the email address and try again." });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = "Sample email not sent. " + ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult AdminMailStatus(Int64 FormId)
        {
            int status = 0;
            try
            {
                _FormBuilder.UpdateAdminMailStatus(FormId, ref status);
                if (status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Email Status updated successfully" });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed to Email update status" });
            }
            catch
            {
                return Json(new { ok = false, messageType = "error", message = "Failed transaction." });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult DeleteMailTemplate(Int64 MailTemplateId)
        {
            try
            {
                Int64 _status = _MailTemplate.DeleteMailTemplate(MailTemplateId);
                if (_status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Record Deleted successfully" });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed Deleting page" });
            }
            catch
            {
                return Json(new { ok = false, messageType = "error", message = "Failed transaction." });
            }
        }

        public class AuthorizeAttribute : ActionFilterAttribute
        {
            BLL.Users _user = new BLL.Users();
            BLL.Roles _Roles = new BLL.Roles();
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                string userRole = null;
                int status = 0;

                var user = filterContext.HttpContext.User;

                if (user?.Identity != null && user.Identity.IsAuthenticated)
                {
                    userRole = user.FindFirst(ClaimTypes.Role)?.Value;
                    string emailFromClaim = user.FindFirst(ClaimTypes.Email)?.Value;

                    if (!string.IsNullOrEmpty(emailFromClaim))
                    {
                        var objuser = _user.GetAdminUsersGetByEmail(emailFromClaim, ref status);
                        if (objuser != null)
                        {
                            filterContext.HttpContext.Session.SetString("UserName", objuser.UserName ?? "");
                            filterContext.HttpContext.Session.SetString("UserId", objuser.UserId.ToString());
                            filterContext.HttpContext.Session.SetString("UserEmail", emailFromClaim ?? "");
                            filterContext.HttpContext.Session.SetString("chapterid", objuser.ChapterId.ToString());
                            filterContext.HttpContext.Session.SetString("userrole", userRole ?? "");
                        }
                    }
                }

                if (string.IsNullOrEmpty(userRole))
                {
                    filterContext.Result = new RedirectToActionResult("LogOn", "Account", new { area = "FormBuilder" });
                    return;
                }

                int roleStatus = 0;
                List<Entities.Roles> lstRoles = _Roles.GetRolesList(ref roleStatus);
                List<string> allowedRoles = lstRoles.Select(r => r.RoleName.Trim()).ToList();

                var userRoles = userRole.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Trim()).ToList();

                bool isAuthorized = userRoles.Any(r => allowedRoles.Contains(r));

                if (!isAuthorized)
                {
                    filterContext.Result = new RedirectToActionResult("Unauthorized", "Account", new { area = "FormBuilder" });
                    return;
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }
}