using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class MembershipTypes
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Method

        public Int64 DeleteMembershipType(Int64 MembershipTypeId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MembershipTypeId",MembershipTypeId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembershipTypesDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertMembershipType(Entities.MembershipTypes objMembershipType)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MembershipTypeId",objMembershipType.MembershipTypeId),
                    new SqlParameter("@ChapterId",(objMembershipType.ChapterId==0?(object)DBNull.Value:objMembershipType.ChapterId)),
                    new SqlParameter("@MembershipType",objMembershipType.MembershipType),
                    new SqlParameter("@Price",objMembershipType.Price),
                    new SqlParameter("@Validity",(objMembershipType.Validity==0?(object)DBNull.Value:objMembershipType.Validity)),
                    new SqlParameter("@IsActive",objMembershipType.IsActive),
                    new SqlParameter("@DisplayOrder",(objMembershipType.DisplayOrder==0?(object)DBNull.Value:objMembershipType.DisplayOrder)),
                    new SqlParameter("@UpdatedBy",objMembershipType.UpdatedBy),
                    new SqlParameter("@UpdatedTime",objMembershipType.UpdatedTime),
                     new SqlParameter("@FormattedField",objMembershipType.FormattedField),

                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembershipTypesInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateMembershipTypeDisplayOrder(int DisplayOrder, Int64 MembershipTypeId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MembershipTypeId",MembershipTypeId),
                    new SqlParameter("@DisplayOrder",DisplayOrder),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembershipTypesUpdateDisplayOrder", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateMembershipTypeStatus(Int64 MembershipTypeId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MembershipTypeId",MembershipTypeId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MembershipTypesUpdateStatus", ref _sqlP);
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

        public DataTable GetMembershipTypeById(Int64 MembershipTypeId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MembershipTypeId",MembershipTypeId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembershipTypesGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMembershipTypesListByVariable(Int64 ChapterId,string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {   new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembershipTypesGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMembershipTypesList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MembershipTypesGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
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
