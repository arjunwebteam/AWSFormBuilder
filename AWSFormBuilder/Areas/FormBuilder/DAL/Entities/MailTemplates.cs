using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class MailTemplates
    {
        public Int64 RId { get; set; }

        public Int64 MailTemplateId { get; set; }

        public string Heading { get; set; }
        public string BCC { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string MailType { get; set; }

        public string LogoUrl { get; set; }

        public string FormIds { get; set; }


        public List<Int64> SelectedFormIds { get; set; } = new List<Int64>();

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
    }

    public class FormListItem
    {
        public Int64 FormId { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }

    public class SendMail
    {
        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        public string MailTemplateName { get; set; }

        public string Heading { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
