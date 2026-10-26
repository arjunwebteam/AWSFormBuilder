using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class MenuItems
    {
        DAL.MenuItems _MenuItems = new DAL.MenuItems();

        #region Methods

        public Int64 DeleteMenuItems(Int64 MenuItemId)
        {
            Int64 _status = 0;
            if (MenuItemId != 0)
            {
                _status = _MenuItems.DeleteMenuItems(MenuItemId);
            }
            return _status;
        }


        public Int64 UpdateMenuItemsOrderNo(int Position, Int64 MenuItemId)
        {
            Int64 _status = 0;
            _status = _MenuItems.UpdateMenuItemsOrderNo(Position, MenuItemId);
            return _status;
        }
        public Int64 InsertMenuItems(Entities.MenuItems objMenuItems)
        {
            Int64 _status = 0;
            if (objMenuItems != null)
            {
                _status = _MenuItems.InsertMenuItems(objMenuItems);
            }
            return _status;
        }

        public Int64 UpdateMenuItemsStatus(Int64 MenuItemId)
        {
            Int64 _status = 0;
            if (MenuItemId != 0)
            {
                _status = _MenuItems.UpdateMenuItemsStatus(MenuItemId);
            }
            return _status;
        }
        public List<Entities.MenuItems> GetMenuItemsDD(Int64 ChapterId, ref List<Entities.MenuItems> lstMenuItems2, ref List<Entities.MenuItems> lstMenuItems3, ref List<Entities.MenuItems> lstMenuItems4, ref int Status)
        {
            List<Entities.MenuItems> lstMenuItems = GetMenuItemsAll(ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ChapterId, ref Status);
            return lstMenuItems;
        }
        public List<Entities.MenuItems> ExistingMenuItemsGetbyId(Int64 PageDetailId, ref int Status)
        {
            DataTable dt = null;
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();

            dt = _MenuItems.ExistingMenuItemsGetbyId(PageDetailId, ref Status);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.MenuItems objMenuItems = new Entities.MenuItems();
                    objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                    objMenuItems.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                    objMenuItems.DisplayName = dr["DisplayName"].ToString();
                    objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                    objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                    objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                    objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                    objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                    objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                    objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                    objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                    objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                    objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                    objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);

                    lstMenuItems.Add(objMenuItems);
                }
            }

            return lstMenuItems;
        }
        public List<Entities.MenuItems> GetMenuItemsAll(ref List<Entities.MenuItems> lstMenuItems2, ref List<Entities.MenuItems> lstMenuItems3, ref List<Entities.MenuItems> lstMenuItems4, Int64 ChapterId, ref int Status)
        {
            DataTable dt = _MenuItems.GetMenuItems(ChapterId, ref Status);
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems.Add(objMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems2.Add(objMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems3.Add(objMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems4.Add(objMenuItems);
                    }
                }
            }
            return lstMenuItems;
        }

        public List<Entities.MenuItems> ExistingMenuItemsGetList(ref int Status)
        {
            DataTable dt = null;
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();

            dt = _MenuItems.ExistingMenuItemsGetList(ref Status);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.MenuItems objMenuItems = new Entities.MenuItems();
                    objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                    objMenuItems.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                    objMenuItems.DisplayName = dr["DisplayName"].ToString();
                    objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                    objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                    objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                    objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                    objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                    objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                    objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                    objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                    objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                    objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                    objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);

                    lstMenuItems.Add(objMenuItems);
                }
            }

            return lstMenuItems;
        }


        #endregion

        #region Entity Loading

        public Entities.MenuItems GetMenuItemsById(Int64 MenuItemId, ref int Status)
        {
            DataTable dt = null;
            Entities.MenuItems objMenuItems = new Entities.MenuItems();
            if (MenuItemId != 0)
            {
                dt = _MenuItems.GetMenuItemsById(MenuItemId, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objMenuItems.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"]);
                    objMenuItems.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                    objMenuItems.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                    objMenuItems.ParentName = (dt.Rows[0]["ParentName"] != DBNull.Value ? dt.Rows[0]["ParentName"].ToString() : null);
                    objMenuItems.PageLevel = (dt.Rows[0]["PageLevel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageLevel"]) : 1);
                    objMenuItems.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                    objMenuItems.IdPath = (dt.Rows[0]["IdPath"] != DBNull.Value ? dt.Rows[0]["IdPath"].ToString() : "0"); 
                    objMenuItems.IsFooterBar = Convert.ToBoolean(dt.Rows[0]["IsFooterBar"]);
                    objMenuItems.IsMenuBar = Convert.ToBoolean(dt.Rows[0]["IsMenuBar"]);
                    objMenuItems.IsQuickLinks = Convert.ToBoolean(dt.Rows[0]["IsQuickLinks"]);
                    objMenuItems.IsTopBar = Convert.ToBoolean(dt.Rows[0]["IsTopBar"]); 
                    objMenuItems.Position = (dt.Rows[0]["Position"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Position"]) : 0);
                    //objMenuItems.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                    //objMenuItems.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);

                    objMenuItems.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objMenuItems.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objMenuItems.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objMenuItems.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
                    objMenuItems.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                }
            }
            return objMenuItems;
        }

        public List<Entities.MenuItems> GetMenuItemsByParentId(Int64 CategoryParentId, ref int Status)
        {
            DataTable dt = null;
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();
            if (CategoryParentId != 0)
            {
                dt = _MenuItems.GetMenuItemsByParentId(CategoryParentId, ref Status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems(); 
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0"); 
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]); 
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        lstMenuItems.Add(objMenuItems);
                    }
                }
            }
            return lstMenuItems;
        }

        public List<Entities.MenuItems> GetMenuItemsByLevel(Int32 CategoryLevel, ref int Status)
        {
            DataTable dt = null;
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();
            if (CategoryLevel != 0)
            {
                dt = _MenuItems.GetMenuItemsByLevel(CategoryLevel, ref Status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);

                        lstMenuItems.Add(objMenuItems);
                    }
                }
            }
            return lstMenuItems;
        }

        public Entities.MenuItems GetMenuItemsByName(string CategoryName, ref int Status)
        {
            DataTable dt = null;
            Entities.MenuItems objMenuItems = new Entities.MenuItems();
            if (CategoryName != null && CategoryName.Trim() != "")
            {
                dt = _MenuItems.GetMenuItemsByName(CategoryName, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objMenuItems.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"]);
                    objMenuItems.ChapterId = Convert.ToInt64(dt.Rows[0]["ChapterId"]);
                    objMenuItems.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                    objMenuItems.PageLevel = (dt.Rows[0]["PageLevel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageLevel"]) : 1);
                    objMenuItems.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                    objMenuItems.IdPath = (dt.Rows[0]["IdPath"] != DBNull.Value ? dt.Rows[0]["IdPath"].ToString() : "0");
                    objMenuItems.IsFooterBar = Convert.ToBoolean(dt.Rows[0]["IsFooterBar"]);
                    objMenuItems.IsMenuBar = Convert.ToBoolean(dt.Rows[0]["IsMenuBar"]);
                    objMenuItems.IsQuickLinks = Convert.ToBoolean(dt.Rows[0]["IsQuickLinks"]);
                    objMenuItems.IsTopBar = Convert.ToBoolean(dt.Rows[0]["IsTopBar"]);
                    objMenuItems.Position = (dt.Rows[0]["Position"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Position"]) : 0);
                    objMenuItems.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objMenuItems.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objMenuItems.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objMenuItems.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
                    objMenuItems.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                }
            }
            return objMenuItems;
        }

        public List<Entities.MenuItems> GetMenuItemsAll(ref List<Entities.MenuItems> lstMenuItems2, ref List<Entities.MenuItems> lstMenuItems3, ref List<Entities.MenuItems> lstMenuItems4, Int64 ChapterId, bool IsFooterBar, bool IsMenuBar, bool IsQuickLinks, ref int Status)
        {
            DataTable dt = _MenuItems.GetMenuItems(ChapterId, IsFooterBar, IsMenuBar, IsQuickLinks, ref Status);
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems.Add(objMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems2.Add(objMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems3.Add(objMenuItems);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.MenuItems objMenuItems = new Entities.MenuItems();
                        objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                        objMenuItems.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objMenuItems.DisplayName = dr["DisplayName"].ToString();
                        objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objMenuItems.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objMenuItems.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objMenuItems.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objMenuItems.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objMenuItems.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objMenuItems.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objMenuItems.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objMenuItems.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objMenuItems.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"]) : 0);
                        objMenuItems.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt64(dr["MenuPageId"]) : 0);
                        objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                        objMenuItems.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                        lstMenuItems4.Add(objMenuItems);
                    }
                }
            }
            return lstMenuItems;
        }

        public List<Entities.MenuItems> GetMenuItemsDD(Int64 ChapterId, bool IsFooterBar, bool IsMenuBar, bool IsQuickLinks, ref List<Entities.MenuItems> lstMenuItems2, ref List<Entities.MenuItems> lstMenuItems3, ref List<Entities.MenuItems> lstMenuItems4, ref int Status)
        {   
            List<Entities.MenuItems> lstMenuItems = GetMenuItemsAll(ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ChapterId, IsFooterBar,  IsMenuBar, IsQuickLinks, ref Status); 
            return lstMenuItems;
        }

        #endregion

        #region Front-End

        public List<Entities.MenuItems> GetMenuItemsForMenu(Int64 ChapterId, ref List<Entities.MenuItems> lstMenuItems2, ref List<Entities.MenuItems> lstMenuItems3, ref List<Entities.MenuItems> lstMenuItems4, ref int status)
        {
            DataTable dt = null;
            List<Entities.MenuItems> lstMenuItems = new List<Entities.MenuItems>();
            if (ChapterId != 0)
            {
                dt = _MenuItems.FEGetMenuItemsForMenu(ChapterId, ref status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["PageLevel"]) == 1)
                        {
                            Entities.MenuItems objMenuItems = new Entities.MenuItems();
                            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstMenuItems.Add(objMenuItems);
                        }
                        if (Convert.ToInt32(dr["PageLevel"]) == 2)
                        {
                            Entities.MenuItems objMenuItems = new Entities.MenuItems();
                            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstMenuItems2.Add(objMenuItems);
                        }
                        if (Convert.ToInt32(dr["PageLevel"]) == 3)
                        {
                            Entities.MenuItems objMenuItems = new Entities.MenuItems();
                            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstMenuItems3.Add(objMenuItems);
                        }
                        if (Convert.ToInt32(dr["PageLevel"]) == 4)
                        {
                            Entities.MenuItems objMenuItems = new Entities.MenuItems();
                            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
                            objMenuItems.DisplayName = dr["DisplayName"].ToString();
                            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
                            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
                            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
                            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
                            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
                            lstMenuItems4.Add(objMenuItems);
                        }
                    } 
                }
            }
            return lstMenuItems;
        }

        #endregion
    }
}
