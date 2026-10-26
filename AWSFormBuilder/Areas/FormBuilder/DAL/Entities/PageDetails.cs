using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
    public class PageDetails
    {
        public Int64 RId { get; set; }
        public Int64 PageDetailId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string PageUrl { get; set; }
        public string DocumentUrl { get; set; }
        public string Target { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string TopLine { get; set; }
        public Boolean IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string OtherUrl { get; set; }
        public Int64 MenuPageId { get; set; }
        public Int64 MenuItemId { get; set; }

        public string DisplayName { get; set; }

        public string ChapterName { get; set; }
        public Int64 ChapterId { get; set; }

        public string ParentName { get; set; }
        public Int64 PageParentId { get; set; }
        public string ParentUrl { get; set; }
        public Int32 Position { get; set; }
        public Boolean IsTopBar { get; set; }
        public Boolean IsMenuBar { get; set; }
        public Boolean IsQuickLinks { get; set; }
        public Boolean IsFooterBar { get; set; }

        public List<Entities.News> lstNews { get; set; }
        public List<Entities.Chapters> lstChapters { get; set; }
        public List<Entities.MenuItems> lstQuickLinkItems { get; set; }

        public List<Entities.PageDetails> lstInnerPages { get; set; }
        public List<Entities.MenuItems> lstInnerPageCategories { get; set; }
        public Entities.Members objMembers { get; set; }
        public Entities.PageDetails objCInnerPages { get; set; }
        public Entities.PageDetails objPInnerPages { get; set; }
        public Entities.PageDetails objPHInnerPages { get; set; }
        public Entities.PageDetails objPMInnerPages { get; set; }
        public Entities.PageDetails objSInnerPages { get; set; }
        public Entities.PageDetails objseodetails { get; set; }
        public List<Entities.MenuItems> lstMenuItems { get; set; }
        public List<Entities.MenuItems> lstMenuItems2 { get; set; }
        public List<Entities.MenuItems> lstMenuItems3 { get; set; }
        public List<Entities.MenuItems> lstMenuItems4 { get; set; }
       
        public List<Entities.MenuItems> FooterMenuItems { get; set; }
        



        public string AddPage { get; set; }
        public Int64 ExistingMenuItemId { get; set; }

    }

    public class MenuPages
    {
        public Int64 RId { get; set; }
        public Int64 MenuPageId { get; set; }
        public Int64 MenuItemId { get; set; }
        public Int64 PageDetailId { get; set; }
        public string Heading { get; set; } 
    }
}
