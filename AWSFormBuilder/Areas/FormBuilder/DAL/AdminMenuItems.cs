using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class AdminMenuItems
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        public Int64 DeleteAdminMenuItems(Int64 MenuItemId)
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
                _dbAccess.SP_ExecuteScalar("AdminMenuItemsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertAdminMenuItems(Entities.AdminMenuItems objAdminMenuItems, ref string Logo)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MenuItemId",objAdminMenuItems.MenuItemId),
                    new SqlParameter("@ChapterId",objAdminMenuItems.ChapterId),
                    new SqlParameter("@DisplayName",objAdminMenuItems.DisplayName),
                    new SqlParameter("@PageParentId",(objAdminMenuItems.PageParentId == 0 ?DBNull.Value:(object)objAdminMenuItems.PageParentId)),

                    new SqlParameter("@IsTopBar",(objAdminMenuItems.IsTopBar)),
                    new SqlParameter("@IsMenuBar",(objAdminMenuItems.IsMenuBar )),
                    new SqlParameter("@IsQuickLinks",(objAdminMenuItems.IsQuickLinks)),
                    new SqlParameter("@IsFooterBar",(objAdminMenuItems.IsFooterBar)),
                    new SqlParameter("@Position",(objAdminMenuItems.Position == 0 ?DBNull.Value:(object)objAdminMenuItems.Position)),
                    new SqlParameter("@PageUrl",(objAdminMenuItems.PageUrl==null?(object)DBNull.Value:objAdminMenuItems.PageUrl.Trim())),
                    new SqlParameter("@OtherUrl",(objAdminMenuItems.OtherUrl==null?(object)DBNull.Value:objAdminMenuItems.OtherUrl.Trim())),
                    new SqlParameter("@IsActive",objAdminMenuItems.IsActive),
                    new SqlParameter("@UpdatedBy",objAdminMenuItems.UpdatedBy),
                    new SqlParameter("@UpdatedDate",objAdminMenuItems.UpdatedDate),
                    new SqlParameter("@InsertedBy",objAdminMenuItems.InsertedBy),
                    new SqlParameter("@InsertedDate",objAdminMenuItems.InsertedDate),
                    new SqlParameter("@QStatus",0),
                  new SqlParameter("@Logo",Logo),
                    new SqlParameter("@IsEdit",(objAdminMenuItems.IsEdit == false ?DBNull.Value:(object)objAdminMenuItems.IsEdit)),
                    new SqlParameter("@IsView",(objAdminMenuItems.IsView == false ?DBNull.Value:(object)objAdminMenuItems.IsView)),
                    new SqlParameter("@IsDelete",(objAdminMenuItems.IsDelete == false ?DBNull.Value:(object)objAdminMenuItems.IsDelete)),
                    new SqlParameter("@IsExport",(objAdminMenuItems.IsExport == false ?DBNull.Value:(object)objAdminMenuItems.IsExport)),
                    };


                _sqlP[17].SqlDbType = SqlDbType.NVarChar;
                _sqlP[17].Size = 512;
                _sqlP[17].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[16].Direction = System.Data.ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar("AdminMenuItemsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[16].Value);
                Logo = _sqlP[17].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetAdminMenuItemsById(Int64 MenuItemId, ref int Status)
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
                dt = _dbAccess.GetDataTable("AdminMenuItemsGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAdminMenuItemsByLevel(Int64 PageLevel, ref int Status)
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
                dt = _dbAccess.GetDataTable("AdminMenuItemsGetByLevel", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAdminMenuItemsByParentId(Int64 PageParentId, ref int Status)
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
                dt = _dbAccess.GetDataTable("AdminMenuItemsGetByParentId", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAdminMenuItemsByName(string DisplayName, ref int Status)
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
                dt = _dbAccess.GetDataTable("AdminMenuItemsGetByName", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateAdminMenuItemsStatus(Int64 MenuItemId)
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
                _dbAccess.SP_ExecuteScalar("AdminMenuItemsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetAdminMenuItems(Int64 ChapterId, ref int Status)
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
                dt = _dbAccess.GetDataTable("AdminMenuItemsGetList", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #region FrontEnd

        public DataTable FEGetAdminMenuItemsForMenu(Int64 ChapterId, ref int status)
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
                dt = _dbAccess.GetDataTable("FEAdminMenuItemsGetListForMenu", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion

        #region AssignMenu

        public DataTable GetRolesAssignMenu(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("SP_AssignMenu_GetRoles", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataSet GetAssignMenuList(Int32 MenuItemId, Int32 UserId, Int32 RoleId, ref int status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@RoleId",RoleId)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("SP_AssihnMenu_GetSubMenu", ref _sqlP);
                status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable GetUsersByRole(Int32 RoleId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@RoleId",RoleId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("SP_Users_GetUsersByRole", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int32 InsertRoleMenu(Int32 RoleId, string MenuIds, Int32 MenuId, Int32 UserId, string CreatedBy, DateTime CreatedDate)
        {
            Int32 _status = 0;
            try
            {
                _sqlP = new[]
                    {

                     new SqlParameter("@CreatedDate",CreatedDate),
                    new SqlParameter("@CreatedBy",CreatedBy),
                    new SqlParameter("@UserId", UserId),
                    new SqlParameter("@AssignedMenus", MenuIds),
                    new SqlParameter("@MenuId", MenuId),
                    new SqlParameter("@Roleid",RoleId),
                    new SqlParameter("@QStatus",0),
                    };
                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("SP_AssignMenu_InsertRoleMenu", ref _sqlP);
                _status = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateRoleMenuBasedAccess(AWSFormBuilder.Entities.Role_Menu objMenuItems)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MenuId",objMenuItems.MenuId),
                    new SqlParameter("@RoleMenuMasterId",objMenuItems.RoleMenuMasterId),
                    new SqlParameter("@RoleId",objMenuItems.RoleId),
                    new SqlParameter("@IsAdd",(objMenuItems.IsAdd == false ?DBNull.Value:(object)objMenuItems.IsAdd)),
                    new SqlParameter("@IsEdit",(objMenuItems.IsEdit == false ?DBNull.Value:(object)objMenuItems.IsEdit)),
                    new SqlParameter("@IsView",(objMenuItems.IsView == false ?DBNull.Value:(object)objMenuItems.IsView)),
                    new SqlParameter("@IsDelete",(objMenuItems.IsDelete == false ?DBNull.Value:(object)objMenuItems.IsDelete)),
                    new SqlParameter("@IsExport",(objMenuItems.IsExport == false ?DBNull.Value:(object)objMenuItems.IsExport)),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@ParentId",(objMenuItems.ParentId == 0 ?DBNull.Value:(object)objMenuItems.ParentId)),
                    new SqlParameter("@UserId",(objMenuItems.UserId == 0 ?DBNull.Value:(object)objMenuItems.UserId)),
                    };
                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateRoleMenuBasedAccess", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public Int64 SingleMenuUpdate(AWSFormBuilder.Entities.Role_Menu objMenuItems)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@MenuId",objMenuItems.MenuId),
                    new SqlParameter("@RoleId",objMenuItems.RoleId),
                    new SqlParameter("@IsAdd",(objMenuItems.IsAdd == false ?DBNull.Value:(object)objMenuItems.IsAdd)),
                    new SqlParameter("@IsEdit",(objMenuItems.IsEdit == false ?DBNull.Value:(object)objMenuItems.IsEdit)),
                    new SqlParameter("@IsView",(objMenuItems.IsView == false ?DBNull.Value:(object)objMenuItems.IsView)),
                    new SqlParameter("@IsDelete",(objMenuItems.IsDelete == false ?DBNull.Value:(object)objMenuItems.IsDelete)),
                    new SqlParameter("@IsExport",(objMenuItems.IsExport == false ?DBNull.Value:(object)objMenuItems.IsExport)),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@ParentId",(objMenuItems.ParentId == 0 ?DBNull.Value:(object)objMenuItems.ParentId)),
                    new SqlParameter("@UserId",(objMenuItems.UserId == 0 ?DBNull.Value:(object)objMenuItems.UserId)),
                    new SqlParameter("@Type",(objMenuItems.Type == null ?DBNull.Value:(object)objMenuItems.Type)),
                    };
                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("SingleMenuUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public Int64 UpdateCloneRoleIds(Int64 RoleId = 0, Int64 RoleIds = 0)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {

                    new SqlParameter("@RoleId",RoleId),
                    new SqlParameter("@RoleIds",RoleIds),
                    new SqlParameter("@QStatus",0),
                    };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateCloneRoleIds", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 RemoveRoleMenuAccess(Int64 RoleMenuMasterId, Int64 ParentId, Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@RoleMenuMasterId",RoleMenuMasterId),
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@ParentId",ParentId)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("RemoveRoleMenuAccess", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[0].Value);
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
