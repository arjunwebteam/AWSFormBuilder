using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
namespace AWSFormBuilder.Areas.FormBuilder.Controllers
{
    [Area("FormBuilder")]

    public class AdminMenuItemsController : Controller
    {
        BLL.AdminMenuItems _AdminMenuItems = new BLL.AdminMenuItems();
        BLL.Chapters _Chapters = new BLL.Chapters();
        BLL.PageDetails _PageDetails = new BLL.PageDetails();
        BLL.AdminMenuItems _adminmenu = new BLL.AdminMenuItems();
        BLL.Users _users = new BLL.Users();

        [Authorize]

        public IActionResult Index(Int64 ChapterId = 0, Int64 mid = 0)
        {
            List<Entities.Chapters> lstChapters = new List<Entities.Chapters>();
            List<Entities.PageDetails> lstPageDetails = new List<Entities.PageDetails>();
            Entities.AdminMenuItems objmenuitem = new Entities.AdminMenuItems();
            int status = 0;

            try
            {
                lstPageDetails = _PageDetails.GetPageDetailsList(ref status);
                lstChapters = _Chapters.GetChaptersList(ref status);
                objmenuitem = _adminmenu.GetAdminMenuItemsById(mid, ref status);
            }
            catch
            {
                status = -1;
            }

            Int64 UserId = Convert.ToInt64(
                User.FindFirst("UserId")?.Value ?? "0");
            // ✅ Get UserRole from Claims (replaces HttpCookie)
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            string userRole = userRoleClaim?.Value ?? "";

            // ✅ Get ChapterId from Claims (replaces HttpCookie)
            var chapterIdClaim = User.FindFirst("ChapterId");
            string chapterIdValue = chapterIdClaim?.Value ?? "0";

            // ✅ Set Session values (replaces Session["key"] = value)
            HttpContext.Session.SetString("userrole", userRole);
            HttpContext.Session.SetString("chapterid", chapterIdValue);

            // ✅ Get user role details
            Entities.UserRoles objuserroles = new Entities.UserRoles();
            objuserroles = _users.GetRoleDetialsById(UserId, mid, ref status);

            // ✅ Store permissions in Session
            HttpContext.Session.SetString("IsEdit", objuserroles.IsEdit.ToString());
            HttpContext.Session.SetString("IsView", objuserroles.IsView.ToString());
            HttpContext.Session.SetString("IsDelete", objuserroles.IsDelete.ToString());
            HttpContext.Session.SetString("IsExport", objuserroles.IsExport.ToString());
            HttpContext.Session.SetString("IsAdd", objuserroles.IsAdd.ToString());

            // ✅ Pass permissions to View via ViewBag
            ViewBag.IsEdit = objuserroles.IsEdit;
            ViewBag.IsView = objuserroles.IsView;
            ViewBag.IsDelete = objuserroles.IsDelete;
            ViewBag.IsExport = objuserroles.IsExport;
            ViewBag.IsAdd = objuserroles.IsAdd;

            ViewBag.mid = mid;
            ViewBag.ChapterId = ChapterId;
            ViewBag.lstChapters = lstChapters;
            ViewBag.lstPageDetails = lstPageDetails;

            return View();
        }



