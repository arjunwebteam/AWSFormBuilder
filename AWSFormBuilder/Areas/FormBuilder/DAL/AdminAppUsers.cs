using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class AdminAppUsers
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Admin

        public DataTable GetAppUsersList(ref int status, ref String AppuserIds)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@AppuserIds",AppuserIds),

                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersDeviceGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 InsertAppUsers(Entities.AdminAppUsers objAppUsers, ref Int64 UserID)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@UserID",UserID),
                    new SqlParameter("@Name",(objAppUsers.Name == null ?DBNull.Value:(object)objAppUsers.Name)),
                    new SqlParameter("@Email",(objAppUsers.Email == null ?DBNull.Value:(object)objAppUsers.Email)),
                    new SqlParameter("@Phone",(objAppUsers.Phone == null ?DBNull.Value:(object)objAppUsers.Phone)),
                    new SqlParameter("@DeviceID",(objAppUsers.DeviceID == null ?DBNull.Value:(object)objAppUsers.DeviceID)),
                    new SqlParameter("@AppVersion",(objAppUsers.AppVersion == null ?DBNull.Value:(object)objAppUsers.AppVersion)),
                    new SqlParameter("@AppType",(objAppUsers.AppType == null ?DBNull.Value:(object)objAppUsers.AppType)),
                    new SqlParameter("@OneSignalID",(objAppUsers.OneSignalID == null ?DBNull.Value:(object)objAppUsers.OneSignalID)),
                    new SqlParameter("@Status",(objAppUsers.Status == false ?DBNull.Value:(object)objAppUsers.Status)),
                    new SqlParameter("@Field1",(objAppUsers.Field1 == null ?DBNull.Value:(object)objAppUsers.Field1)),
                    new SqlParameter("@Field2",(objAppUsers.Field2 == null ?DBNull.Value:(object)objAppUsers.Field2)),
                    new SqlParameter("@UpdatedBy",(objAppUsers.Email == null ?DBNull.Value:(object)objAppUsers.Email)),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@TempleReligiousEvents",(objAppUsers.TempleReligiousEvents == null ?DBNull.Value:(object)objAppUsers.TempleReligiousEvents)),
                    new SqlParameter("@CulturalEvent",(objAppUsers.CulturalEvent == null ?DBNull.Value:(object)objAppUsers.CulturalEvent)),
                    new SqlParameter("@TempleAnnouncement",(objAppUsers.TempleAnnouncement == null ?DBNull.Value:(object)objAppUsers.TempleAnnouncement)),
                    new SqlParameter("@QStatus",0)
                    };



                _sqlP[0].SqlDbType = SqlDbType.NVarChar;
                _sqlP[0].Size = 512;
                _sqlP[0].Direction = System.Data.ParameterDirection.InputOutput;




                _sqlP[16].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUsersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[16].Value);

                UserID = Convert.ToInt64(_sqlP[0].Value);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }





        public Int64 InsertAppUserNotifications(Entities.AdminAppUsers objAppUsers)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@NotificationID",objAppUsers.NotificationID),
                    new SqlParameter("@Title",(objAppUsers.Title == null ?DBNull.Value:(object)objAppUsers.Title)),
                    new SqlParameter("@Body",(objAppUsers.Body == null ?DBNull.Value:(object)objAppUsers.Body)),
                    new SqlParameter("@ActivityTobeopen",(objAppUsers.ActivityTobeopen == null ?DBNull.Value:(object)objAppUsers.ActivityTobeopen)),
                    new SqlParameter("@Values",(objAppUsers.Values == null ?DBNull.Value:(object)objAppUsers.Values)),
                    new SqlParameter("@DeviceID",(objAppUsers.DeviceID  == null ?DBNull.Value:(object)objAppUsers.DeviceID )),
                    //new SqlParameter("@Field1",(objAppUsers.Field1 == null ?DBNull.Value:(object)objAppUsers.Field1)),
                    new SqlParameter("@NotificationUserId",objAppUsers.NotificationUserId),
                    new SqlParameter("@AppuserId",(objAppUsers.AppuserId == null ?DBNull.Value:(object)objAppUsers.AppuserId)),
                    new SqlParameter("@UpdatedBy",objAppUsers.Title),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUserNotificationsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetAppUsersListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("AppUsersGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetAppUsersById(Int64 UserID, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AppUsersGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 AppUsersDelete(Int64 UserID)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUsersDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateAppUserstatus(Int64 UserID)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserID",UserID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUsersUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable AppUsersGetById(ref String DeviceID, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@DeviceID", DeviceID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("GetAppUsersById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }





        public DataTable UserNotifiation(ref String DeviceID, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@DeviceID", DeviceID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AppUsersNotification", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public DataTable UserList(ref String DeviceID, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@DeviceID", DeviceID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("UsersAppGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable UsersList(ref int status, ref string category)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                     new SqlParameter("@QStatus",0),
                     new SqlParameter("@category",category)


                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("SwitchStateList", ref _sqlP);
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
