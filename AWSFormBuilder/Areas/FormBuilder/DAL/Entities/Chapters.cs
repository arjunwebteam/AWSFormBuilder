using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AWSFormBuilder.Entities
{
   public class Chapters
    {
        public Int64 RId { get; set; }
        public Int64 ChapterId { get; set; }
        public string ChapterName { get; set; }
        public string ShortName { get; set; }
        public string ShortDescription { get; set; }
        public string ParentChapterName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        public Int64 OrderNo { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CoordinatorEmail { get; set; }
        public string CoordinatorName { get; set; }
        public string CoordinatorPhone { get; set; }
        public string IsNotification { get; set; }


    }
}
