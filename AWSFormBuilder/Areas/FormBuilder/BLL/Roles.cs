using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
   public class Roles
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.Roles _Roles = new AWSFormBuilder.Areas.FormBuilder.DAL.Roles();

        #region Methods

        public Int64 InsertRoles(Entities.Roles objRoles)
        {
            Int64 _status = 0;
            if (objRoles != null)
            {
                _status = _Roles.InsertRoles(objRoles);

            }
            return _status;
        }

        public Int64 DeleteRoles(Int64 RoleId)
        {
            Int64 _status = 0;
            _status = _Roles.DeleteRoles(RoleId);
            return _status;
        }

        public Int64 UpdateRolesStatus(Int64 RoleId)
        {
            Int64 _status = 0;
            _status = _Roles.UpdateRolesStatus(RoleId);
            return _status;
        }

       

        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.Roles> GetRolesList(ref int status)
        {
            List<AWSFormBuilder.Entities.Roles> lstRoles = new List<Entities.Roles>();
            DataTable dt = _Roles.GetRolesList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Roles objlstRoles = new AWSFormBuilder.Entities.Roles();

                    objlstRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objlstRoles.RoleName = dr["RoleName"].ToString();
                    objlstRoles.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstRoles.Add(objlstRoles);
                }

            }
            return lstRoles;
        }
public List<AWSFormBuilder.Entities.Roles> RolesGetByRoleId(Int64 RoleId, ref int status)
        {
            List<AWSFormBuilder.Entities.Roles> lstRoles = new List<Entities.Roles>();
            DataTable dt = _Roles.RolesGetByRoleId(RoleId,ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Roles objlstRoles = new AWSFormBuilder.Entities.Roles();

                    objlstRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objlstRoles.RoleName = dr["RoleName"].ToString();
                    objlstRoles.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    lstRoles.Add(objlstRoles);
                }

            }
            return lstRoles;
        }


     
        public AWSFormBuilder.Entities.Roles GetRolesById(Int64 RoleId, ref int status)
        {
            AWSFormBuilder.Entities.Roles objRoles = new AWSFormBuilder.Entities.Roles();
            DataTable dt = new DataTable();
            if (RoleId != 0)
            {
                dt = _Roles.GetRolesById(RoleId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objRoles.RoleId = Convert.ToInt64(dt.Rows[0]["RoleId"].ToString());
                    objRoles.RoleName = dt.Rows[0]["RoleName"].ToString();
                    objRoles.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

                }
            }
            return objRoles;
        }

        public List<AWSFormBuilder.Entities.Roles> GetRolesListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.Roles> lstRoles = new List<AWSFormBuilder.Entities.Roles>();
            DataTable dt = _Roles.GetRolesListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _Roles.GetRolesListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Roles objRoles = new AWSFormBuilder.Entities.Roles();

                    objRoles.RId = Convert.ToInt64(dr["RId"].ToString());
                    objRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objRoles.UserCount = Convert.ToInt64(dr["UserCount"].ToString());
                    objRoles.RoleName = dr["RoleName"].ToString();
                    objRoles.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objRoles.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);
                    objRoles.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);

                    lstRoles.Add(objRoles);
                }
            }
            return lstRoles;
        }

        public Entities.Roles RolesGetByRoleName(string RoleName, ref int status)
        {
            DataTable dt = _Roles.RolesGetByRoleName(RoleName, ref status);
            Entities.Roles objRoles = new Entities.Roles();

            if (dt.Rows.Count == 1)
            {
                objRoles.RoleId = Convert.ToInt64(dt.Rows[0]["RoleId"].ToString());
                objRoles.RoleName = dt.Rows[0]["RoleName"].ToString();
                objRoles.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

            }

            return objRoles;
        }

        #endregion
    }
}
