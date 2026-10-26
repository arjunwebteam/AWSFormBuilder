using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class InnerPages
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Method

        public Int64 DeleteInnerPages(Int64 InnerPageId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageId",InnerPageId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPagesDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertInnerPages(Entities.InnerPages objInnerPages)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@InnerPageId",objInnerPages.InnerPageId),
                    new SqlParameter("@InnerPageCategoryId",(objInnerPages.InnerPageCategoryId!= 0?(object)objInnerPages.InnerPageCategoryId:DBNull.Value.ToString())),
                    new SqlParameter("@Heading",objInnerPages.Heading),
                    new SqlParameter("@Description",objInnerPages.Description),
                    new SqlParameter("@DisplayOrder",objInnerPages.DisplayOrder),
                    new SqlParameter("@IsActive",objInnerPages.IsActive),
                    new SqlParameter("@PageTitle",(objInnerPages.PageTitle!= null?(object)objInnerPages.PageTitle:DBNull.Value.ToString())),
                    new SqlParameter("@MetaDescription",(objInnerPages.MetaDescription!= null?(object)objInnerPages.MetaDescription:DBNull.Value.ToString())),
                    new SqlParameter("@MetaKeywords",(objInnerPages.MetaKeywords!= null?(object)objInnerPages.MetaKeywords:DBNull.Value.ToString())),
                    new SqlParameter("@Topline",(objInnerPages.Topline!= null?(object)objInnerPages.Topline:DBNull.Value.ToString())),
                    new SqlParameter("@InsertedBy",objInnerPages.UpdatedBy),
                    new SqlParameter("@InsertedTime",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objInnerPages.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[14].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPagesInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[14].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateInnerPagesStatus(Int64 InnerPageId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@InnerPageId",InnerPageId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPagesUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region Admin Section

        public DataTable GetInnerPagesById(Int64 InnerPageId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageId",InnerPageId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPagesGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetInnerPagesListByVariable(Int64 InnerPageCategoryId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageCategoryId",InnerPageCategoryId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPagesGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetInnerPagesListById(Int64 InnerPageId, string Heading, Int64 ChapterId, Int64 MemberId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@InnerPageId",InnerPageId),
                    new SqlParameter("@Heading",Heading),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@MemberId",MemberId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPagesGetListById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateInnerPagesDisplayOrder(int DisplayOrder, Int64 InnerPageId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageId",InnerPageId),
                    new SqlParameter("@DisplayOrder",DisplayOrder),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPagesUpdateDisplayOrder", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }



      
        #endregion 
    }
}
