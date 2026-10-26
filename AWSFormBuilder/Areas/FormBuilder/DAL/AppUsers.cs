using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class AppUsers
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;
        public Int64 InsertAppUser(Entities.AppUsers objAppUsers, ref Int64 AppUserId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@UserId",AppUserId),
                    new SqlParameter("@DeviceId",(objAppUsers.DeviceID != null ? objAppUsers.DeviceID :  (object)DBNull.Value)),
                    new SqlParameter("@AndroidVersion",(objAppUsers.AndroidVersion != null ? objAppUsers.AndroidVersion :  (object)DBNull.Value)),
                    new SqlParameter("@IOSVersion",(objAppUsers.IOSVersion != null ? objAppUsers.IOSVersion :  (object)DBNull.Value)),
                    new SqlParameter("@Comments",(objAppUsers.Comments != null ? objAppUsers.Comments :  (object)DBNull.Value)),
                    new SqlParameter("@IsApproved",(objAppUsers.IsApproved != false ? (object)objAppUsers.IsApproved:DBNull.Value)),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",(objAppUsers.UpdatedBy != null ? objAppUsers.UpdatedBy :  (object)DBNull.Value)),
                    new SqlParameter("@OneSignalDeviceId",(objAppUsers.OneSignalDeviceId != null ? objAppUsers.OneSignalDeviceId :  (object)DBNull.Value)),


                    };

                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 512;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUsersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[0].Value);
                AppUserId = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }






        public Int64 AdminInsertAppUser(Entities.AppUsers objAppUsers)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@UserId",objAppUsers.UserID),
                    new SqlParameter("@Name",(objAppUsers.Name !=null ? (object)objAppUsers.Name:DBNull.Value)),
                    new SqlParameter("@Email",(objAppUsers.Email !=null ? (object)objAppUsers.Email:DBNull.Value)),
                    new SqlParameter("@Phone",(objAppUsers.Phone !=null ? (object)objAppUsers.Phone:DBNull.Value)),
                    new SqlParameter("@DeviceId",(objAppUsers.DeviceID != null ? objAppUsers.DeviceID :  (object)DBNull.Value)),
                    new SqlParameter("@AppVersion",(objAppUsers.AppVersion != null ? objAppUsers.AppVersion :  (object)DBNull.Value)),
                    new SqlParameter("@AppType",(objAppUsers.AppType != null ? objAppUsers.AppType :  (object)DBNull.Value)),
                    new SqlParameter("@OneSignalID",(objAppUsers.OneSignalID != null ? objAppUsers.OneSignalID :  (object)DBNull.Value)),
                    new SqlParameter("@Status",(objAppUsers.Status != false ? (object)objAppUsers.Status:DBNull.Value)),
                    new SqlParameter("@Field1",(objAppUsers.Field1 !=null ? (object)objAppUsers.Field1:DBNull.Value)),
                    new SqlParameter("@Field2",(objAppUsers.Field2 !=null ? (object)objAppUsers.Field2:DBNull.Value)),
                    new SqlParameter("@UpdatedBy",DateTime.UtcNow),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@TempleReligiousEvents",(objAppUsers.TempleReligiousEvents !=null ? (object)objAppUsers.TempleReligiousEvents:DBNull.Value)),
                    new SqlParameter("@CulturalEvent",(objAppUsers.CulturalEvent !=null ? (object)objAppUsers.CulturalEvent:DBNull.Value)),
                    new SqlParameter("@TempleAnnouncement",(objAppUsers.TempleAnnouncement !=null ? (object)objAppUsers.TempleAnnouncement:DBNull.Value)),


                    };

                _sqlP[1].SqlDbType = SqlDbType.NVarChar;
                _sqlP[1].Size = 512;
                _sqlP[1].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUsersInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[0].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        public DataTable SubGetLogReportListByVariable(Int64 LogId,  string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@LogId",LogId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",Total),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("SubLogReportGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable AppusersGetByid(Int64 AppUserId, ref Int32 Status)
        {
            DataTable dt = new DataTable();
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",AppUserId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AppUsersGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable APIMobileAppInfoGetList(ref Int32 status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("APIMobileAppInfoGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable UsersGetById(ref String DeviceID, ref int status)
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






        public Int64 InsertAppUserNotifications(Entities.AppUsers objAppUsers, ref string imageurl)
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
                     new SqlParameter("@imageurl",imageurl),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[10].SqlDbType = SqlDbType.NVarChar;
                _sqlP[10].Size = 512;
                _sqlP[10].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[11].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppUserNotificationsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[11].Value);
                imageurl = _sqlP[10].Value.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        public DataTable GetNotificationListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
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
                dt = _dbAccess.GetDataTable("NotificationGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
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





        public DataTable GetviewById(Int64 NotificationID, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@NotificationID", NotificationID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AppViewGetById", ref _sqlP);
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



        public Int64 AppNotificationDelete(Int64 NotificationID)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@NotificationID", NotificationID),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppNotificationDelete", ref _sqlP);
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

        public DataTable UsersList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                     new SqlParameter("@QStatus",0)



                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("Appusersgetlistall", ref _sqlP);
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
