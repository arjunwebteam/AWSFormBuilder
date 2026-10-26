using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
   public class ChapterEvents
    {
        public Int64 RId { get; set; }
        public Int64 ChapterEventId { get; set; } 
        public Int64 ChapterId { get; set; }
        public Int64 EventId { get; set; }
    }
}
