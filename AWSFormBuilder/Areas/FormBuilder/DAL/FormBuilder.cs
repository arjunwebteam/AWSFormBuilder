using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using SqlParameter = Microsoft.Data.SqlClient.SqlParameter;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class FormBuilder
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;
        private readonly IConfiguration _configuration;

        public FormBuilder()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public FormBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetFormSubmissionsList(Int64 formId, ref int status)
        {
            DataTable dt = new DataTable();
            try
            {
                _sqlP = new SqlParameter[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("GetFormSubmissionsList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                status = 0;
                throw ex;
            }
            return dt;
        }

        public DataTable GetFormSubmissionDetail(Int64 submissionId, ref int status)
        {
            DataTable dt = new DataTable();
            try
            {
                _sqlP = new SqlParameter[]
                {
                    new SqlParameter("@SubmissionId", submissionId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("GetFormSubmissionDetail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                status = 0;
                throw ex;
            }
            return dt;
        }

        public DataTable GetFormSubmissionsListWithPagination(Int64 formId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new SqlParameter[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@Search", Search ?? ""),
                    new SqlParameter("@Sort", Sort ?? "SubmittedDate DESC"),
                    new SqlParameter("@PageNo", PageNo),
                    new SqlParameter("@Items", Items),
                    new SqlParameter("@Total", 0)
                };
                _sqlP[5].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("GetFormSubmissionsListWithPagination", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #region Form Builder
        public DataTable GetFormsListByVariable(Int64 ChapterId, string Search, string Sort, int PageNo, int Items, Int64 UserId, bool IsSuperAdmin, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId", ChapterId),
                    new SqlParameter("@Search", Search),
                    new SqlParameter("@Sort", Sort),
                    new SqlParameter("@PageNo", PageNo),
                    new SqlParameter("@Items", Items),
                    new SqlParameter("@UserId", UserId),
                    new SqlParameter("@IsSuperAdmin", IsSuperAdmin),
                    new SqlParameter("@Total", Total)
                };
                _sqlP[7].Direction = ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FormsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        // ADDED - UserIds that currently have access to a form
        public DataTable GetFormAccessUsers(Int64 formId, ref int status)
        {
            DataTable dt = new DataTable();
            try
            {
                _sqlP = new SqlParameter[]
                {
                    new SqlParameter("@FormId", (int)formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("USP_GetFormAccessUsers", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                status = -1;
                throw ex;
            }
            return dt;
        }

        // ADDED - saves the selected UserIds (comma separated) for a form
        public Int64 SaveFormAccess(Int64 formId, string userIdsCsv, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new SqlParameter[]
                {
                    new SqlParameter("@FormId", (int)formId),
                    new SqlParameter("@UserIdsCsv", (object)userIdsCsv ?? DBNull.Value),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[2].Direction = ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar("USP_SaveFormAccess", ref _sqlP);
                status = Convert.ToInt32(_sqlP[2].Value);
                result = status;
            }
            catch (Exception ex)
            {
                status = -1;
                throw ex;
            }
            return result;
        }

        public Int64 UpdateFormEnable(Int64 formId, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateFormEnable", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
                result = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public Int64 UpdateFormEmail(Int64 formId, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("USP_FormStatusUpdate", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
                result = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        // ✅ ADDED — mirrors UpdateFormEmail, but flips Forms.IsAdminMail instead of Forms.IsActive.
        // Backs the "Active" pill on the Admin Mail Configuration page.
        public Int64 UpdateAdminMailStatus(Int64 formId, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("USP_FormAdminMailStatusUpdate", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
                result = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Int64 DeleteForm(Int64 formId, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("USP_DeleteForm", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
                result = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public Int64 DeleteFormSubmission(Int64 submissionId, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@submissionId", submissionId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("DeleteFormSubmission", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
                result = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Int64 SaveFormSchema(int? formId, string title, string formSchema, Int64 chapterId, string createdBy, string logoUrl, int? logoWidth, int? logoHeight, string designJson, string conditionsJson, ref int status)
        {
            Int64 newFormId = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@FormId", (object)formId ?? DBNull.Value),
            new SqlParameter("@Title", title),
            new SqlParameter("@FormSchema", formSchema),
            new SqlParameter("@ChapterId", chapterId),
            new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value),
            new SqlParameter("@LogoUrl", (object)logoUrl ?? DBNull.Value),
            new SqlParameter("@LogoWidth", (object)logoWidth ?? DBNull.Value),
            new SqlParameter("@LogoHeight", (object)logoHeight ?? DBNull.Value),
            new SqlParameter("@DesignJson", (object)designJson ?? DBNull.Value),
            new SqlParameter("@ConditionsJson", (object)conditionsJson ?? DBNull.Value),   // ✅ ADDED
            new SqlParameter("@QStatus", 0),
            new SqlParameter("@NewFormId", 0L)   // ✅ ADDED — FormId now comes back on its own param, no longer reused from @QStatus
        };

                _sqlP[10].Direction = ParameterDirection.Output;   // @QStatus  — 1 success, -1 failed
                _sqlP[11].Direction = ParameterDirection.Output;   // @NewFormId — ✅ ADDED

                _dbAccess.SP_ExecuteScalar("USP_SaveFormSchema", ref _sqlP);

                status = Convert.ToInt32(_sqlP[10].Value);

                newFormId = (_sqlP[11].Value != DBNull.Value)
                    ? Convert.ToInt64(_sqlP[11].Value)
                    : 0;
            }
            catch (Exception ex)
            {
                status = -1;   // ✅ ADDED — a thrown exception here now also reports -1 to the caller, not just an unset 0
                throw ex;
            }
            return newFormId;
        }
        public DataTable FormSubmissionsExportToExcel(string Search, string Sort, Int64 FormId)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@Search", Search),
            new SqlParameter("@Sort", Sort),
            new SqlParameter("@FormId", FormId)
        };

                dt = _dbAccess.GetDataTable("FormSubmissionsExportToExcel", ref _sqlP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetFormSchema(Int64 formId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("USP_GetFormSchema", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetFormlist(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[0].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("GetFormlist", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetFormsListByChapterId(Int64 ChapterId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId", ChapterId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("USP_GetFormsListByChapterId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public Int64 SaveThankYouContent(Int64 formId, string content, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@FormId", formId),
            new SqlParameter("@ThankYouContent", (object)content ?? DBNull.Value),
            new SqlParameter("@QStatus", 0)
        };
                _sqlP[2].Direction = ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar("USP_SaveFormThankYouContent", ref _sqlP);
                status = Convert.ToInt32(_sqlP[2].Value);
                result = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Int64 SaveFormSubmission(Int64 formId, string submittedData, string submittedBy,
            string paymentStatus, string paymentTxnId, string paymentGateway, decimal? paymentAmount, string paymentCurrency,
            ref int status)
        {
            Int64 newSubmissionId = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@SubmittedData", submittedData),
                    new SqlParameter("@SubmittedBy", (object)submittedBy ?? DBNull.Value),
                    new SqlParameter("@PaymentStatus", (object)paymentStatus ?? DBNull.Value),
                    new SqlParameter("@PaymentTxnId", (object)paymentTxnId ?? DBNull.Value),
                    new SqlParameter("@PaymentGateway", (object)paymentGateway ?? DBNull.Value),
                    new SqlParameter("@PaymentAmount", (object)paymentAmount ?? DBNull.Value),
                    new SqlParameter("@PaymentCurrency", (object)paymentCurrency ?? DBNull.Value),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[8].Direction = ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar("USP_SaveFormSubmission", ref _sqlP);
                status = Convert.ToInt32(_sqlP[8].Value);
                newSubmissionId = status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newSubmissionId;
        }

        public Int64 SaveFormSubmission(Int64 formId, string submittedData, string submittedBy, ref int status)
        {
            return SaveFormSubmission(formId, submittedData, submittedBy, null, null, null, null, null, ref status);
        }

        // ================= ADDED - SHORT URL =================
        // Returns the short code saved for a form (null if none). status: 1 found, 0 none, -1 error
        public string GetFormShortCode(Int64 formId, ref int status)
        {
            string code = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                DataTable dt = _dbAccess.GetDataTable("USP_GetFormShortUrl", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ShortCode"] != DBNull.Value)
                    code = dt.Rows[0]["ShortCode"].ToString();
            }
            catch (Exception ex)
            {
                status = -1;
                throw ex;
            }
            return code;
        }

        // Returns the FormId that a short code points to (0 if not found)
        public Int64 GetFormIdByShortCode(string shortCode, ref int status)
        {
            Int64 formId = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ShortCode", (object)shortCode ?? DBNull.Value),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = ParameterDirection.Output;

                DataTable dt = _dbAccess.GetDataTable("USP_GetFormIdByShortCode", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["FormId"] != DBNull.Value)
                    formId = Convert.ToInt64(dt.Rows[0]["FormId"]);
            }
            catch (Exception ex)
            {
                status = -1;
                throw ex;
            }
            return formId;
        }

        // Insert or update the short code of a form. status: 1 saved, 0 code already used by another form, -1 error
        public Int64 SaveFormShortUrl(Int64 formId, string shortCode, ref int status)
        {
            Int64 result = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", formId),
                    new SqlParameter("@ShortCode", shortCode),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[2].Direction = ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar("USP_SaveFormShortUrl", ref _sqlP);
                status = Convert.ToInt32(_sqlP[2].Value);
                result = status;
            }
            catch (Exception ex)
            {
                status = -1;
                throw ex;
            }
            return result;
        }
        // ================= END SHORT URL =================

        #endregion
    }
}