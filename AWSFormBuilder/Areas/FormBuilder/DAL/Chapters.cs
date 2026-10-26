using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
  public  class Chapters
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public DataTable GetChaptersList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ChaptersGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

     
        public Int64 InsertChapters(Entities.Chapters objChapters)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@ChapterId",objChapters.ChapterId),
                    new SqlParameter("@ChapterName",(objChapters.ChapterName !=null ? (object)objChapters.ChapterName:DBNull.Value)),
                    new SqlParameter("@ShortName",(objChapters.ShortName !=null ? (object)objChapters.ShortName:DBNull.Value)),
                    new SqlParameter("@ShortDescription",(objChapters.ShortDescription !=null ? (object)objChapters.ShortDescription:DBNull.Value)),
                    new SqlParameter("@Description",(objChapters.Description !=null ? (object)objChapters.Description:DBNull.Value)),
                    new SqlParameter("@Address",(objChapters.Address !=null ? (object)objChapters.Address:DBNull.Value)),
                    new SqlParameter("@City",(objChapters.City !=null ? (object)objChapters.City:DBNull.Value)),
                    new SqlParameter("@State",(objChapters.State !=null ? (object)objChapters.State:DBNull.Value)),
                    new SqlParameter("@ZipCode",(objChapters.ZipCode !=null ? (object)objChapters.ZipCode:DBNull.Value)),
                    new SqlParameter("@IsActive",objChapters.IsActive),
                    new SqlParameter("@OrderNo",(objChapters.OrderNo==0?(object)DBNull.Value:objChapters.OrderNo)),
                    new SqlParameter("@UpdatedBy",objChapters.UpdatedBy),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@CoordinatorEmail",(objChapters.CoordinatorEmail !=null ? (object)objChapters.CoordinatorEmail:DBNull.Value)),
                      new SqlParameter("@CoordinatorName",(objChapters.CoordinatorName !=null ? (object)objChapters.CoordinatorName:DBNull.Value)),
                    new SqlParameter("@CoordinatorPhone",(objChapters.CoordinatorPhone !=null ? (object)objChapters.CoordinatorPhone:DBNull.Value)),
                    new SqlParameter("@IsNotification",(objChapters.IsNotification !=null ? (object)objChapters.IsNotification:DBNull.Value)),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[17].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ChaptersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[17].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

       

        public DataTable GetChaptersListByVariable( string Search, string Sort, int PageNo, int Items, ref int Total, Int64 ChapterId)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                     
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                  
                    new SqlParameter("@Total",Total),
                     new SqlParameter("@ChapterId",ChapterId),
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ChaptersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetChaptersById(Int64 ChapterId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ChaptersGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteChapter(Int64 ChapterId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ChaptersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateChaptersStatus(Int64 ChapterId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ChaptersUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateChaptersDisplayOrder(int DisplayOrder, Int64 ChapterId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@DisplayOrder",DisplayOrder),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("ChaptersUpdateDisplayOrder", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetChaptersListById(Int64 CommitteeCategoryId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@CommitteeCategoryId",CommitteeCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ChaptersGetListByCategoryId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetChapters(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                { 
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("GetChapters", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetChaptersListByName(string cname, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@cname",cname),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ChaptersGetByName", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
