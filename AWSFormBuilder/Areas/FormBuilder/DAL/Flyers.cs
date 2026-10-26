using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class Flyers
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 UpdateFlyersStatus(Int64 FlyerId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@FlyerId",FlyerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FlyersUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateFlyersIsPage(Int64 FlyerId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@FlyerId",FlyerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FlyersUpdateIsPage", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertFlyers(Entities.Flyers objFlyers, ref string FlyerUrl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@FlyerId",objFlyers.FlyerId),
                    new SqlParameter("@RedirectUrl",(objFlyers.RedirectUrl == null ?DBNull.Value:(object)objFlyers.RedirectUrl.Trim())),
                    new SqlParameter("@FlyerUrl",FlyerUrl),    
                    new SqlParameter("@PageType",(objFlyers.PageType == null ?DBNull.Value:(object)objFlyers.PageType.Trim())),
                    new SqlParameter("@IsActive",objFlyers.IsActive),
                    new SqlParameter("@UpdatedBy",objFlyers.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@PageContent",(objFlyers.PageContent==null?(object)DBNull.Value:objFlyers.PageContent))

                    };
                _sqlP[2].SqlDbType = SqlDbType.NVarChar;
                _sqlP[2].Size = 512;
                _sqlP[2].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FlyersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);

                FlyerUrl = _sqlP[2].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetFlyerListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("FlyersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetFlyerById(Int64 FlyerId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@FlyerId",FlyerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FlyersGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteFlyer(Int64 FlyerId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@FlyerId",FlyerId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FlyersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
    }
}
