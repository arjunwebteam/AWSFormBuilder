using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class AdminAppUsers
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.AdminAppUsers _AppUsers = new AWSFormBuilder.Areas.FormBuilder.DAL.AdminAppUsers();

        #region Admin

        #region Methods

        public Int64 InsertAppUsers(Entities.AdminAppUsers objAppUsers, ref Int64 UserID)
        {
            Int64 _status = 0;
            if (objAppUsers != null)
            {
                _status = _AppUsers.InsertAppUsers(objAppUsers, ref UserID);

            }
            return _status;
        }

        public Int64 InsertAppUserNotifications(Entities.AdminAppUsers objAppUsers)
        {
            Int64 _status = 0;
            if (objAppUsers != null)
            {
                _status = _AppUsers.InsertAppUserNotifications(objAppUsers);

            }
            return _status;
        }


        public Int64 AppUsersDelete(Int64 UserID)
        {
            Int64 _status = 0;
            _status = _AppUsers.AppUsersDelete(UserID);
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

        public List<Entities.AdminAppUsers> GetAppUsersList(ref int status, ref String AppuserIds)
        {
            List<Entities.AdminAppUsers> lstAppUsers = new List<Entities.AdminAppUsers>();
            DataTable dt = _AppUsers.GetAppUsersList(ref status, ref AppuserIds);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.AdminAppUsers objlstAppUsers = new Entities.AdminAppUsers();

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

        public Entities.AdminAppUsers GetAppUsersById(Int64 UserID, ref int status)
        {
            Entities.AdminAppUsers objAppUsers = new Entities.AdminAppUsers();
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

        public List<Entities.AdminAppUsers> GetAppUsersListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<Entities.AdminAppUsers> lstAppUsers = new List<Entities.AdminAppUsers>();
            DataTable dt = _AppUsers.GetAppUsersListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _AppUsers.GetAppUsersListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.AdminAppUsers objlstAppUsers = new Entities.AdminAppUsers();

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




        public Entities.AdminAppUsers AppUsersGetById(ref String DeviceID, ref int status)
        {
            Entities.AdminAppUsers objAppUsers = new Entities.AdminAppUsers();
            DataTable dt = new DataTable();

            dt = _AppUsers.AppUsersGetById(ref DeviceID, ref status);
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


        public List<Entities.AdminAppUsers> UserNotication(ref String DeviceID, ref int status)
        {

            List<Entities.AdminAppUsers> lstAppUsers = new List<Entities.AdminAppUsers>();
            DataTable dt = _AppUsers.UserNotifiation(ref DeviceID, ref status);


            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.AdminAppUsers objlstAppUsers = new Entities.AdminAppUsers();
                    objlstAppUsers.NotificationID = Convert.ToInt32(dr["NotificationID"].ToString());
                    objlstAppUsers.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objlstAppUsers.Body = (dr["Body"] != DBNull.Value ? dr["Body"].ToString() : null);
                    objlstAppUsers.ActivityTobeopen = (dr["ActivityTobeopen"] != DBNull.Value ? dr["ActivityTobeopen"].ToString() : null);
                    objlstAppUsers.Values = (dr["Values"] != DBNull.Value ? dr["Values"].ToString() : null);
                    objlstAppUsers.NotificationUserId = Convert.ToInt32(dr["NotificationUserId"].ToString());
                    objlstAppUsers.DeviceID = (dr["DeviceID"] != DBNull.Value ? dr["DeviceID"].ToString() : null);
                    objlstAppUsers.AppuserId = (dr["AppuserId"] != DBNull.Value ? dr["AppuserId"].ToString() : null);
                    objlstAppUsers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());

                    lstAppUsers.Add(objlstAppUsers);
                }
            }

            return lstAppUsers;
        }



        public Entities.AdminAppUsers Userlist(ref String DeviceID, ref int status)
        {
            Entities.AdminAppUsers objAppUsers = new Entities.AdminAppUsers();
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


        public List<Entities.AdminAppUsers> UsersList(ref int status, ref String category)
        {
            List<Entities.AdminAppUsers> lstAppUsers = new List<Entities.AdminAppUsers>();
            DataTable dt = _AppUsers.UsersList(ref status, ref category);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.AdminAppUsers objlstAppUsers = new Entities.AdminAppUsers();

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