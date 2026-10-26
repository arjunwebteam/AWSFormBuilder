using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class MailTemplates
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public DataTable GetMailTemplatesList(string MailType, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MailType",MailType),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MailTemplatesGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 InsertMailTemplates(Entities.MailTemplates objMailTemplates)
        {
            Int64 _status = 0;
            try
            {
                string formIdsCsv = (objMailTemplates.SelectedFormIds != null && objMailTemplates.SelectedFormIds.Count > 0)? string.Join(",", objMailTemplates.SelectedFormIds): null;
                _sqlP = new[]
                    {
    new SqlParameter("@MailTemplateId",objMailTemplates.MailTemplateId),
 new SqlParameter("@Heading",objMailTemplates.Heading),                    new SqlParameter("@Subject",objMailTemplates.Subject),
    new SqlParameter("@Description",(objMailTemplates.Description == null ? DBNull.Value : (object)objMailTemplates.Description.Trim())),
    new SqlParameter("@MailType",(objMailTemplates.MailType == null ? DBNull.Value : (object)objMailTemplates.MailType.Trim())),
    new SqlParameter("@LogoUrl",(string.IsNullOrEmpty(objMailTemplates.LogoUrl) ? (object)DBNull.Value : objMailTemplates.LogoUrl)),
    new SqlParameter("@FormIds",(object)formIdsCsv ?? DBNull.Value),
    new SqlParameter("@BCC",(string.IsNullOrEmpty(objMailTemplates.BCC) ? (object)DBNull.Value : objMailTemplates.BCC)),   
    new SqlParameter("@UpdatedBy",objMailTemplates.UpdatedBy),
    new SqlParameter("@UpdatedTime",objMailTemplates.UpdatedTime),
    new SqlParameter("@QStatus",0)
    };
                _sqlP[10].Direction = System.Data.ParameterDirection.Output;   
                _dbAccess.SP_ExecuteScalar("MailTemplatesInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);                  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetMailTemplatesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MailTemplatesGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    
        public DataTable GetMailTemplateById(string TemplateName, Int64 MailTemplateId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@TemplateName",TemplateName),
                    new SqlParameter("@MailTemplateId",MailTemplateId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MailTemplatesGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteMailTemplate(Int64 MailTemplateId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MailTemplateId",MailTemplateId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MailTemplatesDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public DataTable GetMailTemplateByFormIdAndType(Int64 FormId, string MailType, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@FormId", FormId),
            new SqlParameter("@MailType", MailType),
            new SqlParameter("@QStatus", 0)
        };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("USP_GetMailTemplateByFormIdAndType", ref _sqlP);
                status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetMailTemplateByFormId(Int64 FormId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@FormId", FormId),
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("USP_GetMailTemplateByFormId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetFormsListForDropdown(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus", 0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("USP_GetFormsListForDropdown", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
