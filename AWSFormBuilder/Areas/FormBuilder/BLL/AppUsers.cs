using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class AppUsers
    {
        DAL.AppUsers _AppUsers = new DAL.AppUsers();
        DAL.AppInfo _AppInfo = new DAL.AppInfo();


        public Int64 InsertAppUsers(Entities.AppUsers objAppUsers, ref Int64 AppUserId)
        {
            Int64 _status = 0;
            if (objAppUsers != null)
            {
                _status = _AppUsers.InsertAppUser(objAppUsers, ref AppUserId);

            }
            return _status;
        }




        public Int64 AdminInsertAppUser(Entities.AppUsers objAppUsers)
        {
            Int64 _status = 0;
            if (objAppUsers != null)
            {
                _status = _AppUsers.AdminInsertAppUser(objAppUsers);

            }
            return _status;
        }





        public Entities.AppUsers AppusersGetByid(Int64 AppUserId, ref int status)
        {
            DataTable dt = _AppUsers.AppusersGetByid(AppUserId, ref status);
            Entities.AppUsers objAppUsers = new Entities.AppUsers();

            if (dt.Rows.Count == 1)
            {
                objAppUsers.DeviceID = (dt.Rows[0]["DeviceID"] != DBNull.Value ? dt.Rows[0]["DeviceID"].ToString() : "");
                //objAppUsers.OneSignalDeviceId = (dt.Rows[0]["OneSignalDeviceId"] != DBNull.Value ? dt.Rows[0]["OneSignalDeviceId"].ToString() : "");
                //objAppUsers.AndroidVersion = (dt.Rows[0]["AndroidVersion"] != DBNull.Value ? dt.Rows[0]["AndroidVersion"].ToString() : "");
                //objAppUsers.IOSVersion = (dt.Rows[0]["IOSVersion"] != DBNull.Value ? dt.Rows[0]["IOSVersion"].ToString() : "");

            }

            return objAppUsers;
        }




        public List<AWSFormBuilder.Entities.LogSubReport> SubGetLogReportListByVariable(Int64 LogId ,string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.LogSubReport> lstLogReport = new List<AWSFormBuilder.Entities.LogSubReport>();
            DataTable dt = _AppUsers.SubGetLogReportListByVariable(LogId,StartDate, EndDate, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _AppUsers.SubGetLogReportListByVariable(LogId,StartDate, EndDate, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.LogSubReport objLogReport = new AWSFormBuilder.Entities.LogSubReport();

                    objLogReport.RId = Convert.ToInt64(dr["RId"].ToString());
                    objLogReport.LogId = Convert.ToInt64(dr["LogId"].ToString());
                    objLogReport.LogTitle = (dr["LogTitle"] != DBNull.Value ? dr["LogTitle"].ToString() : "");
                    objLogReport.LogDescription = (dr["LogDescription"] != DBNull.Value ? dr["LogDescription"].ToString() : "");
                    objLogReport.LogDate = (dr["LogDate"] != DBNull.Value ? Convert.ToDateTime(dr["LogDate"]) : DateTime.MinValue);
                    objLogReport.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : "");
                    objLogReport.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : "");
                    objLogReport.InsertedDate = (dr["InsertedDate"] != DBNull.Value ? Convert.ToDateTime(dr["InsertedDate"]) : DateTime.MinValue);


                    lstLogReport.Add(objLogReport);
                }
            }
            return lstLogReport;
        }


        #region Admin

        #region Methods



        public Int64 InsertAppUserNotifications(Entities.AppUsers objAppUsers, ref string imageurl)
        {
            Int64 _status = 0;
            if (objAppUsers != null)
            {
                _status = _AppUsers.InsertAppUserNotifications(objAppUsers, ref imageurl);

            }
            return _status;
        }


        public Int64 AppUsersDelete(Int64 UserID)
        {
            Int64 _status = 0;
            _status = _AppUsers.AppUsersDelete(UserID);
            return _status;
        }


        public Int64 AppNotificationDelete(Int64 NotificationID)
        {
            Int64 _status = 0;
            _status = _AppUsers.AppNotificationDelete(NotificationID);
            return _status;
        }



        public Int64 UpdateAppUserstatus(Int64 UserID)
        {
            Int64 _status = 0;
            _status = _AppUsers.UpdateAppUserstatus(UserID);
            return _status;
        }



        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.AppUsers> GetAppUsersList(ref int status, ref String AppuserIds)
        {
            List<AWSFormBuilder.Entities.AppUsers> lstAppUsers = new List<Entities.AppUsers>();
            DataTable dt = _AppUsers.GetAppUsersList(ref status, ref AppuserIds);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.AppUsers objlstAppUsers = new AWSFormBuilder.Entities.AppUsers();

                    objlstAppUsers.UserID = Convert.ToInt32(dr["UserID"].ToString());
                    objlstAppUsers.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objlstAppUsers.Phone = (dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : null);
                    objlstAppUsers.DeviceID = (dr["DeviceID"] != DBNull.Value ? dr["DeviceID"].ToString() : null);
                    objlstAppUsers.AppVersion = (dr["AppVersion"] != DBNull.Value ? dr["AppVersion"].ToString() : null);
                    objlstAppUsers.AppType = (dr["AppType"] != DBNull.Value ? dr["AppType"].ToString() : null);
                    objlstAppUsers.OneSignalID = (dr["OneSignalID"] != DBNull.Value ? dr["OneSignalID"].ToString() : null);


                    lstAppUsers.Add(objlstAppUsers);
                }

            }
            return lstAppUsers;
        }

        public AWSFormBuilder.Entities.AppUsers GetAppUsersById(Int64 UserID, ref int status)
        {
            AWSFormBuilder.Entities.AppUsers objAppUsers = new AWSFormBuilder.Entities.AppUsers();
            DataTable dt = new DataTable();
            if (UserID != 0)
            {
                dt = _AppUsers.GetAppUsersById(UserID, ref status);
                if (dt.Rows.Count == 1)
                {
                    objAppUsers.UserID = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                    objAppUsers.Name = (dt.Rows[0]["Name"] != DBNull.Value ? dt.Rows[0]["Name"].ToString() : null);
                    objAppUsers.Email = (dt.Rows[0]["Email"] != DBNull.Value ? dt.Rows[0]["Email"].ToString() : null);
                    objAppUsers.Phone = (dt.Rows[0]["Phone"] != DBNull.Value ? dt.Rows[0]["Phone"].ToString() : null);
                    objAppUsers.DeviceID = (dt.Rows[0]["DeviceID"] != DBNull.Value ? dt.Rows[0]["DeviceID"].ToString() : null);
                    objAppUsers.AppVersion = (dt.Rows[0]["AppVersion"] != DBNull.Value ? dt.Rows[0]["AppVersion"].ToString() : null);
                    objAppUsers.AppType = (dt.Rows[0]["AppType"] != DBNull.Value ? dt.Rows[0]["AppType"].ToString() : null);
                    objAppUsers.OneSignalID = (dt.Rows[0]["OneSignalID"] != DBNull.Value ? dt.Rows[0]["OneSignalID"].ToString() : null);
                    objAppUsers.Field1 = (dt.Rows[0]["Field1"] != DBNull.Value ? dt.Rows[0]["Field1"].ToString() : null);
                    objAppUsers.Field2 = (dt.Rows[0]["Field2"] != DBNull.Value ? dt.Rows[0]["Field2"].ToString() : null);
                    objAppUsers.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                    objAppUsers.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

                }
            }
            return objAppUsers;
        }

        public List<AWSFormBuilder.Entities.AppUsers> GetNotificationListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.AppUsers> lstAppUsers = new List<AWSFormBuilder.Entities.AppUsers>();
            DataTable dt = _AppUsers.GetNotificationListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _AppUsers.GetAppUsersListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    AWSFormBuilder.Entities.AppUsers objlstAppUsers = new AWSFormBuilder.Entities.AppUsers();
                    objlstAppUsers.RId = Convert.ToInt64(dr["RId"].ToString());
                    objlstAppUsers.NotificationID = Convert.ToInt32(dr["NotificationID"].ToString());
                    objlstAppUsers.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objlstAppUsers.Body = (dr["Body"] != DBNull.Value ? dr["Body"].ToString() : null);
                    objlstAppUsers.ActivityTobeopen = (dr["ActivityTobeopen"] != DBNull.Value ? dr["ActivityTobeopen"].ToString() : null);
                    objlstAppUsers.Values = (dr["Values"] != DBNull.Value ? dr["Values"].ToString() : null);
                    objlstAppUsers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                    objlstAppUsers.imageurl = (dr["imageurl"] != DBNull.Value ? dr["imageurl"].ToString() : null);
                    objlstAppUsers.TotalCount = Convert.ToInt32(dr["TotalCount"].ToString());


                    lstAppUsers.Add(objlstAppUsers);
                }


            }
            return lstAppUsers;
        }


        public List<AWSFormBuilder.Entities.AppUsers> GetAppUsersListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.AppUsers> lstAppUsers = new List<AWSFormBuilder.Entities.AppUsers>();
            DataTable dt = _AppUsers.GetAppUsersListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _AppUsers.GetAppUsersListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.AppUsers objlstAppUsers = new AWSFormBuilder.Entities.AppUsers();

                    objlstAppUsers.RId = Convert.ToInt64(dr["RId"].ToString());
                    objlstAppUsers.UserID = Convert.ToInt32(dr["UserID"].ToString());
                    objlstAppUsers.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objlstAppUsers.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    objlstAppUsers.Phone = (dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : null);
                    objlstAppUsers.DeviceID = (dr["DeviceID"] != DBNull.Value ? dr["DeviceID"].ToString() : null);
                    objlstAppUsers.AppVersion = (dr["AppVersion"] != DBNull.Value ? dr["AppVersion"].ToString() : null);
                    objlstAppUsers.AppType = (dr["AppType"] != DBNull.Value ? dr["AppType"].ToString() : null);
                    objlstAppUsers.OneSignalID = (dr["OneSignalID"] != DBNull.Value ? dr["OneSignalID"].ToString() : null);
                    objlstAppUsers.Status = (dr["Status"] != DBNull.Value ? Convert.ToBoolean(dr["Status"].ToString()) : false);
                    objlstAppUsers.UpdatedBy = dr["UpdatedBy"].ToString();
                    objlstAppUsers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstAppUsers.Add(objlstAppUsers);
                }
            }
            return lstAppUsers;
        }



        public AWSFormBuilder.Entities.AppUsers ViewById(Int64 NotificationID, ref int status)
        {
            AWSFormBuilder.Entities.AppUsers objAppUsers = new AWSFormBuilder.Entities.AppUsers();
            DataTable dt = new DataTable();
            if (NotificationID != 0)
            {
                dt = _AppUsers.GetviewById(NotificationID, ref status);
                if (dt.Rows.Count == 1)
                {




                    objAppUsers.NotificationID = Convert.ToInt32(dt.Rows[0]["NotificationID"].ToString());
                    objAppUsers.Title = (dt.Rows[0]["Title"] != DBNull.Value ? dt.Rows[0]["Title"].ToString() : null);
                    objAppUsers.Body = (dt.Rows[0]["Body"] != DBNull.Value ? dt.Rows[0]["Body"].ToString() : null);
                    objAppUsers.ActivityTobeopen = (dt.Rows[0]["ActivityTobeopen"] != DBNull.Value ? dt.Rows[0]["ActivityTobeopen"].ToString() : null);
                    objAppUsers.Values = (dt.Rows[0]["Values"] != DBNull.Value ? dt.Rows[0]["Values"].ToString() : null);
                    objAppUsers.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());
                    objAppUsers.imageurl = (dt.Rows[0]["imageurl"] != DBNull.Value ? dt.Rows[0]["imageurl"].ToString() : null);


                }
            }
            return objAppUsers;
        }










        public AWSFormBuilder.Entities.AppUsers AppUsersGetById(ref String DeviceID, ref int status)
        {
            AWSFormBuilder.Entities.AppUsers objAppUsers = new AWSFormBuilder.Entities.AppUsers();
            DataTable dt = new DataTable();

            dt = _AppUsers.UsersGetById(ref DeviceID, ref status);
            if (dt.Rows.Count == 1)
            {
                objAppUsers.UserID = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                objAppUsers.Name = (dt.Rows[0]["Name"] != DBNull.Value ? dt.Rows[0]["Name"].ToString() : null);
                objAppUsers.Email = (dt.Rows[0]["Email"] != DBNull.Value ? dt.Rows[0]["Email"].ToString() : null);
                objAppUsers.Phone = (dt.Rows[0]["Phone"] != DBNull.Value ? dt.Rows[0]["Phone"].ToString() : null);
                objAppUsers.DeviceID = (dt.Rows[0]["DeviceID"] != DBNull.Value ? dt.Rows[0]["DeviceID"].ToString() : null);
                objAppUsers.AppVersion = (dt.Rows[0]["AppVersion"] != DBNull.Value ? dt.Rows[0]["AppVersion"].ToString() : null);
                objAppUsers.AppType = (dt.Rows[0]["AppType"] != DBNull.Value ? dt.Rows[0]["AppType"].ToString() : null);
                objAppUsers.OneSignalID = (dt.Rows[0]["OneSignalID"] != DBNull.Value ? dt.Rows[0]["OneSignalID"].ToString() : null);
                objAppUsers.Field1 = (dt.Rows[0]["Field1"] != DBNull.Value ? dt.Rows[0]["Field1"].ToString() : null);
                objAppUsers.Field2 = (dt.Rows[0]["Field2"] != DBNull.Value ? dt.Rows[0]["Field2"].ToString() : null);
                objAppUsers.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objAppUsers.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

            }

            return objAppUsers;
        }

        public List<AWSFormBuilder.Entities.MobileAppInfo> APIMobileAppInfoGetList(ref Int32 status)
        {
            List<AWSFormBuilder.Entities.MobileAppInfo> lstMobileAppInfo = new List<AWSFormBuilder.Entities.MobileAppInfo>();
            DataTable dt = _AppUsers.APIMobileAppInfoGetList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.MobileAppInfo objMobileAppInfo = new AWSFormBuilder.Entities.MobileAppInfo();




                    objMobileAppInfo.AppsettingId = (dr["AppsettingId"] != DBNull.Value ? Convert.ToInt64(dr["AppsettingId"]) : 0);
                    objMobileAppInfo.SplashMiddle = (dr["SplashMiddle"] != DBNull.Value ? dr["SplashMiddle"].ToString() : null);
                    objMobileAppInfo.SplashBottom = (dr["SplashBottom"] != DBNull.Value ? dr["SplashBottom"].ToString() : null);
                    objMobileAppInfo.HomeTopHeader = (dr["HomeTopHeader"] != DBNull.Value ? dr["HomeTopHeader"].ToString() : null);
                    objMobileAppInfo.Customloader = (dr["Customloader"] != DBNull.Value ? dr["Customloader"].ToString() : null);
                    objMobileAppInfo.IOSApp = (dr["IOSApp"] != DBNull.Value ? dr["IOSApp"].ToString() : null);
                    objMobileAppInfo.Androidapp = (dr["Androidapp"] != DBNull.Value ? dr["Androidapp"].ToString() : null);
                    objMobileAppInfo.AppAndroidVersion = (dr["AppAndroidVersion"] != DBNull.Value ? dr["AppAndroidVersion"].ToString() : null);
                    objMobileAppInfo.OtherclasssHeader = (dr["OtherclasssHeader"] != DBNull.Value ? dr["OtherclasssHeader"].ToString() : null);
                    objMobileAppInfo.NotificationAppId = (dr["NotificationAppId"] != DBNull.Value ? dr["NotificationAppId"].ToString() : null);
                    objMobileAppInfo.ServerKey = (dr["ServerKey"] != DBNull.Value ? dr["ServerKey"].ToString() : null);
                    objMobileAppInfo.Androidchannelid = (dr["Androidchannelid"] != DBNull.Value ? dr["Androidchannelid"].ToString() : null);

                    objMobileAppInfo.Iosversion = (dr["Iosversion"] != DBNull.Value ? dt.Rows[0]["Iosversion"].ToString() : null);








                    lstMobileAppInfo.Add(objMobileAppInfo);
                }
            }
            return lstMobileAppInfo;
        }


        public List<AWSFormBuilder.Entities.AppUsers> UserNotication(ref String DeviceID, ref int status)
        {

            List<AWSFormBuilder.Entities.AppUsers> lstAppUsers = new List<Entities.AppUsers>();
            DataTable dt = _AppUsers.UserNotifiation(ref DeviceID, ref status);


            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.AppUsers objlstAppUsers = new AWSFormBuilder.Entities.AppUsers();
                    objlstAppUsers.NotificationID = Convert.ToInt32(dr["NotificationID"].ToString());
                    objlstAppUsers.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objlstAppUsers.Body = (dr["Body"] != DBNull.Value ? dr["Body"].ToString() : null);
                    objlstAppUsers.ActivityTobeopen = (dr["ActivityTobeopen"] != DBNull.Value ? dr["ActivityTobeopen"].ToString() : null);
                    objlstAppUsers.Values = (dr["Values"] != DBNull.Value ? dr["Values"].ToString() : null);
                    objlstAppUsers.NotificationUserId = Convert.ToInt32(dr["NotificationUserId"].ToString());
                    objlstAppUsers.DeviceID = (dr["DeviceID"] != DBNull.Value ? dr["DeviceID"].ToString() : null);
                    objlstAppUsers.AppuserId = (dr["AppuserId"] != DBNull.Value ? dr["AppuserId"].ToString() : null);
                    objlstAppUsers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                    objlstAppUsers.imageurl = (dr["imageurl"] != DBNull.Value ? dr["imageurl"].ToString() : null);

                    lstAppUsers.Add(objlstAppUsers);
                }
            }

            return lstAppUsers;
        }



        public AWSFormBuilder.Entities.AppUsers Userlist(ref String DeviceID, ref int status)
        {
            AWSFormBuilder.Entities.AppUsers objAppUsers = new AWSFormBuilder.Entities.AppUsers();
            DataTable dt = new DataTable();

            dt = _AppUsers.UserList(ref DeviceID, ref status);
            if (dt.Rows.Count == 1)
            {
                objAppUsers.UserID = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                objAppUsers.Name = (dt.Rows[0]["Name"] != DBNull.Value ? dt.Rows[0]["Name"].ToString() : null);
                objAppUsers.Email = (dt.Rows[0]["Email"] != DBNull.Value ? dt.Rows[0]["Email"].ToString() : null);
                objAppUsers.Phone = (dt.Rows[0]["Phone"] != DBNull.Value ? dt.Rows[0]["Phone"].ToString() : null);
                objAppUsers.DeviceID = (dt.Rows[0]["DeviceID"] != DBNull.Value ? dt.Rows[0]["DeviceID"].ToString() : null);
                objAppUsers.AppVersion = (dt.Rows[0]["AppVersion"] != DBNull.Value ? dt.Rows[0]["AppVersion"].ToString() : null);
                objAppUsers.AppType = (dt.Rows[0]["AppType"] != DBNull.Value ? dt.Rows[0]["AppType"].ToString() : null);
                objAppUsers.OneSignalID = (dt.Rows[0]["OneSignalID"] != DBNull.Value ? dt.Rows[0]["OneSignalID"].ToString() : null);
                objAppUsers.Status = (dt.Rows[0]["Status"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["Status"].ToString()) : false);
                objAppUsers.Name = (dt.Rows[0]["Name"] != DBNull.Value ? dt.Rows[0]["Name"].ToString() : null);
                objAppUsers.Field1 = (dt.Rows[0]["Field1"] != DBNull.Value ? dt.Rows[0]["Field1"].ToString() : null);
                objAppUsers.TempleReligiousEvents = (dt.Rows[0]["TempleReligiousEvents"] != DBNull.Value ? dt.Rows[0]["TempleReligiousEvents"].ToString() : null);
                objAppUsers.CulturalEvent = (dt.Rows[0]["CulturalEvent"] != DBNull.Value ? dt.Rows[0]["CulturalEvent"].ToString() : null);
                objAppUsers.TempleAnnouncement = (dt.Rows[0]["TempleAnnouncement"] != DBNull.Value ? dt.Rows[0]["TempleAnnouncement"].ToString() : null);
                objAppUsers.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                objAppUsers.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"].ToString());

            }

            return objAppUsers;
        }


        public List<AWSFormBuilder.Entities.AppUsers> UsersList(ref int status)
        {
            List<AWSFormBuilder.Entities.AppUsers> lstAppUsers = new List<Entities.AppUsers>();
            DataTable dt = _AppUsers.UsersList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.AppUsers objlstAppUsers = new AWSFormBuilder.Entities.AppUsers();

                    objlstAppUsers.UserID = Convert.ToInt64(dr["UserID"].ToString());
                    objlstAppUsers.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objlstAppUsers.Phone = (dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : null);
                    objlstAppUsers.DeviceID = (dr["DeviceID"] != DBNull.Value ? dr["DeviceID"].ToString() : null);
                    objlstAppUsers.AppVersion = (dr["AppVersion"] != DBNull.Value ? dr["AppVersion"].ToString() : null);
                    objlstAppUsers.AppType = (dr["AppType"] != DBNull.Value ? dr["AppType"].ToString() : null);
                    objlstAppUsers.OneSignalID = (dr["OneSignalID"] != DBNull.Value ? dr["OneSignalID"].ToString() : null);


                    lstAppUsers.Add(objlstAppUsers);
                }

            }
            return lstAppUsers;
        }




        #endregion

        #endregion







    }
}
