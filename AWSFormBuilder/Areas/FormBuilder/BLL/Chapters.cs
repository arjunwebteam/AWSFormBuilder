using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
 public   class Chapters
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.Chapters _Chapters = new AWSFormBuilder.Areas.FormBuilder.DAL.Chapters();

        #region Methods

        public Int64 InsertChapters(Entities.Chapters objChapters)
        {
            Int64 _status = 0;
            if (objChapters != null)
            {
                _status = _Chapters.InsertChapters(objChapters);

            }
            return _status;
        }

    
        public Int64 DeleteChapter(Int64 ChapterId)
        {
            Int64 _status = 0;
            _status = _Chapters.DeleteChapter(ChapterId);
            return _status;
        }

        public Int64 UpdateChaptersDisplayOrder(int DisplayOrder, Int64 ChapterId)
        {
            Int64 _status = 0;
            _status = _Chapters.UpdateChaptersDisplayOrder(DisplayOrder, ChapterId);
            return _status;
        }

        public Int64 UpdateChaptersStatus(Int64 ChapterId)
        {
            Int64 _status = 0;
            _status = _Chapters.UpdateChaptersStatus(ChapterId);
            return _status;
        }

        #endregion

        #region Entities filling

        public AWSFormBuilder.Entities.Chapters GetChaptersById(Int64 ChapterId, ref int status)
        {
            AWSFormBuilder.Entities.Chapters objChapters = new AWSFormBuilder.Entities.Chapters();
            DataTable dt = new DataTable();
            if (ChapterId != 0)
            {
                dt = _Chapters.GetChaptersById(ChapterId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objChapters.ChapterId = Convert.ToInt64(dt.Rows[0]["ChapterId"].ToString());
                    objChapters.ChapterName = dt.Rows[0]["ChapterName"] != DBNull.Value ? dt.Rows[0]["ChapterName"].ToString() : null;
                    objChapters.ShortName = dt.Rows[0]["ShortName"] != DBNull.Value ? dt.Rows[0]["ShortName"].ToString() : null;
                    objChapters.ShortDescription = dt.Rows[0]["ShortDescription"] != DBNull.Value ? dt.Rows[0]["ShortDescription"].ToString() : null;
                    objChapters.ParentChapterName = dt.Rows[0]["ParentChapterName"] != DBNull.Value ? dt.Rows[0]["ParentChapterName"].ToString() : null;
                    objChapters.Description = dt.Rows[0]["Description"] != DBNull.Value ? dt.Rows[0]["Description"].ToString() : null;
                    objChapters.Address = dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null;
                    objChapters.City = dt.Rows[0]["City"] != DBNull.Value ? dt.Rows[0]["City"].ToString() : null;
                    objChapters.State = dt.Rows[0]["State"] != DBNull.Value ? dt.Rows[0]["State"].ToString() : null;
                    objChapters.ZipCode = dt.Rows[0]["ZipCode"] != DBNull.Value ? dt.Rows[0]["ZipCode"].ToString() : null;
                    objChapters.IsActive = (dt.Rows[0]["IsActive"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsActive"]) : false);
                    objChapters.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["OrderNo"]) : 0);
                    objChapters.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objChapters.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    objChapters.IsNotification = dt.Rows[0]["IsNotification"] != DBNull.Value ? dt.Rows[0]["IsNotification"].ToString() : null;
                    objChapters.CoordinatorPhone = dt.Rows[0]["CoordinatorPhone"] != DBNull.Value ? dt.Rows[0]["CoordinatorPhone"].ToString() : null;
                    objChapters.CoordinatorName = dt.Rows[0]["CoordinatorName"] != DBNull.Value ? dt.Rows[0]["CoordinatorName"].ToString() : null;
                    objChapters.CoordinatorEmail = dt.Rows[0]["CoordinatorEmail"] != DBNull.Value ? dt.Rows[0]["CoordinatorEmail"].ToString() : null;

                }
            }
            return objChapters;
        }

        public List<AWSFormBuilder.Entities.Chapters> GetChaptersListByVariable(string Search,string Sort, int PageNo, int Items, ref int Total, Int64 ChapterId = 0)
        {
            List<AWSFormBuilder.Entities.Chapters> lstChapters = new List<AWSFormBuilder.Entities.Chapters>();
            DataTable dt = _Chapters.GetChaptersListByVariable(Search, Sort, PageNo, Items,ref Total, ChapterId);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Chapters.GetChaptersListByVariable(Search, Sort, PageNo - 1, Items, ref Total, ChapterId);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Chapters objChapters = new AWSFormBuilder.Entities.Chapters();

                    objChapters.RId = Convert.ToInt64(dr["RId"].ToString());
                    objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"].ToString());
                    objChapters.ChapterName = (dr["ChapterName"] != DBNull.Value ? (dr["ChapterName"].ToString()) : null);
                    objChapters.ShortDescription = (dr["ShortDescription"] != DBNull.Value ? (dr["ShortDescription"].ToString()) : null);
                    objChapters.ShortName = (dr["ShortName"] != DBNull.Value ? (dr["ShortName"].ToString()) : null);
                    objChapters.Description = (dr["Description"] != DBNull.Value ? (dr["Description"].ToString()) : null);
                    objChapters.Address = (dr["Address"] != DBNull.Value ? (dr["Address"].ToString()) : null);
                    objChapters.City = (dr["City"] != DBNull.Value ? (dr["City"].ToString()) : null);
                    objChapters.State = (dr["State"] != DBNull.Value ? (dr["State"].ToString()) : null);
                    objChapters.ZipCode = (dr["ZipCode"] != DBNull.Value ? (dr["ZipCode"].ToString()) : null);
                    objChapters.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objChapters.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objChapters.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? (dr["UpdatedBy"].ToString()) : null);
                    objChapters.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                    lstChapters.Add(objChapters);
                }
            }
            return lstChapters;
        }

        #endregion

        #region

        public List<AWSFormBuilder.Entities.Chapters> GetChaptersList(ref int status)
        {
            List<AWSFormBuilder.Entities.Chapters > lstChapters  = new List<Entities.Chapters> ();
            DataTable dt = _Chapters.GetChaptersList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Chapters objChapters = new AWSFormBuilder.Entities.Chapters();
                    objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"].ToString());
                    objChapters.ChapterName = (dr["ChapterName"] != DBNull.Value ? (dr["ChapterName"].ToString()) : null);
                    objChapters.ShortDescription = (dr["ShortDescription"] != DBNull.Value ? (dr["ShortDescription"].ToString()) : null);
                    objChapters.ShortName = (dr["ShortName"] != DBNull.Value ? (dr["ShortName"].ToString()) : null);
                    objChapters.Description = (dr["Description"] != DBNull.Value ? (dr["Description"].ToString()) : null);
                    objChapters.Address = (dr["Address"] != DBNull.Value ? (dr["Address"].ToString()) : null);
                    objChapters.City = (dr["City"] != DBNull.Value ? (dr["City"].ToString()) : null);
                    objChapters.State = (dr["State"] != DBNull.Value ? (dr["State"].ToString()) : null);
                    objChapters.ZipCode = (dr["ZipCode"] != DBNull.Value ? (dr["ZipCode"].ToString()) : null);
                    objChapters.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objChapters.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objChapters.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? (dr["UpdatedBy"].ToString()) : null);
                    objChapters.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                    objChapters.IsNotification = (dr["IsNotification"] != DBNull.Value ? (dr["IsNotification"].ToString()) : null);

                    lstChapters.Add(objChapters);
                }

            }
            return lstChapters;
        }

        public List<AWSFormBuilder.Entities.Chapters> GetChaptersListById(Int64 CommitteeCategoryId, ref int status)
        {
            List<AWSFormBuilder.Entities.Chapters> lstChapters = new List<Entities.Chapters>();
            DataTable dt = _Chapters.GetChaptersListById(CommitteeCategoryId, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Chapters objChapters = new AWSFormBuilder.Entities.Chapters();
                    objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"].ToString());
                    objChapters.ChapterName = (dr["ChapterName"] != DBNull.Value ? (dr["ChapterName"].ToString()) : null);
                    objChapters.ShortDescription = (dr["ShortDescription"] != DBNull.Value ? (dr["ShortDescription"].ToString()) : null);
                    objChapters.ShortName = (dr["ShortName"] != DBNull.Value ? (dr["ShortName"].ToString()) : null);
                    objChapters.Description = (dr["Description"] != DBNull.Value ? (dr["Description"].ToString()) : null);
                    objChapters.Address = (dr["Address"] != DBNull.Value ? (dr["Address"].ToString()) : null);
                    objChapters.City = (dr["City"] != DBNull.Value ? (dr["City"].ToString()) : null);
                    objChapters.State = (dr["State"] != DBNull.Value ? (dr["State"].ToString()) : null);
                    objChapters.ZipCode = (dr["ZipCode"] != DBNull.Value ? (dr["ZipCode"].ToString()) : null);
                    objChapters.IsActive = (dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false);
                    objChapters.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objChapters.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? (dr["UpdatedBy"].ToString()) : null);
                    objChapters.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                    lstChapters.Add(objChapters);
                }

            }
            return lstChapters;
        }

        public AWSFormBuilder.Entities.Chapters GetChapters(ref int status)
        {
            AWSFormBuilder.Entities.Chapters objChapters = new AWSFormBuilder.Entities.Chapters();
            DataTable dt = new DataTable();
             dt = _Chapters.GetChapters(ref status);
                if (dt.Rows.Count == 1)
                { 
                    objChapters.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["OrderNo"]) : 0);
                    
                } 

            return objChapters;
        }

        public AWSFormBuilder.Entities.Chapters GetChaptersListByName(string cname, ref int status)
        {
            AWSFormBuilder.Entities.Chapters objChapters = new AWSFormBuilder.Entities.Chapters();
            DataTable dt = new DataTable();
            if (cname != "")
            {
                dt = _Chapters.GetChaptersListByName(cname, ref status);
                if (dt.Rows.Count == 1)
                {
                    objChapters.ChapterId = Convert.ToInt64(dt.Rows[0]["ChapterId"].ToString());
                    objChapters.ChapterName = dt.Rows[0]["ChapterName"] != DBNull.Value ? dt.Rows[0]["ChapterName"].ToString() : null;
                    objChapters.ShortName = dt.Rows[0]["ShortName"] != DBNull.Value ? dt.Rows[0]["ShortName"].ToString() : null;
                    objChapters.ShortDescription = dt.Rows[0]["ShortDescription"] != DBNull.Value ? dt.Rows[0]["ShortDescription"].ToString() : null;
                    objChapters.Description = dt.Rows[0]["Description"] != DBNull.Value ? dt.Rows[0]["Description"].ToString() : null;
                    objChapters.Address = dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null;
                    objChapters.City = dt.Rows[0]["City"] != DBNull.Value ? dt.Rows[0]["City"].ToString() : null;
                    objChapters.State = dt.Rows[0]["State"] != DBNull.Value ? dt.Rows[0]["State"].ToString() : null;
                    objChapters.ZipCode = dt.Rows[0]["ZipCode"] != DBNull.Value ? dt.Rows[0]["ZipCode"].ToString() : null;
                    objChapters.IsActive = (dt.Rows[0]["IsActive"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsActive"]) : false);
                    objChapters.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["OrderNo"]) : 0);
                    objChapters.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    objChapters.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());

                }
            }
            return objChapters;
        }

        #endregion

    }


}
