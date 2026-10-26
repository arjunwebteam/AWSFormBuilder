using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class MenuPages
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.MenuPages _MenuPages = new AWSFormBuilder.Areas.FormBuilder.DAL.MenuPages();

        #region Methods

        public Int64 InsertMenuPages(Entities.MenuPages objMenuPages)
        {
            Int64 _status = 0;
            if (objMenuPages != null)
            {
                _status = _MenuPages.InsertMenuPages(objMenuPages);

            }
            return _status;
        }

        public Int64 DeleteMenuPages(Int64 MenuPageId)
        {
            Int64 _status = 0;
            _status = _MenuPages.DeleteMenuPages(MenuPageId);
            return _status;
        } 

        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.MenuPages> GetMenuPagesList(ref int status)
        {
            List<AWSFormBuilder.Entities.MenuPages> lstMenuPages = new List<AWSFormBuilder.Entities.MenuPages>();
            DataTable dt = _MenuPages.GetMenuPagesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.MenuPages objlstMenuPages = new AWSFormBuilder.Entities.MenuPages();

                    objlstMenuPages.RId = Convert.ToInt64(dr["RId"].ToString());
                    objlstMenuPages.MenuPageId = Convert.ToInt64(dr["MenuPageId"].ToString());
                    objlstMenuPages.PageDetailId = Convert.ToInt64(dr["PageDetailId"].ToString());
                    objlstMenuPages.MenuItemId = Convert.ToInt64(dr["MenuItemId"].ToString()); 

                    lstMenuPages.Add(objlstMenuPages);
                }

            }
            return lstMenuPages;
        }

        public AWSFormBuilder.Entities.MenuPages GetMenuPagesById(Int64 MenuPageId, ref int status)
        {
            AWSFormBuilder.Entities.MenuPages objMenuPages = new AWSFormBuilder.Entities.MenuPages();
            DataTable dt = new DataTable();
            if (MenuPageId != 0)
            {
                dt = _MenuPages.GetMenuPagesById(MenuPageId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objMenuPages.MenuPageId = Convert.ToInt64(dt.Rows[0]["MenuPageId"].ToString());
                    objMenuPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"].ToString());
                    objMenuPages.MenuItemId = Convert.ToInt64(dt.Rows[0]["MenuItemId"].ToString()); 
                }
            }
            return objMenuPages;
        }

        public List<AWSFormBuilder.Entities.MenuPages> GetMenuPagesListByVariable(string Search, string Location, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.MenuPages> lstMenuPages = new List<AWSFormBuilder.Entities.MenuPages>();
            DataTable dt = _MenuPages.GetMenuPagesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _MenuPages.GetMenuPagesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.MenuPages objMenuPages = new AWSFormBuilder.Entities.MenuPages();

                    objMenuPages.MenuPageId = Convert.ToInt64(dr["MenuPageId"].ToString());
                    objMenuPages.PageDetailId = Convert.ToInt64(dr["PageDetailId"].ToString());
                    objMenuPages.MenuItemId = Convert.ToInt64(dr["MenuItemId"].ToString()); 

                    lstMenuPages.Add(objMenuPages);
                }
            }
            return lstMenuPages;
        }

        public List<AWSFormBuilder.Entities.PageDetails> MenuPagesList(Int64 MenuItemId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.PageDetails> lstPageDetails = new List<AWSFormBuilder.Entities.PageDetails>();
            DataTable dt = _MenuPages.MenuPagesList(MenuItemId, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _MenuPages.MenuPagesList(MenuItemId, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.PageDetails objPageDetails = new AWSFormBuilder.Entities.PageDetails();

                    objPageDetails.RId = Convert.ToInt64(dr["RId"].ToString());
                    objPageDetails.PageDetailId = Convert.ToInt64(dr["PageDetailId"].ToString());
                    objPageDetails.MenuPageId = (dr["MenuPageId"] != DBNull.Value ? Convert.ToInt32(dr["MenuPageId"]) : 0);
                    objPageDetails.MenuItemId = (dr["MenuItemId"] != DBNull.Value ? Convert.ToInt32(dr["MenuItemId"]) : 0);
                    objPageDetails.Heading = dr["Heading"].ToString();
                    objPageDetails.DisplayName = (dr["DisplayName"] != DBNull.Value ? dr["DisplayName"].ToString() : "");
                    objPageDetails.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : ""); 
                    objPageDetails.UpdatedBy = dr["UpdatedBy"].ToString();
                    objPageDetails.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString()); 

                    lstPageDetails.Add(objPageDetails);
                }
            }
            return lstPageDetails;
        }

        #endregion
    }
}