        [Authorize]
        public ActionResult AdminMenuItemsList(Int64 ChapterId = 0, bool IsEdit = false, bool IsView = false, bool IsDelete = false, bool IsExport = false, bool IsAdd = false, Int64 Mid = 0)
        {
            try
            {
                int status = 0;
                List<Entities.AdminMenuItems> lstAdminMenuItems2 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems3 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems4 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems = _AdminMenuItems.GetAdminMenuItemsAll(ref lstAdminMenuItems2, ref lstAdminMenuItems3, ref lstAdminMenuItems4, ChapterId, ref status);
                if (status == 1)
                {
                    ViewBag.lstAdminMenuItems = lstAdminMenuItems;
                    ViewBag.lstAdminMenuItems2 = lstAdminMenuItems2;
                    ViewBag.lstAdminMenuItems3 = lstAdminMenuItems3;
                    ViewBag.lstAdminMenuItems4 = lstAdminMenuItems4;
                    ViewBag.total = lstAdminMenuItems.Count;
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.ChapterId = ChapterId;
            ViewBag.IsEdit = IsEdit;
            ViewBag.IsView = IsView;
            ViewBag.IsDelete = IsDelete;
            ViewBag.IsExport = IsExport;
            ViewBag.IsAdd = IsAdd;
            ViewBag.Mid = Mid;
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult AdminMenuItemsStatus(Int64 MenuItemId)
        {
            string str = "";
            string message = "";

            try
            {
                Int64 _status = _AdminMenuItems.UpdateAdminMenuItemsStatus(MenuItemId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "status Updated successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed updating user status"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = ex.Message
                });
            }
        }
        [Authorize]
        public ActionResult CreateAdminMenuItems(Int64 ChapterId = 0,  Int64 mid = 0)
        {
            try
            {
                List<Entities.AdminMenuItems> lstAdminMenuItems2 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems3 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems4 = new List<Entities.AdminMenuItems>();
                int status = 0;
        
                List<Entities.Chapters> lstChapters = _Chapters.GetChaptersList(ref status);
            

                List<Entities.AdminMenuItems> lstAdminMenuItems = _AdminMenuItems.GetAdminMenuItemsDD(ChapterId, ref lstAdminMenuItems2, ref lstAdminMenuItems3, ref lstAdminMenuItems4, ref status);
                if (status == 1)
                {

                    ViewBag.lstChapters = lstChapters;
                    ViewBag.lstAdminMenuItems = lstAdminMenuItems;
                    ViewBag.lstAdminMenuItems2 = lstAdminMenuItems2;
                    ViewBag.lstAdminMenuItems3 = lstAdminMenuItems3;
                    ViewBag.lstAdminMenuItems4 = lstAdminMenuItems4;

                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.ChapterId = ChapterId;
            ViewBag.mid = mid;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateAdminMenuItems(IFormFile Logo, Entities.AdminMenuItems objAdminMenuItems, Int64 mid = 0)
        {
            string tempPath = null;
            try
            {
                int status1 = 0;
                BLL.AppInfo _appinfoHelper = new BLL.AppInfo();
                Entities.AppInfo objAppInfoPath = _appinfoHelper.GetAppInfoDetails(ref status1);
                string uploadPath = objAppInfoPath.UploadPath;
                Int64 _status = 0;
                objAdminMenuItems.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objAdminMenuItems.InsertedBy = HttpContext.User.Identity.Name.ToString();
                objAdminMenuItems.UpdatedDate =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                objAdminMenuItems.InsertedDate =AWSFormBuilder.Areas.FormBuilder.BLL.Common.GetTimeZoneAdjustedDateTime();

                // FIXED: one folder used for both saving and renaming (before: save = "Logo", rename = "LayoutLogo")
                string logoDir = Path.Combine(uploadPath, "Logo");

                // FIXED: proc expects only the file EXTENSION (e.g. "jpeg"), or "NA" when no logo is uploaded
                string imageurl = "NA";
                if (Logo != null && Logo.Length > 0)
                {
                    Directory.CreateDirectory(logoDir);

                    string ext = Path.GetExtension(Logo.FileName).TrimStart('.').ToLower();

                    // save with a temporary name first, because MenuItemId is not known until the proc runs
                    tempPath = Path.Combine(logoDir, Guid.NewGuid().ToString("N") + "." + ext);
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        Logo.CopyTo(stream);
                    }
                    imageurl = ext;
                }
                objAdminMenuItems.IsActive = true;
                _status = _AdminMenuItems.InsertAdminMenuItems(objAdminMenuItems, ref imageurl);

                // imageurl now holds the final name built by the proc, e.g. "75-mahi.jpeg" (empty when no logo)
                if (tempPath != null && System.IO.File.Exists(tempPath))
                {
                    if (!string.IsNullOrEmpty(imageurl) && (_status == 1 || _status == 2))
                    {
                        string finalPath = Path.Combine(logoDir, imageurl);
                        System.IO.File.Move(tempPath, finalPath, true);   // overwrite if same name already exists
                    }
                    else
                    {
                        System.IO.File.Delete(tempPath);
                    }
                }

                if (_status == 1)
                {
                    TempData["messageType"] = "success";
                    TempData["message"] = "Record Inserted Successfully";
                }
                else if (_status == 2)
                {
                    TempData["messageType"] = "success"; // Assuming "info" for update
                    TempData["message"] = "Changes has been Updated Successfully";
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data Error";
                }
            }
            catch (Exception ex)
            {
                if (tempPath != null && System.IO.File.Exists(tempPath))
                {
                    System.IO.File.Delete(tempPath);
                }
                TempData["messageType"] = "error";
                TempData["message"] = ex.Message;
                return View();
            }
            return RedirectToAction("Index", "AdminMenuItems", new { mid = mid });
        }

        [Authorize]
        public ActionResult EditAdminMenuItems(Int64 MenuItemId, Int64 mid = 0)
        {
            try
            {
                int _status = 0;
                int _list = 0;
                Entities.AdminMenuItems objAdminMenuItems = _AdminMenuItems.GetAdminMenuItemsById(MenuItemId, ref _status);
                List<Entities.AdminMenuItems> lstAdminMenuItems2 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems3 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems4 = new List<Entities.AdminMenuItems>();
                List<Entities.Chapters> lstChapters = _Chapters.GetChaptersList(ref _status);
                List<Entities.AdminMenuItems> lstAdminMenuItems = _AdminMenuItems.GetAdminMenuItemsDD(objAdminMenuItems.ChapterId, ref lstAdminMenuItems2, ref lstAdminMenuItems3, ref lstAdminMenuItems4, ref _list);
                if (_list == 1)
                {
                    ViewBag.lstAdminMenuItems = lstAdminMenuItems;
                    ViewBag.lstAdminMenuItems2 = lstAdminMenuItems2;
                    ViewBag.lstAdminMenuItems3 = lstAdminMenuItems3;
                    ViewBag.lstAdminMenuItems4 = lstAdminMenuItems4;
                }
                if (_status == 1)
                {
                    ViewBag.lstChapters = lstChapters;
                    ViewBag.objAdminMenuItems = objAdminMenuItems;
                }
                else
                {
                    TempData["messageType"] = "error";
                    TempData["message"] = "Data Error";
                    return RedirectToAction("Index", "AdminMenuItems", new { mid = mid });
                }
                ViewBag.mid = mid;
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "AdminMenuItems", new { mid = mid });
            }
        }

        
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;        // ✅ add this

        [HttpPost]
        [Authorize]
        public IActionResult EditAdminMenuItems(IFormFile Logo,
            Entities.AdminMenuItems objAdminMenuItems,
            Int64 mid = 0)
        {
            try
            {
                string tempPath = null;
                Int64 _status = 0;
                int status1 = 0;
                BLL.AppInfo _appinfoHelper = new BLL.AppInfo();
                Entities.AppInfo objAppInfoPath = _appinfoHelper.GetAppInfoDetails(ref status1);
                string uploadPath = objAppInfoPath.UploadPath;
                // ✅ FIX: _configuration  (NOT Configuration)
                //         ↑ underscore + lowercase = your private field
                bool masterStatus = Convert.ToBoolean(
                    _configuration["AppSettings:masterstatus"]);

                objAdminMenuItems.IsActive = masterStatus;
           
                // FIXED: one folder used for both saving and renaming (before: save = "Logo", rename = "LayoutLogo")
                string logoDir = Path.Combine(uploadPath, "Logo");

                // FIXED: proc expects only the file EXTENSION (e.g. "jpeg"), or "NA" when no logo is uploaded
                string imageurl = "NA";
                if (Logo != null && Logo.Length > 0)
                {
                    Directory.CreateDirectory(logoDir);

                    string ext = Path.GetExtension(Logo.FileName).TrimStart('.').ToLower();

                    // save with a temporary name first, because MenuItemId is not known until the proc runs
                    tempPath = Path.Combine(logoDir, Guid.NewGuid().ToString("N") + "." + ext);
                    using (var stream = new FileStream(tempPath, FileMode.Create))
                    {
                        Logo.CopyTo(stream);
                    }
                    imageurl = ext;
                }
                _status = _AdminMenuItems.InsertAdminMenuItems(objAdminMenuItems, ref imageurl);

                // imageurl now holds the final name built by the proc, e.g. "75-mahi.jpeg" (empty when no logo)
                if (tempPath != null && System.IO.File.Exists(tempPath))
                {
                    if (!string.IsNullOrEmpty(imageurl) && (_status == 1 || _status == 2))
                    {
                        string finalPath = Path.Combine(logoDir, imageurl);
                        System.IO.File.Move(tempPath, finalPath, true);   // overwrite if same name already exists
                    }
                    else
                    {
                        System.IO.File.Delete(tempPath);
                    }
                }
                if (_status == 1 || _status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">" +
                                          (_status == 2 ? "Updated " : "Inserted ") +
                                          "Menu Item details successfully.</div>";

                    return RedirectToAction("Index", "AdminMenuItems",
                        new { area = "Admin", mid = mid });
                }
                else
                {
                    ViewBag.message = "<div class=\"alert alert-danger alert-dismissable\">Failed " +
                                      (_status == 2 ? "updating " : "inserting ") +
                                      "Menu Item details.</div>";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" +
                                      ex.Message + "</div>";
                return View();
            }
        }
        [Authorize]
        public ActionResult ViewAdminMenuItems(Int64 MenuItemId, Int64 mid = 0)
        {
            try
            {
                int _status = 0;
                List<Entities.Chapters> lstChapters = _Chapters.GetChaptersList(ref _status);
                Entities.AdminMenuItems objAdminMenuItems = _AdminMenuItems.GetAdminMenuItemsById(MenuItemId, ref _status);
                if (_status == 1)
                {
                    ViewBag.lstChapters = lstChapters;
                    ViewBag.objAdminMenuItems = objAdminMenuItems;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "AdminMenuItems", new { mid = mid });
                }
                ViewBag.mid = mid;
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "AdminMenuItems", new { mid = mid });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult AdminMenuItemsDelete(Int64 MenuItemId)
        {

            try
            {
                Int64 _status = _AdminMenuItems.DeleteAdminMenuItems(MenuItemId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Record Deleted successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed Deleting the Record"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."

                });
            }
        }

        [Authorize]
        public ActionResult AdminMenuItemsByInstitute(Int64 ChapterId = 0)
        {
            string str = "";
            string message = "";

            try
            {
                List<Entities.AdminMenuItems> lstAdminMenuItems2 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems3 = new List<Entities.AdminMenuItems>();
                List<Entities.AdminMenuItems> lstAdminMenuItems4 = new List<Entities.AdminMenuItems>();
                int status = 0;
                List<Entities.AdminMenuItems> lstAdminMenuItems = _AdminMenuItems.GetAdminMenuItemsDD(ChapterId, ref lstAdminMenuItems2, ref lstAdminMenuItems3, ref lstAdminMenuItems4, ref status);
                if (status == 1)
                {
                    return Json(new { ok = true, data = lstAdminMenuItems, data2 = lstAdminMenuItems2, data3 = lstAdminMenuItems3, data4 = lstAdminMenuItems4 });
                }
                else
                {
                    message = "error";
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                message = "error";
                return Json(new { ok = true, data = str });
            }
        }

        //private readonly IWebHostEnvironment _hostEnvironment;

        //public void logreport(string error)
        //{
        //    try
        //    {
        //        // Get page name
        //        string pageName = Path.GetFileName(HttpContext.Request.Path);

        //        // Build log filename
        //        string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

        //        // ✅ FIX: _hostEnvironment  (NOT IHostEnvironment)
        //        //         ↑ underscore + lowercase = your private field
        //        string folderPath = Path.Combine(_hostEnvironment.WebRootPath,"Content","logfiles");

        //        // Create folder if it doesn't exist
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }

        //        string filepath = Path.Combine(folderPath, filename);

        //        // append:true handles both create and append
        //        using (StreamWriter stwriter = new StreamWriter(filepath, append: true))
        //        {
        //            stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
        //            stwriter.WriteLine("Page : " + pageName);
        //            stwriter.WriteLine("Error: " + error);
        //            stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
        //            stwriter.WriteLine();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Logging failed: " + ex.Message);
        //    }
        //}

        #region AssignMenu

        [Authorize]
        public ActionResult AssignMenu(Int64 mid = 0, Int64 RoleId = 0, Int64 UserId = 0, string UserName = "", string RoleName = "")
        {

            // ✅ Get UserId from Claims — replaces Request.Cookies["UserId"]
         
            Int32 _qstatus = 0;
            List<Entities.Roles> lstRoles = _AdminMenuItems.GetRolesAssignMenu(ref _qstatus);
            ViewBag.lstRoles = lstRoles;
            ViewBag.mid = mid;
            ViewBag.RoleId = RoleId;
            ViewBag.UserId = UserId;
            ViewBag.UserName = UserName;
            ViewBag.RoleName = RoleName;
            return View();
        }

        [Authorize]
        public ActionResult GetAssignMenuList(Int32 RoleId = 0, Int64 mid = 0, Int32 UserId = 0)
        {
            string str = "";
            Int32 MenuId = 0;
            List<Entities.AdminMenuItems> lstMainMenuMaster = new List<Entities.AdminMenuItems>();
            List<Entities.Role_Menu> lstrole_Menus = new List<Entities.Role_Menu>();
            //Int32 UserId = (Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0);
            List<Entities.AdminMenuItems> lstMenuMaster = new List<Entities.AdminMenuItems>();
            List<Entities.Users> lstEmployeeCompanyInformation = new List<Entities.Users>();
            try
            {
                Int32 _qstatus = 0;
                lstMenuMaster = _AdminMenuItems.GetAssignMenuList(ref lstMainMenuMaster, ref lstrole_Menus, MenuId, UserId, RoleId, ref _qstatus);

                lstEmployeeCompanyInformation = _AdminMenuItems.GetUsersByRole(RoleId, ref _qstatus);


            }
            catch (Exception ex)
            {
                str = "<div class=\"error closable\">" + ex.Message + "</div>";
                return Json(new { ok = false, data = str });
            }
            ViewBag.lstMenuMaster = lstMenuMaster;
            ViewBag.lstMainMenuMaster = lstMainMenuMaster;
            ViewBag.lstrole_Menus = lstrole_Menus;
            ViewBag.lstEmployeeCompanyInformation = lstEmployeeCompanyInformation;
            ViewBag.mid = mid;
            ViewBag.UserId = UserId;
            return View();
        }

        [Authorize]
        public ActionResult InsertRoleMenu(string MenuIds = "", Int32 RoleId = 0, Int32 UserId = 0)
        {
            try
            {
                string CreatedBy = HttpContext.User.Identity.Name.ToString();
                DateTime CreatedDate = DateTime.Now;
                Int32 MenuId = 0;

                int _qstatus = 0;
                _qstatus = _AdminMenuItems.InsertRoleMenu(RoleId, MenuIds, MenuId, UserId, CreatedBy, CreatedDate);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true });
                }
                else
                {
                    return Json(new { ok = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { ok = false });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult UpdateRoleBasedAccess([FromBody] Entities.Role_Menu objUserRoles)
        {
            string str = "";
            try
            {
                Int64 _status = _AdminMenuItems.UpdateRoleBasedAccess(objUserRoles);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Successfully given Role access"
                    });
                    //str = "<div class=\"alert alert-success alert-dismissable\">Successfully given Role access to the User..</div>";
                    //return Json(new { ok = true, data = str });
                }
                else
                {

                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed giving Role access to the User"
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."
                });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult SingleMenuUpdate([FromBody] Entities.Role_Menu objUserRoles)
        {
            string str = "";
            try
            {
                Int64 _status = _AdminMenuItems.SingleMenuUpdate(objUserRoles);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Successfully given Role access.."
                    });
                    //str = "<div class=\"alert alert-success alert-dismissable\">Successfully given Role access to the User..</div>";
                    //return Json(new { ok = true, data = str });
                }
                else if (_status == 2)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Update or Remove given Role access.."
                    });
                    //str = "<div class=\"alert alert-success alert-dismissable\">Successfully given Role access to the User..</div>";
                    //return Json(new { ok = true, data = str });
                }
                else
                {

                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed giving Role access to the User.."
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."
                });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateCloneRoleIds(Int64 RoleId = 0, Int64 RoleIds = 0)
        {
            try
            {

                Int64 _status = _AdminMenuItems.UpdateCloneRoleIds(RoleId, RoleIds);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Successfully Update Clone Role access.."
                    });
                    //str = "<div class=\"alert alert-success alert-dismissable\">Successfully given Role access to the User..</div>";
                    //return Json(new { ok = true, data = str });
                }
                else
                {

                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed giving Role access to the User.."
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."
                });
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult RemoveRoleMenuAccess(Int64 RoleMenuMasterId, Int64 ParentId, Int64 UserId)
        {
            string str = "";
            try
            {
                Int64 _status = _AdminMenuItems.RemoveRoleMenuAccess(RoleMenuMasterId, ParentId, UserId);
                if (_status == 1)
                {
                    return Json(new
                    {
                        ok = true,
                        messageType = "success",
                        message = "Remove Role access successfully..!!"
                    });

                }
                else
                {
                    return Json(new
                    {
                        ok = false,
                        messageType = "error",
                        message = "Failed removing role access"
                    });
                    //str = "<div class=\"alert alert-danger alert-dismissable\">Failed removing role access</div>";
                    //return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                return Json(new
                {
                    ok = false,
                    messageType = "error",
                    message = "Failed transaction."
                });

            }
        }

        #endregion

        public class AuthorizeAttribute : ActionFilterAttribute
        {
            BLL.Users _user = new BLL.Users();
            BLL.Roles _Roles = new BLL.Roles();
            private object FormsAuthentication;
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                string userRole = null;
                int status = 0;

                // ✅ Get ClaimsPrincipal — replaces HttpCookie + FormsAuthentication.Decrypt
                var user = filterContext.HttpContext.User;

                if (user?.Identity != null && user.Identity.IsAuthenticated)
                {
                    // ✅ Read role from Claims — replaces authTicket.UserData
                    userRole = user.FindFirst(ClaimTypes.Role)?.Value;

                    // ✅ Read email from Claims — replaces authTicket.Name
                    string emailFromClaim = user.FindFirst(ClaimTypes.Email)?.Value;

                    if (!string.IsNullOrEmpty(emailFromClaim))
                    {
                        // Get user from DB
                        var objuser = _user.GetAdminUsersGetByEmail(
                            emailFromClaim, ref status);

                        // Inside AuthorizeAttribute, where objuser != null:
                        if (objuser != null)
                        {
                            filterContext.HttpContext.Session.SetString("UserName", objuser.UserName ?? "");
                            filterContext.HttpContext.Session.SetString("UserId", objuser.UserId.ToString());
                            filterContext.HttpContext.Session.SetString("UserEmail", emailFromClaim ?? ""); // ✅ ADD THIS
                            filterContext.HttpContext.Session.SetString("chapterid", objuser.ChapterId.ToString());
                            filterContext.HttpContext.Session.SetString("userrole", userRole ?? "");
                        }
                    }
                }

                // ✅ If no role — redirect to LogOn
                //    replaces new UrlHelper(filterContext.RequestContext)
                if (string.IsNullOrEmpty(userRole))
                {
                    filterContext.Result = new RedirectToActionResult(
                        "LogOn", "Account", new { area = "FormBuilder" });
                    return;
                }

                // ✅ Get all allowed roles from DB
                int roleStatus = 0;
                List<Entities.Roles> lstRoles = _Roles.GetRolesList(ref roleStatus);

                // Build flat list of allowed role names
                List<string> allowedRoles = lstRoles
                    .Select(r => r.RoleName.Trim())
                    .ToList();

                // Split user's roles (comma separated) and check
                var userRoles = userRole
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Trim())
                    .ToList();

                bool isAuthorized = userRoles.Any(r => allowedRoles.Contains(r));

                if (!isAuthorized)
                {
                    // ✅ Redirect to Unauthorized — replaces UrlHelper + RedirectResult
                    filterContext.Result = new RedirectToActionResult(
                        "Unauthorized", "Account", new { area = "FormBuilder" });
                    return;
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }
}
