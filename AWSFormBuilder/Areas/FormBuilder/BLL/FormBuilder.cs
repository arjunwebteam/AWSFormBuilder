using AWSFormBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class FormBuilder
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.FormBuilder _FormBuilder = new AWSFormBuilder.Areas.FormBuilder.DAL.FormBuilder();
        public Int64 SaveFormSchema(FormSaveRequest request, string createdBy, Int64 chapterId, ref int status)
        {
            Int64 newFormId = 0;
            newFormId = _FormBuilder.SaveFormSchema(request.FormId, request.Title, request.Schema, chapterId, createdBy, request.LogoUrl, request.LogoWidth, request.LogoHeight, request.Design, request.Conditions, ref status);
            return newFormId;
        }

        public List<Entities.FormBuilder> GetFormlist(ref int status)
        {
            List<Entities.FormBuilder> objFormlist = new List<Entities.FormBuilder>();
            DataTable dt = _FormBuilder.GetFormlist(ref status);

            if (status == 1 && dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    objFormlist.Add(new Entities.FormBuilder
                    {
                        FormId = Convert.ToInt64(row["FormId"]),
                        Title = row["Title"].ToString(),
                        FormSchema = row["FormSchema"].ToString()
                    });
                }
            }

            return objFormlist;
        }
        public Entities.FormBuilder GetFormSchema(Int64 formId, ref int status)
        {
            DataTable dt = _FormBuilder.GetFormSchema(formId, ref status);
     
            Entities.FormBuilder objFormBuilder = new Entities.FormBuilder();
            if (status == 1 && dt.Rows.Count == 1)
            {
                objFormBuilder.FormId = Convert.ToInt64(dt.Rows[0]["FormId"]);
                objFormBuilder.Title = dt.Rows[0]["Title"].ToString();
                objFormBuilder.FormSchema = dt.Rows[0]["FormSchema"].ToString();
                objFormBuilder.LogoUrl = (dt.Rows[0].Table.Columns.Contains("LogoUrl") && dt.Rows[0]["LogoUrl"] != DBNull.Value) ? dt.Rows[0]["LogoUrl"].ToString() : null;
                objFormBuilder.LogoWidth = (dt.Rows[0].Table.Columns.Contains("LogoWidth") && dt.Rows[0]["LogoWidth"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[0]["LogoWidth"]) : (int?)null;
                objFormBuilder.LogoHeight = (dt.Rows[0].Table.Columns.Contains("LogoHeight") && dt.Rows[0]["LogoHeight"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[0]["LogoHeight"]) : (int?)null;

                objFormBuilder.DesignJson = (dt.Rows[0].Table.Columns.Contains("DesignJson") && dt.Rows[0]["DesignJson"] != DBNull.Value) ? dt.Rows[0]["DesignJson"].ToString() : null;
                objFormBuilder.ConditionsJson = (dt.Rows[0].Table.Columns.Contains("ConditionsJson") && dt.Rows[0]["ConditionsJson"] != DBNull.Value) ? dt.Rows[0]["ConditionsJson"].ToString() : null;  // ✅ ADDED
                objFormBuilder.ThankYouContent = (dt.Rows[0].Table.Columns.Contains("ThankYouContent") && dt.Rows[0]["ThankYouContent"] != DBNull.Value) ? dt.Rows[0]["ThankYouContent"].ToString() : null;
                objFormBuilder.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                objFormBuilder.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                objFormBuilder.IsFormEnable = Convert.ToBoolean(dt.Rows[0]["IsFormEnable"]);
                objFormBuilder.IsAdminMail = Convert.ToBoolean(dt.Rows[0]["IsAdminMail"]);
                objFormBuilder.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                objFormBuilder.ModifiedDate = (dt.Rows[0]["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["ModifiedDate"]) : (DateTime?)null);
            }
            return objFormBuilder;
        }

        public bool FormHasPaymentField(string formSchemaJson)
        {
            if (string.IsNullOrWhiteSpace(formSchemaJson)) return false;

            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(formSchemaJson);
                if (doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    foreach (var field in doc.RootElement.EnumerateArray())
                    {
                        if (field.TryGetProperty("type", out var t) &&
                            string.Equals(t.GetString(), "payment", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {

            }
            return false;
        }
        public List<string> GetFormFieldLabels(string formSchemaJson)
        {
            var labels = new List<string>();
            if (string.IsNullOrWhiteSpace(formSchemaJson)) return labels;

            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(formSchemaJson);
                if (doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    foreach (var field in doc.RootElement.EnumerateArray())
                    {

                        string fieldType = field.TryGetProperty("type", out var t) ? t.GetString() : null;
                        if (string.Equals(fieldType, "heading", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(fieldType, "paragraph", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(fieldType, "payment", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        string label = null;
                        if (field.TryGetProperty("label", out var lbl)) label = lbl.GetString();
                        else if (field.TryGetProperty("name", out var nm)) label = nm.GetString();
                        else if (field.TryGetProperty("id", out var id)) label = id.GetString();

                        if (!string.IsNullOrWhiteSpace(label) && !labels.Contains(label))
                            labels.Add(label);
                    }
                }
            }
            catch
            {
            }
            return labels;
        }
        public List<FormSubmissionModel> GetFormSubmissionsList(long formId, string search, string sort, int pageNo, int items, ref int status)
        {
            try
            {
                var result = new List<FormSubmissionModel>();
                int Total = 0;

                DataTable dt = _FormBuilder.GetFormSubmissionsListWithPagination(formId, search, sort, pageNo, items, ref Total);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var submission = new FormSubmissionModel
                        {
                            SubmissionId = Convert.ToInt64(row["SubmissionId"]),
                            FormId = Convert.ToInt64(row["FormId"]),
                            SubmittedData = row["SubmittedData"].ToString(),
                            SubmittedBy = row["SubmittedBy"].ToString(),
                            SubmittedDate = Convert.ToDateTime(row["SubmittedDate"])
                        };
                        result.Add(submission);
                    }
                }

                status = Total;
                return result;
            }
            catch (Exception ex)
            {
                status = 0;
                throw ex;
            }
        }

        public FormSubmissionModel GetFormSubmissionDetail(long submissionId, ref int status)
        {
            FormSubmissionModel model = new FormSubmissionModel();
            DataTable dt = _FormBuilder.GetFormSubmissionDetail(submissionId, ref status);
            if (status == 1 && dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                model.SubmissionId = Convert.ToInt64(row["SubmissionId"]);
                model.FormId = Convert.ToInt64(row["FormId"]);
                model.SubmittedData = row["SubmittedData"].ToString();
                model.SubmittedBy = row["SubmittedBy"].ToString();
                model.SubmittedDate = Convert.ToDateTime(row["SubmittedDate"]);

                if (dt.Columns.Contains("PaymentStatus") && !row.IsNull("PaymentStatus"))
                    model.PaymentStatus = row["PaymentStatus"].ToString();

                if (dt.Columns.Contains("PaymentTxnId") && !row.IsNull("PaymentTxnId"))
                    model.PaymentTxnId = row["PaymentTxnId"].ToString();

                if (dt.Columns.Contains("PaymentGateway") && !row.IsNull("PaymentGateway"))
                    model.PaymentGateway = row["PaymentGateway"].ToString();

                if (dt.Columns.Contains("PaymentAmount") && !row.IsNull("PaymentAmount"))
                    model.PaymentAmount = Convert.ToDecimal(row["PaymentAmount"]);

                if (dt.Columns.Contains("PaymentCurrency") && !row.IsNull("PaymentCurrency"))
                    model.PaymentCurrency = row["PaymentCurrency"].ToString();
            }

            System.Diagnostics.Debug.WriteLine(
                "PaymentStatus: " + model.PaymentStatus);
            System.Diagnostics.Debug.WriteLine(
                "PaymentTxnId: " + model.PaymentTxnId);
            System.Diagnostics.Debug.WriteLine(
                "PaymentGateway: " + model.PaymentGateway);
            System.Diagnostics.Debug.WriteLine(
                "PaymentAmount: " + model.PaymentAmount);
            System.Diagnostics.Debug.WriteLine(
                "PaymentCurrency: " + model.PaymentCurrency);
            return model;
        }
        public List<Entities.FormBuilder> GetFormsListByVariable(Int64 ChapterId, string Search, string Sort, int PageNo, int Items, Int64 UserId, bool IsSuperAdmin, ref int Total)
        {
            List<Entities.FormBuilder> lstForms = new List<Entities.FormBuilder>();
            DataTable dt = _FormBuilder.GetFormsListByVariable(ChapterId, Search, Sort, PageNo, Items, UserId, IsSuperAdmin, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _FormBuilder.GetFormsListByVariable(ChapterId, Search, Sort, PageNo - 1, Items, UserId, IsSuperAdmin, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.FormBuilder objForm = new Entities.FormBuilder();
                    objForm.RId = Convert.ToInt64(dr["Rid"].ToString());
                    objForm.FormId = Convert.ToInt64(dr["FormId"].ToString());
                    objForm.Title = dr["Title"].ToString();
                    objForm.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objForm.IsFormEnable = Convert.ToBoolean(dr["IsFormEnable"]);
                    objForm.CreatedBy = (dr["CreatedBy"] != DBNull.Value ? dr["CreatedBy"].ToString() : null);
                    objForm.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    objForm.ModifiedDate = (dr["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(dr["ModifiedDate"]) : (DateTime?)null);
                    objForm.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterId"]) : 0);
                    objForm.SubmissionCount = (dr["SubmissionCount"] != DBNull.Value ? Convert.ToInt64(dr["SubmissionCount"]) : 0);
                    lstForms.Add(objForm);
                }
            }
            return lstForms;
        }

        // ADDED - UserIds that have access to the form
        public List<long> GetFormAccessUserIds(Int64 formId, ref int status)
        {
            List<long> ids = new List<long>();
            DataTable dt = _FormBuilder.GetFormAccessUsers(formId, ref status);
            if (status == 1 && dt != null)
            {
                foreach (DataRow row in dt.Rows)
                    ids.Add(Convert.ToInt64(row["UserId"]));
            }
            return ids;
        }

        // ADDED - save selected users for a form
        public Int64 SaveFormAccess(Int64 formId, string userIdsCsv, ref int status)
        {
            return _FormBuilder.SaveFormAccess(formId, userIdsCsv, ref status);
        }

        public Int64 UpdateFormStatus(Int64 formId, ref int status)
        {
            return _FormBuilder.UpdateFormEmail(formId, ref status);
        }

        // ✅ ADDED — backs the "Active" pill on the Admin Mail Configuration page (flips Forms.IsAdminMail).
        public Int64 UpdateAdminMailStatus(Int64 formId, ref int status)
        {
            return _FormBuilder.UpdateAdminMailStatus(formId, ref status);
        }
        public Int64 UpdateFormEnable(Int64 formId, ref int status)
        {
            return _FormBuilder.UpdateFormEnable(formId, ref status);
        }
        public Int64 SaveThankYouContent(Int64 formId, string content, ref int status)
        {
            return _FormBuilder.SaveThankYouContent(formId, content, ref status);
        }
        public Int64 DeleteForm(Int64 formId, ref int status)
        {
            return _FormBuilder.DeleteForm(formId, ref status);
        }
        public Int64 DeleteFormSubmission(Int64 submissionId, ref int status)
        {
            return _FormBuilder.DeleteFormSubmission(submissionId, ref status);
        }

        public Int64 SaveFormSubmission(Int64 formId, string submittedData, string submittedBy,
            string paymentStatus, string paymentTxnId, string paymentGateway, decimal? paymentAmount, string paymentCurrency,
            ref int status)
        {
            Int64 newSubmissionId = 0;
            newSubmissionId = _FormBuilder.SaveFormSubmission(formId, submittedData, submittedBy,
                paymentStatus, paymentTxnId, paymentGateway, paymentAmount, paymentCurrency, ref status);
            return newSubmissionId;
        }

        public Int64 SaveFormSubmission(Int64 formId, string submittedData, string submittedBy, ref int status)
        {
            return SaveFormSubmission(formId, submittedData, submittedBy, null, null, null, null, null, ref status);
        }

        // ================= ADDED - SHORT URL =================
        public string GetFormShortCode(Int64 formId, ref int status)
        {
            var dal = new DAL.FormBuilder();
            return dal.GetFormShortCode(formId, ref status);
        }

        public Int64 GetFormIdByShortCode(string shortCode, ref int status)
        {
            var dal = new DAL.FormBuilder();
            return dal.GetFormIdByShortCode(shortCode, ref status);
        }

        public Int64 SaveFormShortUrl(Int64 formId, string shortCode, ref int status)
        {
            var dal = new DAL.FormBuilder();
            return dal.SaveFormShortUrl(formId, shortCode, ref status);
        }
        // ================= END SHORT URL =================
    }
}