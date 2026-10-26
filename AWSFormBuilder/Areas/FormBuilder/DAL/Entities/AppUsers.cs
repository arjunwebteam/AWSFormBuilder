using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Entities
{
    public class AppUsers
    {
        //public Int64 AppUserId { get; set; }
        public string OneSignalDeviceId { get; set; }

        public string DeviceID { get; set; }
        public string AndroidVersion { get; set; }
        public string IOSVersion { get; set; }
        public bool IsApproved { get; set; }
        public string Comments { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public Int64 RId { get; set; }

        public Int64 UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string AppVersion { get; set; }
        public string AppType { get; set; }
        public string OneSignalID { get; set; }
        public Boolean Status { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string TempleReligiousEvents { get; set; }
        public string CulturalEvent { get; set; }
        public string TempleAnnouncement { get; set; }

        public Int64 NotificationID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ActivityTobeopen { get; set; }
        public string Values { get; set; }
        public Int64 NotificationUserId { get; set; }

        public string AppuserId { get; set; }
        public string imageurl { get; set; }



        public Int64 TotalCount { get; set; }
    }

    public class AdminAppUsers
    {
        public Int64 RId { get; set; }

        public Int64 UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string DeviceID { get; set; }
        public string AppVersion { get; set; }
        public string AppType { get; set; }
        public string OneSignalID { get; set; }
        public Boolean Status { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

        public string TempleReligiousEvents { get; set; }
        public string CulturalEvent { get; set; }
        public string TempleAnnouncement { get; set; }

        public Int64 NotificationID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ActivityTobeopen { get; set; }
        public string Values { get; set; }
        public Int64 NotificationUserId { get; set; }
        public string AppuserId { get; set; }


    }

    public class MobileAppInfo
    {


        public Int64 AppsettingId { get; set; }

        public string SplashMiddle { get; set; }
        public string SplashBottom { get; set; }
        public string HomeTopHeader { get; set; }

        public string Customloader { get; set; }

        public string IOSApp { get; set; }

        public string Androidapp { get; set; }
        public string Iosversion { get; set; }

        public string AppAndroidVersion { get; set; }
        public string NotificationAppId { get; set; }
        public string ServerKey { get; set; }
        public string Androidchannelid { get; set; }

        public string OtherclasssHeader { get; set; }


    }
}
