using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class MenuItems
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 DeleteMenuItems(Int64 MenuItemId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MenuItemsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public Int64 UpdateMenuItemsOrderNo(int Position, Int64 MenuItemId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@Position",Position),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MenuItemsUpdateOrderNo", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertMenuItems(Entities.MenuItems objMenuItems)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MenuItemId",objMenuItems.MenuItemId),
                    new SqlParameter("@ChapterId",objMenuItems.ChapterId),
                    new SqlParameter("@DisplayName",objMenuItems.DisplayName), 
                    new SqlParameter("@PageParentId",(objMenuItems.PageParentId == 0 ?DBNull.Value:(object)objMenuItems.PageParentId)),
                    new SqlParameter("@Position",(objMenuItems.Position == 0 ?DBNull.Value:(object)objMenuItems.Position)),
                    new SqlParameter("@IsFooterBar",(objMenuItems.IsFooterBar)),
                    new SqlParameter("@IsMenuBar",(objMenuItems.IsMenuBar )),
                    new SqlParameter("@IsQuickLinks",(objMenuItems.IsQuickLinks)),
                    new SqlParameter("@IsTopBar",(objMenuItems.IsTopBar)),
                    new SqlParameter("@IsActive",objMenuItems.IsActive),
                    new SqlParameter("@UpdatedBy",objMenuItems.UpdatedBy),
                    new SqlParameter("@InsertedBy",objMenuItems.InsertedBy),
                    new SqlParameter("@InsertedDate",objMenuItems.InsertedDate),
                    new SqlParameter("@UpdatedDate",objMenuItems.UpdatedDate),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[14].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MenuItemsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[14].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetMenuItemsById(Int64 MenuItemId, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MenuItemsGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMenuItemsByLevel(Int64 PageLevel, ref int Status)
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
                dt = _dbAccess.GetDataTable("MenuItemsGetByLevel", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMenuItemsByParentId(Int64 PageParentId, ref int Status)
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
                dt = _dbAccess.GetDataTable("MenuItemsGetByParentId", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMenuItemsByName(string DisplayName, ref int Status)
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
                dt = _dbAccess.GetDataTable("MenuItemsGetByName", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateMenuItemsStatus(Int64 MenuItemId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MenuItemsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetMenuItems(Int64 ChapterId, bool IsFooterBar, bool IsMenuBar, bool IsQuickLinks, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@IsFooterBar",IsFooterBar),
                    new SqlParameter("@IsMenuBar",IsMenuBar),
                    new SqlParameter("@IsQuickLinks",IsQuickLinks),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MenuItemsGetListbkp", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetMenuItems(Int64 ChapterId, ref int Status)
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
                dt = _dbAccess.GetDataTable("MenuItemsGetList", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable ExistingMenuItemsGetList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExistingMenuItemsGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable ExistingMenuItemsGetbyId(Int64 PageDetailId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@PageDetailId",PageDetailId)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExistingMenuItemsGetbyId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        
        #region FrontEnd

        public DataTable FEGetMenuItemsForMenu(Int64 ChapterId, ref int status)
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
                dt = _dbAccess.GetDataTable("FEMenuItemsGetListForMenu", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
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
