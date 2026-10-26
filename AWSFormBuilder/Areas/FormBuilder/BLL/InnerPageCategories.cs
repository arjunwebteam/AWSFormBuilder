using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class InnerPageCategories
    {
        DAL.InnerPageCategories _InnerPageCategories = new DAL.InnerPageCategories();

        #region Methods

        public Int64 DeleteInnerPageCategories(Int64 InnerPageCategoryId)
        {
            Int64 _status = 0;
            if (InnerPageCategoryId != 0)
            {
                _status = _InnerPageCategories.DeleteInnerPageCategories(InnerPageCategoryId);
            }
            return _status;
        }

        public Int64 InsertInnerPageCategories(Entities.InnerPageCategories objInnerPageCategories)
        {
            Int64 _status = 0;
            if (objInnerPageCategories != null)
            {
                _status = _InnerPageCategories.InsertInnerPageCategories(objInnerPageCategories);
            }
            return _status;
        }

        public Int64 UpdateInnerPageCategoriesStatus(Int64 InnerPageCategoryId)
        {
            Int64 _status = 0;
            if (InnerPageCategoryId != 0)
            {
                _status = _InnerPageCategories.UpdateInnerPageCategoriesStatus(InnerPageCategoryId);
            }
            return _status;
        }

        #endregion

        #region Entity Loading

        public Entities.InnerPageCategories GetInnerPageCategoriesById(Int64 InnerPageCategoryId, ref int Status)
        {
            DataTable dt = null;
            Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
            if (InnerPageCategoryId != 0)
            {
                dt = _InnerPageCategories.GetInnerPageCategoriesById(InnerPageCategoryId, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dt.Rows[0]["InnerPageCategoryId"]);
                    objInnerPageCategories.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["ChapterId"]) : 0);
                    objInnerPageCategories.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                    objInnerPageCategories.ParentName = (dt.Rows[0]["ParentName"] != DBNull.Value ? dt.Rows[0]["ParentName"].ToString() : null);
                    objInnerPageCategories.PageLevel = (dt.Rows[0]["PageLevel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageLevel"]) : 1);
                    objInnerPageCategories.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                    objInnerPageCategories.IdPath = (dt.Rows[0]["IdPath"] != DBNull.Value ? dt.Rows[0]["IdPath"].ToString() : "0");
                    objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dt.Rows[0]["IsFooterBar"]);
                    objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dt.Rows[0]["IsMenuBar"]);
                    objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dt.Rows[0]["IsQuickLinks"]);
                    objInnerPageCategories.IsTopBar = Convert.ToBoolean(dt.Rows[0]["IsTopBar"]);
                    objInnerPageCategories.Position = (dt.Rows[0]["Position"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Position"]) : 0);
                    objInnerPageCategories.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objInnerPageCategories.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objInnerPageCategories.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
                    objInnerPageCategories.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                    objInnerPageCategories.ChapterName = (dt.Rows[0]["ChapterName"] != DBNull.Value ? dt.Rows[0]["ChapterName"].ToString() : null);

                    
                }
            }
            return objInnerPageCategories;
        }

        public List<Entities.InnerPageCategories> GetInnerPageCategoriesByParentId(Int64 CategoryParentId, ref int Status)
        {
            DataTable dt = null;
            List<Entities.InnerPageCategories> lstInnerPageCategories = new List<Entities.InnerPageCategories>();
            if (CategoryParentId != 0)
            {
                dt = _InnerPageCategories.GetInnerPageCategoriesByParentId(CategoryParentId, ref Status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        lstInnerPageCategories.Add(objInnerPageCategories);
                    }
                }
            }
            return lstInnerPageCategories;
        }

        public List<Entities.InnerPageCategories> GetInnerPageCategoriesByLevel(Int32 CategoryLevel, ref int Status)
        {
            DataTable dt = null;
            List<Entities.InnerPageCategories> lstInnerPageCategories = new List<Entities.InnerPageCategories>();
            if (CategoryLevel != 0)
            {
                dt = _InnerPageCategories.GetInnerPageCategoriesByLevel(CategoryLevel, ref Status);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt32(dr["ChapterId"]) : 0);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);

                        lstInnerPageCategories.Add(objInnerPageCategories);
                    }
                }
            }
            return lstInnerPageCategories;
        }

        public Entities.InnerPageCategories GetInnerPageCategoriesByName(string CategoryName, ref int Status)
        {
            DataTable dt = null;
            Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
            if (CategoryName != null && CategoryName.Trim() != "")
            {
                dt = _InnerPageCategories.GetInnerPageCategoriesByName(CategoryName, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dt.Rows[0]["InnerPageCategoryId"]);
                    objInnerPageCategories.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                    objInnerPageCategories.PageLevel = (dt.Rows[0]["PageLevel"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageLevel"]) : 1);
                    objInnerPageCategories.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                    objInnerPageCategories.IdPath = (dt.Rows[0]["IdPath"] != DBNull.Value ? dt.Rows[0]["IdPath"].ToString() : "0");
                    objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dt.Rows[0]["IsFooterBar"]);
                    objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dt.Rows[0]["IsMenuBar"]);
                    objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dt.Rows[0]["IsQuickLinks"]);
                    objInnerPageCategories.IsTopBar = Convert.ToBoolean(dt.Rows[0]["IsTopBar"]);
                    objInnerPageCategories.Position = (dt.Rows[0]["Position"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Position"]) : 0);
                    objInnerPageCategories.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objInnerPageCategories.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objInnerPageCategories.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
                    objInnerPageCategories.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                }
            }
            return objInnerPageCategories;
        }

        public List<Entities.InnerPageCategories> GetInnerPageCategoriesAll(ref List<Entities.InnerPageCategories> lstInnerPageCategories2, ref List<Entities.InnerPageCategories> lstInnerPageCategories3, ref List<Entities.InnerPageCategories> lstInnerPageCategories4, ref int Status)
        {
            DataTable dt = _InnerPageCategories.GetInnerPageCategories(ref Status);
            List<Entities.InnerPageCategories> lstInnerPageCategories = new List<Entities.InnerPageCategories>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories2.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories3.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories4.Add(objInnerPageCategories);
                    }
                }
            }
            return lstInnerPageCategories;
        }


        public List<Entities.InnerPageCategories> GetInnerPageCategoriesByChapterId(Int64 ChapterId, ref List<Entities.InnerPageCategories> lstInnerPageCategories2, ref List<Entities.InnerPageCategories> lstInnerPageCategories3, ref List<Entities.InnerPageCategories> lstInnerPageCategories4, ref int Status)
        {
            DataTable dt = _InnerPageCategories.GetInnerPageCategoriesByChapterId(ChapterId, ref Status);
            List<Entities.InnerPageCategories> lstInnerPageCategories = new List<Entities.InnerPageCategories>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["PageLevel"]) == 1)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 2)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories2.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 3)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories3.Add(objInnerPageCategories);
                    }
                    if (Convert.ToInt32(dr["PageLevel"]) == 4)
                    {
                        Entities.InnerPageCategories objInnerPageCategories = new Entities.InnerPageCategories();
                        objInnerPageCategories.InnerPageCategoryId = Convert.ToInt64(dr["InnerPageCategoryId"]);
                        objInnerPageCategories.DisplayName = dr["DisplayName"].ToString();
                        objInnerPageCategories.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
                        objInnerPageCategories.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
                        objInnerPageCategories.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
                        objInnerPageCategories.IsFooterBar = Convert.ToBoolean(dr["IsFooterBar"]);
                        objInnerPageCategories.IsMenuBar = Convert.ToBoolean(dr["IsMenuBar"]);
                        objInnerPageCategories.IsQuickLinks = Convert.ToBoolean(dr["IsQuickLinks"]);
                        objInnerPageCategories.IsTopBar = Convert.ToBoolean(dr["IsTopBar"]);
                        objInnerPageCategories.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
                        objInnerPageCategories.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        objInnerPageCategories.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                        objInnerPageCategories.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                        objInnerPageCategories.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        objInnerPageCategories.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                        objInnerPageCategories.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"]) : false);
                        objInnerPageCategories.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"]) : 0);
                        objInnerPageCategories.InnerPageId = (dr["InnerPageId"] != DBNull.Value ? Convert.ToInt64(dr["InnerPageId"]) : 0);
                        lstInnerPageCategories4.Add(objInnerPageCategories);
                    }
                }
            }
            return lstInnerPageCategories;
        }
        #endregion
    }
}
