using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class InnerPageCategories
    {
        public Int64 RId { get; set; }

        public Int64 InnerPageCategoryId { get; set; }
        public Int64 ChapterId { get; set; }
        public Int64 InnerPagesCount { get; set; }

        public string DisplayName { get; set; }
        public Int32 PageLevel { get; set; }
        public Int64 PageParentId { get; set; }
        public string ParentPageName { get; set; }
        public string IdPath { get; set; }
        public Int32 Position { get; set; }
        public Boolean IsTopBar { get; set; }
        public Boolean IsMenuBar { get; set; }
        public Boolean IsQuickLinks { get; set; }
        public Boolean IsFooterBar { get; set; }
        public Boolean IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }

        public bool ParentActive { get; set; }
        public string ParentName { get; set; }
        public int SubMenuItemCount { get; set; }

        public string ChapterName { get; set; }
        public Int64 InnerPageId { get; set; }
    }
}
