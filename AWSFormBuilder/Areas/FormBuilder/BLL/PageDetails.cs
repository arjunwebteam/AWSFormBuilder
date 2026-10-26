using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class PageDetails
    {
        DAL.PageDetails _PageDetails = new DAL.PageDetails();

        #region Methods

        public Int64 DeletePageDetails(Int64 PageDetailId)
        {
            Int64 _status = 0;
            if (PageDetailId != 0)
            {
                _status = _PageDetails.DeletePageDetails(PageDetailId);
            }
            return _status;
        }

        public Int64 InsertPageDetails(Entities.PageDetails objPageDetails, ref string DocumentUrl)
        {
            Int64 _status = 0;
            if (objPageDetails != null)
            {
                _status = _PageDetails.InsertPageDetails(objPageDetails, ref DocumentUrl);
            }
            return _status;
        }


        public Int64 UpdatePageDetailsStatus(Int64 PageDetailId)
        {
            Int64 _status = 0;
            _status = _PageDetails.UpdatePageDetailsStatus(PageDetailId);
            return _status;
        }
        public Int64 PagedetailsRemoveDocumentUrl(Int64 PageDetailId)
        {
            Int64 _status = 0;
            _status = _PageDetails.PagedetailsRemoveDocumentUrl(PageDetailId);
            return _status;
        }

        public AWSFormBuilder.Entities.PageDetails MenuPagesDetailsGetById(ref AWSFormBuilder.Entities.MenuItems objMenuItems, ref AWSFormBuilder.Entities.MenuPages objMenuPages, Int64 PageDetailId, ref int status)
        {
            AWSFormBuilder.Entities.PageDetails objPageDetails = new AWSFormBuilder.Entities.PageDetails();

            DataSet ds = _PageDetails.MenuPagesDetailsGetById(PageDetailId, ref status);
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[0];
                objPageDetails.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
                objPageDetails.Heading = dt.Rows[0]["Heading"].ToString();
                objPageDetails.Description = dt.Rows[0]["Description"].ToString();
                objPageDetails.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                objPageDetails.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);
                objPageDetails.DocumentUrl = (dt.Rows[0]["DocumentUrl"] != DBNull.Value ? dt.Rows[0]["DocumentUrl"].ToString() : null);
                objPageDetails.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : null);
                objPageDetails.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objPageDetails.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objPageDetails.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objPageDetails.TopLine = (dt.Rows[0]["TopLine"] != DBNull.Value ? dt.Rows[0]["TopLine"].ToString() : null);
                objPageDetails.AddPage = (dt.Rows[0]["AddPage"] != DBNull.Value ? dt.Rows[0]["AddPage"].ToString() : null);
                objPageDetails.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objPageDetails.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                objPageDetails.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objPageDetails.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
            }
            if (ds.Tables[1].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[1];
                objMenuPages.MenuPageId = Convert.ToInt64(dt.Rows[0]["MenuPageId"].ToString());
                objMenuPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"].ToString());
                objMenuPages.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"].ToString());
            }
            if (ds.Tables[2].Rows.Count == 1)
            {
                DataTable dt = ds.Tables[2];
                objMenuItems.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"]);
                objMenuItems.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                objMenuItems.DisplayName = dt.Rows[0]["DisplayName"].ToString();
                //objMenuItems.ParentName = (dt.Rows[0]["ParentName"] != DBNull.Value ? dt.Rows[0]["ParentName"].ToString() : null);
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

            return objPageDetails;
        }


        #endregion

        #region Entity Loading

        public List<Entities.PageDetails> GetPageDetailsListByVariable(Int64 ChapterId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _PageDetails.GetPageDetailsListByVariable(ChapterId, Search, Sort, PageNo, Items, ref Total);
            List<Entities.PageDetails> lstPageDetails = new List<Entities.PageDetails>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _PageDetails.GetPageDetailsListByVariable(ChapterId, Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.PageDetails objPageDetails = new Entities.PageDetails();

                    objPageDetails.RId = Convert.ToInt64(dr["Rid"]);
                    objPageDetails.PageDetailId = Convert.ToInt64(dr["PageDetailId"]);
                    objPageDetails.Heading = dr["Heading"].ToString();
                    objPageDetails.Description = dr["Description"].ToString();
                    objPageDetails.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                    objPageDetails.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : null);
                    objPageDetails.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : null);
                    objPageDetails.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : null);
                    objPageDetails.PageTitle = (dr["PageTitle"] != DBNull.Value ? dr["PageTitle"].ToString() : null);
                    objPageDetails.MetaDescription = (dr["MetaDescription"] != DBNull.Value ? dr["MetaDescription"].ToString() : null);
                    objPageDetails.MetaKeywords = (dr["MetaKeywords"] != DBNull.Value ? dr["MetaKeywords"].ToString() : null);
                    objPageDetails.TopLine = (dr["TopLine"] != DBNull.Value ? dr["TopLine"].ToString() : null);
                    objPageDetails.InsertedBy = dr["InsertedBy"].ToString();
                    objPageDetails.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                    objPageDetails.UpdatedBy = dr["UpdatedBy"].ToString();
                    objPageDetails.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    objPageDetails.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    //objPageDetails.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);
                    //objPageDetails.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterId"].ToString()) : 0);

                    lstPageDetails.Add(objPageDetails);
                }
            }
            return lstPageDetails;
        }

        public Entities.PageDetails GetPageDetailsById(Int64 PageDetailsId, ref int status)
        {
            DataTable dt = _PageDetails.GetPageDetailsById(PageDetailsId, ref status);
            Entities.PageDetails objPageDetails = new Entities.PageDetails();

            if (dt.Rows.Count == 1)
            {
                objPageDetails.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
                objPageDetails.Heading = dt.Rows[0]["Heading"].ToString();
                objPageDetails.Description = dt.Rows[0]["Description"].ToString();
                objPageDetails.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                objPageDetails.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);
                objPageDetails.DocumentUrl = (dt.Rows[0]["DocumentUrl"] != DBNull.Value ? dt.Rows[0]["DocumentUrl"].ToString() : null);
                objPageDetails.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : null);
                objPageDetails.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objPageDetails.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objPageDetails.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objPageDetails.TopLine = (dt.Rows[0]["TopLine"] != DBNull.Value ? dt.Rows[0]["TopLine"].ToString() : null);
                objPageDetails.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objPageDetails.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                objPageDetails.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objPageDetails.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
            }

            return objPageDetails;
        }

        public Entities.PageDetails GetPageDetailsListById(Int64 ChapterId, Int64 MenuItemId, ref string IDPath, string Heading, string PageUrl, ref Int64 status)
        {
            DataTable dt = _PageDetails.GetPageDetailsListById(ChapterId, MenuItemId, ref IDPath, Heading, PageUrl, ref status);
            Entities.PageDetails objPageDetails = new Entities.PageDetails();

            if (dt.Rows.Count == 1)
            {
                objPageDetails.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
                objPageDetails.Heading = dt.Rows[0]["Heading"].ToString();
                objPageDetails.Description = dt.Rows[0]["Description"].ToString();
                objPageDetails.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                objPageDetails.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);
                objPageDetails.DocumentUrl = (dt.Rows[0]["DocumentUrl"] != DBNull.Value ? dt.Rows[0]["DocumentUrl"].ToString() : null);
                objPageDetails.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : null);
                objPageDetails.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objPageDetails.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objPageDetails.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objPageDetails.TopLine = (dt.Rows[0]["TopLine"] != DBNull.Value ? dt.Rows[0]["TopLine"].ToString() : null);
                objPageDetails.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objPageDetails.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                objPageDetails.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objPageDetails.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
            }

            return objPageDetails;
        }

        public Entities.PageDetails AppGetPageDetailsListById(Int64 ChapterId, Int64 MenuItemId, ref string IDPath, string Heading, string PageUrl, ref Int64 status)
        {
            DataTable dt = _PageDetails.AppGetPageDetailsListById(ChapterId, MenuItemId, ref IDPath, Heading, PageUrl, ref status);
            Entities.PageDetails objPageDetails = new Entities.PageDetails();

            if (dt.Rows.Count == 1)
            {
                objPageDetails.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
                objPageDetails.Heading = dt.Rows[0]["Heading"].ToString();
                objPageDetails.Description = dt.Rows[0]["Description"].ToString();
                objPageDetails.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                objPageDetails.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);
                objPageDetails.DocumentUrl = (dt.Rows[0]["DocumentUrl"] != DBNull.Value ? dt.Rows[0]["DocumentUrl"].ToString() : null);
                objPageDetails.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : null);
                objPageDetails.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objPageDetails.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objPageDetails.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objPageDetails.TopLine = (dt.Rows[0]["TopLine"] != DBNull.Value ? dt.Rows[0]["TopLine"].ToString() : null);
                objPageDetails.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objPageDetails.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                objPageDetails.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objPageDetails.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
            }

            return objPageDetails;
        }

        public List<AWSFormBuilder.Entities.MenuPages> GetMenuPagesListById(Int64 MenuItemId, ref int status)
        {
            List<AWSFormBuilder.Entities.MenuPages> lstMenuPages = new List<AWSFormBuilder.Entities.MenuPages>();
            DataTable dt = _PageDetails.GetMenuPagesListById(MenuItemId, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.MenuPages objlstMenuPages = new AWSFormBuilder.Entities.MenuPages();

                    objlstMenuPages.PageDetailId = Convert.ToInt64(dr["PageDetailId"].ToString());
                    objlstMenuPages.Heading = dr["Heading"].ToString();

                    lstMenuPages.Add(objlstMenuPages);
                }

            }
            return lstMenuPages;
        }

        public Entities.PageDetails GetBreadCrumbListById(Int64 ChapterId, Int64 MenuItemId, ref Int64 status)
        {
            DataTable dt = _PageDetails.GetBreadCrumbListById(ChapterId, MenuItemId, ref status);
            Entities.PageDetails objPageDetails = new Entities.PageDetails();

            if (dt.Rows.Count == 1)
            { 
                objPageDetails.DisplayName = (dt.Rows[0]["DisplayName"] != DBNull.Value ? dt.Rows[0]["DisplayName"].ToString() : null);
                objPageDetails.ParentName = (dt.Rows[0]["ParentName"] != DBNull.Value ? dt.Rows[0]["ParentName"].ToString() : null);
                objPageDetails.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                objPageDetails.ParentUrl = (dt.Rows[0]["ParentUrl"] != DBNull.Value ? dt.Rows[0]["ParentUrl"].ToString() : null); 
            }

            return objPageDetails;
        }

        public Entities.PageDetails GetBreadCrumbListByUrl(string PageUrl, ref Int64 status)
        {
            DataTable dt = _PageDetails.GetBreadCrumbListByUrl(PageUrl, ref status);
            Entities.PageDetails objPageDetails = new Entities.PageDetails();

            if (dt.Rows.Count == 1)
            {
                objPageDetails.DisplayName = (dt.Rows[0]["DisplayName"] != DBNull.Value ? dt.Rows[0]["DisplayName"].ToString() : null);
                objPageDetails.ParentName = (dt.Rows[0]["ParentName"] != DBNull.Value ? dt.Rows[0]["ParentName"].ToString() : null);
                objPageDetails.PageParentId = (dt.Rows[0]["PageParentId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["PageParentId"]) : 0);
                objPageDetails.ParentUrl = (dt.Rows[0]["ParentUrl"] != DBNull.Value ? dt.Rows[0]["ParentUrl"].ToString() : null);
                objPageDetails.MenuItemId = (dt.Rows[0]["MenuItemId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["MenuItemId"]) : 0);
            }

            return objPageDetails;
        }

        public List<Entities.PageDetails> GetPageDetailsList(ref int Total)
        {
            DataTable dt = _PageDetails.GetPageDetailsList(ref Total);
            List<Entities.PageDetails> lstPageDetails = new List<Entities.PageDetails>();

            if (dt.Rows.Count == 0)
            {
                dt = _PageDetails.GetPageDetailsList(ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.PageDetails objPageDetails = new Entities.PageDetails();
                     
                    objPageDetails.PageDetailId = Convert.ToInt64(dr["PageDetailId"]);
                    objPageDetails.Heading = dr["Heading"].ToString();
                    objPageDetails.Description = dr["Description"].ToString();
                    objPageDetails.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : null);
                    objPageDetails.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : null);
                    objPageDetails.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : null);
                    objPageDetails.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : null);
                    objPageDetails.PageTitle = (dr["PageTitle"] != DBNull.Value ? dr["PageTitle"].ToString() : null);
                    objPageDetails.MetaDescription = (dr["MetaDescription"] != DBNull.Value ? dr["MetaDescription"].ToString() : null);
                    objPageDetails.MetaKeywords = (dr["MetaKeywords"] != DBNull.Value ? dr["MetaKeywords"].ToString() : null);
                    objPageDetails.TopLine = (dr["TopLine"] != DBNull.Value ? dr["TopLine"].ToString() : null);
                    objPageDetails.InsertedBy = dr["InsertedBy"].ToString();
                    objPageDetails.InsertedDate = Convert.ToDateTime(dr["InsertedDate"]);
                    objPageDetails.UpdatedBy = dr["UpdatedBy"].ToString();
                    objPageDetails.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    objPageDetails.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstPageDetails.Add(objPageDetails);
                }
            }
            return lstPageDetails;
        }


        public Entities.PageDetails PageDetailsGetByHeading(string Heading ,ref int status)
        {
            DataTable dt = _PageDetails.PageDetailsGetByHeading(Heading, ref status);
            Entities.PageDetails objPageDetails = new Entities.PageDetails();

            if (dt.Rows.Count == 1)
            {
                objPageDetails.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
                objPageDetails.Heading = dt.Rows[0]["Heading"].ToString();
                objPageDetails.Description = dt.Rows[0]["Description"].ToString();
                objPageDetails.PageUrl = (dt.Rows[0]["PageUrl"] != DBNull.Value ? dt.Rows[0]["PageUrl"].ToString() : null);
                objPageDetails.OtherUrl = (dt.Rows[0]["OtherUrl"] != DBNull.Value ? dt.Rows[0]["OtherUrl"].ToString() : null);
                objPageDetails.DocumentUrl = (dt.Rows[0]["DocumentUrl"] != DBNull.Value ? dt.Rows[0]["DocumentUrl"].ToString() : null);
                objPageDetails.Target = (dt.Rows[0]["Target"] != DBNull.Value ? dt.Rows[0]["Target"].ToString() : null);
                objPageDetails.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objPageDetails.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objPageDetails.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objPageDetails.TopLine = (dt.Rows[0]["TopLine"] != DBNull.Value ? dt.Rows[0]["TopLine"].ToString() : null);
                objPageDetails.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objPageDetails.InsertedDate = Convert.ToDateTime(dt.Rows[0]["InsertedDate"]);
                objPageDetails.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objPageDetails.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]);
            }

            return objPageDetails;
        }

        #endregion
    }
}
