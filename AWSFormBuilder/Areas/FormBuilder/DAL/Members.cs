using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class Members
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Admin

        public Int64 InsertMember(AWSFormBuilder.Entities.Members objMember,ref Int64 MemberId,ref string imageurl, ref string receipturl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",objMember.MemberId),
                    new SqlParameter("@ChapterId",(objMember.ChapterId == 0 ?DBNull.Value:(object)objMember.ChapterId)),
                    new SqlParameter("@Title",(objMember.Title == null ?DBNull.Value:(object)objMember.Title)),
                    new SqlParameter("@Email",(objMember.Email == null ?DBNull.Value:(object)objMember.Email)),
                    new SqlParameter("@Password",(objMember.Password == null ?DBNull.Value:(object)objMember.Password)),
                    new SqlParameter("@FirstName",objMember.FirstName),
                    new SqlParameter("@LastName",objMember.LastName),
                    new SqlParameter("@ProfileImage",imageurl),
                    new SqlParameter("@Occupation",(objMember.Occupation == null ?DBNull.Value:(object)objMember.Occupation)),
                    new SqlParameter("@MemberAge",(objMember.MemberAge == null ?DBNull.Value:(object)objMember.MemberAge)),
                    new SqlParameter("@MemberSkils",(objMember.MemberSkils == null ?DBNull.Value:(object)objMember.MemberSkils)),
                    new SqlParameter("@SpouseFirstName",(objMember.SpouseFirstName == null ?DBNull.Value:(object)objMember.SpouseFirstName)),
                    new SqlParameter("@SpouseLastName",(objMember.SpouseLastName == null ?DBNull.Value:(object)objMember.SpouseLastName)),
                    new SqlParameter("@SpouseOccupation",(objMember.SpouseOccupation == null ?DBNull.Value:(object)objMember.SpouseOccupation)),
                    new SqlParameter("@SpouseEmail",(objMember.SpouseEmail == null ?DBNull.Value:(object)objMember.SpouseEmail)),
                    new SqlParameter("@SpouseCell",(objMember.SpouseCell == null ?DBNull.Value:(object)objMember.SpouseCell)),
                    new SqlParameter("@SpouseSkils",(objMember.SpouseSkils == null ?DBNull.Value:(object)objMember.SpouseSkils)),
                    new SqlParameter("@Address",(objMember.Address  == null?DBNull.Value: (object)objMember.Address)),
                    new SqlParameter("@City",(objMember.City  == null?DBNull.Value: (object)objMember.City)),
                    new SqlParameter("@State",(objMember.State  == null?DBNull.Value: (object)objMember.State)),
                    new SqlParameter("@ZipCode",(objMember.ZipCode  == null?DBNull.Value: (object)objMember.ZipCode)),
                    new SqlParameter("@HomePhone",(objMember.HomePhone  == null?DBNull.Value: (object)objMember.HomePhone)),
                    new SqlParameter("@MobilePhone",(objMember.MobilePhone  == null?DBNull.Value: (object)objMember.MobilePhone)),
                    new SqlParameter("@IsApproved",objMember.IsApproved),
                    new SqlParameter("@IsLockedOut",objMember.IsLockedOut),
                    new SqlParameter("@IsActivated",objMember.IsActivated),
                    new SqlParameter("@DateActivated",(objMember.DateActivated  == DateTime.MinValue?DBNull.Value: (object)objMember.DateActivated)),
                    new SqlParameter("@RegistrationGUID",objMember.RegistrationGUID),                    
                    new SqlParameter("@FailedPasswordAttemptCount",(objMember.FailedPasswordAttemptCount == 0 ?DBNull.Value:(object)objMember.FailedPasswordAttemptCount)),
                    new SqlParameter("@LastPasswordChangedDate",(objMember.LastPasswordChangedDate == DateTime.MinValue ?DBNull.Value:(object)objMember.LastPasswordChangedDate)),
                    new SqlParameter("@LastActivityDate",(objMember.LastActivityDate == DateTime.MinValue ?DBNull.Value:(object)objMember.LastActivityDate)),
                    new SqlParameter("@MembershipTypeId",(objMember.MembershipTypeId == 0 ?DBNull.Value:(object)objMember.MembershipTypeId)),
                    new SqlParameter("@IsVolunteer",objMember.IsVolunteer),
                    new SqlParameter("@IsTeluguorigin",objMember.IsTeluguorigin),
                    new SqlParameter("@Comments",(objMember.Comments  == null?DBNull.Value: (object)objMember.Comments)),
                    new SqlParameter("@ReferredBy",(objMember.ReferredBy  == null?DBNull.Value: (object)objMember.ReferredBy)),
                    new SqlParameter("@InsertedTime",DateTime.UtcNow),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@MembershipOrderId",objMember.objMembershipOrder.MembershipOrderId),
                    new SqlParameter("@Amount",(objMember.objMembershipOrder.Amount == 0 ?DBNull.Value:(object)objMember.objMembershipOrder.Amount)),
                    new SqlParameter("@TransactionId",(objMember.TransactionId == null ?DBNull.Value:(object)objMember.TransactionId)),
                    new SqlParameter("@BankName",(objMember.objMembershipOrder.BankName == null ?DBNull.Value:(object)objMember.objMembershipOrder.BankName)),
                    new SqlParameter("@ChequeNo",(objMember.objMembershipOrder.ChequeNo == null ?DBNull.Value:(object)objMember.objMembershipOrder.ChequeNo)),
                    new SqlParameter("@ChequeDate",(objMember.objMembershipOrder.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objMember.objMembershipOrder.ChequeDate)),
                    new SqlParameter("@PaymentStatusId",(objMember.PaymentStatusId == 0 ?DBNull.Value:(object)objMember.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objMember.PaymentMethodId == 0 ?DBNull.Value:(object)objMember.PaymentMethodId)),
                    new SqlParameter("@PaymentBy",(objMember.objMembershipOrder.PaymentBy == null ?DBNull.Value:(object)objMember.objMembershipOrder.PaymentBy)),
                    new SqlParameter("@AdminComment",(objMember.objMembershipOrder.AdminComment == null ?DBNull.Value:(object)objMember.objMembershipOrder.AdminComment)),
                    new SqlParameter("@UserComment",(objMember.objMembershipOrder.UserComment == null ?DBNull.Value:(object)objMember.objMembershipOrder.UserComment)),
                    new SqlParameter("@OrderDate",(objMember.objMembershipOrder.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objMember.objMembershipOrder.OrderDate)),
                    new SqlParameter("@ExpiryDate",(objMember.objMembershipOrder.ExpiryDate == DateTime.MinValue  ?DBNull.Value:(object)objMember.objMembershipOrder.ExpiryDate)),
                    new SqlParameter("@UpdatedBy",objMember.objMembershipOrder.UpdatedBy),
                     new SqlParameter("@Fax",(objMember.Fax  == null?DBNull.Value: (object)objMember.Fax)),
                     new SqlParameter("@WebsiteAddress",(objMember.WebsiteAddress  == null?DBNull.Value: (object)objMember.WebsiteAddress)),
                     new SqlParameter("@Address2",(objMember.Address2  == null?DBNull.Value: (object)objMember.Address2)),
                    new SqlParameter("@QStatus",0),
                    //MembersInfo
                    new SqlParameter("@MemberInfoId",(objMember.MemberInfoId == 0 ?DBNull.Value:(object)objMember.MemberInfoId)),
                    new SqlParameter("@MBachelors",(objMember.MBachelors  == null?DBNull.Value: (object)objMember.MBachelors)),
                    new SqlParameter("@MAdvanced",(objMember.MAdvanced  == null?DBNull.Value: (object)objMember.MAdvanced)),
                    new SqlParameter("@SPouseBachelors",(objMember.SPouseBachelors  == null?DBNull.Value: (object)objMember.SPouseBachelors)),
                    new SqlParameter("@SPouseAdvanced",(objMember.SPouseAdvanced  == null?DBNull.Value: (object)objMember.SPouseAdvanced)),
                    new SqlParameter("@SelfCity",(objMember.SelfCity  == null?DBNull.Value: (object)objMember.SelfCity)),
                    new SqlParameter("@SelfDistrict",(objMember.SelfDistrict  == null?DBNull.Value: (object)objMember.SelfDistrict)),
                    new SqlParameter("@SelfPhoneNo",(objMember.SelfPhoneNo  == null?DBNull.Value: (object)objMember.SelfPhoneNo)),
                    new SqlParameter("@SelfName",(objMember.SelfName  == null?DBNull.Value: (object)objMember.SelfName)),
                    new SqlParameter("@SelfRelation",(objMember.SelfRelation  == null?DBNull.Value: (object)objMember.SelfRelation)),
                    new SqlParameter("@SpouseCity",(objMember.SpouseCity  == null?DBNull.Value: (object)objMember.SpouseCity)),
                    new SqlParameter("@SpouseDistrict",(objMember.SpouseDistrict  == null?DBNull.Value: (object)objMember.SpouseDistrict)),
                    new SqlParameter("@SpousePhoneNo",(objMember.SpousePhoneNo  == null?DBNull.Value: (object)objMember.SpousePhoneNo)),
                    new SqlParameter("@SpouseName",(objMember.SpouseName  == null?DBNull.Value: (object)objMember.SpouseName)),
                    new SqlParameter("@SpouseRelation",(objMember.SpouseRelation  == null?DBNull.Value: (object)objMember.SpouseRelation)),
                    new SqlParameter("@Donate",(objMember.Donate  == null?DBNull.Value: (object)objMember.Donate)),
                    new SqlParameter("@FrequencyofDonation",(objMember.FrequencyofDonation  == null?DBNull.Value: (object)objMember.FrequencyofDonation)),
                    new SqlParameter("@NoofPayments",(objMember.NoofPayments == 0 ?DBNull.Value:(object)objMember.NoofPayments)),
                    new SqlParameter("@DonationAmount",(objMember.DonationAmount == 0 ?DBNull.Value:(object)objMember.DonationAmount)),
                    new SqlParameter("@TypeofCard",(objMember.TypeofCard  == null?DBNull.Value: (object)objMember.TypeofCard)),
                    new SqlParameter("@CardNumber",(objMember.CardNumber  == null?DBNull.Value: (object)objMember.CardNumber)),
                    new SqlParameter("@ExipryMonth",(objMember.ExipryMonth  == null?DBNull.Value: (object)objMember.ExipryMonth)),
                    new SqlParameter("@ExipryYear",(objMember.ExipryYear  == null?DBNull.Value: (object)objMember.ExipryYear)),
                    new SqlParameter("@Cvv",(objMember.Cvv  == null?DBNull.Value: (object)objMember.Cvv)),
                    new SqlParameter("@SameAdress",(objMember.SameAdress  == null?DBNull.Value: (object)objMember.SameAdress)),
                    new SqlParameter("@BillingAdress",(objMember.BillingAdress  == null?DBNull.Value: (object)objMember.BillingAdress)),
                    new SqlParameter("@BillingCity",(objMember.BillingCity  == null?DBNull.Value: (object)objMember.BillingCity)),
                    new SqlParameter("@BillingState",(objMember.BillingState  == null?DBNull.Value: (object)objMember.BillingState)),
                    new SqlParameter("@BillingZipCode",(objMember.BillingZipCode  == null?DBNull.Value: (object)objMember.BillingZipCode)),
                    new SqlParameter("@NATSInsurance",(objMember.NATSInsurance  == null?DBNull.Value: (object)objMember.NATSInsurance)),
                    new SqlParameter("@BeneficiaryName",(objMember.BeneficiaryName  == null?DBNull.Value: (object)objMember.BeneficiaryName)),
                    new SqlParameter("@UniversityName",(objMember.UniversityName  == null?DBNull.Value: (object)objMember.UniversityName)),
                    new SqlParameter("@VolunteerOrganization",(objMember.VolunteerOrganization  == null?DBNull.Value: (object)objMember.VolunteerOrganization)),
                    new SqlParameter("@AuthorNATS",(objMember.AuthorNATS  == null?DBNull.Value: (object)objMember.AuthorNATS)),
                    new SqlParameter("@AdressNATS",(objMember.AdressNATS  == null?DBNull.Value: (object)objMember.AdressNATS)),
                    new SqlParameter("@PhoneNoNATS",(objMember.PhoneNoNATS  == null?DBNull.Value: (object)objMember.PhoneNoNATS)),
                    new SqlParameter("@EmailNATS",(objMember.EmailNATS  == null?DBNull.Value: (object)objMember.EmailNATS)),
                    new SqlParameter("@KnowledgeNATS",(objMember.KnowledgeNATS  == null?DBNull.Value: (object)objMember.KnowledgeNATS)),
                    new SqlParameter("@Field1",(objMember.Field1  == null?DBNull.Value: (object)objMember.Field1)),
                    new SqlParameter("@Field2",(objMember.Field2  == null?DBNull.Value: (object)objMember.Field2)),
                    new SqlParameter("@Field3",(objMember.Field3  == null?DBNull.Value: (object)objMember.Field3)),
                    new SqlParameter("@Zelle",(objMember.Zelle  == null?DBNull.Value: (object)objMember.Zelle)),
                    new SqlParameter("@ReceiptUrl",receipturl),
                    new SqlParameter("@ZellePartnerName",(objMember.ZellePartnerName  == null?DBNull.Value: (object)objMember.ZellePartnerName)),
                    new SqlParameter("@ChequeHolderName",(objMember.ChequeHolderName  == null?DBNull.Value: (object)objMember.ChequeHolderName)),
                    new SqlParameter("@MaritalStatus",(objMember.MaritalStatus  == null?DBNull.Value: (object)objMember.MaritalStatus)),
                    new SqlParameter("@gender",(objMember.gender  == null?DBNull.Value: (object)objMember.gender))




                };

                _sqlP[7].SqlDbType = SqlDbType.NVarChar;
                _sqlP[7].Size = 256;
                _sqlP[7].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[98].SqlDbType = SqlDbType.NVarChar;
                _sqlP[98].Size = 256;
                _sqlP[98].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[0].Direction = _sqlP[55].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[55].Value);
                MemberId = Convert.ToInt64(_sqlP[0].Value);
                imageurl = _sqlP[7].Value.ToString();

                receipturl = _sqlP[98].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }        

        public Int64 UpdateMemberStatus(Int64 MemberId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                //_dbAccess.SP_ExecuteScalar("MemberUpdateStatus", ref _sqlP);
                _dbAccess.SP_ExecuteScalar("MemberUpdateStatusBKP", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateMember(AWSFormBuilder.Entities.Members objMember)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",objMember.MemberId),
                    new SqlParameter("@ChapterId",(objMember.ChapterId == 0 ?DBNull.Value:(object)objMember.ChapterId)),
                    new SqlParameter("@Title",(objMember.Title == null ?DBNull.Value:(object)objMember.Title)),
                    new SqlParameter("@Email",objMember.Email == null ? DBNull.Value:(object)objMember.Email),
                    new SqlParameter("@FirstName",objMember.FirstName),
                    new SqlParameter("@LastName",objMember.LastName),
                    new SqlParameter("@Occupation",(objMember.Occupation == null ?DBNull.Value:(object)objMember.Occupation)),
                    new SqlParameter("@SpouseFirstName",(objMember.SpouseFirstName == null ?DBNull.Value:(object)objMember.SpouseFirstName)),
                    new SqlParameter("@SpouseLastName",(objMember.SpouseLastName == null ?DBNull.Value:(object)objMember.SpouseLastName)),
                    new SqlParameter("@SpouseOccupation",(objMember.SpouseOccupation == null ?DBNull.Value:(object)objMember.SpouseOccupation)),
                    new SqlParameter("@SpouseSkils",(objMember.SpouseSkils == null ?DBNull.Value:(object)objMember.SpouseSkils)),
                     new SqlParameter("@MemberAge",(objMember.MemberAge == null ?DBNull.Value:(object)objMember.MemberAge)),
                    new SqlParameter("@MemberSkils",(objMember.MemberSkils == null ?DBNull.Value:(object)objMember.MemberSkils)),
                    new SqlParameter("@SpouseEmail",(objMember.SpouseEmail == null ?DBNull.Value:(object)objMember.SpouseEmail)),
                    new SqlParameter("@SpouseCell",(objMember.SpouseCell == null ?DBNull.Value:(object)objMember.SpouseCell)),
                    new SqlParameter("@Address",(objMember.Address  == null?DBNull.Value: (object)objMember.Address)),
                    new SqlParameter("@City",(objMember.City  == null?DBNull.Value: (object)objMember.City)),
                    new SqlParameter("@State",(objMember.State  == null?DBNull.Value: (object)objMember.State)),
                    new SqlParameter("@ZipCode",(objMember.ZipCode  == null?DBNull.Value: (object)objMember.ZipCode)),
                    new SqlParameter("@HomePhone",(objMember.HomePhone  == null?DBNull.Value: (object)objMember.HomePhone)),
                    new SqlParameter("@MobilePhone",(objMember.MobilePhone  == null?DBNull.Value: (object)objMember.MobilePhone)),
                    new SqlParameter("@MembershipTypeId",(objMember.MembershipTypeId == 0 ?DBNull.Value:(object)objMember.MembershipTypeId)),
                    new SqlParameter("@IsVolunteer",objMember.IsVolunteer),
                    new SqlParameter("@IsTeluguorigin",objMember.IsTeluguorigin),
                    new SqlParameter("@Comments",(objMember.Comments  == null?DBNull.Value: (object)objMember.Comments)),
                    new SqlParameter("@ReferredBy",(objMember.ReferredBy  == null?DBNull.Value: (object)objMember.ReferredBy)),
                  
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@MembershipOrderId",objMember.MembershipOrderId),
                    new SqlParameter("@Amount",(objMember.Amount == 0 ?DBNull.Value:(object)objMember.Amount)),
                    new SqlParameter("@TransactionId",(objMember.TransactionId == null ?DBNull.Value:(object)objMember.TransactionId)),
                    new SqlParameter("@BankName",(objMember.BankName == null ?DBNull.Value:(object)objMember.BankName)),
                    new SqlParameter("@ChequeNo",(objMember.ChequeNo == null ?DBNull.Value:(object)objMember.ChequeNo)),
                    new SqlParameter("@ChequeDate",(objMember.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objMember.ChequeDate)),
                    new SqlParameter("@PaymentStatusId",(objMember.PaymentStatusId == 0 ?DBNull.Value:(object)objMember.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objMember.PaymentMethodId == 0 ?DBNull.Value:(object)objMember.PaymentMethodId)),
                    new SqlParameter("@PaymentBy",(objMember.PaymentBy == null ?DBNull.Value:(object)objMember.PaymentBy)),
                    new SqlParameter("@AdminComment",(objMember.AdminComment == null ?DBNull.Value:(object)objMember.AdminComment)),
                    new SqlParameter("@UserComment",(objMember.UserComment == null ?DBNull.Value:(object)objMember.UserComment)),
                    new SqlParameter("@OrderDate",(objMember.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objMember.OrderDate)),
                    new SqlParameter("@ExpiryDate",(objMember.ExpiryDate == DateTime.MinValue  ?DBNull.Value:(object)objMember.ExpiryDate)),
                    new SqlParameter("@UpdatedBy",objMember.UpdatedBy),
                    new SqlParameter("@Fax",(objMember.Fax  == null?DBNull.Value: (object)objMember.Fax)),
                    new SqlParameter("@WebsiteAddress",(objMember.WebsiteAddress  == null?DBNull.Value: (object)objMember.WebsiteAddress)),
                    new SqlParameter("@Address2",(objMember.Address2  == null?DBNull.Value: (object)objMember.Address2)),
                    new SqlParameter("@QStatus",0),
                    //MemberInfo
                    new SqlParameter("@MemberInfoId",(objMember.MemberInfoId == 0 ?DBNull.Value:(object)objMember.MemberInfoId)),
                    new SqlParameter("@MBachelors",(objMember.MBachelors  == null?DBNull.Value: (object)objMember.MBachelors)),
                    new SqlParameter("@MAdvanced",(objMember.MAdvanced  == null?DBNull.Value: (object)objMember.MAdvanced)),
                    new SqlParameter("@SPouseBachelors",(objMember.SPouseBachelors  == null?DBNull.Value: (object)objMember.SPouseBachelors)),
                    new SqlParameter("@SPouseAdvanced",(objMember.SPouseAdvanced  == null?DBNull.Value: (object)objMember.SPouseAdvanced)),
                    new SqlParameter("@SelfCity",(objMember.SelfCity  == null?DBNull.Value: (object)objMember.SelfCity)),
                    new SqlParameter("@SelfDistrict",(objMember.SelfDistrict  == null?DBNull.Value: (object)objMember.SelfDistrict)),
                    new SqlParameter("@SelfPhoneNo",(objMember.SelfPhoneNo  == null?DBNull.Value: (object)objMember.SelfPhoneNo)),
                    new SqlParameter("@SelfName",(objMember.SelfName  == null?DBNull.Value: (object)objMember.SelfName)),
                    new SqlParameter("@SelfRelation",(objMember.SelfRelation  == null?DBNull.Value: (object)objMember.SelfRelation)),
                    new SqlParameter("@SpouseCity",(objMember.SpouseCity  == null?DBNull.Value: (object)objMember.SpouseCity)),
                    new SqlParameter("@SpouseDistrict",(objMember.SpouseDistrict  == null?DBNull.Value: (object)objMember.SpouseDistrict)),
                    new SqlParameter("@SpousePhoneNo",(objMember.SpousePhoneNo  == null?DBNull.Value: (object)objMember.SpousePhoneNo)),
                    new SqlParameter("@SpouseName",(objMember.SpouseName  == null?DBNull.Value: (object)objMember.SpouseName)),
                    new SqlParameter("@SpouseRelation",(objMember.SpouseRelation  == null?DBNull.Value: (object)objMember.SpouseRelation)),
                    new SqlParameter("@Donate",(objMember.Donate  == null?DBNull.Value: (object)objMember.Donate)),
                    new SqlParameter("@FrequencyofDonation",(objMember.FrequencyofDonation  == null?DBNull.Value: (object)objMember.FrequencyofDonation)),
                    new SqlParameter("@NoofPayments",(objMember.NoofPayments == 0 ?DBNull.Value:(object)objMember.NoofPayments)),
                    new SqlParameter("@DonationAmount",(objMember.DonationAmount == 0 ?DBNull.Value:(object)objMember.DonationAmount)),
                    new SqlParameter("@TypeofCard",(objMember.TypeofCard  == null?DBNull.Value: (object)objMember.TypeofCard)),
                    new SqlParameter("@CardNumber",(objMember.CardNumber  == null?DBNull.Value: (object)objMember.CardNumber)),
                    new SqlParameter("@ExipryMonth",(objMember.ExipryMonth  == null?DBNull.Value: (object)objMember.ExipryMonth)),
                    new SqlParameter("@ExipryYear",(objMember.ExipryYear  == null?DBNull.Value: (object)objMember.ExipryYear)),
                    new SqlParameter("@Cvv",(objMember.Cvv  == null?DBNull.Value: (object)objMember.Cvv)),
                    new SqlParameter("@SameAdress",(objMember.SameAdress  == null?DBNull.Value: (object)objMember.SameAdress)),
                    new SqlParameter("@BillingAdress",(objMember.BillingAdress  == null?DBNull.Value: (object)objMember.BillingAdress)),
                    new SqlParameter("@BillingCity",(objMember.BillingCity  == null?DBNull.Value: (object)objMember.BillingCity)),
                    new SqlParameter("@BillingState",(objMember.BillingState  == null?DBNull.Value: (object)objMember.BillingState)),
                    new SqlParameter("@BillingZipCode",(objMember.BillingZipCode  == null?DBNull.Value: (object)objMember.BillingZipCode)),
                    new SqlParameter("@NATSInsurance",(objMember.NATSInsurance  == null?DBNull.Value: (object)objMember.NATSInsurance)),
                    new SqlParameter("@BeneficiaryName",(objMember.BeneficiaryName  == null?DBNull.Value: (object)objMember.BeneficiaryName)),
                    new SqlParameter("@UniversityName",(objMember.UniversityName  == null?DBNull.Value: (object)objMember.UniversityName)),
                    new SqlParameter("@VolunteerOrganization",(objMember.VolunteerOrganization  == null?DBNull.Value: (object)objMember.VolunteerOrganization)),
                    new SqlParameter("@AuthorNATS",(objMember.AuthorNATS  == null?DBNull.Value: (object)objMember.AuthorNATS)),
                    new SqlParameter("@AdressNATS",(objMember.AdressNATS  == null?DBNull.Value: (object)objMember.AdressNATS)),
                    new SqlParameter("@PhoneNoNATS",(objMember.PhoneNoNATS  == null?DBNull.Value: (object)objMember.PhoneNoNATS)),
                    new SqlParameter("@EmailNATS",(objMember.EmailNATS  == null?DBNull.Value: (object)objMember.EmailNATS)),
                    new SqlParameter("@KnowledgeNATS",(objMember.KnowledgeNATS  == null?DBNull.Value: (object)objMember.KnowledgeNATS)),
                    new SqlParameter("@Field1",(objMember.Field1  == null?DBNull.Value: (object)objMember.Field1)),
                    new SqlParameter("@Field2",(objMember.Field2  == null?DBNull.Value: (object)objMember.Field2)),
                    new SqlParameter("@Field3",(objMember.Field3  == null?DBNull.Value: (object)objMember.Field3)),
                    new SqlParameter("@ZellePartnerName",(objMember.ZellePartnerName  == null?DBNull.Value: (object)objMember.ZellePartnerName)),
                    new SqlParameter("@ChequeHolderName",(objMember.ChequeHolderName  == null?DBNull.Value: (object)objMember.ChequeHolderName)),
                    new SqlParameter("@MaritalStatus",(objMember.MaritalStatus  == null?DBNull.Value: (object)objMember.MaritalStatus)),
                    new SqlParameter("@gender",(objMember.gender  == null?DBNull.Value: (object)objMember.gender)),
                    new SqlParameter("@Zelle",(objMember.Zelle  == null?DBNull.Value: (object)objMember.Zelle)),




                };

                _sqlP[44].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[44].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateMemberProfile(AWSFormBuilder.Entities.Members objMember)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",objMember.MemberId),
                    new SqlParameter("@ChapterId",(objMember.ChapterId == 0 ?DBNull.Value:(object)objMember.ChapterId)),
                    new SqlParameter("@Title",(objMember.Title == null ?DBNull.Value:(object)objMember.Title)),
                    new SqlParameter("@FirstName",objMember.FirstName),
                    new SqlParameter("@LastName",objMember.LastName),
                    new SqlParameter("@Occupation",(objMember.Occupation == null ?DBNull.Value:(object)objMember.Occupation)),
                    new SqlParameter("@MemberSkils",(objMember.MemberSkils == null ?DBNull.Value:(object)objMember.MemberSkils)),
                    new SqlParameter("@SpouseSkils",(objMember.SpouseSkils == null ?DBNull.Value:(object)objMember.SpouseSkils)),
                    new SqlParameter("@SpouseFirstName",(objMember.SpouseFirstName == null ?DBNull.Value:(object)objMember.SpouseFirstName)),
                    new SqlParameter("@SpouseLastName",(objMember.SpouseLastName == null ?DBNull.Value:(object)objMember.SpouseLastName)),
                    new SqlParameter("@SpouseOccupation",(objMember.SpouseOccupation == null ?DBNull.Value:(object)objMember.SpouseOccupation)),
                    new SqlParameter("@SpouseEmail",(objMember.SpouseEmail == null ?DBNull.Value:(object)objMember.SpouseEmail)),
                    new SqlParameter("@SpouseCell",(objMember.SpouseCell == null ?DBNull.Value:(object)objMember.SpouseCell)),
                    new SqlParameter("@Address",(objMember.Address  == null?DBNull.Value: (object)objMember.Address)),
                    new SqlParameter("@City",(objMember.City  == null?DBNull.Value: (object)objMember.City)),
                    new SqlParameter("@State",(objMember.State  == null?DBNull.Value: (object)objMember.State)),
                    new SqlParameter("@ZipCode",(objMember.ZipCode  == null?DBNull.Value: (object)objMember.ZipCode)),
                    new SqlParameter("@HomePhone",(objMember.HomePhone  == null?DBNull.Value: (object)objMember.HomePhone)),
                    new SqlParameter("@MobilePhone",(objMember.MobilePhone  == null?DBNull.Value: (object)objMember.MobilePhone)),
                    new SqlParameter("@MembershipTypeId",(objMember.MembershipTypeId == 0 ?DBNull.Value:(object)objMember.MembershipTypeId)),
                    new SqlParameter("@IsVolunteer",objMember.IsVolunteer),
                    new SqlParameter("@Comments",(objMember.Comments  == null?DBNull.Value: (object)objMember.Comments)),
                    new SqlParameter("@ReferredBy",(objMember.ReferredBy  == null?DBNull.Value: (object)objMember.ReferredBy)),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objMember.UpdatedBy),
                    new SqlParameter("@Fax",(objMember.Fax  == null?DBNull.Value: (object)objMember.Fax)),
                    new SqlParameter("@WebsiteAddress",(objMember.WebsiteAddress  == null?DBNull.Value: (object)objMember.WebsiteAddress)),
                    new SqlParameter("@Address2",(objMember.Address2  == null?DBNull.Value: (object)objMember.Address2)),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@SpousePhoneNo",(objMember.SpousePhoneNo == null ?DBNull.Value:(object)objMember.SpousePhoneNo)), 
                    new SqlParameter("@SPouseBachelors",(objMember.SPouseBachelors == null ?DBNull.Value:(object)objMember.SPouseBachelors)),
                    new SqlParameter("@SPouseAdvanced",(objMember.SPouseAdvanced == null ?DBNull.Value:(object)objMember.SPouseAdvanced)),
                     
                    new SqlParameter("@SelfCity",(objMember.SelfCity  == null?DBNull.Value: (object)objMember.SelfCity)),
                    new SqlParameter("@SelfDistrict",(objMember.SelfDistrict  == null?DBNull.Value: (object)objMember.SelfDistrict)),
                    new SqlParameter("@SelfPhoneNo",(objMember.SelfPhoneNo  == null?DBNull.Value: (object)objMember.SelfPhoneNo)),
                    new SqlParameter("@SelfName",(objMember.SelfName  == null?DBNull.Value: (object)objMember.SelfName)),
                    new SqlParameter("@SelfRelation",(objMember.SelfRelation  == null?DBNull.Value: (object)objMember.SelfRelation)),
                    new SqlParameter("@SpouseCity",(objMember.SpouseCity  == null?DBNull.Value: (object)objMember.SpouseCity)),
                    new SqlParameter("@SpouseDistrict",(objMember.SpouseDistrict  == null?DBNull.Value: (object)objMember.SpouseDistrict)), 
                    new SqlParameter("@SpouseName",(objMember.SpouseName  == null?DBNull.Value: (object)objMember.SpouseName)),
                    new SqlParameter("@SpouseRelation",(objMember.SpouseRelation == null ?DBNull.Value:(object)objMember.SpouseRelation)), 
                    new SqlParameter("@MBachelors",(objMember.MBachelors == null ?DBNull.Value:(object)objMember.MBachelors)),
                    new SqlParameter("@MAdvanced",(objMember.MAdvanced == null ?DBNull.Value:(object)objMember.MAdvanced))
                };

                _sqlP[28].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MemberProfileUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[28].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 ProfileEmailUpdate(string Email, Int64 MemberId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ProfileEmailUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 MemberId, ref string ProfileImage)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@ProfileImage",ProfileImage),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 256;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersProfileImage", ref _sqlP);
                ProfileImage = _sqlP[1].Value.ToString();
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetMembersListByVariable(string Search, Int64 MembershipTypeId, Int64 PaymentStatusId, string StartDate, string EndDate, string ExpiryDate, string IsVolunteer,Int64 ChapterId, string type, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MembershipTypeId",MembershipTypeId),
                    new SqlParameter("@PaymentStatusId",PaymentStatusId),
                    new SqlParameter("@StartDate",StartDate),                    
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@ExpiryDate",ExpiryDate),
                    new SqlParameter("@IsVolunteer",IsVolunteer),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@type",type)
                };

                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembersGetListByVariable", ref _sqlP); 
                 Total = Convert.ToInt32(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public DataTable ExportMembersList(Int64 ChapterId, string Search, Int64 MembershipTypeId, Int64 PaymentStatusId,
         string StartDate, string EndDate, string IsVolunteer, string Sort, string type)   // ✅ new parameter
        {
            DataTable dt = null;
            int Total = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@ChapterId", ChapterId),
            new SqlParameter("@MembershipTypeId", MembershipTypeId),
            new SqlParameter("@PaymentStatusId", PaymentStatusId),
            new SqlParameter("@StartDate", StartDate),
            new SqlParameter("@EndDate", EndDate),
            new SqlParameter("@IsVolunteer", IsVolunteer),
            new SqlParameter("@Search", Search),
            new SqlParameter("@Sort", Sort),
            new SqlParameter("@Type", type),          // ✅ new parameter
            new SqlParameter("@Total", Total),
        };
                _sqlP[9].Direction = System.Data.ParameterDirection.Output;   // ✅ index shifted from 8 to 9
                dt = _dbAccess.GetDataTable("ExportMembersGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[9].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetMembersOrderDetailsListByVariable(string Search,string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("MembersOrderDetailsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMembersList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembersGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
public DataTable MemberLogsGetList(Int64 MemberId,ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                     new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MemberLogsGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMemberById(Int64 MemberId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembersGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteMember(Int64 MemberId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 DeleteMemberOrder(Int64 MemberOrderId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberOrderId",MemberOrderId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MemberorderDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 DeleteChildInfo(Int64 ChildInfoId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@ChildInfoId",ChildInfoId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ChildInfoDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataSet GetMembersFullDetailsById(Int64 MemberId, ref int status)
        {
            DataSet ds2 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds2 = _dbAccess.GetDataSet("MembersGetFullDetailsById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds2;
        }

        public DataSet GetMembersFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds2 = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds2 = _dbAccess.GetDataSet("MembersGetFullDetailsByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds2;
        }

        public DataSet GetMemberFullDetailsById(Int64 MemberId, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MemberGetFullDetailsById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataSet GetMemberFullDetailsBySpouseCell(string SpouseCell, string LastName, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@SpouseCell",SpouseCell),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@LastName",LastName)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MembersGetFullDetailsBySpouseCell", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataSet GetMemberFullDetailsBySpouse(string SpouseCell, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@SpouseCell",SpouseCell),
                    new SqlParameter("@QStatus",0),
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MembersGetFullDetailsBySpouse", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }
       
        public DataSet GetMemberFullDetailsByLastName(string LastName, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    
                    new SqlParameter("@LastName",LastName),
                    new SqlParameter("@QStatus",0),
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MembersGetFullDetailsByLastName", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataTable GetMemberOrderById(Int64 MemberOrderId, ref int status)
        {
            DataTable ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberOrderId",MemberOrderId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataTable("MemberOrderById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataSet GetMemberFullDetailsByUserName(string UserName, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@UserName",UserName),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MemberGetFullDetailsByUserName", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public Int64 DeleteAllMembers(string MemberId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };

                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersDeleteAll", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public Int64 CancelMemberSubscription(Int64 MemberId, ref int QStatus)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@MemberId", MemberId),
            new SqlParameter("@QStatus",  0)
        };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("CancelMemberSubscription", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
                QStatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public DataSet GetMemberFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MemberGetFullDetailsByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataSet GetMemberFullDetailsByPhoneNo(string MobilePhone, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MobilePhone",MobilePhone),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("MemberGetFullDetailsByPhoneNo", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataSet FEGetMemberFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds0 = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds0 = _dbAccess.GetDataSet("FEMemberGetFullDetailsByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds0;
        }

        public DataSet AddMembershipRequirement(ref int status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("AddMembershipRequirement", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet AddMembershipRequirementByChapterId(Int64 ChapterId, ref int status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("AddMembershipRequirementByChapterId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public Int64 InsertChildrenInfo(AWSFormBuilder.Entities.ChildrenInfo objChildrenInfo)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@ChildrenInfoId",objChildrenInfo.ChildrenInfoId),
                    new SqlParameter("@MemberId",(objChildrenInfo.MemberId==0?(object)DBNull.Value:objChildrenInfo.MemberId)),
                    new SqlParameter("@Age",(objChildrenInfo.Age==0?(object)DBNull.Value:objChildrenInfo.Age)),
                    new SqlParameter("@FirstName",(objChildrenInfo.FirstName==null?(object)DBNull.Value:objChildrenInfo.FirstName)),
                    new SqlParameter("@LastName",(objChildrenInfo.LastName==null?(object)DBNull.Value:objChildrenInfo.LastName)),
                    new SqlParameter("@Relationship",(objChildrenInfo.Relationship==null?(object)DBNull.Value:objChildrenInfo.Relationship)),
                    new SqlParameter("@Email",(objChildrenInfo.Email==null?(object)DBNull.Value:objChildrenInfo.Email)),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ChildrenInfoInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertMemberOrder(AWSFormBuilder.Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                        new SqlParameter("@MemberId",objMembershipOrders.MemberId),
                        new SqlParameter("@MembershipTypeId",objMembershipOrders.MembershipTypeId),
                        new SqlParameter("@MembershipOrderId",objMembershipOrders.MembershipOrderId),
                        new SqlParameter("@Amount",(objMembershipOrders.Amount == 0 ?DBNull.Value:(object)objMembershipOrders.Amount)),
                        new SqlParameter("@PaymentMethodId",(objMembershipOrders.PaymentMethodId == 0 ?DBNull.Value:(object)objMembershipOrders.PaymentMethodId)),
                        new SqlParameter("@PaymentStatusId",(objMembershipOrders.PaymentStatusId == 0 ?DBNull.Value:(object)objMembershipOrders.PaymentStatusId)),
                        new SqlParameter("@TransactionId",(objMembershipOrders.TransactionId == null ?DBNull.Value:(object)objMembershipOrders.TransactionId)),
                        new SqlParameter("@BankName",(objMembershipOrders.BankName == null ?DBNull.Value:(object)objMembershipOrders.BankName)),
                        new SqlParameter("@ChequeNo",(objMembershipOrders.ChequeNo == null ?DBNull.Value:(object)objMembershipOrders.ChequeNo)),
                        new SqlParameter("@ChequeDate",(objMembershipOrders.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objMembershipOrders.ChequeDate)),
                        new SqlParameter("@PaymentStatus",(objMembershipOrders.PaymentStatus == null ?DBNull.Value:(object)objMembershipOrders.PaymentStatus)),
                        new SqlParameter("@PaymentMethod",(objMembershipOrders.PaymentMethod == null?DBNull.Value:(object)objMembershipOrders.PaymentMethod)),
                        new SqlParameter("@PaymentBy",(objMembershipOrders.PaymentBy == null ?DBNull.Value:(object)objMembershipOrders.PaymentBy)),
                        new SqlParameter("@OrderType",(objMembershipOrders.OrderType == null ?DBNull.Value:(object)objMembershipOrders.OrderType)),
                        new SqlParameter("@AdminComment",(objMembershipOrders.AdminComment == null ?DBNull.Value:(object)objMembershipOrders.AdminComment)),
                        new SqlParameter("@UserComment",(objMembershipOrders.UserComment == null ?DBNull.Value:(object)objMembershipOrders.UserComment)),
                        new SqlParameter("@OrderDate",(objMembershipOrders.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objMembershipOrders.OrderDate)),
                        new SqlParameter("@ExpiryDate",(objMembershipOrders.ExpiryDate == DateTime.MinValue  ?DBNull.Value:(object)objMembershipOrders.ExpiryDate)),
                        new SqlParameter("@IsVolunteer",objMembershipOrders.IsVolunteer),
                        new SqlParameter("@UpdatedTime",objMembershipOrders.UpdatedTime),
                        new SqlParameter("@UpdatedBy",objMembershipOrders.UpdatedBy),
                        new SqlParameter("@QStatus",0)
                    };
                _sqlP[21].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MemberOrderInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[21].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public Int64 UpdateMemberSubscriptions(AWSFormBuilder.Entities.MemberSubscriptions objMemberSubscriptions)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@MemberId",         objMemberSubscriptions.MemberId),
            new SqlParameter("@SubscriptionId",   (objMemberSubscriptions.SubscriptionId == null   ? DBNull.Value : (object)objMemberSubscriptions.SubscriptionId)),
            new SqlParameter("@ProfileId",        (objMemberSubscriptions.ProfileId == null        ? DBNull.Value : (object)objMemberSubscriptions.ProfileId)),
            new SqlParameter("@PaymentProfileId", (objMemberSubscriptions.PaymentProfileId == null ? DBNull.Value : (object)objMemberSubscriptions.PaymentProfileId)),
            new SqlParameter("@RecurringType",    (objMemberSubscriptions.RecurringType == null    ? DBNull.Value : (object)objMemberSubscriptions.RecurringType)),
            new SqlParameter("@RecurringAmount",  objMemberSubscriptions.RecurringAmount),
            new SqlParameter("@RecurringStartTime", objMemberSubscriptions.RecurringStartTime == DateOnly.MinValue
                                                        ? DBNull.Value
                                                        : (object)objMemberSubscriptions.RecurringStartTime.ToDateTime(TimeOnly.MinValue)),
            new SqlParameter("@QStatus", 0)
        };
                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateMemberSubscriptions", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public Int64 MemberTransactionIdUpdate(AWSFormBuilder.Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                        new SqlParameter("@MemberId",objMembershipOrders.MemberId),
                        new SqlParameter("@PaymentMethodId",(objMembershipOrders.PaymentMethodId == 0 ?DBNull.Value:(object)objMembershipOrders.PaymentMethodId)),
                        new SqlParameter("@PaymentStatusId",(objMembershipOrders.PaymentStatusId == 0 ?DBNull.Value:(object)objMembershipOrders.PaymentStatusId)),
                        new SqlParameter("@TransactionId",(objMembershipOrders.TransactionId == null ?DBNull.Value:(object)objMembershipOrders.TransactionId)),
                        new SqlParameter("@PaymentStatus",(objMembershipOrders.PaymentStatus == null ?DBNull.Value:(object)objMembershipOrders.PaymentStatus)),
                        new SqlParameter("@PaymentMethod",(objMembershipOrders.PaymentMethod == null?DBNull.Value:(object)objMembershipOrders.PaymentMethod)),
                        new SqlParameter("@UpdatedTime",objMembershipOrders.UpdatedTime),
                         new SqlParameter("@OrderDate",objMembershipOrders.OrderDate),
                        new SqlParameter("@UpdatedBy",objMembershipOrders.UpdatedBy),
                        new SqlParameter("@QStatus",0)
                    };
                _sqlP[9].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MemberTransactionIdUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[9].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertMemberOrderRenewal(AWSFormBuilder.Entities.MembershipOrders objMembershipOrders, ref string imageurl, ref string receipturl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                          
                        new SqlParameter("@MemberId",objMembershipOrders.MemberId),
                        new SqlParameter("@MembershipTypeId",objMembershipOrders.MembershipTypeId),
                        new SqlParameter("@MembershipOrderId",objMembershipOrders.MembershipOrderId),
                        new SqlParameter("@Amount",(objMembershipOrders.Amount == 0 ?DBNull.Value:(object)objMembershipOrders.Amount)),
                        new SqlParameter("@PaymentMethodId",(objMembershipOrders.PaymentMethodId == 0 ?DBNull.Value:(object)objMembershipOrders.PaymentMethodId)),
                        new SqlParameter("@PaymentStatusId",(objMembershipOrders.PaymentStatusId == 0 ?DBNull.Value:(object)objMembershipOrders.PaymentStatusId)),
                        new SqlParameter("@TransactionId",(objMembershipOrders.TransactionId == null ?DBNull.Value:(object)objMembershipOrders.TransactionId)),
                        new SqlParameter("@BankName",(objMembershipOrders.BankName == null ?DBNull.Value:(object)objMembershipOrders.BankName)),
                        new SqlParameter("@ChequeNo",(objMembershipOrders.ChequeNo == null ?DBNull.Value:(object)objMembershipOrders.ChequeNo)),
                        new SqlParameter("@ChequeDate",(objMembershipOrders.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objMembershipOrders.ChequeDate)),
                        new SqlParameter("@PaymentStatus",(objMembershipOrders.PaymentStatus == null ?DBNull.Value:(object)objMembershipOrders.PaymentStatus)),
                        new SqlParameter("@PaymentMethod",(objMembershipOrders.PaymentMethod == null?DBNull.Value:(object)objMembershipOrders.PaymentMethod)),
                        new SqlParameter("@PaymentBy",(objMembershipOrders.PaymentBy == null ?DBNull.Value:(object)objMembershipOrders.PaymentBy)),
                        new SqlParameter("@OrderType",(objMembershipOrders.OrderType == null ?DBNull.Value:(object)objMembershipOrders.OrderType)),
                        new SqlParameter("@AdminComment",(objMembershipOrders.AdminComment == null ?DBNull.Value:(object)objMembershipOrders.AdminComment)),
                        new SqlParameter("@UserComment",(objMembershipOrders.UserComment == null ?DBNull.Value:(object)objMembershipOrders.UserComment)),
                        new SqlParameter("@OrderDate",(objMembershipOrders.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objMembershipOrders.OrderDate)),
                        new SqlParameter("@ExpiryDate",(objMembershipOrders.ExpiryDate == DateTime.MinValue  ?DBNull.Value:(object)objMembershipOrders.ExpiryDate)),
                        new SqlParameter("@IsVolunteer",objMembershipOrders.IsVolunteer),
                        new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                        new SqlParameter("@UpdatedBy",objMembershipOrders.UpdatedBy),
                        new SqlParameter("@ReceiptUrl",receipturl),
                       new SqlParameter("@QStatus",0),
                           new SqlParameter("@ZellePartnerName",(objMembershipOrders.ZellePartnerName  == null?DBNull.Value: (object)objMembershipOrders.ZellePartnerName)),
                        new SqlParameter("@ChequeHolderName",(objMembershipOrders.ChequeHolderName  == null?DBNull.Value: (object)objMembershipOrders.ChequeHolderName)),
                        new SqlParameter("@Zelle",(objMembershipOrders.Zelle  == null?DBNull.Value: (object)objMembershipOrders.Zelle)),


                    };
                _sqlP[21].SqlDbType = SqlDbType.NVarChar;
                _sqlP[21].Size = 256;
                _sqlP[21].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[22].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MemberOrderInsertRenewal", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[22].Value);
                receipturl = _sqlP[21].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 ChangePassword(Int64 MemberId, string Password, string FType)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@Password",Password), 
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@FType",FType)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersChangePassword", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetPassword(Int64 _Memberid, ref int _QStatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] {
                new SqlParameter("@MemberId",_Memberid),
                new SqlParameter("@QStatus",_QStatus)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("MembersGetPassword", ref _sqlP);
                _QStatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateMemberProfileImage(Int64 MemberId, ref string ProfileImage)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@ProfileImage",ProfileImage),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 256;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MemberProfileImage", ref _sqlP);
                ProfileImage = _sqlP[1].Value.ToString();
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



        #endregion

        #region Front-End

        public Int64 FEInsertMember(AWSFormBuilder.Entities.Members objMember, ref Int64 MemberId, ref string imageurl, ref string receipturl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MemberId",objMember.MemberId),
                    new SqlParameter("@ChapterId",(objMember.ChapterId == 0 ?DBNull.Value:(object)objMember.ChapterId)),
                    new SqlParameter("@Title",(objMember.Title == null ?DBNull.Value:(object)objMember.Title)),
                    new SqlParameter("@Email",objMember.Email ),
                    new SqlParameter("@Password",(objMember.Password == null ?DBNull.Value:(object)objMember.Password)),
                    new SqlParameter("@FirstName",objMember.FirstName),
                    new SqlParameter("@LastName",objMember.LastName),
                    new SqlParameter("@ProfileImage",imageurl),
                    new SqlParameter("@Occupation",(objMember.Occupation == null ?DBNull.Value:(object)objMember.Occupation)),
                    new SqlParameter("@MemberAge",(objMember.MemberAge == null ?DBNull.Value:(object)objMember.MemberAge)),
                    new SqlParameter("@MemberSkils",(objMember.MemberSkils == null ?DBNull.Value:(object)objMember.MemberSkils)),
                    new SqlParameter("@SpouseFirstName",(objMember.SpouseFirstName == null ?DBNull.Value:(object)objMember.SpouseFirstName)),
                    new SqlParameter("@SpouseLastName",(objMember.SpouseLastName == null ?DBNull.Value:(object)objMember.SpouseLastName)),
                    new SqlParameter("@SpouseOccupation",(objMember.SpouseOccupation == null ?DBNull.Value:(object)objMember.SpouseOccupation)),
                    new SqlParameter("@SpouseEmail",(objMember.SpouseEmail == null ?DBNull.Value:(object)objMember.SpouseEmail)),
                    new SqlParameter("@SpouseCell",(objMember.SpouseCell == null ?DBNull.Value:(object)objMember.SpouseCell)),
                    new SqlParameter("@SpouseSkils",(objMember.SpouseSkils == null ?DBNull.Value:(object)objMember.SpouseSkils)),
                    new SqlParameter("@Address",(objMember.Address  == null?DBNull.Value: (object)objMember.Address)),
                    new SqlParameter("@City",(objMember.City  == null?DBNull.Value: (object)objMember.City)),
                    new SqlParameter("@State",(objMember.State  == null?DBNull.Value: (object)objMember.State)),
                    new SqlParameter("@ZipCode",(objMember.ZipCode  == null?DBNull.Value: (object)objMember.ZipCode)),
                    new SqlParameter("@HomePhone",(objMember.HomePhone  == null?DBNull.Value: (object)objMember.HomePhone)),
                    new SqlParameter("@MobilePhone",(objMember.MobilePhone  == null?DBNull.Value: (object)objMember.MobilePhone)),
                    new SqlParameter("@IsApproved",objMember.IsApproved),
                    new SqlParameter("@IsLockedOut",objMember.IsLockedOut),
                    new SqlParameter("@IsActivated",objMember.IsActivated),
                    new SqlParameter("@DateActivated",(objMember.DateActivated  == DateTime.MinValue?DBNull.Value: (object)objMember.DateActivated)),
                    new SqlParameter("@RegistrationGUID",objMember.RegistrationGUID),
                    new SqlParameter("@FailedPasswordAttemptCount",(objMember.FailedPasswordAttemptCount == 0 ?DBNull.Value:(object)objMember.FailedPasswordAttemptCount)),
                    new SqlParameter("@LastPasswordChangedDate",(objMember.LastPasswordChangedDate == DateTime.MinValue ?DBNull.Value:(object)objMember.LastPasswordChangedDate)),
                    new SqlParameter("@LastActivityDate",(objMember.LastActivityDate == DateTime.MinValue ?DBNull.Value:(object)objMember.LastActivityDate)),
                    new SqlParameter("@MembershipTypeId",(objMember.MembershipTypeId == 0 ?DBNull.Value:(object)objMember.MembershipTypeId)),
                    new SqlParameter("@IsVolunteer",objMember.IsVolunteer),
                    new SqlParameter("@IsTeluguorigin",objMember.IsTeluguorigin),
                    new SqlParameter("@Comments",(objMember.Comments  == null?DBNull.Value: (object)objMember.Comments)),
                    new SqlParameter("@ReferredBy",(objMember.ReferredBy  == null?DBNull.Value: (object)objMember.ReferredBy)),
                    new SqlParameter("@InsertedTime",objMember.InsertedTime),
                    new SqlParameter("@UpdatedTime",objMember.UpdatedTime),
                    new SqlParameter("@MembershipOrderId",objMember.objMembershipOrder.MembershipOrderId),
                    new SqlParameter("@Amount",(objMember.objMembershipOrder.Amount == 0 ?DBNull.Value:(object)objMember.objMembershipOrder.Amount)),
                    new SqlParameter("@TransactionId",(objMember.objMembershipOrder.TransactionId == null ?DBNull.Value:(object)objMember.objMembershipOrder.TransactionId)),
                    new SqlParameter("@BankName",(objMember.objMembershipOrder.BankName == null ?DBNull.Value:(object)objMember.objMembershipOrder.BankName)),
                    new SqlParameter("@ChequeNo",(objMember.objMembershipOrder.ChequeNo == null ?DBNull.Value:(object)objMember.objMembershipOrder.ChequeNo)),
                    new SqlParameter("@ChequeDate",(objMember.objMembershipOrder.ChequeDate == DateTime.MinValue ?DBNull.Value:(object)objMember.objMembershipOrder.ChequeDate)),
                    new SqlParameter("@PaymentStatusId",(objMember.objMembershipOrder.PaymentStatusId == 0 ?DBNull.Value:(object)objMember.objMembershipOrder.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objMember.objMembershipOrder.PaymentMethodId == 0 ?DBNull.Value:(object)objMember.objMembershipOrder.PaymentMethodId)),
                    new SqlParameter("@PaymentBy",(objMember.objMembershipOrder.PaymentBy == null ?DBNull.Value:(object)objMember.objMembershipOrder.PaymentBy)),
                    new SqlParameter("@AdminComment",(objMember.objMembershipOrder.AdminComment == null ?DBNull.Value:(object)objMember.objMembershipOrder.AdminComment)),
                    new SqlParameter("@UserComment",(objMember.objMembershipOrder.UserComment == null ?DBNull.Value:(object)objMember.objMembershipOrder.UserComment)),
                    new SqlParameter("@OrderDate",(objMember.objMembershipOrder.OrderDate == DateTime.MinValue ?DBNull.Value:(object)objMember.objMembershipOrder.OrderDate)),
                    new SqlParameter("@ExpiryDate",(objMember.objMembershipOrder.ExpiryDate == DateTime.MinValue  ?DBNull.Value:(object)objMember.objMembershipOrder.ExpiryDate)),
                    new SqlParameter("@UpdatedBy",objMember.objMembershipOrder.UpdatedBy),
                     new SqlParameter("@Fax",(objMember.Fax  == null?DBNull.Value: (object)objMember.Fax)),
                     new SqlParameter("@WebsiteAddress",(objMember.WebsiteAddress  == null?DBNull.Value: (object)objMember.WebsiteAddress)),
                     new SqlParameter("@Address2",(objMember.Address2  == null?DBNull.Value: (object)objMember.Address2)),
                    new SqlParameter("@QStatus",0),
                    //MembersInfo
                    new SqlParameter("@MemberInfoId",(objMember.MemberInfoId == 0 ?DBNull.Value:(object)objMember.MemberInfoId)),
                    new SqlParameter("@MBachelors",(objMember.MBachelors  == null?DBNull.Value: (object)objMember.MBachelors)),
                    new SqlParameter("@MAdvanced",(objMember.MAdvanced  == null?DBNull.Value: (object)objMember.MAdvanced)),
                    new SqlParameter("@SPouseBachelors",(objMember.SPouseBachelors  == null?DBNull.Value: (object)objMember.SPouseBachelors)),
                    new SqlParameter("@SPouseAdvanced",(objMember.SPouseAdvanced  == null?DBNull.Value: (object)objMember.SPouseAdvanced)),
                    new SqlParameter("@SelfCity",(objMember.SelfCity  == null?DBNull.Value: (object)objMember.SelfCity)),
                    new SqlParameter("@SelfDistrict",(objMember.SelfDistrict  == null?DBNull.Value: (object)objMember.SelfDistrict)),
                    new SqlParameter("@SelfPhoneNo",(objMember.SelfPhoneNo  == null?DBNull.Value: (object)objMember.SelfPhoneNo)),
                    new SqlParameter("@SelfName",(objMember.SelfName  == null?DBNull.Value: (object)objMember.SelfName)),
                    new SqlParameter("@SelfRelation",(objMember.SelfRelation  == null?DBNull.Value: (object)objMember.SelfRelation)),
                    new SqlParameter("@SpouseCity",(objMember.SpouseCity  == null?DBNull.Value: (object)objMember.SpouseCity)),
                    new SqlParameter("@SpouseDistrict",(objMember.SpouseDistrict  == null?DBNull.Value: (object)objMember.SpouseDistrict)),
                    new SqlParameter("@SpousePhoneNo",(objMember.SpousePhoneNo  == null?DBNull.Value: (object)objMember.SpousePhoneNo)),
                    new SqlParameter("@SpouseName",(objMember.SpouseName  == null?DBNull.Value: (object)objMember.SpouseName)),
                    new SqlParameter("@SpouseRelation",(objMember.SpouseRelation  == null?DBNull.Value: (object)objMember.SpouseRelation)),
                    new SqlParameter("@Donate",(objMember.Donate  == null?DBNull.Value: (object)objMember.Donate)),
                    new SqlParameter("@FrequencyofDonation",(objMember.FrequencyofDonation  == null?DBNull.Value: (object)objMember.FrequencyofDonation)),
                    new SqlParameter("@NoofPayments",(objMember.NoofPayments == 0 ?DBNull.Value:(object)objMember.NoofPayments)),
                    new SqlParameter("@DonationAmount",(objMember.DonationAmount == 0 ?DBNull.Value:(object)objMember.DonationAmount)),
                    new SqlParameter("@TypeofCard",(objMember.TypeofCard  == null?DBNull.Value: (object)objMember.TypeofCard)),
                    new SqlParameter("@CardNumber",(objMember.CardNumber  == null?DBNull.Value: (object)objMember.CardNumber)),
                    new SqlParameter("@ExipryMonth",(objMember.ExipryMonth  == null?DBNull.Value: (object)objMember.ExipryMonth)),
                    new SqlParameter("@ExipryYear",(objMember.ExipryYear  == null?DBNull.Value: (object)objMember.ExipryYear)),
                    new SqlParameter("@Cvv",(objMember.Cvv  == null?DBNull.Value: (object)objMember.Cvv)),
                    new SqlParameter("@SameAdress",(objMember.SameAdress  == null?DBNull.Value: (object)objMember.SameAdress)),
                    new SqlParameter("@BillingAdress",(objMember.BillingAdress  == null?DBNull.Value: (object)objMember.BillingAdress)),
                    new SqlParameter("@BillingCity",(objMember.BillingCity  == null?DBNull.Value: (object)objMember.BillingCity)),
                    new SqlParameter("@BillingState",(objMember.BillingState  == null?DBNull.Value: (object)objMember.BillingState)),
                    new SqlParameter("@BillingZipCode",(objMember.BillingZipCode  == null?DBNull.Value: (object)objMember.BillingZipCode)),
                    new SqlParameter("@NATSInsurance",(objMember.NATSInsurance  == null?DBNull.Value: (object)objMember.NATSInsurance)),
                    new SqlParameter("@BeneficiaryName",(objMember.BeneficiaryName  == null?DBNull.Value: (object)objMember.BeneficiaryName)),
                    new SqlParameter("@UniversityName",(objMember.UniversityName  == null?DBNull.Value: (object)objMember.UniversityName)),
                    new SqlParameter("@VolunteerOrganization",(objMember.VolunteerOrganization  == null?DBNull.Value: (object)objMember.VolunteerOrganization)),
                    new SqlParameter("@AuthorNATS",(objMember.AuthorNATS  == null?DBNull.Value: (object)objMember.AuthorNATS)),
                    new SqlParameter("@AdressNATS",(objMember.AdressNATS  == null?DBNull.Value: (object)objMember.AdressNATS)),
                    new SqlParameter("@PhoneNoNATS",(objMember.PhoneNoNATS  == null?DBNull.Value: (object)objMember.PhoneNoNATS)),
                    new SqlParameter("@EmailNATS",(objMember.EmailNATS  == null?DBNull.Value: (object)objMember.EmailNATS)),
                    new SqlParameter("@KnowledgeNATS",(objMember.KnowledgeNATS  == null?DBNull.Value: (object)objMember.KnowledgeNATS)),
                    new SqlParameter("@Field1",(objMember.Field1  == null?DBNull.Value: (object)objMember.Field1)),
                    new SqlParameter("@Field2",(objMember.Field2  == null?DBNull.Value: (object)objMember.Field2)),
                    new SqlParameter("@Field3",(objMember.Field3  == null?DBNull.Value: (object)objMember.Field3)),
                    new SqlParameter("@Zelle",(objMember.Zelle  == null?DBNull.Value: (object)objMember.Zelle)),

                    new SqlParameter("@ReceiptUrl",receipturl),
                    new SqlParameter("@ZellePartnerName",(objMember.ZellePartnerName  == null?DBNull.Value: (object)objMember.ZellePartnerName)),
                    new SqlParameter("@ChequeHolderName",(objMember.ChequeHolderName  == null?DBNull.Value: (object)objMember.ChequeHolderName)),
                    new SqlParameter("@MaritalStatus",(objMember.MaritalStatus  == null?DBNull.Value: (object)objMember.MaritalStatus)),
                    new SqlParameter("@gender",(objMember.gender  == null?DBNull.Value: (object)objMember.gender)),

                    };
                  
                _sqlP[7].SqlDbType = SqlDbType.NVarChar;
                _sqlP[7].Size = 256;
                _sqlP[7].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[98].SqlDbType = SqlDbType.NVarChar;
                _sqlP[98].Size = 256;
                _sqlP[98].Direction = System.Data.ParameterDirection.InputOutput;
                
                _sqlP[0].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[55].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEMembersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[55].Value);

                MemberId = Convert.ToInt64(_sqlP[0].Value);
                imageurl = _sqlP[7].Value.ToString();

                receipturl = _sqlP[98].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable FEGetMembersListByVariable(Int64 MembershipTypeId, string Search, string Sort, int PageNo, int Items, ref int Total)
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
                    new SqlParameter("@MembershipTypeId", MembershipTypeId),
                    new SqlParameter("@Total",Total),
                    
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEMembersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEMemberGetFullDetails(Int64 MemberId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEMemberGetFullDetails", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public Int64 UnlockMember(Int64 MemberId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembersUnlock", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetMembersListByChapterId(Int64 ChapterId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembersGetListByChapterId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public Int64 MembersBulkUploads(string ShipmentsXML, ref string Summary, out DataTable dtResults)  
        {
            Int64 _status = 0;
            dtResults = new DataTable();
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@ShipmentsXML", ShipmentsXML),
            new SqlParameter("@Summary", Summary),
            new SqlParameter("@QStatus", 0)
        };
                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 3000000;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;

                dtResults = _dbAccess.GetDataTable("NewMembersBulkImport", ref _sqlP);

                _status = Convert.ToInt64(_sqlP[2].Value);
                Summary = _sqlP[1].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
      
        public Entities.MemberSubscriptions GetMemberSubscriptionByProfileId(
    string profileId, ref int status)
        {
            Entities.MemberSubscriptions objSub = null;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@ProfileId", profileId),
            new SqlParameter("@QStatus",   0)
        };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                DataTable dt = _dbAccess.GetDataTable(
                    "GetMemberSubscriptionByProfileId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    objSub = new Entities.MemberSubscriptions();
                    objSub.MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"]);
                    objSub.SubscriptionId = dt.Rows[0]["SubscriotionId"] != DBNull.Value
                                                ? dt.Rows[0]["SubscriotionId"].ToString() : null;
                    objSub.ProfileId = dt.Rows[0]["ProfileId"] != DBNull.Value
                                                ? dt.Rows[0]["ProfileId"].ToString() : null;
                    objSub.PaymentProfileId = dt.Rows[0]["PaymentProfileId"] != DBNull.Value
                                                ? dt.Rows[0]["PaymentProfileId"].ToString() : null;
                    objSub.RecurringType = dt.Rows[0]["RecurringType"] != DBNull.Value
                                                ? dt.Rows[0]["RecurringType"].ToString() : null;
                    objSub.RecurringAmount = dt.Rows[0]["RecurringAmount"] != DBNull.Value
                                                ? Convert.ToDecimal(dt.Rows[0]["RecurringAmount"]) : 0;
                    objSub.RecurringStartTime = dt.Rows[0]["RecurringStartTime"] != DBNull.Value
                                                ? DateOnly.FromDateTime(
                                                    Convert.ToDateTime(dt.Rows[0]["RecurringStartTime"]))
                                                : DateOnly.MinValue;
                }
            }
            catch (Exception ex) { throw ex; }
            return objSub;
        }
        public Entities.MemberSubscriptions GetMemberSubscriptionBySubId(
            string subscriptionId, ref int status)
        {
            Entities.MemberSubscriptions objSub = null;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@SubscriptionId", subscriptionId),
            new SqlParameter("@QStatus", 0)
        };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                DataTable dt = _dbAccess.GetDataTable(
                    "GetMemberSubscriptionBySubId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    objSub = new Entities.MemberSubscriptions();
                    objSub.MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"]);
                    objSub.SubscriptionId = dt.Rows[0]["SubscriotionId"] != DBNull.Value
                                                ? dt.Rows[0]["SubscriotionId"].ToString() : null;
                    objSub.ProfileId = dt.Rows[0]["ProfileId"] != DBNull.Value
                                                ? dt.Rows[0]["ProfileId"].ToString() : null;
                    objSub.PaymentProfileId = dt.Rows[0]["PaymentProfileId"] != DBNull.Value
                                                ? dt.Rows[0]["PaymentProfileId"].ToString() : null;
                    objSub.RecurringType = dt.Rows[0]["RecurringType"] != DBNull.Value
                                                ? dt.Rows[0]["RecurringType"].ToString() : null;
                    objSub.RecurringAmount = dt.Rows[0]["RecurringAmount"] != DBNull.Value
                                                ? Convert.ToDecimal(dt.Rows[0]["RecurringAmount"]) : 0;
                    objSub.RecurringStartTime = dt.Rows[0]["RecurringStartTime"] != DBNull.Value
                                                ? DateOnly.FromDateTime(
                                                    Convert.ToDateTime(dt.Rows[0]["RecurringStartTime"]))
                                                : DateOnly.MinValue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSub;
        }
        public bool IsPaymentAlreadyProcessed(string paymentId, ref int status)
        {
            bool result = false;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@PaymentId", paymentId),
            new SqlParameter("@QStatus",   0)
        };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                DataTable dt = _dbAccess.GetDataTable(
                    "IsPaymentAlreadyProcessed", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
                result = (dt != null && dt.Rows.Count > 0);
            }
            catch (Exception ex) { throw ex; }
            return result;
        }

        public void SaveProcessedPayment(string paymentId, long memberId)
        {
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@PaymentId", paymentId),
            new SqlParameter("@MemberId",  memberId),
            new SqlParameter("@QStatus",   0)
        };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("SaveProcessedPayment", ref _sqlP);
            }
            catch (Exception ex) { throw ex; }
        }
        public Entities.MemberSubscriptions GetMemberSubscriptionByEmail(
    string email, ref int status)
        {
            Entities.MemberSubscriptions objSub = null;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@Email",   email),
            new SqlParameter("@QStatus", 0)
        };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                DataTable dt = _dbAccess.GetDataTable(
                    "GetMemberSubscriptionByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    objSub = new Entities.MemberSubscriptions();
                    objSub.MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"]);
                    objSub.SubscriptionId = dt.Rows[0]["SubscriotionId"] != DBNull.Value
                                                ? dt.Rows[0]["SubscriotionId"].ToString() : null;
                    objSub.ProfileId = dt.Rows[0]["ProfileId"] != DBNull.Value
                                                ? dt.Rows[0]["ProfileId"].ToString() : null;
                    objSub.RecurringType = dt.Rows[0]["RecurringType"] != DBNull.Value
                                                ? dt.Rows[0]["RecurringType"].ToString() : null;
                    objSub.RecurringAmount = dt.Rows[0]["RecurringAmount"] != DBNull.Value
                                                ? Convert.ToDecimal(dt.Rows[0]["RecurringAmount"]) : 0;
                    objSub.RecurringStartTime = dt.Rows[0]["RecurringStartTime"] != DBNull.Value
                                                ? DateOnly.FromDateTime(
                                                    Convert.ToDateTime(dt.Rows[0]["RecurringStartTime"]))
                                                : DateOnly.MinValue;
                }
            }
            catch (Exception ex) { throw ex; }
            return objSub;
        }
        public Int64 UpdateMemberOrderExpiryBySubId(
       Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
            // ✅ Only 3 params needed — MemberId, ExpiryDate, QStatus
            new SqlParameter("@MemberId",   objMembershipOrders.MemberId),
            new SqlParameter("@ExpiryDate", objMembershipOrders.ExpiryDate),
            new SqlParameter("@QStatus",    0)
        };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar(
                    "UpdateMemberOrderExpiryBySubId", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateSubscriptionNextDate(
            Entities.MemberSubscriptions objMemberSubscriptions)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@MemberId",       objMemberSubscriptions.MemberId),
            new SqlParameter("@SubscriptionId", objMemberSubscriptions.SubscriptionId
                                                    ?? (object)DBNull.Value),
            new SqlParameter("@NextDate",       objMemberSubscriptions.RecurringStartTime
                                                    .ToDateTime(TimeOnly.MinValue)),
            new SqlParameter("@QStatus",        0)
        };
                _sqlP[3].Direction = System.Data.ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar(
                    "UpdateSubscriptionNextDate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        // ================================================================
        // 4. UpdateSubscriptionStatus - DAL
        // ================================================================
        public Int64 UpdateSubscriptionStatus(
            Entities.MemberSubscriptions objMemberSubscriptions)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
            new SqlParameter("@MemberId",       objMemberSubscriptions.MemberId),
            new SqlParameter("@SubscriptionId", objMemberSubscriptions.SubscriptionId
                                                    ?? (object)DBNull.Value),
            new SqlParameter("@Status",         objMemberSubscriptions.RecurringType
                                                    ?? (object)DBNull.Value),
            new SqlParameter("@QStatus",        0)
        };
                _sqlP[3].Direction = System.Data.ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar(
                    "UpdateSubscriptionStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion



    }
}
