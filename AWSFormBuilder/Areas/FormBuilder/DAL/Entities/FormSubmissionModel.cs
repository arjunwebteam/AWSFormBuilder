using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
    public class FormSubmissionModel
    {
        public Int64 SubmissionId { get; set; }

        public Int64 FormId { get; set; }

        public string SubmittedData { get; set; }

        public string SubmittedBy { get; set; }

        public DateTime SubmittedDate { get; set; }

        // Payment details
        public string PaymentStatus { get; set; }

        public string PaymentTxnId { get; set; }

        public string PaymentGateway { get; set; }

        public decimal? PaymentAmount { get; set; }

        public string PaymentCurrency { get; set; }
    }
}
