using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class MailTemplates
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.MailTemplates _MailTemplates = new AWSFormBuilder.Areas.FormBuilder.DAL.MailTemplates();


        SendMail _sentmail = new SendMail();

        #region Methods

        public Int64 InsertMailTemplates(Entities.MailTemplates objMailTemplates)
        {
            Int64 _status = 0;
            if (objMailTemplates != null)
            {
                _status = _MailTemplates.InsertMailTemplates(objMailTemplates);
            }
            return _status;
        }

        public Int64 DeleteMailTemplate(Int64 MailTemplateId)
        {
            Int64 _status = 0;
            _status = _MailTemplates.DeleteMailTemplate(MailTemplateId);
            return _status;
        }

        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.MailTemplates> GetMailTemplatesList(string MailType, ref int status)
        {
            List<AWSFormBuilder.Entities.MailTemplates> lstMailTemplates = new List<Entities.MailTemplates>();
            DataTable dt = _MailTemplates.GetMailTemplatesList(MailType, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.MailTemplates objlstMailTemplates = new AWSFormBuilder.Entities.MailTemplates();

                    objlstMailTemplates.MailTemplateId = Convert.ToInt64(dr["MailTemplateId"].ToString());
                    objlstMailTemplates.Heading = dr["Heading"].ToString();
                    objlstMailTemplates.Subject = dr["Subject"].ToString();
                    objlstMailTemplates.Description = dr["Description"].ToString();
                    objlstMailTemplates.MailType = (dr["MailType"] != DBNull.Value ? dr["MailType"].ToString() : "");
                    objlstMailTemplates.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstMailTemplates.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstMailTemplates.Add(objlstMailTemplates);
                }
            }
            return lstMailTemplates;
        }
        // ✅ ADDED — used by the "Admin Mail" / "User Mail" buttons on FormsList
        public Entities.MailTemplates GetMailTemplateByFormIdAndType(Int64 FormId, string MailType, ref int status)
        {
            Entities.MailTemplates obj = new Entities.MailTemplates();
            DataTable dt = _MailTemplates.GetMailTemplateByFormIdAndType(FormId, MailType, ref status);

            if (status == 1 && dt != null && dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                obj.MailTemplateId = Convert.ToInt64(row["MailTemplateId"]);
                obj.Heading = row["Heading"].ToString();
                obj.Subject = row["Subject"].ToString();
                obj.Description = row["Description"].ToString();
                obj.MailType = (row["MailType"] != DBNull.Value ? row["MailType"].ToString() : "");
                obj.LogoUrl = (dt.Columns.Contains("LogoUrl") && row["LogoUrl"] != DBNull.Value) ? row["LogoUrl"].ToString() : null;
                obj.BCC = (dt.Columns.Contains("BCC") && row["BCC"] != DBNull.Value) ? row["BCC"].ToString() : null;
                obj.FormIds = (dt.Columns.Contains("FormIds") && row["FormIds"] != DBNull.Value) ? row["FormIds"].ToString() : null;
                obj.SelectedFormIds = string.IsNullOrEmpty(obj.FormIds)
                    ? new List<Int64>()
                    : obj.FormIds.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Convert.ToInt64(s.Trim())).ToList();
            }
            return obj;
        }
        public AWSFormBuilder.Entities.MailTemplates GetMailTemplateById(string TemplateName, Int64 MailTemplateId, ref int status)
        {
            AWSFormBuilder.Entities.MailTemplates objMailTemplates = new AWSFormBuilder.Entities.MailTemplates();
            DataTable dt = new DataTable();
            dt = _MailTemplates.GetMailTemplateById(TemplateName, MailTemplateId, ref status);
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];

                objMailTemplates.MailTemplateId = Convert.ToInt64(row["MailTemplateId"].ToString());
                objMailTemplates.Heading = row["Heading"].ToString();
                objMailTemplates.Subject = row["Subject"].ToString();
                objMailTemplates.Description = row["Description"].ToString();
                objMailTemplates.MailType = (row["MailType"] != DBNull.Value ? row["MailType"].ToString() : "");
                objMailTemplates.UpdatedBy = row["UpdatedBy"].ToString();
                objMailTemplates.UpdatedTime = Convert.ToDateTime(row["UpdatedTime"].ToString());
                objMailTemplates.BCC = (dt.Columns.Contains("BCC") && row["BCC"] != DBNull.Value)
    ? row["BCC"].ToString() : null; // ✅ ADDED
                objMailTemplates.LogoUrl = (dt.Columns.Contains("LogoUrl") && row["LogoUrl"] != DBNull.Value)
                    ? row["LogoUrl"].ToString() : null;

                objMailTemplates.FormIds = (dt.Columns.Contains("FormIds") && row["FormIds"] != DBNull.Value)
                    ? row["FormIds"].ToString() : null;

                objMailTemplates.SelectedFormIds = string.IsNullOrEmpty(objMailTemplates.FormIds)
                    ? new List<Int64>()
                    : objMailTemplates.FormIds.Split(',')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => Convert.ToInt64(s.Trim()))
                        .ToList();
            }
            return objMailTemplates;
        }

        public List<AWSFormBuilder.Entities.MailTemplates> GetMailTemplatesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.MailTemplates> lstMailTemplates = new List<AWSFormBuilder.Entities.MailTemplates>();
            DataTable dt = _MailTemplates.GetMailTemplatesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _MailTemplates.GetMailTemplatesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.MailTemplates objMailTemplates = new AWSFormBuilder.Entities.MailTemplates();

                    objMailTemplates.RId = Convert.ToInt64(dr["RId"].ToString());
                    objMailTemplates.MailTemplateId = Convert.ToInt64(dr["MailTemplateId"].ToString());
                    objMailTemplates.Heading = dr["Heading"].ToString();
                    objMailTemplates.Subject = dr["Subject"].ToString();
                    objMailTemplates.Description = dr["Description"].ToString();
                    objMailTemplates.MailType = (dr["MailType"] != DBNull.Value ? dr["MailType"].ToString() : "");
                    objMailTemplates.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objMailTemplates.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                    lstMailTemplates.Add(objMailTemplates);
                }
            }
            return lstMailTemplates;
        }


        public List<Entities.FormListItem> GetFormsListForDropdown(List<Int64> selectedFormIds, ref int status)
        {
            List<Entities.FormListItem> lstForms = new List<Entities.FormListItem>();
            DataTable dt = _MailTemplates.GetFormsListForDropdown(ref status);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Int64 formId = Convert.ToInt64(dr["FormId"]);
                    lstForms.Add(new Entities.FormListItem
                    {
                        FormId = formId,
                        Title = dr["Title"].ToString(),
                        IsSelected = selectedFormIds != null && selectedFormIds.Contains(formId)
                    });
                }
            }
            return lstForms;
        }

        public Entities.MailTemplates GetMailTemplateByFormId(Int64 FormId, ref int status)
        {
            Entities.MailTemplates objMailTemplates = new Entities.MailTemplates();
            DataTable dt = _MailTemplates.GetMailTemplateByFormId(FormId, ref status);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    objMailTemplates.MailTemplateId = Convert.ToInt64(row["MailTemplateId"]);
                    objMailTemplates.Heading = row["Heading"].ToString();
                    objMailTemplates.Subject = row["Subject"].ToString();
                    objMailTemplates.Description = row["Description"].ToString();
                    objMailTemplates.MailType = (row["MailType"] != DBNull.Value ? row["MailType"].ToString() : "");
                    objMailTemplates.LogoUrl = (row["LogoUrl"] != DBNull.Value ? row["LogoUrl"].ToString() : null);
                    objMailTemplates.BCC = (row["BCC"] != DBNull.Value ? row["BCC"].ToString() : null);
                }
            }
            return objMailTemplates;
        }


        public List<Entities.MailTemplates> GetMailTemplatesByFormId(Int64 FormId, ref int status)
        {
            List<Entities.MailTemplates> lstTemplates = new List<Entities.MailTemplates>();
            DataTable dt = _MailTemplates.GetMailTemplateByFormId(FormId, ref status);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Entities.MailTemplates obj = new Entities.MailTemplates();
                    obj.MailTemplateId = Convert.ToInt64(row["MailTemplateId"]);
                    obj.Heading = row["Heading"].ToString();
                    obj.Subject = row["Subject"].ToString();
                    obj.Description = row["Description"].ToString();
                    obj.MailType = (row["MailType"] != DBNull.Value ? row["MailType"].ToString() : "");
                    obj.LogoUrl = (row["LogoUrl"] != DBNull.Value ? row["LogoUrl"].ToString() : null);
                    obj.BCC = (row["BCC"] != DBNull.Value ? row["BCC"].ToString() : null);
                    lstTemplates.Add(obj);
                }
            }
            return lstTemplates;
        }

        public string BuildBodyFromTemplate(Entities.MailTemplates template, Dictionary<string, string> fieldValues, string siteBaseUrl = null)
        {
            if (template == null || string.IsNullOrEmpty(template.Description))
                return string.Empty;

            string body = template.Description;

            if (fieldValues != null)
            {
                foreach (var kvp in fieldValues)
                {
                    if (string.IsNullOrWhiteSpace(kvp.Key)) continue;

                    string placeholder = "[" + kvp.Key + "]";
                    body = Regex.Replace(
                        body,
                        Regex.Escape(placeholder),
                        (kvp.Value ?? "").Replace("$", "$$"),
                        RegexOptions.IgnoreCase);
                }
            }

            string logoUrl = template.LogoUrl;
            if (!string.IsNullOrEmpty(logoUrl) && logoUrl.StartsWith("/") && !string.IsNullOrEmpty(siteBaseUrl))
            {
                logoUrl = siteBaseUrl.TrimEnd('/') + logoUrl;
            }

            body = Regex.Replace(body, Regex.Escape("[MailLogo]"), (logoUrl ?? "").Replace("$", "$$"), RegexOptions.IgnoreCase);

            // ✅ ADDED — any placeholder that had no matching value (e.g. [PaymentAmount] on a
            // form with no payment field) is stripped out instead of showing the raw [Bracket] text
            body = Regex.Replace(body, @"\[[A-Za-z0-9_ ]+\]", "");

            return body;
        }

        public bool SendTemplateMailForForm(Int64 formId, Dictionary<string, string> fieldValues, string toEmail, string ccAdminEmail, string siteBaseUrl = null)
        {
            int status = 0;
            int status1 = 0;
            BLL.FormBuilder _FormBuilder = new BLL.FormBuilder();

            List<Entities.MailTemplates> templates = GetMailTemplatesByFormId(formId, ref status);
            if (templates == null || templates.Count == 0)
                return false;

            var formtemplates = _FormBuilder.GetFormSchema(formId, ref status1);
            if (formtemplates == null)
                return false;

            bool anySent = false;

            var adminTemplate = templates.FirstOrDefault(t =>
                t != null && t.MailTemplateId != 0 &&
                (t.MailType ?? "").Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase));

            var userTemplate = templates.FirstOrDefault(t =>
                t != null && t.MailTemplateId != 0 &&
                !(t.MailType ?? "").Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase));

            // Admin mail
            if (formtemplates.IsAdminMail == true && adminTemplate != null && !string.IsNullOrEmpty(ccAdminEmail))
            {
                string body = BuildBodyFromTemplate(adminTemplate, fieldValues, siteBaseUrl);
                string response = _sentmail.SendMailSendinbrevo(ccAdminEmail, adminTemplate.Subject, body, null);
                anySent |= IsBrevoSuccess(response);
            }

            // User mail
            if (formtemplates.IsActive == true && userTemplate != null && !string.IsNullOrEmpty(toEmail))
            {
                string body = BuildBodyFromTemplate(userTemplate, fieldValues, siteBaseUrl);
                string response = _sentmail.SendMailSendinbrevo(toEmail, userTemplate.Subject, body, null);
                anySent |= IsBrevoSuccess(response);
            }

            return anySent;
        }
        private bool IsBrevoSuccess(string brevoResponse)
        {
            return !string.IsNullOrEmpty(brevoResponse) && brevoResponse.Contains("messageId");
        }

        // ✅ ADDED — used by the "Sample Send Mail" button on the Add/Edit Mail Template pages.
        // Sends whatever Subject/Description is currently in the form (as-is, placeholders included)
        // to the single email address the admin types in, so they can eyeball the real rendered mail.
        public bool SendSampleMail(string toEmail, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(toEmail) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
                return false;

            string response = _sentmail.SendMailSendinbrevo(toEmail.Trim(), subject, body, null);
            return IsBrevoSuccess(response);
        }

        #endregion
    }
}
