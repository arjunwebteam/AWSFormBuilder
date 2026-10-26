using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
   public class ChapterCommittees
    {
        public Int64 RId { get; set; }
        public Int64 ChapterCommitteeId { get; set; }
        public Int64 ChapterId { get; set; }
        public Int64 CommitteeCategoryId { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 OrderNo { get; set; }
        public string UpdatedBy { get; set; } 
        public DateTime UpdatedTime { get; set; }
    }
}
