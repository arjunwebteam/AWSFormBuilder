using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
   public class ChapterNews
    {
        public Int64 RId { get; set; }
        public Int64 ChapterNewsId { get; set; }
        public Int64 NewsId { get; set; }
        public Int64 ChapterId { get; set; }
    }
}
