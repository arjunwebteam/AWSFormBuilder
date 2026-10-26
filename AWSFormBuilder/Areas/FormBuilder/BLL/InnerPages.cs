using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class InnerPages
    {
        DAL.InnerPages _InnerPages = new DAL.InnerPages();

        #region Methods

        public Int64 DeleteInnerPages(Int64 InnerPageId)
        {
            Int64 _status = 0;
            if (InnerPageId != 0)
            {
                _status = _InnerPages.DeleteInnerPages(InnerPageId);
            }
            return _status;
        }

        public Int64 InsertInnerPages(Entities.InnerPages objInnerPages)
        {
            Int64 _status = 0;
            if (objInnerPages != null)
            {
                _status = _InnerPages.InsertInnerPages(objInnerPages);
            }
            return _status;
        }

        public Int64 UpdateInnerPagesDisplayOrder(int DisplayOrder, Int64 InnerPageId)
        {
            Int64 _status = 0;
            _status = _InnerPages.UpdateInnerPagesDisplayOrder(DisplayOrder, InnerPageId);
            return _status;
        }

        public Int64 UpdateInnerPagesStatus(Int64 InnerPageId)
        {
            Int64 _status = 0;
            _status = _InnerPages.UpdateInnerPagesStatus(InnerPageId);
            return _status;
        }

        #endregion

        #region Entity Loading

        public List<Entities.InnerPages> GetInnerPagesListByVariable(Int64 InnerPageCategoryId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _InnerPages.GetInnerPagesListByVariable(InnerPageCategoryId,Search, Sort, PageNo, Items, ref Total);
            List<Entities.InnerPages> lstInnerPages = new List<Entities.InnerPages>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _InnerPages.GetInnerPagesListByVariable(InnerPageCategoryId,Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.InnerPages objInnerPages = new Entities.InnerPages();

                    objInnerPages.RId = Convert.ToInt64(dr["Rid"]);
                    objInnerPages.InnerPageId = Convert.ToInt64(dr["InnerPageId"]);
                    objInnerPages.InnerPageCategoryId = (dr["InnerPageCategoryId"] != DBNull.Value ?Convert.ToInt64(dr["InnerPageCategoryId"]) : 0);
                    objInnerPages.CategoryName = dr["CategoryName"].ToString();
                    objInnerPages.Heading = dr["Heading"].ToString();
                    objInnerPages.Description = dr["Description"].ToString();
                    objInnerPages.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objInnerPages.PageTitle = (dr["PageTitle"] != DBNull.Value ? dr["PageTitle"].ToString() : null);
                    objInnerPages.MetaDescription = (dr["MetaDescription"] != DBNull.Value ? dr["MetaDescription"].ToString() : null);
                    objInnerPages.MetaKeywords = (dr["MetaKeywords"] != DBNull.Value ? dr["MetaKeywords"].ToString() : null);
                    objInnerPages.Topline = (dr["Topline"] != DBNull.Value ? dr["Topline"].ToString() : null);
                    objInnerPages.IsActive = (dr["IsActive"] != DBNull.Value ?Convert.ToBoolean(dr["IsActive"].ToString()) : false);
                    objInnerPages.InsertedBy = dr["InsertedBy"].ToString();
                    objInnerPages.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objInnerPages.UpdatedBy = dr["UpdatedBy"].ToString();
                    objInnerPages.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstInnerPages.Add(objInnerPages);
                }
            }
            return lstInnerPages;
        }

        public Entities.InnerPages GetInnerPagesById(Int64 InnerPagesId, ref int status)
        {
            DataTable dt = _InnerPages.GetInnerPagesById(InnerPagesId, ref status);
            Entities.InnerPages objInnerPages = new Entities.InnerPages();

            if (dt.Rows.Count == 1)
            {
                objInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objInnerPages.InnerPageCategoryId = (dt.Rows[0]["InnerPageCategoryId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["InnerPageCategoryId"]) : 0);
                objInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objInnerPages.Description = dt.Rows[0]["Description"].ToString();
                objInnerPages.DisplayOrder = (dt.Rows[0]["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DisplayOrder"]) : 0);
                objInnerPages.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objInnerPages.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objInnerPages.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objInnerPages.Topline = (dt.Rows[0]["Topline"] != DBNull.Value ? dt.Rows[0]["Topline"].ToString() : null);
                objInnerPages.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objInnerPages.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"]);
                objInnerPages.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objInnerPages.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
            }

            return objInnerPages;
        }

        public Entities.InnerPages GetInnerPagesListById(Int64 InnerPagesId, string Heading, Int64 ChapterId, Int64 MemberId, ref int status)
        {
            DataTable dt = _InnerPages.GetInnerPagesListById(InnerPagesId, Heading, ChapterId, MemberId, ref status);
            Entities.InnerPages objInnerPages = new Entities.InnerPages();

            if (dt.Rows.Count == 1)
            {
                objInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
                objInnerPages.InnerPageCategoryId = (dt.Rows[0]["InnerPageCategoryId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["InnerPageCategoryId"]) : 0);
                objInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objInnerPages.Description = dt.Rows[0]["Description"].ToString();
                objInnerPages.DisplayOrder = (dt.Rows[0]["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DisplayOrder"]) : 0);
                objInnerPages.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objInnerPages.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objInnerPages.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objInnerPages.Topline = (dt.Rows[0]["Topline"] != DBNull.Value ? dt.Rows[0]["Topline"].ToString() : null);
                objInnerPages.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                objInnerPages.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"]);
                objInnerPages.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objInnerPages.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
            }

            return objInnerPages;
        }



        #endregion
    }
}
