using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
    //Today
    public class AdminMenuItems
    {
        public Int64 RId { get; set; }
        public Int64 MenuItemId { get; set; }
        public Int64 RoleMenuMasterId { get; set; }
        public Int64 MenuItemCount { get; set; }
        public Int64 ChapterId { get; set; }
        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public string comma_separated_ids { get; set; }
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
        public Int64 MenuPageId { get; set; }
        public Int64 PageDetailId { get; set; }
        public string Heading { get; set; }
        public string Target { get; set; }
        public string PageUrl { get; set; }
        public string DocumentUrl { get; set; }
        public string ChapterName { get; set; }
        public string OtherUrl { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsView { get; set; }
        public bool IsDelete { get; set; }
        public bool IsExport { get; set; }

    }

    public class RoleMenu
    {
        public Int64 MenuRolesId { get; set; }
        public Int64 UserId { get; set; }
        public Int64 RoleId { get; set; }
        public Int64 MenuItemId { get; set; }
        public string DisplayName { get; set; }
        public string PageUrl { get; set; }
        public string IdPath { get; set; }

    }

    public class Role_Menu
    {
        public Int32 RoleId { get; set; }
        public Int64 RoleMenuMasterId { get; set; }
        public Int64 ParentId { get; set; }

        public Int64 UserId { get; set; }


        public Int32 MenuId { get; set; }
        public string MenuName { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Int32 EmployeeCompanyId { get; set; }

        public Boolean IsAdd { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsView { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsExport { get; set; }
    }
}
