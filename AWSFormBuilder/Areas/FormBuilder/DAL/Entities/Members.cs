using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class Members
    {
        #region

        public Int64 RId { get; set; }

        public Int64 MemberId { get; set; }

        public string SubscriptionId { get; set; }

        

        public Int64 ChapterId { get; set; }

        public String Title { get; set; }
        public string UserName { get; set; }

        public string RecurringType { get; set; }
        public string Honeypot { get; set; }
        public string recaptchaToken { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string Description { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public string Occupation { get; set; }

        public string SpouseFirstName { get; set; }

        public string SpouseLastName { get; set; }

        public string SpouseOccupation { get; set; }

        public string OrderType { get; set; }

        public string SpouseEmail { get; set; }

        public string SpouseCell { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public Boolean IsApproved { get; set; }

        public Boolean IsLockedOut { get; set; }

        public Boolean IsActivated { get; set; }

        public DateTime DateActivated { get; set; }

        public Guid RegistrationGUID { get; set; }

        public Decimal MemberAmount { get; set; }

        public Decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public Int64 PaymentStatusId { get; set; }

        public Int64 PaymentMethodId { get; set; }

        public Int64 MembershipOrderId { get; set; }

        public string CardNumber { get; set; }

        public string CSVMonth { get; set; }

        public string CSVYear { get; set; }

        public string Cvv { get; set; }

        public string PaymentStatus { get; set; }

        public string Fax { get; set; }

        public string WebsiteAddress { get; set; }

        public string Address2 { get; set; }

        public string PaymentMethod { get; set; }

        public string AdminComment { get; set; }

        public string UserComment { get; set; }

        public string BankName { get; set; }

        public string ChequeNo { get; set; }

        public DateTime ChequeDate { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public DateTime LastActivityDate { get; set; }

        public Int64 MembershipTypeId { get; set; }

        public string MembershipType { get; set; }

        public Boolean IsVolunteer { get; set; }

        public Boolean IsTeluguorigin { get; set; }

        public string Comments { get; set; }

        public string PaymentBy { get; set; }

        public string ReferredBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }
        public string MemberAge { get; set; }
        public string MemberSkils { get; set; }
        public string SpouseSkils { get; set; }

        public MembershipOrders objMembershipOrder = new MembershipOrders();

        public ChildrenInfo objChildrenInfo = new ChildrenInfo();

        public List<ChildrenInfo> lstChildrenInfo { get; set; }

        public List<MembershipOrders> lstMembershipOrder { get; set; }

        public List<MembershipTypes> lstMembershipType { get; set; }



        public string ChapterName { get; set; }

        public Int32 DisplayOrder { get; set; }


        public Int64 CommitteeMemberCount { get; set; }

        public string Designation { get; set; }

        public Int64 UserCount { get; set; }
        public string  Zelle { get; set; }
        public string ReceiptUrl { get; set; }
        public string ZellePartnerName { get; set; }
        public string ChequeHolderName { get; set; }


        //Category


        public Int64 CommitteeCategoryId { get; set; }

        public Int64 CommitteeMemberId { get; set; }

        public string CategoryName { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }



        //MemberInfo
        public Int64 MemberInfoId { get;set;}
        public string MBachelors { get; set; }
        public string MAdvanced { get; set; }
        public string SPouseBachelors { get; set; }
        public string SPouseAdvanced { get; set; }
        public string SelfCity { get; set; }
        public string SelfDistrict { get; set; }
        public string SelfPhoneNo { get; set; }
        public string SelfName { get; set; }
        public string SelfRelation { get; set; }
        public string SpouseCity { get; set; }
        public string SpouseDistrict { get; set; }
        public string SpousePhoneNo { get; set; }
        public string SpouseName { get; set; }
        public string SpouseRelation { get; set; }
        public string Donate { get; set; }
        public string FrequencyofDonation { get; set; }
        public Int64 NoofPayments { get; set; }
        public decimal DonationAmount { get; set; }
         public string TypeofCard { get; set; }
        public string ExipryMonth { get; set; }
        public string ExipryYear { get; set; }
        public string SameAdress { get; set; }
        public string BillingAdress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }
        public string BeneficiaryName { get; set; }
        public string UniversityName { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string NATSInsurance { get; set; }
        public string VolunteerOrganization { get; set; }
        public string AuthorNATS { get; set; }
        public string AdressNATS { get; set; }

        public string PhoneNoNATS { get; set; }
        public string EmailNATS { get; set; }
        public string KnowledgeNATS { get; set; }
        public string SgHoneypot { get; set; }
        public string MaritalStatus { get; set; }
        public string gender { get; set; }
        // In Members Entity
        [System.Xml.Serialization.XmlElement("Child1_FirstName")]
        public string Child1FirstName { get; set; }

        [System.Xml.Serialization.XmlElement("Child1_LastName")]
        public string Child1LastName { get; set; }

        [System.Xml.Serialization.XmlElement("Child1_Age")]
        public string Child1Age { get; set; }

        [System.Xml.Serialization.XmlElement("Child2_FirstName")]
        public string Child2FirstName { get; set; }

        [System.Xml.Serialization.XmlElement("Child2_LastName")]
        public string Child2LastName { get; set; }

        [System.Xml.Serialization.XmlElement("Child2_Age")]
        public string Child2Age { get; set; }

        [System.Xml.Serialization.XmlElement("Child3_FirstName")]
        public string Child3FirstName { get; set; }

        [System.Xml.Serialization.XmlElement("Child3_LastName")]
        public string Child3LastName { get; set; }

        [System.Xml.Serialization.XmlElement("Child3_Age")]
        public string Child3Age { get; set; }
        #endregion
    }

    public class MembershipOrders
    {
        public Int64 MembershipOrderId { get; set; }

        public Int64 MemberId { get; set; }

        public Int64 MembershipTypeId { get; set; }

        public string MembershipType { get; set; }

        public string OrderType { get; set; }

        public Decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public Int64 PaymentStatusId { get; set; }

        public Int64 PaymentMethodId { get; set; }

        public string PaymentBy { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentMethod { get; set; }

        public string AdminComment { get; set; }

        public string UserComment { get; set; }

        public string BankName { get; set; }

        public string ChequeNo { get; set; }

        public string CardNumber { get; set; }

        public string CSVMonth { get; set; }

        public string CSVYear { get; set; }

        public string Cvv { get; set; }

        public string CSVExpiry { get; set; }

        public DateTime ChequeDate { get; set; }

        public DateTime OrderDate { get; set; }
        public string ReceiptUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Int32 Expiry { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }
        public bool IsVolunteer { get; set; }

        public string Zelle { get; set; }
        
        public string ZellePartnerName { get; set; }
        public string ChequeHolderName { get; set; }



    }

    public class ChildrenInfo
    {
        public Int64 ChildrenInfoId { get; set; }

        public Int64 MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Relationship { get; set; }
        public string Email { get; set; }

    }
    public class MemberSubscriptions
    {
        public string SubscriptionId { get; set; }

        public Int64 MemberId { get; set; }

        public string ProfileId { get; set; }

        public string PaymentProfileId { get; set; }

        public DateOnly RecurringStartTime { get; set; }

        public string RecurringType { get; set; }
        public decimal RecurringAmount { get; set; }

    }

}
