using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class AdminMenuItems
    {
        DAL.AdminMenuItems _AdminMenuItems = new DAL.AdminMenuItems();

        #region Methods

        public Int64 DeleteAdminMenuItems(Int64 MenuItemId)
        {
            Int64 _status = 0;
            if (MenuItemId != 0)
            {
                _status = _AdminMenuItems.DeleteAdminMenuItems(MenuItemId);
            }
            return _status;
        }

        public Int64 InsertAdminMenuItems(Entities.AdminMenuItems objAdminMenuItems, ref string Logo)
        {
            Int64 _status = 0;
            if (objAdminMenuItems != null)
            {
                _status = _AdminMenuItems.InsertAdminMenuItems(objAdminMenuItems, ref Logo);
            }
            return _status;
        }

        public Int64 UpdateAdminMenuItemsStatus(Int64 MenuItemId)
        {
            Int64 _status = 0;
            if (MenuItemId != 0)
            {
                _status = _AdminMenuItems.UpdateAdminMenuItemsStatus(MenuItemId);
            }
            return _status;
        }

        //public Int64 RemoveRoleMenuAccess(Int64 RoleMenuMasterId, Int64 ParentId)
        //{
        //    Int64 _status = 0;
        //    if (RoleMenuMasterId != 0)
        //    {
        //        _status = _AdminMenuItems.RemoveRoleMenuAccess(RoleMenuMasterId, ParentId);
        //    }
        //    return _status;
        //}


        public Int64 RemoveRoleMenuAccess(Int64 RoleMenuMasterId, Int64 ParentId, Int64 UserId)
        {
            Int64 _status = 0;
            if (RoleMenuMasterId != 0)
            {
                _status = _AdminMenuItems.RemoveRoleMenuAccess(RoleMenuMasterId, ParentId, UserId);
            }
            return _status;
        }










        #endregion

        #region Entity Loading

        public Entities.AdminMenuItems GetAdminMenuItemsById(Int64 MenuItemId, ref int Status)
        {
            DataTable dt = null;
            Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
            if (MenuItemId != 0)
            {
                dt = _AdminMenuItems.GetAdminMenuItemsById(MenuItemId, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objAdminMenuItems.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"]);
                    objAdminMenuItems.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                    objAdminMenuItems.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                    objAdminMenuItems.ParentName = (dt.Rows[0]["ParentName"] != DBNull.Value ? dt.Rows[0]["ParentName"].ToString() : null);
                    objAdminMenuItems.PageLevel = (dt.Rows[0]["PageLevel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageLevel"]) : 1);
                    objAdminMenuItems.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                    objAdminMenuItems.IdPath = (dt.Rows[0]["IdPath"] != DBNull.Value ? dt.Rows[0]["IdPath"].ToString() : "0"); 
                    objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dt.Rows[0]["IsFooterBar"]);
                    objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dt.Rows[0]["IsMenuBar"]);
                    objAdminMenuItems.IsQuickLinks = (dt.Rows[0]["IsQuickLinks"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsQuickLinks"]) : false);
                    objAdminMenuItems.IsTopBar = Convert.ToBoolean(dt.Rows[0]["IsTopBar"]); 
                    objAdminMenuItems.Position = (dt.Rows[0]["Position"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Position"]) : 0);
                    objAdminMenuItems.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                    objAdminMenuItems.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);
                    objAdminMenuItems.Logo = (dt.Rows[0]["Logo"] != DBNull.Value ? dt.Rows[0]["Logo"].ToString() : null);
                    objAdminMenuItems.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objAdminMenuItems.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objAdminMenuItems.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
                    objAdminMenuItems.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                    objAdminMenuItems.IsEdit = (dt.Rows[0]["IsEdit"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsEdit"]) : false);
                    objAdminMenuItems.IsView = (dt.Rows[0]["IsView"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsView"]) : false);
                    objAdminMenuItems.IsDelete = (dt.Rows[0]["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsDelete"]) : false);
                    objAdminMenuItems.IsExport = (dt.Rows[0]["IsExport"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsExport"]) : false);
                }
            }
            return objAdminMenuItems;
        }

        public List<Entities.AdminMenuItems> GetAdminMenuItemsByParentId(Int64 CategoryParentId, ref int Status)
        {
            DataTable dt = null;
            List<Entities.AdminMenuItems> lstAdminMenuItems = new List<Entities.AdminMenuItems>();
            if (CategoryParentId != 0)
            {
                dt = _AdminMenuItems.GetAdminMenuItemsByParentId(CategoryParentId, ref Status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems(); 
                        objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objAdminMenuItems.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                        objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0"); 
                        objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objAdminMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]); 
                        objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objAdminMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objAdminMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objAdminMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objAdminMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        lstAdminMenuItems.Add(objAdminMenuItems);
                    }
                }
            }
            return lstAdminMenuItems;
        }

        public List<Entities.AdminMenuItems> GetAdminMenuItemsByLevel(Int32 CategoryLevel, ref int Status)
        {
            DataTable dt = null;
            List<Entities.AdminMenuItems> lstAdminMenuItems = new List<Entities.AdminMenuItems>();
            if (CategoryLevel != 0)
            {
                dt = _AdminMenuItems.GetAdminMenuItemsByLevel(CategoryLevel, ref Status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                        objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objAdminMenuItems.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                        objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objAdminMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objAdminMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objAdminMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objAdminMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objAdminMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);

                        lstAdminMenuItems.Add(objAdminMenuItems);
                    }
                }
            }
            return lstAdminMenuItems;
        }

        public Entities.AdminMenuItems GetAdminMenuItemsByName(string CategoryName, ref int Status)
        {
            DataTable dt = null;
            Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
            if (CategoryName != null && CategoryName.Trim() != "")
            {
                dt = _AdminMenuItems.GetAdminMenuItemsByName(CategoryName, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objAdminMenuItems.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"]);
                    objAdminMenuItems.ChapterId = Convert.ToInt64(dt.Rows[0]["ChapterId"]);
                    objAdminMenuItems.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                    objAdminMenuItems.PageLevel = (dt.Rows[0]["PageLevel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageLevel"]) : 1);
                    objAdminMenuItems.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                    objAdminMenuItems.IdPath = (dt.Rows[0]["IdPath"] != DBNull.Value ? dt.Rows[0]["IdPath"].ToString() : "0");
                    objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dt.Rows[0]["IsFooterBar"]);
                    objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dt.Rows[0]["IsMenuBar"]);
                    objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dt.Rows[0]["IsQuickLinks"]);
                    objAdminMenuItems.IsTopBar = Convert.ToBoolean(dt.Rows[0]["IsTopBar"]);
                    objAdminMenuItems.Position = (dt.Rows[0]["Position"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Position"]) : 0);
                    objAdminMenuItems.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objAdminMenuItems.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objAdminMenuItems.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
                    objAdminMenuItems.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                }
            }
            return objAdminMenuItems;
        }

        public List<Entities.AdminMenuItems> GetAdminMenuItemsAll(ref List<Entities.AdminMenuItems> lstAdminMenuItems2, ref List<Entities.AdminMenuItems> lstAdminMenuItems3, ref List<Entities.AdminMenuItems> lstAdminMenuItems4, Int64 ChapterId, ref int Status)
        {
            DataTable dt = _AdminMenuItems.GetAdminMenuItems(ChapterId, ref Status);
            List<Entities.AdminMenuItems> lstAdminMenuItems = new List<Entities.AdminMenuItems>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                        objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objAdminMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objAdminMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objAdminMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objAdminMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objAdminMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objAdminMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objAdminMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objAdminMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstAdminMenuItems.Add(objAdminMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                        objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objAdminMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objAdminMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objAdminMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objAdminMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objAdminMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objAdminMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objAdminMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objAdminMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstAdminMenuItems2.Add(objAdminMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                        objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objAdminMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objAdminMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objAdminMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objAdminMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objAdminMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objAdminMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objAdminMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objAdminMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstAdminMenuItems3.Add(objAdminMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                        objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objAdminMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objAdminMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objAdminMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objAdminMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objAdminMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objAdminMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objAdminMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objAdminMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objAdminMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objAdminMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objAdminMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objAdminMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstAdminMenuItems4.Add(objAdminMenuItems);
                    }
                }
            }
            return lstAdminMenuItems;
        }

        public List<Entities.AdminMenuItems> GetAdminMenuItemsDD(Int64 ChapterId, ref List<Entities.AdminMenuItems> lstAdminMenuItems2, ref List<Entities.AdminMenuItems> lstAdminMenuItems3, ref List<Entities.AdminMenuItems> lstAdminMenuItems4, ref int Status)
        {   
            List<Entities.AdminMenuItems> lstAdminMenuItems = GetAdminMenuItemsAll(ref lstAdminMenuItems2, ref lstAdminMenuItems3, ref lstAdminMenuItems4, ChapterId, ref Status); 
            return lstAdminMenuItems;
        }

        #endregion

        #region Front-End

        public List<Entities.AdminMenuItems> GetAdminMenuItemsForMenu(Int64 ChapterId, ref List<Entities.AdminMenuItems> lstAdminMenuItems2, ref List<Entities.AdminMenuItems> lstAdminMenuItems3, ref List<Entities.AdminMenuItems> lstAdminMenuItems4, ref int status)
        {
            DataTable dt = null;
            List<Entities.AdminMenuItems> lstAdminMenuItems = new List<Entities.AdminMenuItems>();
            if (ChapterId != 0)
            {
                dt = _AdminMenuItems.FEGetAdminMenuItemsForMenu(ChapterId, ref status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["PageLevel"]) == 1)
                        {
                            Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                            objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objAdminMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objAdminMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstAdminMenuItems.Add(objAdminMenuItems);
                        }
                        if (Convert.ToInt32(dr["PageLevel"]) == 2)
                        {
                            Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                            objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objAdminMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objAdminMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstAdminMenuItems2.Add(objAdminMenuItems);
                        }
                        if (Convert.ToInt32(dr["PageLevel"]) == 3)
                        {
                            Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                            objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objAdminMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objAdminMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstAdminMenuItems3.Add(objAdminMenuItems);
                        }
                        if (Convert.ToInt32(dr["PageLevel"]) == 4)
                        {
                            Entities.AdminMenuItems objAdminMenuItems = new Entities.AdminMenuItems();
                            objAdminMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objAdminMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objAdminMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objAdminMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objAdminMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objAdminMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objAdminMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objAdminMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objAdminMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objAdminMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objAdminMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objAdminMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstAdminMenuItems4.Add(objAdminMenuItems);
                        }
                    } 
                }
            }
            return lstAdminMenuItems;
        }

        #endregion

        #region AccessMenu

        public List<Entities.Roles> GetRolesAssignMenu(ref int status)
        {
            List<Entities.Roles> lstRoles = new List<Entities.Roles>();
            DataTable dt = new DataTable();

            dt = _AdminMenuItems.GetRolesAssignMenu(ref status);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Roles objRoles = new Entities.Roles();

                    objRoles.RoleId = Convert.ToInt32(dr["RoleId"].ToString());
                    objRoles.RoleName = dr["RoleName"].ToString();

                    lstRoles.Add(objRoles);
                }

            }
            return lstRoles;
        }

        public List<Entities.AdminMenuItems> GetAssignMenuList(ref List<Entities.AdminMenuItems> lstMainMenuMaster, ref List<Entities.Role_Menu> lstrole_Menus, Int32 MenuId, Int32 UserId, Int32 RoleId, ref int status)
        {
            List<Entities.AdminMenuItems> lstMenuMaster = new List<Entities.AdminMenuItems>();
            DataSet ds = new DataSet();

            ds = _AdminMenuItems.GetAssignMenuList(MenuId, UserId, RoleId, ref status);

            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Entities.AdminMenuItems objMenuMaster = new Entities.AdminMenuItems();

                    objMenuMaster.MenuItemId = Convert.ToInt32(dr["MenuItemId"].ToString());
                    objMenuMaster.MenuItemCount = Convert.ToInt32(dr["MenuItemCount"].ToString());
                    objMenuMaster.DisplayName = dr["DisplayName"].ToString();
                    objMenuMaster.comma_separated_ids = (dr["comma_separated_ids"] != DBNull.Value ? dr["comma_separated_ids"].ToString() : null);


                    lstMainMenuMaster.Add(objMenuMaster);
                }

            }

            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.AdminMenuItems objMenuMaster1 = new Entities.AdminMenuItems();

                    objMenuMaster1.DisplayName = dr["DisplayName"].ToString();
                    objMenuMaster1.MenuItemId = Convert.ToInt32(dr["MenuItemId"].ToString());
                    objMenuMaster1.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt32(dr["PageParentId"]) : 0);

                    lstMenuMaster.Add(objMenuMaster1);
                }

            }

            if (ds.Tables[2].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Entities.Role_Menu objRole_Menu = new Entities.Role_Menu();

                    objRole_Menu.MenuId = Convert.ToInt32(dr["MenuId"].ToString());
                    objRole_Menu.RoleMenuMasterId = (dr["RoleMenuMasterId"] != DBNull.Value ? Convert.ToInt64(dr["RoleMenuMasterId"]) : 0);
                    objRole_Menu.IsAdd = (dr["IsAdd"] != DBNull.Value ? Convert.ToBoolean(dr["IsAdd"]) : false);
                    objRole_Menu.IsEdit = (dr["IsEdit"] != DBNull.Value ? Convert.ToBoolean(dr["IsEdit"]) : false);
                    objRole_Menu.IsView = (dr["IsView"] != DBNull.Value ? Convert.ToBoolean(dr["IsView"]) : false);
                    objRole_Menu.IsDelete = (dr["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dr["IsDelete"]) : false);
                    objRole_Menu.IsExport = (dr["IsExport"] != DBNull.Value ? Convert.ToBoolean(dr["IsExport"]) : false);

                    lstrole_Menus.Add(objRole_Menu);
                }

            }

            return lstMenuMaster;
        }

        public List<Entities.Users> GetUsersByRole(Int32 RoleId, ref int status)
        {
            List<Entities.Users> lstUsers = new List<Entities.Users>();
            DataTable dt = new DataTable();

            dt = _AdminMenuItems.GetUsersByRole(RoleId, ref status);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Users objUsers = new Entities.Users();

                    objUsers.UserId = Convert.ToInt32(dr["UserId"].ToString());
                    objUsers.UserName = dr["UserName"].ToString();

                    lstUsers.Add(objUsers);
                }

            }
            return lstUsers;
        }

        public Int32 InsertRoleMenu(Int32 RoleId, string MenuIds, Int32 MenuId, Int32 UserId, string CreatedBy, DateTime CreatedDate)
        {
            Int32 _status = 0;
            if (RoleId != 0 && (MenuIds != ""))
            {
                _status = _AdminMenuItems.InsertRoleMenu(RoleId, MenuIds, MenuId, UserId, CreatedBy, CreatedDate);

            }
            return _status;
        }

        public Int64 UpdateRoleBasedAccess(AWSFormBuilder.Entities.Role_Menu objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _AdminMenuItems.UpdateRoleMenuBasedAccess(objUser);
            }
            return _status;
        }
        public Int64 SingleMenuUpdate(AWSFormBuilder.Entities.Role_Menu objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _AdminMenuItems.SingleMenuUpdate(objUser);
            }
            return _status;
        }
        public Int64 UpdateCloneRoleIds(Int64 RoleId = 0, Int64 RoleIds = 0)
        {
            Int64 _status = 0;
            if (RoleIds != 0)
            {
                _status = _AdminMenuItems.UpdateCloneRoleIds(RoleId, RoleIds);
            }
            return _status;
        }

        #endregion
    }
}
