using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWSFormBuilder.Areas.FormBuilder.DAL;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class Form1
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.Form1 _Form1 = new AWSFormBuilder.Areas.FormBuilder.DAL.Form1();

        #region Methods

        public Int64 InsertForm1(Entities.Form1 objForm1)
        {
            Int64 _status = 0;
            if (objForm1 != null)
            {
                _status = _Form1.InsertForm1(objForm1);

            }
            return _status;
        }

        public Int64 DeleteForm1(Int64 CompetitionRegistrationId)
        {
            Int64 _status = 0;
            _status = _Form1.DeleteForm1ById(CompetitionRegistrationId);
            return _status;
        }

        public Int64 UpdateForm1(Entities.Form1 objForm1)
        {
            Int64 _status = 0;
            if (objForm1 != null)
            {
                _status = _Form1.UpdateForm1(objForm1);

            }
            return _status;
        }


        public Int64 UpdateForm1StatusById(Int64 CompetitionRegistrationId)
        {
            Int64 _status = 0;
            _status = _Form1.UpdateForm1StatusById(CompetitionRegistrationId);
            return _status;
        }

        public Int64 FEInsertForm1(Entities.Form1 objForm1, ref Int64 CompetitionRegistrationId)
        {
            Int64 _status = 0;
            if (objForm1 != null)
            {
                _status = _Form1.FEInsertForm1(objForm1, ref CompetitionRegistrationId);

            }
            return _status;
        }

        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.Form1> GetForm1List(ref int status)
        {
            List<AWSFormBuilder.Entities.Form1> lstForm1 = new List<AWSFormBuilder.Entities.Form1>();
            DataTable dt = _Form1.GetForm1List(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Form1 objlstForm1 = new AWSFormBuilder.Entities.Form1();

                   
                    objlstForm1.CompetitionRegistrationId = Convert.ToInt64(dr["CompetitionRegistrationId"].ToString());
                    objlstForm1.EventId = (dr["EventId"] != DBNull.Value ? Convert.ToInt64(dr["EventId"]) : 0);
                    objlstForm1.RegistrationCategoryId = (dr["RegistrationCategoryId"] != DBNull.Value ? Convert.ToInt64(dr["RegistrationCategoryId"]) : 0);
                    objlstForm1.MemberId = (dr["MemberId"] != DBNull.Value ? Convert.ToInt64(dr["MemberId"]) : 0);
                    objlstForm1.FirstName = (dr["FirstName"] != DBNull.Value ? dr["FirstName"].ToString() : null);
                    objlstForm1.LastName = (dr["LastName"] != DBNull.Value ? dr["LastName"].ToString() : null);
                    objlstForm1.DateofBirth = Convert.ToDateTime(dr["DateofBirth"].ToString());
                    objlstForm1.GradeinSchool = (dr["GradeinSchool"] != DBNull.Value ? dr["GradeinSchool"].ToString() : null);
                    objlstForm1.SchoolName = (dr["SchoolName"] != DBNull.Value ? dr["SchoolName"].ToString() : null);
                    objlstForm1.ContactFullName = (dr["ContactFullName"] != DBNull.Value ? dr["ContactFullName"].ToString() : null);
                    objlstForm1.ContactPhone = (dr["ContactPhone"] != DBNull.Value ? dr["ContactPhone"].ToString() : null);
                    objlstForm1.ContactEmail = (dr["ContactEmail"] != DBNull.Value ? dr["ContactEmail"].ToString() : null);
                    objlstForm1.Notes = (dr["Notes"] != DBNull.Value ? dr["Notes"].ToString() : null);
                    objlstForm1.TermsandConditions = Convert.ToBoolean(dr["TermsandConditions"] != DBNull.Value ? Convert.ToBoolean(dr["TermsandConditions"]) : false);
                    objlstForm1.IsApproved = Convert.ToBoolean(dr["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsApproved"]) : false);
                    objlstForm1.ApprovedDate = Convert.ToDateTime(dr["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApprovedDate"]) : DateTime.MinValue);
                    objlstForm1.PaymentStatusId = Convert.ToInt64(dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objlstForm1.PaymentMethodId = Convert.ToInt64(dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objlstForm1.PaymentDate = Convert.ToDateTime(dr["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(dr["PaymentDate"]) : DateTime.MinValue);
                    objlstForm1.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objlstForm1.AmountPaid = Convert.ToDecimal(dr["AmountPaid"] != DBNull.Value ? dr["AmountPaid"].ToString() : null);
                    objlstForm1.BankName = (dr["BankName"] != DBNull.Value ? dr["BankName"].ToString() : null);
                    objlstForm1.ChequeNo = (dr["ChequeNo"] != DBNull.Value ? dr["ChequeNo"].ToString() : null);
                    objlstForm1.ChequeDate = Convert.ToDateTime(dr["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dr["ChequeDate"]) : DateTime.MinValue);
                    objlstForm1.AdminComments = (dr["AdminComments"] != DBNull.Value ? dr["AdminComments"].ToString() : null);
                    objlstForm1.Field1 = (dr["Field1"] != DBNull.Value ? dr["Field1"].ToString() : null);
                    objlstForm1.Field2 = (dr["Field2"] != DBNull.Value ? dr["Field2"].ToString() : null);
                    objlstForm1.Field3 = (dr["Field3"] != DBNull.Value ? dr["Field3"].ToString() : null);
                    objlstForm1.InstertedBy = dr["InstertedBy"].ToString();
                    objlstForm1.InsertedDate = Convert.ToDateTime(dr["InsertedDate"].ToString());
                    objlstForm1.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstForm1.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());


                    lstForm1.Add(objlstForm1);
                }

            }
            return lstForm1;
        }

        public AWSFormBuilder.Entities.Form1 GetForm1ById(Int64 CompetitionRegistrationId, ref int status)
        {
            AWSFormBuilder.Entities.Form1 objForm1 = new AWSFormBuilder.Entities.Form1();
            DataTable dt = new DataTable();
            if (CompetitionRegistrationId != 0)
            {
                dt = _Form1.GetForm1ById(CompetitionRegistrationId, ref status);
                if (dt.Rows.Count == 1)
                {
                    
                    objForm1.CompetitionRegistrationId = Convert.ToInt64(dt.Rows[0]["CompetitionRegistrationId"].ToString());
                    objForm1.EventId = (dt.Rows[0]["EventId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["EventId"]) : 0);
                    objForm1.RegistrationCategoryId = (dt.Rows[0]["RegistrationCategoryId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["RegistrationCategoryId"]) : 0);
                    objForm1.MemberId = (dt.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["MemberId"]) : 0);
                    objForm1.FirstName = (dt.Rows[0]["FirstName"] != DBNull.Value ? dt.Rows[0]["FirstName"].ToString() : null);
                    objForm1.LastName = (dt.Rows[0]["LastName"] != DBNull.Value ? dt.Rows[0]["LastName"].ToString() : null);
                    objForm1.DateofBirth = Convert.ToDateTime(dt.Rows[0]["DateofBirth"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateofBirth"]) : DateTime.MinValue);
                    objForm1.GradeinSchool = (dt.Rows[0]["GradeinSchool"] != DBNull.Value ? dt.Rows[0]["GradeinSchool"].ToString() : null);
                    objForm1.SchoolName = (dt.Rows[0]["SchoolName"] != DBNull.Value ? dt.Rows[0]["SchoolName"].ToString() : null);
                    objForm1.ContactFullName = (dt.Rows[0]["ContactFullName"] != DBNull.Value ? dt.Rows[0]["ContactFullName"].ToString() : null);
                    objForm1.ContactPhone = (dt.Rows[0]["ContactPhone"] != DBNull.Value ? dt.Rows[0]["ContactPhone"].ToString() : null);
                    objForm1.ContactEmail = (dt.Rows[0]["ContactEmail"] != DBNull.Value ? dt.Rows[0]["ContactEmail"].ToString() : null);
                    objForm1.Notes = (dt.Rows[0]["Notes"] != DBNull.Value ? dt.Rows[0]["Notes"].ToString() : null);
                    objForm1.TermsandConditions = Convert.ToBoolean(dt.Rows[0]["TermsandConditions"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["TermsandConditions"]) : false);
                    objForm1.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsApproved"]) : false);
                    objForm1.ApprovedDate = Convert.ToDateTime(dt.Rows[0]["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["ApprovedDate"]) : DateTime.MinValue);
                    objForm1.PaymentStatusId = Convert.ToInt64(dt.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PaymentStatusId"]) : 0);
                    objForm1.PaymentMethodId = Convert.ToInt64(dt.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PaymentMethodId"]) : 0);
                    objForm1.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["PaymentDate"]) : DateTime.MinValue);
                    objForm1.TransactionId = (dt.Rows[0]["TransactionId"] != DBNull.Value ? dt.Rows[0]["TransactionId"].ToString() : null);
                    objForm1.AmountPaid = Convert.ToDecimal(dt.Rows[0]["AmountPaid"] != DBNull.Value ? dt.Rows[0]["AmountPaid"].ToString() : null);
                    objForm1.BankName = (dt.Rows[0]["BankName"] != DBNull.Value ? dt.Rows[0]["BankName"].ToString() : null);
                    objForm1.ChequeNo = (dt.Rows[0]["ChequeNo"] != DBNull.Value ? dt.Rows[0]["ChequeNo"].ToString() : null);
                    objForm1.ChequeDate = Convert.ToDateTime(dt.Rows[0]["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["ChequeDate"]) : DateTime.MinValue);
                    objForm1.AdminComments = (dt.Rows[0]["AdminComments"] != DBNull.Value ? dt.Rows[0]["AdminComments"].ToString() : null);
                    objForm1.Field1 = (dt.Rows[0]["Field1"] != DBNull.Value ? dt.Rows[0]["Field1"].ToString() : null);
                    objForm1.Field2 = (dt.Rows[0]["Field2"] != DBNull.Value ? dt.Rows[0]["Field2"].ToString() : null);
                    objForm1.Field3 = (dt.Rows[0]["Field3"] != DBNull.Value ? dt.Rows[0]["Field3"].ToString() : null);
                    objForm1.InstertedBy = (dt.Rows[0]["InstertedBy"] != DBNull.Value ? dt.Rows[0]["InstertedBy"].ToString() : null);
                    objForm1.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedDate"]) : DateTime.MinValue);
                    objForm1.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objForm1.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]) : DateTime.MinValue);
                    objForm1.PaymentStatus = (dt.Rows[0]["PaymentStatus"] != DBNull.Value ? dt.Rows[0]["PaymentStatus"].ToString() : null);
                    objForm1.PaymentMethod = (dt.Rows[0]["PaymentMethod"] != DBNull.Value ? dt.Rows[0]["PaymentMethod"].ToString() : null);
                    objForm1.CategoryName = (dt.Rows[0]["CategoryName"] != DBNull.Value ? dt.Rows[0]["CategoryName"].ToString() : null);
                    objForm1.EventName = (dt.Rows[0]["EventName"] != DBNull.Value ? dt.Rows[0]["EventName"].ToString() : null);
                }
            }
            return objForm1;
        }

        public List<AWSFormBuilder.Entities.Form1> GetForm1ListByVariable(Int64 EventId, Int64 RegistrationCategoryId, Int64 PaymentMethodId, Int64 PaymentStatusId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.Form1> lstForm1 = new List<AWSFormBuilder.Entities.Form1>();
            DataTable dt = _Form1.GetForm1ListByVariable(EventId, RegistrationCategoryId, PaymentMethodId, PaymentStatusId, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Form1.GetForm1ListByVariable(EventId, RegistrationCategoryId, PaymentMethodId, PaymentStatusId, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Form1 objForm1 = new AWSFormBuilder.Entities.Form1();

                    objForm1.Rid = Convert.ToInt64(dr["Rid"].ToString());
                    objForm1.CompetitionRegistrationId = Convert.ToInt64(dr["CompetitionRegistrationId"].ToString());
                    objForm1.EventId = (dr["EventId"] != DBNull.Value ? Convert.ToInt64(dr["EventId"]) : 0);
                    objForm1.RegistrationCategoryId = (dr["RegistrationCategoryId"] != DBNull.Value ? Convert.ToInt64(dr["RegistrationCategoryId"]) : 0);
                    objForm1.MemberId = (dr["MemberId"] != DBNull.Value ? Convert.ToInt64(dr["MemberId"]) : 0);
                    objForm1.FirstName = (dr["FirstName"] != DBNull.Value ? dr["FirstName"].ToString() : null);
                    objForm1.LastName = (dr["LastName"] != DBNull.Value ? dr["LastName"].ToString() : null);
                    objForm1.DateofBirth = Convert.ToDateTime(dr["DateofBirth"] != DBNull.Value ? Convert.ToDateTime(dr["DateofBirth"]) : DateTime.MinValue);
                    objForm1.GradeinSchool = (dr["GradeinSchool"] != DBNull.Value ? dr["GradeinSchool"].ToString() : null);
                    objForm1.SchoolName = (dr["SchoolName"] != DBNull.Value ? dr["SchoolName"].ToString() : null);
                    objForm1.ContactFullName = (dr["ContactFullName"] != DBNull.Value ? dr["ContactFullName"].ToString() : null);
                    objForm1.ContactPhone = (dr["ContactPhone"] != DBNull.Value ? dr["ContactPhone"].ToString() : null);
                    objForm1.ContactEmail = (dr["ContactEmail"] != DBNull.Value ? dr["ContactEmail"].ToString() : null);
                    objForm1.Notes = (dr["Notes"] != DBNull.Value ? dr["Notes"].ToString() : null);
                    objForm1.TermsandConditions = (dr["TermsandConditions"] != DBNull.Value ? Convert.ToBoolean(dr["TermsandConditions"]) : false);
                    objForm1.IsApproved = (dr["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dr["IsApproved"]) : false);
                    objForm1.ApprovedDate = (dr["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApprovedDate"]) : DateTime.MinValue);
                    objForm1.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objForm1.PaymentMethodId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objForm1.PaymentDate = Convert.ToDateTime(dr["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(dr["PaymentDate"]) : DateTime.MinValue);
                    objForm1.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objForm1.AmountPaid = Convert.ToDecimal(dr["AmountPaid"] != DBNull.Value ? dr["AmountPaid"].ToString() : null);
                    objForm1.BankName = (dr["BankName"] != DBNull.Value ? dr["BankName"].ToString() : null);
                    objForm1.ChequeNo = (dr["ChequeNo"] != DBNull.Value ? dr["ChequeNo"].ToString() : null);
                    objForm1.ChequeDate = (dr["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dr["ChequeDate"]) : DateTime.MinValue);
                    objForm1.AdminComments = (dr["AdminComments"] != DBNull.Value ? dr["AdminComments"].ToString() : null);
                    objForm1.Field1 = (dr["Field1"] != DBNull.Value ? dr["Field1"].ToString() : null);
                    objForm1.Field2 = (dr["Field2"] != DBNull.Value ? dr["Field2"].ToString() : null);
                    objForm1.Field3 = (dr["Field3"] != DBNull.Value ? dr["Field3"].ToString() : null);
                    objForm1.InstertedBy = (dr["InstertedBy"] != DBNull.Value ? dr["InstertedBy"].ToString() : null);
                    objForm1.InsertedDate = Convert.ToDateTime(dr["InsertedDate"] != DBNull.Value ? Convert.ToDateTime(dr["InsertedDate"]) : DateTime.MinValue);
                    objForm1.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                    objForm1.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedDate"]) : DateTime.MinValue);
                    objForm1.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null);
                    objForm1.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null);

                    lstForm1.Add(objForm1);
                }
            }
            return lstForm1;
        }


        #endregion
    }
}
