using AWSFormBuilder.Areas.FormBuilder.BLL;
using AWSFormBuilder.Areas.FormBuilder.DAL;
using AWSFormBuilder.Areas.FormBuilder.Models;
using AWSFormBuilder.Entities;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;

using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]
    public class FormController : Controller
    {
        BLL.Users _Users = new BLL.Users();
        BLL.FormBuilder _FormBuilder = new BLL.FormBuilder();
        DAL.FormBuilder _DFormSubmissions = new DAL.FormBuilder();
        Entities.MailTemplates objMailTemplates = new Entities.MailTemplates();
        BLL.MailTemplates _mailTemplatesBLL = new BLL.MailTemplates();
        BLL.SendMail _sentmail = new BLL.SendMail();

        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (int Count, DateTime WindowStart)> _ipSubmissions
            = new System.Collections.Concurrent.ConcurrentDictionary<string, (int Count, DateTime WindowStart)>();



        [Authorize]
        public ActionResult Index(Int64 ChapterId = 0, Int64 mid = 0)
        {
            long userId = 0;

            var userIdStr = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdStr))
            {
                long.TryParse(userIdStr, out userId);
            }
            Entities.UserRoles objuserroles = new();
            int status = 0;
            try
            {
                objuserroles = _Users.GetRoleDetialsById(userId, mid, ref status);

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
            return View();
        }
        [Authorize]
        public ActionResult FormBuilder(string id = null, Int64 mid = 0)
        {
            long userId = 0;
            Int64 formId = 0;
            Int32 formStatus = 0;
            Int32 roleStatus = 0;

            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    formId = BLL.FormLinkObfuscator.DecryptFormId(id);
                }
                catch
                {
                    return NotFound();
                }
            }

            ViewBag.FormId = formId;

            if (formId > 0)
            {
                var form = _FormBuilder.GetFormSchema(formId, ref formStatus);
                if (formStatus == 1 && form != null)
                {
                    ViewBag.FormTitle = form.Title;
                    ViewBag.FormSchema = form.FormSchema;
                    ViewBag.LogoUrl = form.LogoUrl;
                    ViewBag.LogoWidth = form.LogoWidth;
                    ViewBag.LogoHeight = form.LogoHeight;
                    ViewBag.FormDesign = form.DesignJson;
                    ViewBag.FormConditions = form.ConditionsJson;
                    ViewBag.IsActive = form.IsActive;
                }
            }

            var userIdStr = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdStr))
            {
                long.TryParse(userIdStr, out userId);
            }

            Entities.UserRoles objuserroles = new();

            try
            {
                objuserroles = _Users.GetRoleDetialsById(userId, mid, ref roleStatus);

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
                roleStatus = -1;

                ViewBag.IsEdit = false;
                ViewBag.IsView = false;
                ViewBag.IsDelete = false;
                ViewBag.IsExport = false;
                ViewBag.IsAdd = false;
                ViewBag.mid = mid;
            }

            return View();
        }
        [Authorize]
        public ActionResult EditThankYouPage(Int64 id, string embed = null)
        {
            int status = 0;
            var form = _FormBuilder.GetFormSchema(id, ref status);
            if (status != 1 || form == null) return NotFound();

            ViewBag.FormId = id;
            ViewBag.FormTitle = form.Title;
            ViewBag.ThankYouContent = form.ThankYouContent;
            ViewBag.IsEmbed = embed == "1";
            return View();
        }
        [Authorize]
        [HttpPost]
        public JsonResult CopyForm(Int64 FormId)
        {
            int status = 0;
            try
            {
                int getStatus = 0;
                var sourceForm = _FormBuilder.GetFormSchema(FormId, ref getStatus);
                if (getStatus != 1 || sourceForm == null)
                {
                    return Json(new { ok = false, messageType = "error", message = "Original form not found" });
                }

                string createdBy = HttpContext.User.Identity.Name;
                Int64 chapterId = (HttpContext.Session.GetString("chapterid") != null
                    ? Convert.ToInt64(HttpContext.Session.GetString("chapterid"))
                    : 1);

                var copyRequest = new FormSaveRequest
                {
                    FormId = 0, // 0 = insert as a new form, not an update
                    Title = sourceForm.Title + " - Copy",
                    Schema = sourceForm.FormSchema,
                    LogoUrl = sourceForm.LogoUrl,
                    LogoWidth = sourceForm.LogoWidth,
                    LogoHeight = sourceForm.LogoHeight,
                    Design = sourceForm.DesignJson,
                    Conditions = sourceForm.ConditionsJson
                };

                Int64 newFormId = _FormBuilder.SaveFormSchema(copyRequest, createdBy, chapterId, ref status);

                if (status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Form copied successfully", newFormId = newFormId });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed to copy form" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, messageType = "error", message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditThankYouPage(Int64 FormId, string ThankYouContent, string embed = null)
        {
            int status = 0;
            try
            {
                _FormBuilder.SaveThankYouContent(FormId, ThankYouContent, ref status);
                TempData["messageType"] = status > 0 ? "success" : "error";
                TempData["message"] = status > 0
                    ? "Thank You Page updated successfully"
                    : "Failed to update Thank You Page";
            }
            catch (Exception ex)
            {
                TempData["messageType"] = "error";
                TempData["message"] = ex.Message;
            }
            return RedirectToAction("EditThankYouPage", new { id = FormId, embed = embed });
        }
        [Authorize]
        [HttpPost]

        public JsonResult UploadFormLogo(IFormFile logo)
        {
            try
            {
                if (logo == null || logo.Length == 0)
                    return Json(new { success = false, message = "No file selected" });

                int status1 = 0;
                BLL.AppInfo _appinfo = new BLL.AppInfo();
                var objappinfo = _appinfo.GetAppInfoDetails(ref status1);

                string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(logo.FileName);


                string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "FormLogos", "NormalImages", fileName);


                Directory.CreateDirectory(Path.GetDirectoryName(normalPath));

                using (var stream = new FileStream(normalPath, FileMode.Create))
                {
                    logo.CopyTo(stream);
                }

                string relativeUrl = objappinfo.BaseUrl + "Areas/FormBuilder/Content/FormLogos/NormalImages/" + fileName;

                return Json(new { success = true, url = relativeUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public JsonResult UploadFormTermsPdf(IFormFile termsPdf)
        {
            try
            {
                if (termsPdf == null || termsPdf.Length == 0)
                    return Json(new { success = false, message = "No file selected" });

                string ext = Path.GetExtension(termsPdf.FileName).ToLower();
                if (ext != ".pdf")
                    return Json(new { success = false, message = "Only PDF files are allowed" });

                int status1 = 0;
                BLL.AppInfo _appinfo = new BLL.AppInfo();
                var objappinfo = _appinfo.GetAppInfoDetails(ref status1);

                string fileName = Guid.NewGuid().ToString("N") + ext;

                string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "uploads", "FormTerms", fileName);


                Directory.CreateDirectory(Path.GetDirectoryName(normalPath));

                using (var stream = new FileStream(normalPath, FileMode.Create))
                {
                    termsPdf.CopyTo(stream);
                }

                string relativeUrl = objappinfo.BaseUrl + "Areas/FormBuilder/Content/uploads/FormTerms/" + fileName;

                return Json(new { success = true, url = relativeUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UploadFormFieldFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Json(new { success = false, message = "No file selected" });

                int status1 = 0;
                BLL.AppInfo _appinfo = new BLL.AppInfo();
                var objappinfo = _appinfo.GetAppInfoDetails(ref status1);

                string ext = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString("N") + ext;

                string normalPath = Path.Combine("Areas", "FormBuilder", "Content", "uploads", "FormSubmissionFiles", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(normalPath));

                using (var stream = new FileStream(normalPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                string relativeUrl = objappinfo.BaseUrl + "Areas/FormBuilder/Content/uploads/FormSubmissionFiles/" + fileName;


                return Json(new { success = true, url = relativeUrl, fileName = file.FileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult SaveFormSchema([FromBody] FormSaveRequest request)
        {
            int status = 0;
            Int64 LogId = 0;

            try
            {
                var objLogReport = new Entities.ApplicationLogs
                {
                    LogTitle = request.Title,
                    LogDescription = $"New Form Creating",
                    LogDate = DateTime.Now,
                    InsertedBy = request.Title ?? "Anonymous",
                    InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                    UpdatedBy = request.Title ?? "Anonymous",
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                };
                _Users.LogReportInsert(objLogReport, ref LogId);
                string createdBy = HttpContext.User.Identity.Name;
                Int64 chapterId = (HttpContext.Session.GetString("chapterid") != null
                    ? Convert.ToInt64(HttpContext.Session.GetString("chapterid"))
                    : 1);

                Int64 formId = _FormBuilder.SaveFormSchema(request, createdBy, chapterId, ref status);

                _Users.LogSubReportInsert(new Entities.Logdetails
                {
                    LogId = LogId,
                    LogTitle = "Form Ofter Inserting",
                    LogDescription = status == 1
                                         ? $"form inserted successfully. formId: {formId}"
                                         : $"form insert failed. Status: {status}",
                    LogDate = DateTime.Now,
                    InsertedBy = request.Title ?? "Anonymous",
                    InsertedDate = DateTime.Now,
                    UpdatedBy = request.Title ?? "Anonymous",
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                });

                // ✅ CHANGED — success now driven by status (1/-1 contract), not formId > 0
                return Json(new { success = status == 1, formId = formId, status = status });
            }
            catch (Exception ex)
            {
                _Users.LogSubReportInsert(new Entities.Logdetails
                {
                    LogId = LogId,
                    LogTitle = "form Inserting Failed",
                    LogDescription = "Failed uploading form Details.",
                    LogDate = DateTime.Now,
                    InsertedBy = request.Title ?? "Anonymous",
                    InsertedDate = DateTime.Now,
                    UpdatedBy = request.Title ?? "Anonymous",
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                });
                return Json(new { success = false, message = ex.Message, status = -1 });
            }
        }


        [Authorize]
        public ActionResult FormsList(Int64 ChapterId = 0, string Search = "", string SortColumn = "CreatedDate", string SortOrder = "DESC", int PageNo = 1, int Items = 25, bool IsEdit = false, bool IsView = false, bool IsDelete = false, bool IsExport = false, bool IsAdd = false, Int64 Mid = 0)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            List<Entities.FormBuilder> lstForms = new List<Entities.FormBuilder>();

            // ADDED - logged-in user + role (SuperAdmin/DeveloperAdmin see every form)
            Int64 currentUserId = HttpContext.Session.GetString("UserId") != null
                ? Convert.ToInt64(HttpContext.Session.GetString("UserId")) : 0;
            string currentRole = HttpContext.Session.GetString("userrole") ?? "";
            bool isSuperAdmin = currentRole.Contains("SuperAdmin") || currentRole.Contains("DeveloperAdmin");

            try
            {
                lstForms = _FormBuilder.GetFormsListByVariable(ChapterId, Search, Sort, PageNo, Items, currentUserId, isSuperAdmin, ref Total);
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }

            ViewBag.ChapterId = ChapterId;
            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstForms = lstForms;
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

        // ADDED - modal data: all users + the UserIds that already have access to this form
        [Authorize]
        public JsonResult GetFormAccessUsers(Int64 FormId)
        {
            try
            {
                string role = HttpContext.Session.GetString("userrole") ?? "";
                if (!(role.Contains("SuperAdmin") || role.Contains("DeveloperAdmin")))
                    return Json(new { ok = false, message = "Only SuperAdmin can manage form access" });

                int total = 0;
                var lstAllUsers = _Users.GetUserListByVariable("", 0, "", "", "UserName ASC", 1, 100000, ref total);

                int aStatus = 0;
                var selectedIds = _FormBuilder.GetFormAccessUserIds(FormId, ref aStatus);

                var users = lstAllUsers
                    .Where(u => !(u.RoleName ?? "").Contains("SuperAdmin") && u.Designation != "DeveloperAdmin")
                    .Select(u => new { userId = u.UserId, userName = u.UserName, email = u.Email })
                    .ToList();

                return Json(new { ok = true, users = users, selectedIds = selectedIds });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        // ADDED - modal Submit
        [Authorize]
        [HttpPost]
        public JsonResult SaveFormAccess(Int64 FormId, string UserIds)
        {
            int status = 0;
            try
            {
                string role = HttpContext.Session.GetString("userrole") ?? "";
                if (!(role.Contains("SuperAdmin") || role.Contains("DeveloperAdmin")))
                    return Json(new { ok = false, messageType = "error", message = "Only SuperAdmin can manage form access" });

                _FormBuilder.SaveFormAccess(FormId, UserIds, ref status);
                if (status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Form access updated successfully" });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed to update form access" });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, messageType = "error", message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult FormStatus(Int64 FormId)
        {
            int status = 0;
            try
            {
                _FormBuilder.UpdateFormStatus(FormId, ref status);
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
        public JsonResult UpdateFormEnable(Int64 FormId)
        {
            int status = 0;
            try
            {
                _FormBuilder.UpdateFormEnable(FormId, ref status);
                if (status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Form Status updated successfully" });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed to update Form Status" });
            }
            catch
            {
                return Json(new { ok = false, messageType = "error", message = "Failed transaction." });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult DeleteForm(Int64 FormId)
        {
            int status = 0;
            try
            {
                _FormBuilder.DeleteForm(FormId, ref status);
                if (status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Form deleted successfully" });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed to delete form" });
            }
            catch
            {
                return Json(new { ok = false, messageType = "error", message = "Failed transaction." });
            }
        }
        [Authorize]
        [HttpPost]
        public JsonResult DeleteFormSubmission(Int64 SubmissionId)
        {
            int status = 0;
            try
            {
                _FormBuilder.DeleteFormSubmission(SubmissionId, ref status);
                if (status == 1)
                    return Json(new { ok = true, messageType = "success", message = "Record deleted successfully" });
                else
                    return Json(new { ok = false, messageType = "error", message = "Failed to delete Record" });
            }
            catch
            {
                return Json(new { ok = false, messageType = "error", message = "Failed transaction." });
            }
        }

        private static readonly string[] _reservedShortCodes = { "admin", "api", "content", "account", "form", "fill" };

        private static string GenerateShortCode(int length)
        {
            const string alphabet = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789"; // no look-alike chars
            var bytes = new byte[length];
            System.Security.Cryptography.RandomNumberGenerator.Fill(bytes);
            var sb = new System.Text.StringBuilder(length);
            foreach (var b in bytes) sb.Append(alphabet[b % alphabet.Length]);
            return sb.ToString();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetShortUrl(string token)
        {
            int status = 0;
            try
            {
                Int64 formId = BLL.FormLinkObfuscator.DecryptFormId(token);
                string code = _FormBuilder.GetFormShortCode(formId, ref status);
                return Json(new { ok = true, code = code });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }


        [Authorize]
        [HttpPost]
        public JsonResult SaveShortUrl(string token, string code)
        {
            int status = 0;
            try
            {
                Int64 formId = BLL.FormLinkObfuscator.DecryptFormId(token);
                code = (code ?? "").Trim();

                if (code == "")
                {
                    string existing = _FormBuilder.GetFormShortCode(formId, ref status);
                    if (!string.IsNullOrEmpty(existing))
                        return Json(new { ok = true, code = existing });

                    for (int i = 0; i < 10; i++)
                    {
                        string candidate = GenerateShortCode(6);
                        status = 0;
                        _FormBuilder.SaveFormShortUrl(formId, candidate, ref status);
                        if (status == 1) return Json(new { ok = true, code = candidate });
                        if (status == -1) break;
                    }
                    return Json(new { ok = false, message = "Could not generate a short URL. Please try again." });
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(code, "^[A-Za-z0-9_-]{3,30}$"))
                    return Json(new { ok = false, message = "Only letters, numbers, - and _ (3-30 characters) are allowed." });

                if (_reservedShortCodes.Contains(code.ToLower()))
                    return Json(new { ok = false, message = "This short URL is reserved. Please choose another." });

                _FormBuilder.SaveFormShortUrl(formId, code, ref status);
                if (status == 1) return Json(new { ok = true, code = code });
                if (status == 0) return Json(new { ok = false, message = "This short URL is already taken. Please choose another." });
                return Json(new { ok = false, message = "Failed to save short URL." });
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }


        [HttpGet("/form/{code}")]
        public ActionResult ShortRedirect(string code)
        {
            int status = 0;
            Int64 formId = _FormBuilder.GetFormIdByShortCode(code, ref status);

            if (formId > 0)
            {
                string token = BLL.FormLinkObfuscator.EncryptFormId(formId);
                return Redirect(Url.Content("~/Form/" + Uri.EscapeDataString(token)));
            }

            // not a short code -> treat it as the encrypted form token
            try
            {
                BLL.FormLinkObfuscator.DecryptFormId(code);
            }
            catch
            {
                return NotFound();
            }

            return Fill(code);
        }

        [Authorize]
        public ActionResult RenderForm(string id = null)
        {
            Int64 formId;
            try
            {
                formId = BLL.FormLinkObfuscator.DecryptFormId(id);
            }
            catch
            {
                return NotFound();
            }

            int status = 0;
            var form = _FormBuilder.GetFormSchema(formId, ref status);
            if (status != 1 || form == null) return NotFound();

            PopulateRenderFormViewBags(form);

            return View(form);
        }
        public ActionResult Fill(string id)
        {
            Int64 formId;
            try
            {
                formId = BLL.FormLinkObfuscator.DecryptFormId(id);
            }
            catch
            {
                return NotFound();
            }

            int status = 0;
            var form = _FormBuilder.GetFormSchema(formId, ref status);
            if (status != 1 || form == null) return NotFound();

            if (!form.IsFormEnable)
            {
                ViewBag.FormClosed = true;
                return View("RenderForm", form);
            }

            PopulateRenderFormViewBags(form);

            return View("RenderForm", form);
        }

        private void PopulateRenderFormViewBags(Entities.FormBuilder form)
        {
            try
            {
                var fields = System.Text.Json.JsonSerializer.Deserialize<List<System.Text.Json.JsonElement>>(form.FormSchema);
                var paymentField = fields?.FirstOrDefault(f =>
                    f.TryGetProperty("type", out var t) && t.GetString() == "payment");

                if (paymentField.HasValue && paymentField.Value.ValueKind != System.Text.Json.JsonValueKind.Undefined)
                {
                    var pf = paymentField.Value;
                    ViewBag.PaymentEnabled = true;
                    ViewBag.PaymentGateway = pf.TryGetProperty("gateway", out var g) ? g.GetString() : "";
                    ViewBag.PaymentPublicKey = pf.TryGetProperty("publicKey", out var pk) ? pk.GetString() : "";
                    ViewBag.PaymentMode = pf.TryGetProperty("gatewayMode", out var m) ? m.GetString() : "sandbox";
                    ViewBag.PaymentCurrency = pf.TryGetProperty("currency", out var c) ? c.GetString() : "USD";
                }
                else
                {
                    ViewBag.PaymentEnabled = false;
                }

                var captchaField = fields?.FirstOrDefault(f =>
                    f.TryGetProperty("type", out var ct) && ct.GetString() == "captcha");

                if (captchaField.HasValue && captchaField.Value.ValueKind != System.Text.Json.JsonValueKind.Undefined)
                {
                    var cf = captchaField.Value;
                    ViewBag.CaptchaEnabled = true;
                    ViewBag.CaptchaType = cf.TryGetProperty("captchaType", out var cty) ? cty.GetString() : "hcaptcha";
                    ViewBag.CaptchaSiteKey = cf.TryGetProperty("captchaSiteKey", out var csk) ? csk.GetString() : "";
                }
                else
                {
                    ViewBag.CaptchaEnabled = false;
                }
            }
            catch
            {
                ViewBag.PaymentEnabled = false;
                ViewBag.CaptchaEnabled = false;
            }
        }
        [HttpPost]
        public ActionResult SubmitForm(Int64 formId, [FromBody] Entities.FormSubmitRequest request)
        {
            int status = 0;
            Int64 LogId = 0;

            // ✅ ADDED — who is submitting? (filled below from the First Name / Last Name / Email fields)
            string insertedByName = "Anonymous";
            string updatedByName = "Anonymous";

            try
            {
                int formStatus = 0;
                var form = _FormBuilder.GetFormSchema(formId, ref formStatus);
                string formTitle = (formStatus == 1 && form != null && !string.IsNullOrEmpty(form.Title))
                                        ? form.Title : $"Form #{formId}";

                var submittedFields = request?.Data ?? new Dictionary<string, object>();
                string firstName = FindFieldValue(submittedFields, "firstname", "fname", "givenname");
                string lastName = FindFieldValue(submittedFields, "lastname", "lname", "surname", "familyname");
                string emailValue = FindFieldValue(submittedFields, "email");
                string fullName = $"{firstName} {lastName}".Trim();

                insertedByName = !string.IsNullOrWhiteSpace(fullName) ? fullName
                               : (!string.IsNullOrWhiteSpace(emailValue) ? emailValue : "Anonymous");
                updatedByName = !string.IsNullOrWhiteSpace(emailValue) ? emailValue : insertedByName;

                var objLogReport = new Entities.ApplicationLogs
                {
                    LogTitle = formTitle,
                    LogDescription = $"Form Submission Started FormId {formId}",
                    LogDate = DateTime.Now,
                    InsertedBy = insertedByName,
                    InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                    UpdatedBy = updatedByName,
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                };
                _Users.LogReportInsert(objLogReport, ref LogId);

                System.Diagnostics.Debug.WriteLine($"=== SUBMIT FORM CALLED ===");
                System.Diagnostics.Debug.WriteLine($"FormId: {formId}");
                System.Diagnostics.Debug.WriteLine($"Request is null: {request == null}");

                // Sub-log: request null check
                _Users.LogSubReportInsert(new Entities.Logdetails
                {
                    LogId = LogId,
                    LogTitle = "Request Validation",
                    LogDescription = request == null
                                         ? $"Request payload was null. formId: {formId}"
                                         : $"Request payload received. formId: {formId}",
                    LogDate = DateTime.Now,
                    InsertedBy = insertedByName,
                    InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                    UpdatedBy = updatedByName,
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                });

                if (request != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Data count: {request.Data?.Count ?? 0}");
                    System.Diagnostics.Debug.WriteLine($"Payment is null: {request.Payment == null}");

                    if (request.Payment != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Payment Status: {request.Payment.Status}");
                        System.Diagnostics.Debug.WriteLine($"Payment TxnId: {request.Payment.TxnId}");
                        System.Diagnostics.Debug.WriteLine($"Payment Gateway: {request.Payment.Gateway}");
                        System.Diagnostics.Debug.WriteLine($"Payment Amount: {request.Payment.Amount}");
                        System.Diagnostics.Debug.WriteLine($"Payment Currency: {request.Payment.Currency}");

                        // Sub-log: payment details received
                        _Users.LogSubReportInsert(new Entities.Logdetails
                        {
                            LogId = LogId,
                            LogTitle = "Payment Details Received",
                            LogDescription = $"formId: {formId}, Status: {request.Payment.Status}, TxnId: {request.Payment.TxnId}, Gateway: {request.Payment.Gateway}, Amount: {request.Payment.Amount}, Currency: {request.Payment.Currency}",
                            LogDate = DateTime.Now,
                            InsertedBy = insertedByName,
                            InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                            UpdatedBy = updatedByName,
                            UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                        });
                    }
                }

                // ✅ ADDED — bot detection (honeypot + too-fast submission + per-IP rate limit)
                string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                bool isBot = false;
                string botReason = "";

                if (!string.IsNullOrWhiteSpace(request?.HpCheck))
                {
                    isBot = true;
                    botReason = "Honeypot field filled";
                }
                else if (!string.IsNullOrEmpty(request?.FormLoadedAt) &&
                         DateTime.TryParse(request.FormLoadedAt, null,
                             System.Globalization.DateTimeStyles.RoundtripKind, out DateTime loadedAt))
                {
                    var elapsed = DateTime.UtcNow - loadedAt.ToUniversalTime();
                    if (elapsed.TotalSeconds < 2.5)
                    {
                        isBot = true;
                        botReason = $"Submitted too fast ({elapsed.TotalSeconds:0.0}s)";
                    }
                }

                if (!isBot)
                {
                    var now = DateTime.UtcNow;
                    var entry = _ipSubmissions.AddOrUpdate(clientIp,
                        _ => (1, now),
                        (_, old) => (now - old.WindowStart < TimeSpan.FromSeconds(60)) ? (old.Count + 1, old.WindowStart) : (1, now));

                    if (entry.Count > 5)
                    {
                        isBot = true;
                        botReason = $"Rate limit exceeded ({entry.Count} submissions in 60s)";
                    }
                }

                if (isBot)
                {
                    // Bot data is not trusted, so this log keeps "Bot" instead of the typed name/email
                    _Users.LogSubReportInsert(new Entities.Logdetails
                    {
                        LogId = LogId,
                        LogTitle = "Bot Submission Blocked",
                        LogDescription = $"formId: {formId}, IP: {clientIp}, Reason: {botReason}",
                        LogDate = DateTime.Now,
                        InsertedBy = "Bot",
                        InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                        UpdatedBy = "Bot",
                        UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                    });

                    // Fake success — no DB insert, no emails — so the bot doesn't retry/adjust
                    return Json(new { success = true, submissionId = 0 });
                }

                var submittedData = request?.Data ?? new Dictionary<string, object>();
                string json = System.Text.Json.JsonSerializer.Serialize(submittedData);
                string submittedBy = HttpContext.User?.Identity?.Name ?? "anonymous";

                Int64 submissionId = 0;
                string saveError = null;

                try
                {
                    submissionId = _FormBuilder.SaveFormSubmission(
                        formId,
                        json,
                        submittedBy,
                        request?.Payment?.Status,
                        request?.Payment?.TxnId,
                        request?.Payment?.Gateway,
                        request?.Payment?.Amount,
                        request?.Payment?.Currency,
                        ref status);
                }
                catch (Exception saveEx)
                {
                    saveError = saveEx.Message + (saveEx.InnerException != null ? " | Inner: " + saveEx.InnerException.Message : "");
                }

                _Users.LogSubReportInsert(new Entities.Logdetails
                {
                    LogId = LogId,
                    LogTitle = submissionId > 0 ? "form Insert" : "form Insert Failed",
                    LogDescription = submissionId > 0
                        ? $"form inserted successfully. formId: {formId}"
                        : $"form insert failed. formId: {formId}, Status: {status}, DataFields: {submittedData.Count}, Reason: "
                          + (saveError != null ? $"Exception - {saveError}"
                             : status == -1 ? "Stored procedure returned -1 (database insert failed)"
                             : $"Stored procedure returned {status}, no submission id was created"),
                    LogDate = DateTime.Now,
                    InsertedBy = insertedByName,
                    InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                    UpdatedBy = updatedByName,
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                });
                // ✅ ADDED — mail-template send, only runs after a successful save.
                if (submissionId > 0)
                {
                    try
                    {
                        int status1 = 0;

                        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var kvp in submittedData)
                        {
                            fieldValues[kvp.Key] = kvp.Value?.ToString() ?? "";
                        }

                        if (request?.Payment != null)
                        {
                            if (!string.IsNullOrEmpty(request.Payment.Status)) fieldValues["PaymentStatus"] = request.Payment.Status;
                            if (!string.IsNullOrEmpty(request.Payment.TxnId)) fieldValues["PaymentTxnId"] = request.Payment.TxnId;
                            if (!string.IsNullOrEmpty(request.Payment.Gateway)) fieldValues["PaymentGateway"] = request.Payment.Gateway;
                            if (request.Payment.Amount.HasValue) fieldValues["PaymentAmount"] = request.Payment.Amount.Value.ToString("0.00");
                            if (!string.IsNullOrEmpty(request.Payment.Currency)) fieldValues["PaymentCurrency"] = request.Payment.Currency;
                        }
                        fieldValues["SubmissionId"] = submissionId.ToString();
                        fieldValues["SubmittedDate"] = DateTime.Now.ToString("MM/dd/yyyy");

                        string submitterEmail = FindEmailValue(fieldValues);
                        string siteBaseUrl = $"{Request.Scheme}://{Request.Host}";

                        var objMailTemplates = _mailTemplatesBLL.GetMailTemplateByFormId(formId, ref status1);
                        //string adminEmail = objMailTemplates?.BCC;

                        var templates = _mailTemplatesBLL.GetMailTemplatesByFormId(formId, ref status1)
                                        ?? new List<Entities.MailTemplates>();

                        var adminTemplate = templates.FirstOrDefault(t => t != null && t.MailTemplateId != 0 &&
                            (t.MailType ?? "").Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase));
                        string adminEmail = adminTemplate?.BCC;
                        var userTemplate = templates.FirstOrDefault(t => t != null && t.MailTemplateId != 0 &&
                            !(t.MailType ?? "").Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase));

                        // Sub-log: mail template lookup
                        _Users.LogSubReportInsert(new Entities.Logdetails
                        {
                            LogId = LogId,
                            LogTitle = "Mail Template Process",
                            LogDescription = objMailTemplates != null
                                                 ? $"Mail template found for formId: {formId}. AdminEmail: {adminEmail}, UserEmail: {submitterEmail}"
                                                 : $"No mail template found for formId: {formId}",
                            LogDate = DateTime.Now,
                            InsertedBy = insertedByName,
                            InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                            UpdatedBy = updatedByName,
                            UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                        });

                        // ===================== ADMIN MAIL =====================
                        if (form != null && form.IsAdminMail == true && adminTemplate != null && !string.IsNullOrEmpty(adminEmail))
                        {
                            string body = _mailTemplatesBLL.BuildBodyFromTemplate(adminTemplate, fieldValues, siteBaseUrl);
                            string response = _sentmail.SendMailSendinbrevoadmin(adminEmail, adminTemplate.Subject, body);
                            bool sent = !string.IsNullOrEmpty(response) && response.Contains("messageId");

                            // Sub-log: admin mail result (sent / failed + reason)
                            _Users.LogSubReportInsert(new Entities.Logdetails
                            {
                                LogId = LogId,
                                LogTitle = sent ? "Admin Mail Sent" : "Admin Mail Failed",
                                LogDescription = sent
                                    ? $"Admin mail sent for formId: {formId}, To: {adminEmail}"
                                    : $"Admin mail failed for formId: {formId}, To: {adminEmail}, Reason: {(string.IsNullOrEmpty(response) ? "Empty response from mail server" : response)}",
                                LogDate = DateTime.Now,
                                InsertedBy = insertedByName,
                                InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                                UpdatedBy = updatedByName,
                                UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                            });
                        }
                        else
                        {
                            // Sub-log: admin mail skipped (reason)
                            _Users.LogSubReportInsert(new Entities.Logdetails
                            {
                                LogId = LogId,
                                LogTitle = "Admin Mail Skipped",
                                LogDescription = $"Admin mail not sent for formId: {formId}. Reason: "
                                                 + (form == null || form.IsAdminMail != true ? "Admin mail is turned off for this form. "
                                                    : adminTemplate == null ? "No Admin mail template found. "
                                                    : "Admin email  is empty. "),
                                LogDate = DateTime.Now,
                                InsertedBy = insertedByName,
                                InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                                UpdatedBy = updatedByName,
                                UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                            });
                        }

                        // ===================== USER MAIL =====================
                        if (form != null && form.IsActive == true && userTemplate != null && !string.IsNullOrEmpty(submitterEmail))
                        {
                            string body = _mailTemplatesBLL.BuildBodyFromTemplate(userTemplate, fieldValues, siteBaseUrl);
                            string response = _sentmail.SendMailSendinbrevouser(submitterEmail, userTemplate.Subject, body);
                            bool sent = !string.IsNullOrEmpty(response) && response.Contains("messageId");

                            // Sub-log: user mail result (sent / failed + reason)
                            _Users.LogSubReportInsert(new Entities.Logdetails
                            {
                                LogId = LogId,
                                LogTitle = sent ? "User Mail Sent" : "User Mail Failed",
                                LogDescription = sent
                                    ? $"User mail sent for formId: {formId}, To: {submitterEmail}"
                                    : $"User mail failed for formId: {formId}, To: {submitterEmail}, Reason: {(string.IsNullOrEmpty(response) ? "Empty response from mail server" : response)}",
                                LogDate = DateTime.Now,
                                InsertedBy = insertedByName,
                                InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                                UpdatedBy = updatedByName,
                                UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                            });
                        }
                        else
                        {
                            // Sub-log: user mail skipped (reason)
                            _Users.LogSubReportInsert(new Entities.Logdetails
                            {
                                LogId = LogId,
                                LogTitle = "User Mail Skipped",
                                LogDescription = $"User mail not sent for formId: {formId}. Reason: "
                                                 + (form == null || form.IsActive != true ? "Form is not active. "
                                                    : userTemplate == null ? "No User mail template found. "
                                                    : "Submitter email not found in submitted data. "),
                                LogDate = DateTime.Now,
                                InsertedBy = insertedByName,
                                InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                                UpdatedBy = updatedByName,
                                UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                            });
                        }
                    }
                    catch (Exception mailEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"[SubmitForm] Mail send failed: {mailEx.Message}");

                        // Sub-log: mail send exception
                        _Users.LogSubReportInsert(new Entities.Logdetails
                        {
                            LogId = LogId,
                            LogTitle = "Mail Send Failed",
                            LogDescription = $"Mail send threw an exception for formId: {formId}, Error: {mailEx.Message}",
                            LogDate = DateTime.Now,
                            InsertedBy = insertedByName,
                            InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                            UpdatedBy = updatedByName,
                            UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                        });
                    }
                }
                else
                {
                    // Sub-log: submission failed, mail block skipped entirely
                    _Users.LogSubReportInsert(new Entities.Logdetails
                    {
                        LogId = LogId,
                        LogTitle = "Mail Skipped - No Submission",
                        LogDescription = $"submissionId was 0 or less for formId: {formId}. Mail block not executed.",
                        LogDate = DateTime.Now,
                        InsertedBy = insertedByName,
                        InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                        UpdatedBy = updatedByName,
                        UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                    });
                }

                return Json(new { success = submissionId > 0, submissionId = submissionId });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in SubmitForm: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                // Sub-log: top-level exception
                _Users.LogSubReportInsert(new Entities.Logdetails
                {
                    LogId = LogId,
                    LogTitle = "SubmitForm Exception",
                    LogDescription = $"Unhandled exception in SubmitForm. formId: {formId}. Error: {ex.Message}",
                    LogDate = DateTime.Now,
                    InsertedBy = insertedByName,
                    InsertedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime(),
                    UpdatedBy = updatedByName,
                    UpdatedDate = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime()
                });

                return Json(new { success = false, message = ex.Message });
            }
        }

        private static string FindFieldValue(Dictionary<string, object> data, params string[] hints)
        {
            if (data == null) return "";

            foreach (var kvp in data)
            {
                // "First Name" / "first_name" / "First-Name" all become "firstname"
                string key = new string((kvp.Key ?? "").Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
                if (key == "") continue;

                foreach (var hint in hints)
                {
                    if (key == hint || key.Contains(hint))
                    {
                        string val = kvp.Value?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(val)) return val;
                    }
                }
            }
            return "";
        }
        private string FindEmailValue(Dictionary<string, string> fieldValues)
        {
            string[] commonEmailKeys = { "Email", "EmailAddress", "UserEmail", "ContactEmail" };
            foreach (var key in commonEmailKeys)
            {
                if (fieldValues.TryGetValue(key, out string val) && !string.IsNullOrWhiteSpace(val))
                    return val;
            }
            return null;
        }
        public class FormFieldInfo
        {
            public string Id { get; set; }
            public string Label { get; set; }
        }
        [Authorize]
        public ActionResult FormSubmissions(string formId, string Search = "", string SortColumn = "SubmittedDate", string SortOrder = "DESC", int PageNo = 1, int Items = 25, Int64 mid = 0)
        {
            Int64 realFormId;
            try
            {
                realFormId = BLL.FormLinkObfuscator.DecryptFormId(formId);
            }
            catch
            {
                return NotFound();
            }

            int status = 0;
            var form = _FormBuilder.GetFormSchema(realFormId, ref status);
            if (status != 1 || form == null) return NotFound();

            var schemaFields = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(form.FormSchema);

            // ✅ ADDED
            ViewBag.HasPayment = schemaFields != null && schemaFields.Any(f =>
                f.ContainsKey("type") &&
                string.Equals(f["type"]?.ToString(), "payment", StringComparison.OrdinalIgnoreCase));
            string[] desiredLabels = { "First Name", "Last Name", "Email", "Phone Number" };

            var allFields = schemaFields
       .Where(f => f.ContainsKey("type") && f["type"].ToString() != "heading" && f["type"].ToString() != "paragraph")

       .Where(f => f.ContainsKey("label") && f["label"] != null && !string.IsNullOrWhiteSpace(f["label"].ToString()))
       .Select(f => new FormFieldInfo
       {

           Id = f.ContainsKey("id") ? f["id"].ToString() : f["label"].ToString(),
           Label = f["label"].ToString()
       })
       .ToList();

            var summaryFields = desiredLabels
                .Select(label => allFields.FirstOrDefault(f => string.Equals(f.Label, label, StringComparison.OrdinalIgnoreCase)))
                .Where(f => f != null)
                .Select(f => (dynamic)f)
                .ToList();

            string[] nonExportableTypes = { "heading", "paragraph", "image", "fileupload", "captcha", "payment" };
            var exportableFields = schemaFields
                .Where(f => f.ContainsKey("type") && !nonExportableTypes.Contains(f["type"].ToString(), StringComparer.OrdinalIgnoreCase))
                .Where(f => f.ContainsKey("label") && f["label"] != null && !string.IsNullOrWhiteSpace(f["label"].ToString()))
                .Select(f => new FormFieldInfo { Id = f.ContainsKey("id") ? f["id"].ToString() : f["label"].ToString(), Label = f["label"].ToString() })
                .Select(f => (dynamic)f)
                .ToList();

            ViewBag.FormId = realFormId;
            ViewBag.FormTitle = form.Title;
            ViewBag.SummaryFields = summaryFields;
            ViewBag.AllFormFields = exportableFields;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            int Total = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "SubmittedDate DESC");

            var submissions = _FormBuilder.GetFormSubmissionsList(realFormId, Search, Sort, PageNo, Items, ref Total);
            long userId = 0;

            var userIdStr = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(userIdStr))
            {
                long.TryParse(userIdStr, out userId);
            }
            Entities.UserRoles objuserroles = new();
            try
            {
                objuserroles = _Users.GetRoleDetialsById(userId, mid, ref status);

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

            ViewBag.lstSubmissions = submissions;
            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;

            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetSubmissionDetail(Int64 id)
        {
            try
            {
                int status = 0;

                var submission = _FormBuilder.GetFormSubmissionDetail(id, ref status);

                if (submission == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Submission not found."
                    });
                }

                return Json(new
                {
                    success = true,

                    submissionId = submission.SubmissionId,
                    formId = submission.FormId,
                    submittedBy = submission.SubmittedBy,
                    submittedDate = submission.SubmittedDate.ToString("dd/MM/yyyy hh:mm tt"),
                    data = submission.SubmittedData,

                    // Payment details
                    paymentStatus = submission.PaymentStatus,
                    paymentTxnId = submission.PaymentTxnId,
                    paymentGateway = submission.PaymentGateway,
                    paymentAmount = submission.PaymentAmount,
                    paymentCurrency = submission.PaymentCurrency
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        public ActionResult ThankYou(string id)
        {
            Int64 formId;
            try
            {
                formId = BLL.FormLinkObfuscator.DecryptFormId(id);
            }
            catch
            {
                return NotFound();
            }

            int status = 0;
            var form = _FormBuilder.GetFormSchema(formId, ref status);
            if (status != 1 || form == null) return NotFound();

            ViewBag.LogoUrl = form.LogoUrl;
            ViewBag.Title = form.Title;
            ViewBag.FormId = form.FormId;
            ViewBag.ThankYouContent = form.ThankYouContent;
            return View();
        }
        [Authorize]
        public IActionResult FormSubmissionsExportToExcel(string search = "", string sortColumn = "SubmittedDate", string sortOrder = "DESC", long formId = 0, string fields = "", Int64 mid = 0)
        {
            try
            {
                string sort = !string.IsNullOrEmpty(sortColumn)
                                ? $"{sortColumn} {sortOrder}"
                                : "";

                DataTable dtRaw = _DFormSubmissions.FormSubmissionsExportToExcel(search, sort, formId);

                if (dtRaw == null || dtRaw.Rows.Count == 0)
                {
                    TempData["message"] = "Export failed: No records found.";
                    return RedirectToAction("FormSubmissions", new { formId = BLL.FormLinkObfuscator.EncryptFormId(formId), mid = mid });
                }

                // ✅ ADDED - form లో payment field ఉందా లేదా
                bool hasPayment = false;
                string formTitle = "Form";
                var schemaFieldLabels = new List<string>();

                try
                {
                    int schemaStatus = 0;
                    var formForSchema = _FormBuilder.GetFormSchema(formId, ref schemaStatus);

                    if (formForSchema != null)
                    {
                        if (!string.IsNullOrEmpty(formForSchema.Title))
                            formTitle = formForSchema.Title;

                        if (!string.IsNullOrEmpty(formForSchema.FormSchema))
                        {
                            var schemaFields = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(formForSchema.FormSchema);
                            if (schemaFields != null)
                            {
                                // ✅ ADDED - payment field check
                                hasPayment = schemaFields.Any(f =>
                                    f.TryGetValue("type", out var pt) &&
                                    string.Equals(pt?.ToString(), "payment", StringComparison.OrdinalIgnoreCase));

                                string[] nonExportableTypes = { "heading", "paragraph", "image", "fileupload", "captcha", "payment" };

                                foreach (var f in schemaFields)
                                {
                                    string type = f.TryGetValue("type", out var t) ? t?.ToString() : null;
                                    string label = f.TryGetValue("label", out var l) ? l?.ToString() : null;

                                    if (string.IsNullOrWhiteSpace(label)) continue;
                                    if (nonExportableTypes.Contains(type, StringComparer.OrdinalIgnoreCase)) continue;

                                    if (!schemaFieldLabels.Contains(label, StringComparer.OrdinalIgnoreCase))
                                        schemaFieldLabels.Add(label);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                HashSet<string> selectedFields = null;
                if (!string.IsNullOrWhiteSpace(fields))
                {
                    selectedFields = new HashSet<string>(
                        fields.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(f => f.Trim()),
                        StringComparer.OrdinalIgnoreCase);
                }

                bool includeSubmissionId = selectedFields == null || selectedFields.Contains("Submission Id");
                bool includeSubmittedBy = selectedFields == null || selectedFields.Contains("Submitted By");
                bool includeSubmittedDate = selectedFields == null || selectedFields.Contains("Submitted Date");

                var exportFieldLabels = schemaFieldLabels
                    .Where(label => selectedFields == null || selectedFields.Contains(label))
                    .ToList();

                // ✅ CHANGED - payment flags: hasPayment true అయితేనే
                bool includePaymentGateway = hasPayment && (selectedFields == null || selectedFields.Contains("Payment Gateway"));
                bool includePaymentStatus = hasPayment && (selectedFields == null || selectedFields.Contains("Payment Status"));
                bool includePaymentAmount = hasPayment && (selectedFields == null || selectedFields.Contains("Amount"));
                bool includePaymentTxnId = hasPayment && (selectedFields == null || selectedFields.Contains("Transaction ID"));

                DataTable dtFinal = new DataTable();

                if (includeSubmissionId)
                    dtFinal.Columns.Add("Submission Id", typeof(string));
                if (includeSubmittedBy)
                    dtFinal.Columns.Add("Submitted By", typeof(string));
                if (includeSubmittedDate)
                    dtFinal.Columns.Add("Submitted Date", typeof(string));

                foreach (var label in exportFieldLabels)
                {
                    if (!dtFinal.Columns.Contains(label))
                        dtFinal.Columns.Add(label, typeof(string));
                }

                if (includePaymentStatus && !dtFinal.Columns.Contains("Payment Status"))
                    dtFinal.Columns.Add("Payment Status", typeof(string));
                if (includePaymentTxnId && !dtFinal.Columns.Contains("Payment Txn Id"))
                    dtFinal.Columns.Add("Payment Txn Id", typeof(string));
                if (includePaymentGateway && !dtFinal.Columns.Contains("Payment Gateway"))
                    dtFinal.Columns.Add("Payment Gateway", typeof(string));
                if (includePaymentAmount && !dtFinal.Columns.Contains("Payment Amount"))
                    dtFinal.Columns.Add("Payment Amount", typeof(string));

                if (dtFinal.Columns.Count == 0)
                {
                    TempData["message"] = "Export failed: No fields selected.";
                    return RedirectToAction("FormSubmissions", new { formId = BLL.FormLinkObfuscator.EncryptFormId(formId), mid = mid });
                }

                foreach (DataRow row in dtRaw.Rows)
                {
                    string jsonData = row["SubmittedData"].ToString();

                    var rawSubmissionData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                    var submissionData = rawSubmissionData != null
                        ? new Dictionary<string, object>(rawSubmissionData, StringComparer.OrdinalIgnoreCase)
                        : null;

                    DataRow newRow = dtFinal.NewRow();

                    if (includeSubmissionId)
                        newRow["Submission Id"] = row["SubmissionId"];
                    if (includeSubmittedBy)
                        newRow["Submitted By"] = row["SubmittedBy"];
                    if (includeSubmittedDate)
                        newRow["Submitted Date"] = Convert.ToDateTime(row["SubmittedDate"]).ToString("dd/MM/yyyy hh:mm tt");

                    foreach (var label in exportFieldLabels)
                    {
                        if (submissionData != null && submissionData.TryGetValue(label, out var val))
                            newRow[label] = val?.ToString() ?? "";
                        else
                            newRow[label] = "";
                    }

                    if (includePaymentStatus)
                        newRow["Payment Status"] = row["PaymentStatus"] != DBNull.Value ? row["PaymentStatus"].ToString() : "";
                    if (includePaymentTxnId)
                        newRow["Payment Txn Id"] = row["PaymentTxnId"] != DBNull.Value ? row["PaymentTxnId"].ToString() : "";
                    if (includePaymentGateway)
                        newRow["Payment Gateway"] = row["PaymentGateway"] != DBNull.Value ? row["PaymentGateway"].ToString() : "";
                    if (includePaymentAmount)
                        newRow["Payment Amount"] = row["PaymentAmount"] != DBNull.Value ? row["PaymentAmount"].ToString() : "";

                    dtFinal.Rows.Add(newRow);
                }

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("FormSubmissions-Export");

                    for (int col = 0; col < dtFinal.Columns.Count; col++)
                    {
                        var headerCell = worksheet.Cell(1, col + 1);
                        headerCell.Value = dtFinal.Columns[col].ColumnName;
                        headerCell.Style.Font.Bold = true;
                        headerCell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromArgb(173, 216, 230);
                    }

                    for (int row = 0; row < dtFinal.Rows.Count; row++)
                    {
                        for (int col = 0; col < dtFinal.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = dtFinal.Rows[row][col]?.ToString() ?? "";
                        }
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        // ✅ CHANGED - GetFormSchema మళ్ళీ call చేయకుండా పైన తీసుకున్న formTitle వాడాం
                        string safeTitle = string.Join("_", formTitle.Split(Path.GetInvalidFileNameChars()));
                        string fileName = $"FormSubmissions-{safeTitle}-{DateTime.UtcNow:dd-MM-yyyy}.xlsx";

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            fileName
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = $"Export failed: {ex.Message}";
                return RedirectToAction("FormSubmissions", new { formId = BLL.FormLinkObfuscator.EncryptFormId(formId), mid = mid });
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

                var userRoles = userRole
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Trim())
                    .ToList();

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