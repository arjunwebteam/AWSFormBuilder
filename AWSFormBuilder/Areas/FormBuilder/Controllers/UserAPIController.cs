using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        [HttpPost]
        public IActionResult addUser([FromQuery] FormsUserSyncRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.RoleName))
                return BadRequest(new { ok = false, message = "Email and RoleName are required" });

            try
            {
                var now = AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                var rolesBll = new AWSFormBuilder.Areas.FormBuilder.BLL.Roles();
                int s = 0;
                var role = rolesBll.RolesGetByRoleName(req.RoleName, ref s);
                if (role == null || role.RoleId == 0)
                {
                    rolesBll.InsertRoles(new AWSFormBuilder.Entities.Roles
                    {
                        RoleId = 0,
                        RoleName = req.RoleName,
                        IsActive = true,
                        UpdatedBy = "AssociationSync",
                        UpdatedTime = now
                    });
                    role = rolesBll.RolesGetByRoleName(req.RoleName, ref s);
                }
                if (role == null || role.RoleId == 0)
                    return StatusCode(500, new { ok = false, message = "Could not create role in Forms DB" });

                var minDate = new DateTime(1900, 1, 1);
                SqlParameter P(string n, object v) => new SqlParameter(n, v ?? DBNull.Value);
                var qStatus = new SqlParameter("@QStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };

                SqlParameter[] p =
                {
                    P("@UserId", 0),
                    P("@ChapterId", req.ChapterId),
                    P("@UserName", req.UserName),
                    P("@RoleName", req.RoleName),
                    P("@Email", req.Email),
                    P("@Designation", req.Designation),
                    P("@MobilePhone", req.MobilePhone),
                    P("@IsApproved", true),
                    P("@IsLockedOut", false),
                    P("@IsActivated", true),
                    P("@DateActivated", now),
                    P("@RegistrationGUID", Guid.NewGuid()),
                    P("@FailedPasswordAttemptCount", 0),
                    P("@LastPasswordChangedDate", minDate),
                    P("@LastLoginDate", minDate),
                    P("@InsertedTime", now),
                    P("@StartDate", now),
                    P("@EndDate", now),
                    P("@InsertedBy", "AssociationSync"),
                    P("@UpdatedTime", now),
                    P("@UpdatedBy", "AssociationSync"),
                         P("@IsAdd", req.IsAdd),
     P("@IsEdit", req.IsEdit),
     P("@IsView", req.IsView),
     P("@IsDelete", req.IsDelete),
     P("@IsExport", req.IsExport),
                    P("@MemberId", 0),
                    P("@ChapterIds", null),
                    P("@RoleId", role.RoleId),
                    qStatus
                };

                new AWSFormBuilder.Areas.FormBuilder.DAL.DBAccess().SP_ExecuteScalar("UsersProfileInsert", ref p);

                return Ok(new { ok = true, status = Convert.ToInt32(qStatus.Value) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult removeUser([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new { ok = false, message = "Email is required" });

            try
            {
                var qStatus = new SqlParameter("@QStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };

                SqlParameter[] p =
                {
                    new SqlParameter("@Email", email),
                    qStatus
                };

                new AWSFormBuilder.Areas.FormBuilder.DAL.DBAccess().SP_ExecuteScalar("UsersRemoveByEmail", ref p);

                int status = Convert.ToInt32(qStatus.Value);
                if (status == -1)
                    return StatusCode(500, new { ok = false, message = "Remove failed in Forms DB" });

                return Ok(new { ok = true, status = status });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }
    }

    public class FormsUserSyncRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobilePhone { get; set; }
        public string Designation { get; set; }
        public string RoleName { get; set; }
        public long ChapterId { get; set; }

        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsExport { get; set; }
    }
}