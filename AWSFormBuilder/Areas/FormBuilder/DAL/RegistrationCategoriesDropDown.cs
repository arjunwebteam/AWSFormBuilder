using AWSFormBuilder.Areas.FormBuilder.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
 public class RegistrationCategoriesDropDown
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public DataTable GetRegistrationCategoriesDropDownList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("RegistrationCategoriesDropDownGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 InsertRegistrationCategoriesDropDown(Entities.RegistrationCategoriesDropDown objRegistrationCategoriesDropDown)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@RegistrationCategoriesCategoryId",objRegistrationCategoriesDropDown.RegistrationCategoriesCategoryId),
                    new SqlParameter("@CategoryName",objRegistrationCategoriesDropDown.CategoryName),
                    new SqlParameter("@Title",(objRegistrationCategoriesDropDown.Title == null ?DBNull.Value:(object)objRegistrationCategoriesDropDown.Title)),
                    new SqlParameter("@OrderNo",(objRegistrationCategoriesDropDown.OrderNo == 0 ?DBNull.Value:(object)objRegistrationCategoriesDropDown.OrderNo)),
                    new SqlParameter("@IsActive",objRegistrationCategoriesDropDown.IsActive),
                    new SqlParameter("@Price",(objRegistrationCategoriesDropDown.Price == 0 ?DBNull.Value:(object)objRegistrationCategoriesDropDown.Price)),
                    new SqlParameter("@Field1",(objRegistrationCategoriesDropDown.Field1 == null ?DBNull.Value:(object)objRegistrationCategoriesDropDown.Field1)),
                    new SqlParameter("@Field2",(objRegistrationCategoriesDropDown.Field2 == null ?DBNull.Value:(object)objRegistrationCategoriesDropDown.Field2)),
                    new SqlParameter("@InsertedBy",(objRegistrationCategoriesDropDown.InsertedBy == null ?DBNull.Value:(object)objRegistrationCategoriesDropDown.InsertedBy)),
                    new SqlParameter("@InsertedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0),
                  
                    };
                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("RegistrationCategoriesDropDownInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetRegistrationCategoriesDropDownListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                    new SqlParameter("@Total",Total)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("RegistrationCategoriesDropDownGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetRegistrationCategoriesById(Int64 RegistrationCategoriesCategoryId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@RegistrationCategoriesCategoryId",RegistrationCategoriesCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("RegistrationCategoriesDropDownGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteRegistrationCategoriesCategory(Int64 RegistrationCategoriesCategoryId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@RegistrationCategoriesCategoryId",RegistrationCategoriesCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("RegistrationCategoriesDropDownDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateRegistrationCategoriesStatus(Int64 RegistrationCategoriesCategoryId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@RegistrationCategoriesCategoryId",RegistrationCategoriesCategoryId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("RegistrationCategoriesDropDownUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateRegistrationCategoriesDisplayOrder(int DisplayOrder, Int64 RegistrationCategoriesCategoryId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@RegistrationCategoriesCategoryId",RegistrationCategoriesCategoryId),
                    new SqlParameter("@OrderNo",DisplayOrder),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("RegistrationCategoriesDropDownUpdateOrderNo", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
    }
}
