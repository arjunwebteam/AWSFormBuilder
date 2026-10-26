using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class Form1
    {
        DBAccess _dbAccess = new DBAccess();

        SqlParameter[] _sqlP;

        public Int64 InsertForm1(AWSFormBuilder.Entities.Form1 objForm1)
        {

            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@CompetitionRegistrationId",objForm1.CompetitionRegistrationId),
                    new SqlParameter("@EventId",(objForm1.EventId == 0 ?DBNull.Value:(object)objForm1.EventId)),
                    new SqlParameter("@RegistrationCategoryId",(objForm1.RegistrationCategoryId == 0 ?DBNull.Value:(object)objForm1.RegistrationCategoryId)),
                    new SqlParameter("@MemberId",(objForm1.MemberId == 0 ?DBNull.Value:(object)objForm1.MemberId)),
                    new SqlParameter("@FirstName",(objForm1.FirstName== null ?DBNull.Value:(object)objForm1.FirstName)),
                    new SqlParameter("@LastName",(objForm1.LastName== null ?DBNull.Value:(object)objForm1.LastName)),
                    new SqlParameter("@DateofBirth",(objForm1.DateofBirth == DateTime.MinValue ?DBNull.Value:(object)objForm1.DateofBirth)),
                    new SqlParameter("@GradeinSchool",(objForm1.GradeinSchool== null ?DBNull.Value:(object)objForm1.GradeinSchool)),
                    new SqlParameter("@SchoolName",(objForm1.SchoolName== null ?DBNull.Value:(object)objForm1.SchoolName)),
                    new SqlParameter("@ContactFullName",(objForm1.ContactFullName== null ?DBNull.Value:(object)objForm1.ContactFullName)),
                    new SqlParameter("@ContactPhone",(objForm1.ContactPhone== null ?DBNull.Value:(object)objForm1.ContactPhone)),
                    new SqlParameter("@ContactEmail",(objForm1.ContactEmail== null ?DBNull.Value:(object)objForm1.ContactEmail)),
                    new SqlParameter("@Notes",(objForm1.Notes== null ?DBNull.Value:(object)objForm1.Notes)),
                    new SqlParameter("@TermsandConditions",objForm1.TermsandConditions),
                    new SqlParameter("@IsApproved",objForm1.IsApproved),
                    new SqlParameter("@ApprovedDate",(objForm1.ApprovedDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.ApprovedDate)),
                    new SqlParameter("@PaymentStatusId",(objForm1.PaymentStatusId == 0 ?DBNull.Value:(object)objForm1.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objForm1.PaymentMethodId == 0 ?DBNull.Value:(object)objForm1.PaymentMethodId)),
                    new SqlParameter("@PaymentDate",(objForm1.PaymentDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.PaymentDate)),
                    new SqlParameter("@TransactionId",(objForm1.TransactionId== null ?DBNull.Value:(object)objForm1.TransactionId)),
                    new SqlParameter("@AmountPaid",(objForm1.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objForm1.AmountPaid)),   
                    new SqlParameter("@BankName",(objForm1.BankName== null ?DBNull.Value:(object)objForm1.BankName)),
                    new SqlParameter("@ChequeNo",(objForm1.ChequeNo== null ?DBNull.Value:(object)objForm1.ChequeNo)),
                    new SqlParameter("@ChequeDate",(objForm1.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.ChequeDate)),
                    new SqlParameter("@AdminComments",(objForm1.AdminComments== null ?DBNull.Value:(object)objForm1.AdminComments)),
                    new SqlParameter("@Field1",(objForm1.Field1== null ?DBNull.Value:(object)objForm1.Field1)),
                    new SqlParameter("@Field2",(objForm1.Field2== null ?DBNull.Value:(object)objForm1.Field2)),
                    new SqlParameter("@Field3",(objForm1.Field3== null ?DBNull.Value:(object)objForm1.Field3)),
                    new SqlParameter("@InstertedBy",objForm1.InstertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objForm1.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[32].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Form1Insert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[32].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 FEInsertForm1(AWSFormBuilder.Entities.Form1 objForm1, ref Int64 CompetitionRegistrationId)
        {

            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@CompetitionRegistrationId",objForm1.CompetitionRegistrationId),
                    new SqlParameter("@EventId",(objForm1.EventId == 0 ?DBNull.Value:(object)objForm1.EventId)),
                    new SqlParameter("@RegistrationCategoryId",(objForm1.RegistrationCategoryId == 0 ?DBNull.Value:(object)objForm1.RegistrationCategoryId)),
                    new SqlParameter("@MemberId",(objForm1.MemberId == 0 ?DBNull.Value:(object)objForm1.MemberId)),
                    new SqlParameter("@FirstName",(objForm1.FirstName== null ?DBNull.Value:(object)objForm1.FirstName)),
                    new SqlParameter("@LastName",(objForm1.LastName== null ?DBNull.Value:(object)objForm1.LastName)),
                    new SqlParameter("@DateofBirth",(objForm1.DateofBirth == DateTime.MinValue ?DBNull.Value:(object)objForm1.DateofBirth)),
                    new SqlParameter("@GradeinSchool",(objForm1.GradeinSchool== null ?DBNull.Value:(object)objForm1.GradeinSchool)),
                    new SqlParameter("@SchoolName",(objForm1.SchoolName== null ?DBNull.Value:(object)objForm1.SchoolName)),
                    new SqlParameter("@ContactFullName",(objForm1.ContactFullName== null ?DBNull.Value:(object)objForm1.ContactFullName)),
                    new SqlParameter("@ContactPhone",(objForm1.ContactPhone== null ?DBNull.Value:(object)objForm1.ContactPhone)),
                    new SqlParameter("@ContactEmail",(objForm1.ContactEmail== null ?DBNull.Value:(object)objForm1.ContactEmail)),
                    new SqlParameter("@Notes",(objForm1.Notes== null ?DBNull.Value:(object)objForm1.Notes)),
                    new SqlParameter("@TermsandConditions",objForm1.TermsandConditions),
                    new SqlParameter("@IsApproved",objForm1.IsApproved),
                    new SqlParameter("@ApprovedDate",(objForm1.ApprovedDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.ApprovedDate)),
                    new SqlParameter("@PaymentStatusId",(objForm1.PaymentStatusId == 0 ?DBNull.Value:(object)objForm1.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objForm1.PaymentMethodId == 0 ?DBNull.Value:(object)objForm1.PaymentMethodId)),
                    new SqlParameter("@PaymentDate",(objForm1.PaymentDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.PaymentDate)),
                    new SqlParameter("@TransactionId",(objForm1.TransactionId== null ?DBNull.Value:(object)objForm1.TransactionId)),
                    new SqlParameter("@AmountPaid",(objForm1.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objForm1.AmountPaid)),
                    new SqlParameter("@BankName",(objForm1.BankName== null ?DBNull.Value:(object)objForm1.BankName)),
                    new SqlParameter("@ChequeNo",(objForm1.ChequeNo== null ?DBNull.Value:(object)objForm1.ChequeNo)),
                    new SqlParameter("@ChequeDate",(objForm1.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.ChequeDate)),
                    new SqlParameter("@AdminComments",(objForm1.AdminComments== null ?DBNull.Value:(object)objForm1.AdminComments)),
                    new SqlParameter("@Field1",(objForm1.Field1== null ?DBNull.Value:(object)objForm1.Field1)),
                    new SqlParameter("@Field2",(objForm1.Field2== null ?DBNull.Value:(object)objForm1.Field2)),
                    new SqlParameter("@Field3",(objForm1.Field3== null ?DBNull.Value:(object)objForm1.Field3)),
                    new SqlParameter("@InstertedBy",objForm1.InstertedBy),
                    new SqlParameter("@InsertedDate",objForm1.InsertedDate),
                    new SqlParameter("@UpdatedBy",objForm1.UpdatedBy),
                    new SqlParameter("@UpdatedDate",objForm1.UpdatedDate),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[0].Direction = _sqlP[32].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEForm1Insert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[32].Value);

                CompetitionRegistrationId = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateForm1(AWSFormBuilder.Entities.Form1 objForm1)
        {

            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@CompetitionRegistrationId",objForm1.CompetitionRegistrationId),
                    new SqlParameter("@EventId",(objForm1.EventId == 0 ?DBNull.Value:(object)objForm1.EventId)),
                    new SqlParameter("@RegistrationCategoryId",(objForm1.RegistrationCategoryId == 0 ?DBNull.Value:(object)objForm1.RegistrationCategoryId)),
                    new SqlParameter("@MemberId",(objForm1.MemberId == 0 ?DBNull.Value:(object)objForm1.MemberId)),
                    new SqlParameter("@FirstName",(objForm1.FirstName== null ?DBNull.Value:(object)objForm1.FirstName)),
                    new SqlParameter("@LastName",(objForm1.LastName== null ?DBNull.Value:(object)objForm1.LastName)),
                    new SqlParameter("@DateofBirth",(objForm1.DateofBirth == DateTime.MinValue ?DBNull.Value:(object)objForm1.DateofBirth)),
                    new SqlParameter("@GradeinSchool",(objForm1.GradeinSchool== null ?DBNull.Value:(object)objForm1.GradeinSchool)),
                    new SqlParameter("@SchoolName",(objForm1.SchoolName== null ?DBNull.Value:(object)objForm1.SchoolName)),
                    new SqlParameter("@ContactFullName",(objForm1.ContactFullName== null ?DBNull.Value:(object)objForm1.ContactFullName)),
                    new SqlParameter("@ContactPhone",(objForm1.ContactPhone== null ?DBNull.Value:(object)objForm1.ContactPhone)),
                    new SqlParameter("@ContactEmail",(objForm1.ContactEmail== null ?DBNull.Value:(object)objForm1.ContactEmail)),
                    new SqlParameter("@Notes",(objForm1.Notes== null ?DBNull.Value:(object)objForm1.Notes)),
                    new SqlParameter("@TermsandConditions",objForm1.TermsandConditions),
                    new SqlParameter("@IsApproved",objForm1.IsApproved),
                    new SqlParameter("@ApprovedDate",(objForm1.ApprovedDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.ApprovedDate)),
                    new SqlParameter("@PaymentStatusId",(objForm1.PaymentStatusId == 0 ?DBNull.Value:(object)objForm1.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objForm1.PaymentMethodId == 0 ?DBNull.Value:(object)objForm1.PaymentMethodId)),
                    new SqlParameter("@PaymentDate",(objForm1.PaymentDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.PaymentDate)),
                    new SqlParameter("@TransactionId",(objForm1.TransactionId== null ?DBNull.Value:(object)objForm1.TransactionId)),
                    new SqlParameter("@AmountPaid",(objForm1.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objForm1.AmountPaid)),
                    new SqlParameter("@BankName",(objForm1.BankName== null ?DBNull.Value:(object)objForm1.BankName)),
                    new SqlParameter("@ChequeNo",(objForm1.ChequeNo== null ?DBNull.Value:(object)objForm1.ChequeNo)),
                    new SqlParameter("@ChequeDate",(objForm1.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objForm1.ChequeDate)),
                    new SqlParameter("@AdminComments",(objForm1.AdminComments== null ?DBNull.Value:(object)objForm1.AdminComments)),
                    new SqlParameter("@Field1",(objForm1.Field1== null ?DBNull.Value:(object)objForm1.Field1)),
                    new SqlParameter("@Field2",(objForm1.Field2== null ?DBNull.Value:(object)objForm1.Field2)),
                    new SqlParameter("@Field3",(objForm1.Field3== null ?DBNull.Value:(object)objForm1.Field3)),
                    new SqlParameter("@InstertedBy",objForm1.InstertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objForm1.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[32].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEForm1Update", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[32].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetForm1ListByVariable(Int64 EventId, Int64 RegistrationCategoryId, Int64 PaymentMethodId, Int64 PaymentStatusId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@RegistrationCategoryId",RegistrationCategoryId),
                    new SqlParameter("@PaymentMethodId",PaymentMethodId),
                    new SqlParameter("@PaymentStatusId",PaymentStatusId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Form1GetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetForm1ById(Int64 CompetitionRegistrationId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@CompetitionRegistrationId",CompetitionRegistrationId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Form1GetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteForm1ById(Int64 CompetitionRegistrationId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@CompetitionRegistrationId",CompetitionRegistrationId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Form1Delete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateForm1StatusById(Int64 CompetitionRegistrationId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@CompetitionRegistrationId",CompetitionRegistrationId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("Form1UpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetForm1List(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Form1GetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable Form1ExportToExcel(Int64 EventId, Int64 RegistrationCategoryId, Int64 MemberId, Int64 PaymentMethodId, Int64 PaymentStatusId, string Search, string Sort, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@RegistrationCategoryId",RegistrationCategoryId),
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@PaymentMethodId",PaymentMethodId),
                    new SqlParameter("@PaymentStatusId",PaymentStatusId),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Form1ExportToExcel", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
