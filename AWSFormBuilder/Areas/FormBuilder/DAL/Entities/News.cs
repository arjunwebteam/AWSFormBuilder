using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
   public class News
    {
       public Int64 RId { get; set; }
       
       public Int64 NewsId { get; set; }

        public string Title { get; set; }

        public string NewsText { get; set; }

        public string ImageUrl { get; set; }

        public DateTime PostDate { get; set; }

        public Int64 OrderNo { get; set; }

        public Boolean IsActive { get; set; }

        public string UpdatedBy { get; set; }
        public Int64 ChapterId { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string ChapterName { get; set; }

        public string ChapterIds { get; set; }

        public DateTime ExpiryDate { get; set; }

        public ChapterNews objChapterCommittees = new ChapterNews();

        public List<ChapterNews> lstChapterNews { get; set; }
    }
}
