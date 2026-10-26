using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class Users
    {
        public Int64 RId { get; set; }

        public Int64 UserId { get; set; }

        public string UserName { get; set; }

        public string RoleIds { get; set; }

        public string Roles { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }        

        public string ConfirmPassword { get; set; }

        public string ProfileImage { get; set; }

        public string Designation { get; set; }

        public string MobilePhone { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsActivated { get; set; }

        public DateTime DateActivated { get; set; }

        public Guid RegistrationGUID { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public Int64 ChapterId { get; set; }

        public Int64 MemberId { get; set; }

        public string ChapterName { get; set; }
        public string ChapterIds { get; set; }
        public List<ChapterUsers> lstChapterUsers { get; set; }

        #region

        public Int64 RoleId { get; set; }

        public string RoleName { get; set; }

        #endregion
    }

    public class ChangePasswordModel
    {
        public Int64 UserId { get; set; }

        public Int64 MemberId { get; set; }

        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
        public Int64 CPMemberId { get; set; }
    }

    public class LogOnModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        public string CulturalEmail { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string Captcha { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime LastActivityDate { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Captcha { get; set; }
        public string ChapterName { get; set; }
    }

    public class Roles
    {
        public string RoleName { get; set; }

        public Int64 RId { get; set; }
        public Int64 RoleId { get; set; }
        public Int64 UserCount { get; set; }
        public Int64 UserRoleId { get; set; }
        public Int64 UserId { get; set; }
        public Boolean IsAdd { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsView { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsExport { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 ParentId { get; set; }
        public Int64 SubRoleCount { get; set; }
        public string comma_separated_ids { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

    }

    public class UserRoles
    {
        public string RoleName { get; set; }

        public Int64 RoleId { get; set; }

        public string RoleIds { get; set; }
        public string IsAdds { get; set; }
        public string IsEdits { get; set; }
        public string IsViews { get; set; }
        public string IsDeletes { get; set; }
        public string IsExports { get; set; }


        public Int64 UserId { get; set; }
        public Boolean IsAdd { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsView { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsExport { get; set; }
        public Int64 UserRoleId { get; set; }
        public Int64 RoleWiseAccesId { get; set; }
        public Int64 ParentId { get; set; }


    }
    public class ChapterUsers
    {
        public Int64 RId { get; set; }
        public Int64 ChapterUserId { get; set; }
        public Int64 ChapterId { get; set; }
        public Int64 UserId { get; set; }
    }
    public class LogReport
    {
        public Int64 RId { get; set; }
        public Int64 LogId { get; set; }
        public string LogTitle { get; set; }
        public string LogDescription { get; set; }
        public DateTime LogDate { get; set; }
        public string InsertedBy { get; set; }

        public DateTime InsertedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string XMLLogCounts { get; set; }

    }
    public class LogSubReport
    {

        public Int64 RId { get; set; }
        public Int64 LogSubReportId { get; set; }
        public Int64 LogId { get; set; }
        public string LogTitle { get; set; }
        public string LogDescription { get; set; }
        public DateTime LogDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string XMLLogCounts { get; set; }

    }

    public class ApplicationLogs
    {

        public Int64 RId { get; set; }
        public Int64 LogId { get; set; }
        public string LogTitle { get; set; }
        public string LogDescription { get; set; }
        public DateTime LogDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string XMLLogCounts { get; set; }

    }
    public class Logdetails
    {

        public Int64 RId { get; set; }
        public Int64 LogSubReportId { get; set; }
        public Int64 LogId { get; set; }
        public string LogTitle { get; set; }
        public string LogDescription { get; set; }
        public DateTime LogDate { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string XMLLogCounts { get; set; }

    }
}
