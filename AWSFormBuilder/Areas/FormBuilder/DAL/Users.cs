using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class Users
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;
        public Int64 LogSubReportInsert(AWSFormBuilder.Entities.Logdetails objLogReport)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@LogSubReportId",objLogReport.LogSubReportId),
                    new SqlParameter("@LogId",objLogReport.LogId),
                    new SqlParameter("@LogTitle",(objLogReport.LogTitle == null ?DBNull.Value:(object)objLogReport.LogTitle)),
                    new SqlParameter("@LogDescription",(objLogReport.LogDescription == null ?DBNull.Value:(object)objLogReport.LogDescription)),
                    new SqlParameter("@LogDate",(objLogReport.LogDate == DateTime.MinValue ?DBNull.Value:(object)objLogReport.LogDate)),
                    new SqlParameter("@InsertedDate",objLogReport.InsertedDate),
                    new SqlParameter("@InsertedBy",objLogReport.InsertedBy),
                    new SqlParameter("@UpdatedDate",objLogReport.UpdatedDate),
                    new SqlParameter("@UpdatedBy",objLogReport.UpdatedBy),

                    new SqlParameter("@QStatus",0),
                   // new SqlParameter("@XMLLogCounts",(objLogReport.XMLLogCounts == null ?DBNull.Value:(object)objLogReport.XMLLogCounts))
                    };

                _sqlP[9].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEInsertLogSubReport", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[9].Value);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 LogReportInsert(AWSFormBuilder.Entities.ApplicationLogs objLogReport, ref Int64 LogId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@LogId",objLogReport.LogId),
                    new SqlParameter("@LogTitle",(objLogReport.LogTitle == null ?DBNull.Value:(object)objLogReport.LogTitle)),
                    new SqlParameter("@LogDescription",(objLogReport.LogDescription == null ?DBNull.Value:(object)objLogReport.LogDescription)),
                    new SqlParameter("@LogDate",(objLogReport.LogDate == DateTime.MinValue ?DBNull.Value:(object)objLogReport.LogDate)),
                    new SqlParameter("@InsertedDate",objLogReport.InsertedDate),
                    new SqlParameter("@InsertedBy",objLogReport.InsertedBy),
                    new SqlParameter("@UpdatedDate",objLogReport.UpdatedDate),
                    new SqlParameter("@UpdatedBy",objLogReport.UpdatedBy),

                    new SqlParameter("@QStatus",0),
                   // new SqlParameter("@XMLLogCounts",(objLogReport.XMLLogCounts == null ?DBNull.Value:(object)objLogReport.XMLLogCounts))
                    };

                _sqlP[0].SqlDbType = SqlDbType.NVarChar;
                _sqlP[0].Size = 512;
                _sqlP[0].Direction = System.Data.ParameterDirection.InputOutput;




                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FELogReportInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[8].Value);


                LogId = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertUserProfile(AWSFormBuilder.Entities.Users objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                    new SqlParameter("@ChapterId",(objUser.ChapterId == 0 ?DBNull.Value:(object)objUser.ChapterId)),
                    new SqlParameter("@UserName",(objUser.UserName == null ?DBNull.Value:(object)objUser.UserName.Trim())),
                    new SqlParameter("@ChapterIds",(objUser.ChapterIds == null ?DBNull.Value:(object)objUser.ChapterIds.Trim())),
                    new SqlParameter("@RoleName",(objUser.RoleName == null ?DBNull.Value:(object)objUser.RoleName.Trim())),
                    new SqlParameter("@Email",(objUser.Email == null ?DBNull.Value:(object)objUser.Email.Trim())),
                    new SqlParameter("@Designation",(objUser.Designation == null ?DBNull.Value:(object)objUser.Designation)),
                    new SqlParameter("@MobilePhone",(objUser.MobilePhone == null ?DBNull.Value:(object)objUser.MobilePhone)),
                    new SqlParameter("@IsApproved",objUser.IsApproved),
                    new SqlParameter("@IsLockedOut",objUser.IsLockedOut),
                    new SqlParameter("@IsActivated",objUser.IsActivated),
                    new SqlParameter("@DateActivated",DBNull.Value),
                    new SqlParameter("@RegistrationGUID",objUser.RegistrationGUID),
                    new SqlParameter("@FailedPasswordAttemptCount",objUser.FailedPasswordAttemptCount),
                    new SqlParameter("@LastPasswordChangedDate",DBNull.Value),
                    new SqlParameter("@LastLoginDate",DBNull.Value),
                    new SqlParameter("@InsertedBy",objUser.InsertedBy),
                    new SqlParameter("@InsertedTime",objUser.InsertedTime),
                    new SqlParameter("@UpdatedBy",objUser.UpdatedBy),
                    new SqlParameter("@UpdatedTime",objUser.UpdatedTime),
                  
               
                   new SqlParameter("@StartDate",(objUser.StartDate == DateTime.MinValue ?DBNull.Value:(object)objUser.StartDate)),
                   new SqlParameter("@EndDate",(objUser.EndDate == DateTime.MinValue ?DBNull.Value:(object)objUser.EndDate)),

                    new SqlParameter("@MemberId",(objUser.MemberId == 0 ?DBNull.Value:(object)objUser.MemberId)),
                    new SqlParameter("@RoleId",(objUser.RoleId == 0 ?DBNull.Value:(object)objUser.RoleId)),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[24].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersProfileInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[24].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserAccess(AWSFormBuilder.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                     new SqlParameter("@RoleIds",objUser.RoleIds),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateUserAccess", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetUserListByVariable(string RoleName,Int64 UserId, string RoleId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@RoleId",RoleId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@RoleName",RoleName),
                    new SqlParameter("@Total",Total)
                };

                _sqlP[7].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[7].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 ChangePassword(string UserId, string Password)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersChangePassword", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 ChangePassword(Int64 UserId, string Password)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersChangePasswordnew", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetUserRolesList(string keyword, ref int Total)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Qstatus",0),
                     new SqlParameter("@keyword",keyword)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("UserRolesGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable UserRolesSubDropDownGetList(string keyword, ref int Total)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Qstatus",0),
                    new SqlParameter("@keyword",keyword)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("UserRolesSubDropDownGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable GetUserRolesListById(Int64 UserId, ref int Total)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@Qstatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("UserRolesGetById", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public Int64 DeleteUser(Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 DeleteAllUser()
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersDeleteAll", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetUserByEmail(string Email, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAdminUsersGetByEmail(string Email, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AdminUsersGetByEmail", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetUserByPhoneNo(string MobilePhone, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MobilePhone",MobilePhone),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetByPhoneNo", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //public DataTable GetUserDetailsById(Int64 UserId, ref Int64 _QStatus)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        _sqlP = new[] 
        //        {
        //            new SqlParameter("@UserId", UserId),
        //            new SqlParameter("@QStatus", _QStatus)
        //        };

        //        _sqlP[1].Direction = System.Data.ParameterDirection.Output;
        //        dt = _dbAccess.GetDataTable("UsersGetById", ref _sqlP);
        //        _QStatus = Convert.ToInt64(_sqlP[1].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dt;
        //}
        public DataSet GetUserDetailsById(Int64 UserId, ref int Status)
        {
            DataSet dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataSet("UsersGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetUserByUserName(string UserName, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserName",UserName),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersGetByUserName", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetPassword(string _userid, ref int _QStatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] {
                new SqlParameter("@UserId",_userid),
                new SqlParameter("@QStatus",_QStatus)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;

                dt = _dbAccess.GetDataTable("UsersGetPassword", ref _sqlP);
                _QStatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UnlockUser(Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersUnlock", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserStatus(Int64 UserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 UserId, ref string ProfileImage)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@ProfileImage",ProfileImage),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 256;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UsersProfileImage", ref _sqlP);
                ProfileImage = _sqlP[1].Value.ToString();
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateUser(AWSFormBuilder.Entities.Users objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                    new SqlParameter("@UserName",(objUser.UserName==null?(object)DBNull.Value:objUser.UserName)),
                    new SqlParameter("@ChapterIds",(objUser.ChapterIds==null?(object)DBNull.Value:objUser.ChapterIds)),
                    new SqlParameter("@Email",(objUser.Email == null ?DBNull.Value:(object)objUser.Email.Trim())),
                    new SqlParameter("@Designation",(objUser.Designation == null ?DBNull.Value:(object)objUser.Designation)),
                    new SqlParameter("@MobilePhone",(objUser.MobilePhone == null ?DBNull.Value:(object)objUser.MobilePhone)),
                    new SqlParameter("@UpdatedBy",objUser.UpdatedBy),
                    new SqlParameter("@UpdatedTime",objUser.UpdatedTime),
                         new SqlParameter("@StartDate",(objUser.StartDate == DateTime.MinValue ?DBNull.Value:(object)objUser.StartDate)),
                   new SqlParameter("@EndDate",(objUser.EndDate == DateTime.MinValue ?DBNull.Value:(object)objUser.EndDate)),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateUser", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateRegistrationGUID(Int64 UserId, string IsActivated, Guid RegistrationGUID)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@IsActivated",IsActivated),
                    new SqlParameter("@DateActivated",DateTime.UtcNow),
                    new SqlParameter("@RegistrationGUID",RegistrationGUID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UserUpdateRegistrationGUID", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #region LogReport

        public Int64 InsertLogReport(AWSFormBuilder.Entities.LogReport objLogReport)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {

                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@XMLLogCounts",(objLogReport.XMLLogCounts == null ?DBNull.Value:(object)objLogReport.XMLLogCounts))
                    };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEInsertLogReport", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region RoleBasedAssign

        public Int64 UpdateRoleBasedAccess(AWSFormBuilder.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                    new SqlParameter("@UserRoleId",objUser.UserRoleId),
                    new SqlParameter("@RoleId",objUser.RoleId),
                    new SqlParameter("@IsAdd",(objUser.IsAdd == false ?DBNull.Value:(object)objUser.IsAdd)),
                    new SqlParameter("@IsEdit",(objUser.IsEdit == false ?DBNull.Value:(object)objUser.IsEdit)),
                    new SqlParameter("@IsView",(objUser.IsView == false ?DBNull.Value:(object)objUser.IsView)),
                    new SqlParameter("@IsDelete",(objUser.IsDelete == false ?DBNull.Value:(object)objUser.IsDelete)),
                    new SqlParameter("@IsExport",(objUser.IsExport == false ?DBNull.Value:(object)objUser.IsExport)),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@ParentId",(objUser.ParentId == 0 ?DBNull.Value:(object)objUser.ParentId)),
                    };
                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateRoleBasedAccess", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public Int64 UpdateRolesWiseAcces(AWSFormBuilder.Entities.UserRoles objUser)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserId",objUser.UserId),
                    new SqlParameter("@RoleWiseAccesId",objUser.RoleWiseAccesId),
                    new SqlParameter("@RoleId",objUser.RoleId),
                    new SqlParameter("@IsAdd",(objUser.IsAdd == false ?DBNull.Value:(object)objUser.IsAdd)),
                    new SqlParameter("@IsEdit",(objUser.IsEdit == false ?DBNull.Value:(object)objUser.IsEdit)),
                    new SqlParameter("@IsView",(objUser.IsView == false ?DBNull.Value:(object)objUser.IsView)),
                    new SqlParameter("@IsDelete",(objUser.IsDelete == false ?DBNull.Value:(object)objUser.IsDelete)),
                    new SqlParameter("@IsExport",(objUser.IsExport == false ?DBNull.Value:(object)objUser.IsExport)),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@ParentId",(objUser.ParentId == 0 ?DBNull.Value:(object)objUser.ParentId)),
                    };
                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateRolesWiseAcces", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetRoleDetialsById(Int64 UserId, Int64 mid, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@mid",mid),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AdminRoleBasedMenuGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 RemoveRoleAccess(Int64 UserRoleId, Int64 ParentId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@UserRoleId",UserRoleId),
                    new SqlParameter("@ParentId",ParentId)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("RemoveRoleAccess", ref _sqlP);
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
