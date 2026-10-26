using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AWSFormBuilder.Entities
{
    public class DynamicForms
    {

    }

    public class FormSchemas
    {
        public Int64 FormId { get; set; }
        public string FormName { get; set; }
        public bool IsActive { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public List<FormField> Fields { get; set; }
        public string FormSchema { get; set; }
    }

    public class FormField
    {
        public Int64 FormFieldId { get; set; }
        public Int64 FormId { get; set; }
        public string Label { get; set; }
        public string Type { get; set; } // E.g., text, number, date, etc.
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public Int32 OrderNo { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public List<FormField> Fields { get; set; }
    }
 
}
