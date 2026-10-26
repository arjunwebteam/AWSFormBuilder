using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class InnerPageCategories
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 DeleteInnerPageCategories(Int64 InnerPageCategoryId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageCategoryId",InnerPageCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPageCategoriesDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertInnerPageCategories(Entities.InnerPageCategories objInnerPageCategories)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@InnerPageCategoryId",objInnerPageCategories.InnerPageCategoryId),
                    new SqlParameter("@ChapterId",(objInnerPageCategories.ChapterId == 0 ?DBNull.Value:(object)objInnerPageCategories.ChapterId)),
                    new SqlParameter("@DisplayName",objInnerPageCategories.DisplayName), 
                    new SqlParameter("@PageParentId",(objInnerPageCategories.PageParentId == 0 ?DBNull.Value:(object)objInnerPageCategories.PageParentId)),
                    new SqlParameter("@Position",(objInnerPageCategories.Position == 0 ?DBNull.Value:(object)objInnerPageCategories.Position)),
                    new SqlParameter("@IsFooterBar",(objInnerPageCategories.IsFooterBar)),
                    new SqlParameter("@IsMenuBar",(objInnerPageCategories.IsMenuBar )),
                    new SqlParameter("@IsQuickLinks",(objInnerPageCategories.IsQuickLinks)),
                    new SqlParameter("@IsTopBar",(objInnerPageCategories.IsTopBar)),
                    new SqlParameter("@IsActive",objInnerPageCategories.IsActive),
                    new SqlParameter("@UpdatedBy",objInnerPageCategories.UpdatedBy),
                    new SqlParameter("@InsertedBy",objInnerPageCategories.InsertedBy),
                    new SqlParameter("@InsertedDate",DateTime.Now),
                    new SqlParameter("@UpdatedDate",DateTime.Now),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[14].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPageCategoriesInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[14].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetInnerPageCategoriesById(Int64 InnerPageCategoryId, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageCategoryId",InnerPageCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPageCategoriesGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetInnerPageCategoriesByLevel(Int64 PageLevel, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@PageLevel",PageLevel),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPageCategoriesGetByLevel", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetInnerPageCategoriesByParentId(Int64 PageParentId, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@PageParentId",PageParentId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPageCategoriesGetByParentId", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetInnerPageCategoriesByName(string DisplayName, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@DisplayName",DisplayName),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPageCategoriesGetByName", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateInnerPageCategoriesStatus(Int64 InnerPageCategoryId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@InnerPageCategoryId",InnerPageCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("InnerPageCategoriesUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetInnerPageCategories(ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("InnerPageCategoriesGetList", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetInnerPageCategoriesByChapterId(Int64 ChapterId, ref int Status)
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
                dt = _dbAccess.GetDataTable("InnerPageCategoriesGetListByChapterId", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}
