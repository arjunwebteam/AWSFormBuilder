using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
   public class Enquiries
    {
        public Int64 RId { get; set; }

        public Int64 EnquiryId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string Subject { get; set; }

        public string PhoneNo { get; set; }

        public Boolean IsActive { get; set; }

        public DateTime InsertedTime { get; set; }

        public Int64 EventId { get; set; }

        public string Field1 { get; set; }

        public string Field2 { get; set; }
        public string ChapterName { get; set; }
        

        public string EventName { get; set; }
        public string SgHoneypot { get; set; }
    }
}
