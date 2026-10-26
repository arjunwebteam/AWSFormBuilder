using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
   
        public class FormPaymentSettingsModel
        {
            public Int64 FormPaymentId { get; set; }
            public Int64 FormId { get; set; }
            public bool IsEnabled { get; set; }
            public string Gateway { get; set; }
            public string AmountType { get; set; }
            public decimal? FixedAmount { get; set; }
            public string Currency { get; set; } = "USD";
            public string ProductName { get; set; }
            public string ButtonText { get; set; } = "Pay Now";
            public DateTime CreatedDate { get; set; }
            public DateTime? ModifiedDate { get; set; }
        }

        public class FormPaymentSaveRequest
        {
            public Int64 FormId { get; set; }
            public bool IsEnabled { get; set; }
            public string Gateway { get; set; }
            public string AmountType { get; set; }
            public decimal? FixedAmount { get; set; }
            public string Currency { get; set; }
            public string ProductName { get; set; }
            public string ButtonText { get; set; }
        }
    
}
