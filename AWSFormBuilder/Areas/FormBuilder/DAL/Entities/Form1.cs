using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class Form1
    {
        public Int64 Rid { get; set; } 

        public  Int64  CompetitionRegistrationId {get; set;} 

        public  Int64  EventId  {get; set;}

        public  Int64 RegistrationCategoryId {get; set;} 

        public  Int64  MemberId {get; set;}

        public  string  FirstName {get; set;}

        public  string  LastName {get; set;}

        public  DateTime  DateofBirth {get; set;}

        public  string  GradeinSchool {get; set;}

        public  string  SchoolName {get; set;} 

        public  string  ContactFullName {get; set;} 

        public  string  ContactPhone {get; set;} 

        public  string  ContactEmail {get; set;}

        public  string  Notes {get; set;}

        public  bool  TermsandConditions {get; set;} 

        public  bool IsApproved {get; set;}

        public  DateTime ApprovedDate {get; set;}

        public  Int64  PaymentStatusId {get; set;}

        public  Int64  PaymentMethodId {get; set;}

        public  DateTime  PaymentDate {get; set;}

        public  string  TransactionId {get; set;}

        public  Decimal  AmountPaid {get; set;}

        public  string  BankName {get; set;}

        public  string  ChequeNo {get; set;}

        public  DateTime  ChequeDate {get; set;}

        public  string  AdminComments {get; set;}

        public  string  Field1 {get; set;}

        public  string  Field2 {get; set;}

        public  string  Field3 {get; set;}

        public  string InstertedBy {get; set;}

        public  DateTime  InsertedDate {get; set;} 

        public  string  UpdatedBy {get; set;}

        public DateTime UpdatedDate { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentMethod { get; set; }

        public string CategoryName { get; set; }

        public string EventName { get; set; }

        public string ChapterName { get; set; }

        public string CardNumber { get; set; }

        public string CSVMonth { get; set; }

        public string CSVYear { get; set; }

        public string Cvv { get; set; }

        public string CSVExpiry { get; set; }
    }
}
