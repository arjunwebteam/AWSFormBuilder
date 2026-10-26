using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class Users
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.Users _users = new AWSFormBuilder.Areas.FormBuilder.DAL.Users();

        #region Methods
        public Int64 LogSubReportInsert(AWSFormBuilder.Entities.Logdetails objLogReport)
        {
            Int64 _status = 0;
            if (objLogReport != null)
            {
                _status = _users.LogSubReportInsert(objLogReport);
            }
            return _status;
        }

        public Int64 LogReportInsert(AWSFormBuilder.Entities.ApplicationLogs objLogReport, ref Int64 LogId)
        {
            Int64 _status = 0;
            if (objLogReport != null)
            {
                _status = _users.LogReportInsert(objLogReport, ref LogId);
            }
            return _status;
        }
        public Int64 InsertUserProfile(AWSFormBuilder.Entities.Users objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _users.InsertUserProfile(objUser);
            }
            return _status;
        }

        public Int64 UpdateUserAccess(AWSFormBuilder.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _users.UpdateUserAccess(objUser);
            }
            return _status;
        }

        public Int64 DeleteUser(Int64 UserId)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.DeleteUser(UserId);
            }
            return _status;
        }

        public Int64 DeleteAllUser()
        {
            Int64 _status = 0;
              _status = _users.DeleteAllUser();            
            return _status;
        }

        public Int64 UpdateUserStatus(Int64 UserId)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UpdateUserStatus(UserId);
            }
            return _status;
        }

        public Int64 UnlockUser(Int64 UserId)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UnlockUser(UserId);
            }
            return _status;
        }

        public Int64 ChangePassword(string UserId, string Password)
        {
            Int64 _status = 0;
            if (UserId != "" && Password != null && Password.Trim() != "")
            {
                _status = _users.ChangePassword(UserId, Password);
            }
            return _status;
        }


        public Int64 ChangePassword(Int64 UserId, string Password)
        {
            Int64 _status = 0;
            if (UserId != 0 && Password != null && Password.Trim() != "")
            {
                _status = _users.ChangePassword(UserId, Password);
            }
            return _status;
        }

        public string GetPassword(string _userid, ref int _qstatus)
        {
            string _password = "";
            DataTable dt = _users.GetPassword(_userid, ref _qstatus);
            if (dt.Rows.Count == 1)
            {
                _password = dt.Rows[0]["Password"].ToString();
            }
            return _password;
        }

        public Int64 UpdateRegistrationGUID(Int64 UserId, string IsActivated, Guid RegistrationGUID)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UpdateRegistrationGUID(UserId, IsActivated, RegistrationGUID);
            }
            return _status;
        }

        public Int64 UpdateUser(AWSFormBuilder.Entities.Users objUser)
        {
            Int64 _status = 0;
            if (objUser != null && objUser.UserId != 0)
            {
                _status = _users.UpdateUser(objUser);
            }
            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 UserId, ref string ProfileImage)
        {
            Int64 _status = 0;
            if (UserId != 0)
            {
                _status = _users.UpdateUserProfileImage(UserId, ref ProfileImage);
            }
            return _status;
        }

        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.Roles> GetUserRolesList(string keyword, ref int Total)
        {
            List<AWSFormBuilder.Entities.Roles> lstRoles = new List<AWSFormBuilder.Entities.Roles>();
            DataTable dt = _users.GetUserRolesList(keyword, ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Roles objRoles = new AWSFormBuilder.Entities.Roles();
                    objRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objRoles.RoleName =dr["RoleName"].ToString();
                    objRoles.SubRoleCount = (dr["SubRoleCount"] != DBNull.Value ? Convert.ToInt64(dr["SubRoleCount"]) : 0);
                    objRoles.comma_separated_ids = (dr["comma_separated_ids"] != DBNull.Value ? dr["comma_separated_ids"].ToString() : null);

                    lstRoles.Add(objRoles);
                }

            }
            return lstRoles;
        }

        public List<AWSFormBuilder.Entities.Roles> UserRolesSubDropDownGetList(string keyword, ref int Total)
        {
            List<AWSFormBuilder.Entities.Roles> lstRoles = new List<AWSFormBuilder.Entities.Roles>();
            DataTable dt = _users.UserRolesSubDropDownGetList(keyword, ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Roles objRoles = new AWSFormBuilder.Entities.Roles();
                    objRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objRoles.RoleName = dr["RoleName"].ToString();
                    objRoles.ParentId = (dr["ParentId"] != DBNull.Value ? Convert.ToInt64(dr["ParentId"]) : 0);

                    lstRoles.Add(objRoles);
                }

            }
            return lstRoles;
        }

        public List<AWSFormBuilder.Entities.Roles> GetUserRolesListById(Int64 UserId, ref int Total)
        {
            List<AWSFormBuilder.Entities.Roles> lstRoles = new List<AWSFormBuilder.Entities.Roles>();
            DataTable dt = _users.GetUserRolesListById(UserId,ref Total);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Roles objRoles = new AWSFormBuilder.Entities.Roles();
                    objRoles.RoleId = Convert.ToInt64(dr["RoleId"].ToString());
                    objRoles.RoleName = dr["RoleName"].ToString();
                    objRoles.UserRoleId = (dr["UserRoleId"] != DBNull.Value ? Convert.ToInt64(dr["UserRoleId"]) : 0);

                    objRoles.IsAdd = (dr["IsAdd"] != DBNull.Value ? Convert.ToBoolean(dr["IsAdd"]) : false);
                    objRoles.IsEdit = (dr["IsEdit"] != DBNull.Value ? Convert.ToBoolean(dr["IsEdit"]) : false);
                    objRoles.IsView = (dr["IsView"] != DBNull.Value ? Convert.ToBoolean(dr["IsView"]) : false);
                    objRoles.IsDelete = (dr["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dr["IsDelete"]) : false);
                    objRoles.IsExport = (dr["IsExport"] != DBNull.Value ? Convert.ToBoolean(dr["IsExport"]) : false);
                    lstRoles.Add(objRoles);
                }

            }
            return lstRoles;
        }

        public Entities.Users GetUserByUserName(string UserName, ref int status)
        {
            AWSFormBuilder.Entities.Users _objuser = new AWSFormBuilder.Entities.Users();
            DataTable dt = new DataTable();

            if (UserName != null && UserName.Trim() != "")
            {
                dt = _users.GetUserByUserName(UserName, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.Designation = (dt.Rows[0]["Designation"] != DBNull.Value ?dt.Rows[0]["Designation"].ToString() : null);
                    _objuser.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : null);
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = (dt.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]) : false);                    
                    _objuser.FailedPasswordAttemptCount = (dt.Rows[0]["FailedPasswordAttemptCount"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["FailedPasswordAttemptCount"]) : 0);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.InsertedBy = dt.Rows[0]["InsertedBy"].ToString();
                    _objuser.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"]);
                    _objuser.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    _objuser.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);

                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                }
            }
            return _objuser;
        }

        public Entities.Users GetUserByEmail(string Email, ref int status)
        {
            AWSFormBuilder.Entities.Users _objuser = new AWSFormBuilder.Entities.Users();
            DataTable dt = _users.GetUserByEmail(Email, ref status);

            if (Email != null && Email.Trim() != "")
            {
                dt = _users.GetUserByEmail(Email, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.Password  = (dt.Rows[0]["Password"] != DBNull.Value ? dt.Rows[0]["Password"].ToString() : null);
                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                    _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
                    _objuser.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
                    _objuser.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                }
            }
            return _objuser;
        }

        public Entities.Users GetAdminUsersGetByEmail(string Email, ref int status)
        {
            AWSFormBuilder.Entities.Users _objuser = new AWSFormBuilder.Entities.Users();
            DataTable dt = _users.GetAdminUsersGetByEmail(Email, ref status);

            if (Email != null && Email.Trim() != "")
            {
                dt = _users.GetAdminUsersGetByEmail(Email, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.Password = (dt.Rows[0]["Password"] != DBNull.Value ? dt.Rows[0]["Password"].ToString() : null);
                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                    _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
                    _objuser.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
                    _objuser.EndDate = (dt.Rows[0]["EndDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["EndDate"]) : DateTime.MinValue);
                    _objuser.StartDate = (dt.Rows[0]["StartDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["StartDate"]) : DateTime.MinValue);
                    _objuser.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                    _objuser.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                }
            }
            return _objuser;
        }

        public Entities.Users GetUserByPhoneNo(string MobilePhone, ref int status)
        {
            AWSFormBuilder.Entities.Users _objuser = new AWSFormBuilder.Entities.Users();
            DataTable dt = _users.GetUserByPhoneNo(MobilePhone, ref status);

            if (MobilePhone != null && MobilePhone.Trim() != "")
            {
                dt = _users.GetUserByPhoneNo(MobilePhone, ref status);
                if (dt.Rows.Count == 1)
                {
                    _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
                    _objuser.UserName = dt.Rows[0]["UserName"].ToString();
                    _objuser.Email = dt.Rows[0]["Email"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    _objuser.LastPasswordChangedDate = (dt.Rows[0]["LastPasswordChangedDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);
                    _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dt.Rows[0]["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]) : DateTime.MinValue);

                    if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
                    }
                    _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
                    _objuser.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
                    _objuser.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                    _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
                    _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
                }
            }
            return _objuser;
        }

        public List<Entities.Users> GetUserListByVariable(string RoleName,Int64 UserId, string RoleIds, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<Entities.Users> lstUsers = new List<Entities.Users>();
            DataTable dt = _users.GetUserListByVariable(RoleName,UserId, RoleIds,Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _users.GetUserListByVariable(RoleName,UserId, RoleIds, Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0 && Total != -1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Users _objuser = new Entities.Users();

                    _objuser.UserId = Convert.ToInt64(dr["UserId"]);
                    _objuser.UserName = dr["UserName"].ToString();
                    _objuser.Email = dr["Email"].ToString();
                    _objuser.Designation = dr["Designation"].ToString();
                    _objuser.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    _objuser.IsLockedOut = Convert.ToBoolean(dr["IsLockedOut"]);
                    _objuser.RId = Convert.ToInt64(dr["RId"]);
                    _objuser.IsActivated = Convert.ToBoolean(dr["IsActivated"]);
                    _objuser.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    _objuser.LastLoginDate = (dr["LastLoginDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastLoginDate"]) : DateTime.MinValue);

                    if (dr["RegistrationGUID"] != DBNull.Value)
                    {
                        _objuser.RegistrationGUID = Guid.Parse(dr["RegistrationGUID"].ToString());
                    }

                    _objuser.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : "");
                    _objuser.RoleName = (dr["RoleName"] != DBNull.Value ? dr["RoleName"].ToString() : "");
                    if (_objuser.RoleName != "")
                    {
                        _objuser.RoleName = _objuser.RoleName.Remove(_objuser.RoleName.Length-1, 1);
                    }
                    _objuser.RoleId = (dr["RoleId"] != DBNull.Value ? Convert.ToInt64(dr["RoleId"]) : 0);
                    _objuser.InsertedTime = (dr["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dr["InsertedTime"]) : DateTime.MinValue);
                    _objuser.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
                    _objuser.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
                    _objuser.UpdatedTime = (dr["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedTime"]) : DateTime.MinValue);
                    _objuser.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : "");
                    _objuser.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : "");
                    _objuser.Password = (dr["Password"] != DBNull.Value ? dr["Password"].ToString() : "");

                    lstUsers.Add(_objuser);
                }
            }
            return lstUsers;
        }
        public Entities.Users GetUserDetailsById(Int64 UserId, ref int status)
        {
            DataSet ds = _users.GetUserDetailsById(UserId,ref status);
            Entities.Users _objuser = new Entities.Users();
            List<Entities.ChapterUsers> lstChapterUsers = new List<Entities.ChapterUsers>();

            if (ds.Tables[0].Rows.Count == 1)
            {
                _objuser.UserId = Convert.ToInt64(ds.Tables[0].Rows[0]["UserId"]);
                _objuser.ChapterId = Convert.ToInt64(ds.Tables[0].Rows[0]["ChapterId"]);
                _objuser.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                _objuser.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                _objuser.IsApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApproved"]);
                _objuser.IsLockedOut = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLockedOut"]);
                _objuser.IsActivated = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActivated"]);
                _objuser.DateActivated = (ds.Tables[0].Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["DateActivated"]) : DateTime.MinValue);

                if (ds.Tables[0].Rows[0]["RegistrationGUID"] != DBNull.Value)
                {
                    _objuser.RegistrationGUID = Guid.Parse(ds.Tables[0].Rows[0]["RegistrationGUID"].ToString());
                }

                _objuser.ProfileImage = (ds.Tables[0].Rows[0]["ProfileImage"] != DBNull.Value ? ds.Tables[0].Rows[0]["ProfileImage"].ToString() : "");
                _objuser.Designation = (ds.Tables[0].Rows[0]["Designation"] != DBNull.Value ? ds.Tables[0].Rows[0]["Designation"].ToString() : "");
                _objuser.InsertedTime = (ds.Tables[0].Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["InsertedTime"]) : DateTime.MinValue);
                _objuser.StartDate = (ds.Tables[0].Rows[0]["StartDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]) : DateTime.MinValue);
                _objuser.EndDate = (ds.Tables[0].Rows[0]["EndDate"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]) : DateTime.MinValue);
                _objuser.UpdatedTime = (ds.Tables[0].Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]) : DateTime.MinValue);
                _objuser.RoleName = (ds.Tables[0].Rows[0]["RoleName"] != DBNull.Value ? ds.Tables[0].Rows[0]["RoleName"].ToString() : "");
                _objuser.RoleIds = (ds.Tables[0].Rows[0]["RoleIds"] != DBNull.Value ? ds.Tables[0].Rows[0]["RoleIds"].ToString() : "");
                _objuser.MobilePhone = (ds.Tables[0].Rows[0]["MobilePhone"] != DBNull.Value ? ds.Tables[0].Rows[0]["MobilePhone"].ToString() : "");
                _objuser.ChapterIds = (ds.Tables[0].Rows[0]["ChapterIds"] != DBNull.Value ? ds.Tables[0].Rows[0]["ChapterIds"].ToString() : "");


            }

            

            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.ChapterUsers objChapterUsers = new Entities.ChapterUsers();

                    objChapterUsers.ChapterUserId = (dr["ChapterUserId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterUserId"].ToString()) : 0);
                    objChapterUsers.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterId"].ToString()) : 0);
                    objChapterUsers.UserId = (dr["UserId"] != DBNull.Value ? Convert.ToInt64(dr["UserId"].ToString()) : 0);

                    lstChapterUsers.Add(objChapterUsers);
                }
            }
            _objuser.lstChapterUsers = lstChapterUsers;

           
            return _objuser;
        }

        //public Entities.Users GetUserDetailsById(Int64 UserId, ref Int64 _qStatus)
        //{
        //    AWSFormBuilder.Entities.Users _objuser = new AWSFormBuilder.Entities.Users();
        //    DataTable dt = new DataTable();

        //    if (UserId != 0)
        //    {
        //        dt = _users.GetUserDetailsById(UserId, ref _qStatus);
        //        if (dt.Rows.Count == 1)
        //        {
        //            _objuser.UserId = Convert.ToInt64(dt.Rows[0]["UserId"]);
        //            _objuser.ChapterId = Convert.ToInt64(dt.Rows[0]["ChapterId"]);
        //            _objuser.UserName = dt.Rows[0]["UserName"].ToString();
        //            _objuser.Email = dt.Rows[0]["Email"].ToString();
        //            _objuser.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
        //            _objuser.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
        //            _objuser.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
        //            _objuser.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);

        //            if (dt.Rows[0]["RegistrationGUID"] != DBNull.Value)
        //            {
        //                _objuser.RegistrationGUID = Guid.Parse(dt.Rows[0]["RegistrationGUID"].ToString());
        //            }

        //            _objuser.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : "");
        //            _objuser.Designation = (dt.Rows[0]["Designation"] != DBNull.Value ? dt.Rows[0]["Designation"].ToString() : "");
        //            _objuser.InsertedTime = (dt.Rows[0]["InsertedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["InsertedTime"]) : DateTime.MinValue);
        //            _objuser.UpdatedTime = (dt.Rows[0]["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]) : DateTime.MinValue);
        //            _objuser.RoleName = (dt.Rows[0]["RoleName"] != DBNull.Value ? dt.Rows[0]["RoleName"].ToString() : "");
        //            _objuser.RoleIds = (dt.Rows[0]["RoleIds"] != DBNull.Value ? dt.Rows[0]["RoleIds"].ToString() : "");
        //            _objuser.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : "");
        //        }
        //    }
        //    return _objuser;
        //}

        #endregion

        #region LogReport

        public Int64 InsertLogReport(List<AWSFormBuilder.Entities.LogReport> lstlogreports)
        {
            Entities.LogReport objLogReport = new Entities.LogReport();
            if (lstlogreports != null && lstlogreports.Count != 0)
            {

                objLogReport.XMLLogCounts = BLL.Common.CreateXMLForObject(lstlogreports);
            }
            Int64 _status = 0;
            if (objLogReport != null)
            {
                _status = _users.InsertLogReport(objLogReport);
            }
            return _status;
        }

        #endregion

        #region RoleBasedAccess

        public Int64 UpdateRoleBasedAccess(AWSFormBuilder.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _users.UpdateRoleBasedAccess(objUser);
            }
            return _status;
        }
        public Int64 UpdateRolesWiseAcces(AWSFormBuilder.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            if (objUser != null)
            {
                _status = _users.UpdateRolesWiseAcces(objUser);
            }
            return _status;
        }

        public Entities.UserRoles GetRoleDetialsById(Int64 UserId, Int64 mid, ref int Status)
        {
            DataTable dt = null;
            Entities.UserRoles objuserroles = new Entities.UserRoles();
            if (UserId != 0)
            {
                dt = _users.GetRoleDetialsById(UserId, mid, ref Status);
                if (dt.Rows.Count == 1)
                {
                    objuserroles.UserRoleId = Convert.ToInt64(dt.Rows[0]["UserRoleId"]);
                    objuserroles.RoleId = Convert.ToInt64(dt.Rows[0]["RoleId"]);
                    objuserroles.IsAdd = (dt.Rows[0]["IsAdd"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsAdd"]) : false);
                    objuserroles.IsEdit = (dt.Rows[0]["IsEdit"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsEdit"]) : false);
                    objuserroles.IsView = (dt.Rows[0]["IsView"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsView"]) : false);
                    objuserroles.IsDelete = (dt.Rows[0]["IsDelete"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsDelete"]) : false);
                    objuserroles.IsExport = (dt.Rows[0]["IsExport"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsExport"]) : false);
                }
            }
            return objuserroles;
        }

        public Int64 RemoveRoleAccess(Int64 UserRoleId, Int64 ParentId)
        {
            Int64 _status = 0;
            if (UserRoleId != 0)
            {
                _status = _users.RemoveRoleAccess(UserRoleId, ParentId);
            }
            return _status;
        }

        #endregion

    }
}
