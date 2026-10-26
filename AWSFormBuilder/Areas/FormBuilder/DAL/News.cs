using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
  public  class News
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public DataTable GetNewsList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("NewsGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 InsertNews(Entities.News objNews, ref string ImageUrl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@NewsId",objNews.NewsId),
                    new SqlParameter("@Title",objNews.Title),
                    new SqlParameter("@NewsText",objNews.NewsText),
                    new SqlParameter("@ImageUrl",ImageUrl),
                    new SqlParameter("@PostDate",objNews.PostDate),
                    new SqlParameter("@OrderNo",objNews.OrderNo),
                    new SqlParameter("@IsActive",objNews.IsActive),
                    new SqlParameter("@UpdatedBy",objNews.UpdatedBy),
                    new SqlParameter("@UpdatedTime",objNews.UpdatedTime),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@ChapterIds",(objNews.ChapterIds == null ?DBNull.Value:(object)objNews.ChapterIds)),
                    new SqlParameter("@ExpiryDate",objNews.ExpiryDate),
                    };
                _sqlP[3].SqlDbType = SqlDbType.NVarChar;
                _sqlP[3].Size = 256;
                _sqlP[3].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[9].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("NewsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[9].Value);

                ImageUrl = _sqlP[3].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetNewsListByVariable(Int64 ChapterId, Int64 MemberId,string Type, string Search, string Sort, int PageNo, int Items, ref int Total)
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
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@Type",Type)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("NewsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAPINewsListByVariable(Int64 ChapterId, Int64 MemberId, string Search, string Sort, int PageNo, int Items, ref int Total)
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
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@ChapterId",ChapterId)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("GetAPINewsListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataSet GetNewsById(Int64 NewsId, ref int status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@NewsId",NewsId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("NewsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public Int64 DeleteNews(Int64 NewsId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@NewsId",NewsId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("NewsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateNewsStatus(Int64 NewsId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@NewsId",NewsId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("NewsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateNewsDisplayOrder(int OrderNo, Int64 NewsId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@NewsId",NewsId),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("NewsUpdateOrderNo", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #region Front-end

        public DataTable FEGetNewsList(Int64 ChapterId, ref int status)
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
                dt = _dbAccess.GetDataTable("FEGetNewsList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FENewsGetListByVariable(Int64 NewsId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@NewsId",NewsId),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@pageNo",PageNo),
                    new SqlParameter("@Items",Items),                    
                    new SqlParameter("@Total",0)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FENewsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FENewsGetListByVariableByChapterId(Int64 NewsId, string Type, Int64 ChapterId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@NewsId",NewsId),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@pageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@Type",Type)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FENewsGetListByVariableByChapterId", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion
    }
}
