using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AWSFormBuilder.Areas.FormBuilder.DAL;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using SqlParameter = Microsoft.Data.SqlClient.SqlParameter;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class AppInfo
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;
        private readonly IConfiguration _configuration;

        public AppInfo()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        // ✅ KEEP THIS - existing constructor stays as is
        public AppInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Admin
   

            public DataTable GetAppInfoDetails(ref int status)
            {
                DataTable dt = null;

                // ✅ correct key path
                string type = _configuration["AppSettings:Environment"];

                try
                {
                    _sqlP = new[]
                    {
                new SqlParameter("@QStatus", 0),
                new SqlParameter("@Type",    type)
            };
                    _sqlP[0].Direction = ParameterDirection.Output;

                    dt = _dbAccess.GetDataTable("AppInfoGetList", ref _sqlP);
                    status = Convert.ToInt32(_sqlP[0].Value);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dt;
            }

        //public DataTable GetAppInfoDetails(ref int status)
        //{
        //    DataTable dt = null;
        //    string type = _configuration["AppSettings:Environment"];
        //    try
        //    {
        //        _sqlP = new[] 
        //        {
        //            new SqlParameter("@QStatus",0),
        //             new SqlParameter("@Type",type)
        //        };
        //        _sqlP[0].Direction = System.Data.ParameterDirection.Output;
        //        dt = _dbAccess.GetDataTable("AppInfoGetList", ref _sqlP);
        //        status = Convert.ToInt32(_sqlP[0].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dt;
        //}
        public DataSet FEGetListMainLayout(Int64 ChapterId, string headingName, string Email, ref int Status)
        {
            DataSet ds = null;
            string type = _configuration["AppSettings:Environment"];
            try
            {
                _sqlP = new[]
                    {
                        new SqlParameter("@Email",Email),
                        new SqlParameter("@ChapterId",ChapterId),
                        new SqlParameter("@QStatus",0),
                        new SqlParameter("@Type",type),
                        new SqlParameter("@headingName", headingName)
                    };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEGetListMainLayout", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataTable GetAppInfoDetailsByChapterId(Int64 ChapterId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("AppInfoGetListByChapterId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateAppInfoDetails(Entities.AppInfo objAppInfo, ref string LayoutLogo, ref string faviconlogo, ref string Loginlogo,ref string MailLogo)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@AppInfoId",objAppInfo.AppInfoId),
                    new SqlParameter("@ChapterId",(objAppInfo.ChapterId==0?(object)DBNull.Value:objAppInfo.ChapterId)),
                    new SqlParameter("@SiteName",objAppInfo.SiteName),
                    new SqlParameter("@CompanyAddress",objAppInfo.CompanyAddress),
                    new SqlParameter("@CompanyWebSite",objAppInfo.CompanyWebSite),
                    new SqlParameter("@CompanyEmail",objAppInfo.CompanyEmail),
                    new SqlParameter("@CompanyPhone",(objAppInfo.CompanyPhone==null?(object)DBNull.Value:objAppInfo.CompanyPhone.Trim())),
                    new SqlParameter("@PresidentEmail",(objAppInfo.PresidentEmail==null?(object)DBNull.Value:objAppInfo.PresidentEmail.Trim())),
                    new SqlParameter("@PresidentPhone",(objAppInfo.PresidentPhone==null?(object)DBNull.Value:objAppInfo.PresidentPhone.Trim())),
                    new SqlParameter("@SecretaryEmail",(objAppInfo.SecretaryEmail==null?(object)DBNull.Value:objAppInfo.SecretaryEmail.Trim())),
                    new SqlParameter("@SecretaryPhone",(objAppInfo.SecretaryPhone==null?(object)DBNull.Value:objAppInfo.SecretaryPhone.Trim())),
                    new SqlParameter("@CustomerCareNumber",(objAppInfo.CustomerCareNumber==null?(object)DBNull.Value:objAppInfo.CustomerCareNumber.Trim())),
                    new SqlParameter("@TollFreeNumber",(objAppInfo.TollFreeNumber==null?(object)DBNull.Value:objAppInfo.TollFreeNumber.Trim())),
                    new SqlParameter("@FacebookUrl",(objAppInfo.FacebookUrl==null?(object)DBNull.Value:objAppInfo.FacebookUrl.Trim())),
                    new SqlParameter("@TwitterUrl",(objAppInfo.TwitterUrl==null?(object)DBNull.Value:objAppInfo.TwitterUrl.Trim())),
                    new SqlParameter("@YoutubeUrl",(objAppInfo.YoutubeUrl==null?(object)DBNull.Value:objAppInfo.YoutubeUrl.Trim())),
                    new SqlParameter("@SupportEmail",(objAppInfo.SupportEmail==null?(object)DBNull.Value:objAppInfo.SupportEmail.Trim())),
                    new SqlParameter("@EnqueryEmail",(objAppInfo.EnqueryEmail==null?(object)DBNull.Value:objAppInfo.EnqueryEmail.Trim())),
                    new SqlParameter("@PageTitle",(objAppInfo.PageTitle==null?(object)DBNull.Value:objAppInfo.PageTitle.Trim())),
                    new SqlParameter("@MetaDescription",(objAppInfo.MetaDescription==null?(object)DBNull.Value:objAppInfo.MetaDescription.Trim())),
                    new SqlParameter("@MetaKeywords",(objAppInfo.MetaKeywords==null?(object)DBNull.Value:objAppInfo.MetaKeywords.Trim())),
                    new SqlParameter("@Topline",(objAppInfo.Topline==null?(object)DBNull.Value:objAppInfo.Topline.Trim())),
                    new SqlParameter("@PageItems",(objAppInfo.PageItems==0?(object)DBNull.Value:objAppInfo.PageItems)),
                    new SqlParameter("@UpdatedBy",objAppInfo.UpdatedBy),
                    new SqlParameter("@UpdatedTime",objAppInfo.UpdatedTime),


                    new SqlParameter("@LayoutLogo",LayoutLogo),
                    new SqlParameter("@faviconlogo",faviconlogo),
                    new SqlParameter("@Loginlogo",Loginlogo),
                    new SqlParameter("@MailLogo",MailLogo),


                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@BaseUrl",(objAppInfo.BaseUrl==null?(object)DBNull.Value:objAppInfo.BaseUrl.Trim())),
                    new SqlParameter("@UploadPath",(objAppInfo.UploadPath==null?(object)DBNull.Value:objAppInfo.UploadPath.Trim())),
                    new SqlParameter("@UserUploadPath",(objAppInfo.UserUploadPath==null?(object)DBNull.Value:objAppInfo.UserUploadPath.Trim())),
                    new SqlParameter("@UserSiteUrl",(objAppInfo.UserSiteUrl==null?(object)DBNull.Value:objAppInfo.UserSiteUrl.Trim())),
                    new SqlParameter("@ServerMapUrl",(objAppInfo.ServerMapUrl==null?(object)DBNull.Value:objAppInfo.ServerMapUrl.Trim())),
                    new SqlParameter("@AdminImageUrl",(objAppInfo.AdminImageUrl==null?(object)DBNull.Value:objAppInfo.AdminImageUrl.Trim())),
                    new SqlParameter("@AdminSiteUrl",(objAppInfo.AdminSiteUrl==null?(object)DBNull.Value:objAppInfo.AdminSiteUrl.Trim())),
                    new SqlParameter("@MailName",(objAppInfo.MailName==null?(object)DBNull.Value:objAppInfo.MailName.Trim())),
                    new SqlParameter("@SenderEmail",(objAppInfo.SenderEmail==null?(object)DBNull.Value:objAppInfo.SenderEmail.Trim())),
                    new SqlParameter("@MemberEmail",(objAppInfo.MemberEmail==null?(object)DBNull.Value:objAppInfo.MemberEmail.Trim())),
                    new SqlParameter("@ExhibitEmail",(objAppInfo.ExhibitEmail==null?(object)DBNull.Value:objAppInfo.ExhibitEmail.Trim())),
                    new SqlParameter("@EventsEmail",(objAppInfo.EventsEmail==null?(object)DBNull.Value:objAppInfo.EventsEmail.Trim())),
                    new SqlParameter("@ContactEmail",(objAppInfo.ContactEmail==null?(object)DBNull.Value:objAppInfo.ContactEmail.Trim())),
                    new SqlParameter("@DonationEmail",(objAppInfo.DonationEmail==null?(object)DBNull.Value:objAppInfo.DonationEmail.Trim())),
                    new SqlParameter("@VolunteerEmail",(objAppInfo.VolunteerEmail==null?(object)DBNull.Value:objAppInfo.VolunteerEmail.Trim())),
                    new SqlParameter("@SponsorshipEmail",(objAppInfo.SponsorshipEmail==null?(object)DBNull.Value:objAppInfo.SponsorshipEmail.Trim())),
                    new SqlParameter("@BrevoKey",(objAppInfo.BrevoKey==null?(object)DBNull.Value:objAppInfo.BrevoKey.Trim())),
                    new SqlParameter("@AndroidVersion",(objAppInfo.AndroidVersion==0?(object)DBNull.Value:objAppInfo.AndroidVersion)),
                    new SqlParameter("@IOSVersion",(objAppInfo.IOSVersion==0?(object)DBNull.Value:objAppInfo.IOSVersion)),
                    new SqlParameter("@DesktopVersion",(objAppInfo.DesktopVersion==0?(object)DBNull.Value:objAppInfo.DesktopVersion)),
                    //new SqlParameter("@BrevoKey",(objAppInfo.BrevoKey==null?(object)DBNull.Value:objAppInfo.BrevoKey.Trim())),
                    new SqlParameter("@AppUpdate",(objAppInfo.AppUpdate==null?(object)DBNull.Value:objAppInfo.AppUpdate.Trim())),
                    new SqlParameter("@CapchaSiteKey",(objAppInfo.CapchaSiteKey==null?(object)DBNull.Value:objAppInfo.CapchaSiteKey.Trim())),
                    new SqlParameter("@CapchaSecreatKey",(objAppInfo.CapchaSecreatKey==null?(object)DBNull.Value:objAppInfo.CapchaSecreatKey.Trim())),
                    new SqlParameter("@ShowCapcha",(objAppInfo.ShowCapcha==null?(object)DBNull.Value:objAppInfo.ShowCapcha.Trim())),
                    new SqlParameter("@InstagramUrl",(objAppInfo.InstagramUrl==null?(object)DBNull.Value:objAppInfo.InstagramUrl.Trim())),
                    new SqlParameter("@GooglePlusUrl",(objAppInfo.GooglePlusUrl==null?(object)DBNull.Value:objAppInfo.GooglePlusUrl.Trim())),
                    new SqlParameter("@WhatsappNumber",(objAppInfo.WhatsappNumber==null?(object)DBNull.Value:objAppInfo.WhatsappNumber.Trim())),
                    new SqlParameter("@GoogleAnalyticsScript",(objAppInfo.GoogleAnalyticsScript==null?(object)DBNull.Value:objAppInfo.GoogleAnalyticsScript.Trim())),
                    new SqlParameter("@WhatsappScript",(objAppInfo.WhatsappScript==null?(object)DBNull.Value:objAppInfo.WhatsappScript.Trim())),
                    new SqlParameter("@TimeZones",(objAppInfo.TimeZones==null?(object)DBNull.Value:objAppInfo.TimeZones.Trim())),
                    new SqlParameter("@CAPTCHA",(objAppInfo.CAPTCHA==null?(object)DBNull.Value:objAppInfo.CAPTCHA.Trim())),
                    new SqlParameter("@Email",(objAppInfo.Email==null?(object)DBNull.Value:objAppInfo.Email.Trim())),
                   

                };




                 _sqlP[25].SqlDbType = SqlDbType.NVarChar;
                _sqlP[25].Size = 512;
                _sqlP[25].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[26].SqlDbType = SqlDbType.NVarChar;
                _sqlP[26].Size = 512;
                _sqlP[26].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[27].SqlDbType = SqlDbType.NVarChar;
                _sqlP[27].Size = 512;
                _sqlP[27].Direction = System.Data.ParameterDirection.InputOutput;



                _sqlP[29].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppInfoInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[29].Value);


                LayoutLogo = _sqlP[25].Value.ToString();
                faviconlogo = _sqlP[26].Value.ToString();
                Loginlogo = _sqlP[27].Value.ToString();
                MailLogo = _sqlP[28].Value.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 GetAppInfoEmail(ref string CompanyEmail)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@CompanyEmail",CompanyEmail),
                    new SqlParameter("@QStatus",0)
                    };
                _sqlP[0].SqlDbType = SqlDbType.NVarChar;
                _sqlP[0].Size = 100;
                _sqlP[0].Direction = System.Data.ParameterDirection.InputOutput;
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("AppInfoGetByCompanyEmail", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
                CompanyEmail = _sqlP[0].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion
        public Int64 AppUpdateAppInfoDetails(Entities.MobileAppInfo objAppInfo, ref string SplashMiddle, ref string SplashBottom, ref string HomeTopHeader, ref string Customloader, ref string OtherclasssHeader)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@AppsettingId",objAppInfo.AppsettingId),
                    new SqlParameter("@IOSApp",(objAppInfo.IOSApp==null?(object)DBNull.Value:objAppInfo.IOSApp.Trim())),
                    new SqlParameter("@Androidapp",(objAppInfo.Androidapp==null?(object)DBNull.Value:objAppInfo.Androidapp.Trim())),
                    new SqlParameter("@Iosversion",(objAppInfo.Iosversion==null?(object)DBNull.Value:objAppInfo.Iosversion.Trim())),
                    new SqlParameter("@AppAndroidVersion",(objAppInfo.AppAndroidVersion==null?(object)DBNull.Value:objAppInfo.AppAndroidVersion.Trim())),
                                       new SqlParameter("@SplashMiddle",SplashMiddle),
                    new SqlParameter("@SplashBottom",SplashBottom),
                    new SqlParameter("@HomeTopHeader",HomeTopHeader),
                     new SqlParameter("@Customloader",Customloader),
                      new SqlParameter("@OtherclasssHeader",OtherclasssHeader),

                     new SqlParameter("@QStatus",0),
                      new SqlParameter("@NotificationAppId",(objAppInfo.NotificationAppId==null?(object)DBNull.Value:objAppInfo.NotificationAppId.Trim())),
                    new SqlParameter("@ServerKey",(objAppInfo.ServerKey==null?(object)DBNull.Value:objAppInfo.ServerKey.Trim())),
                    new SqlParameter("@Androidchannelid",(objAppInfo.Androidchannelid==null?(object)DBNull.Value:objAppInfo.Androidchannelid.Trim())),


                };


                _sqlP[5].SqlDbType = SqlDbType.NVarChar;
                _sqlP[5].Size = 512;
                _sqlP[5].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[6].SqlDbType = SqlDbType.NVarChar;
                _sqlP[6].Size = 512;
                _sqlP[6].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[7].SqlDbType = SqlDbType.NVarChar;
                _sqlP[7].Size = 512;
                _sqlP[7].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[8].SqlDbType = SqlDbType.NVarChar;
                _sqlP[8].Size = 512;
                _sqlP[8].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[9].SqlDbType = SqlDbType.NVarChar;
                _sqlP[9].Size = 512;
                _sqlP[9].Direction = System.Data.ParameterDirection.InputOutput;


                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("MobileAppInfoInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);


                SplashMiddle = _sqlP[5].Value.ToString();
                SplashBottom = _sqlP[6].Value.ToString();
                HomeTopHeader = _sqlP[7].Value.ToString();
                Customloader = _sqlP[8].Value.ToString();
                OtherclasssHeader = _sqlP[9].Value.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }


        #region Front End
        public DataTable APPGetAppInfoDetails(ref int status)
        {
            DataTable dt = null;

            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0),

                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MobileAppInfoGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet FEFinalGetListInitialLoad(Int64 ChapterId, ref int Status)
        {
            DataSet ds = null;
            string type = _configuration["AppSettings:Environment"];

            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@Type",type)

                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEFinalHomePageInitialLoad", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }








        public DataSet FEGetListInitialFlyer(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEGetListInitialFlyer", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet FEGetListAppInfo(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[] 
                    {
                        new SqlParameter("@QStatus",0)
                    };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEGetListAppInfo", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet FEGetListInitialLoad(Int64 ChapterId, ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("FEHomePageInitialLoad", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        //public DataSet FEFinalGetListInitialLoad(Int64 ChapterId, ref int Status)
        //{
        //    DataSet ds = null;
        //    string type = System.Configuration.ConfigurationManager.AppSettings["Environment"];
        //    try
        //    {
        //        _sqlP = new[]
        //        {
        //            new SqlParameter("@ChapterId",ChapterId),
        //            new SqlParameter("@QStatus",0),
        //            new SqlParameter("@Type",type)

        //        };
        //        _sqlP[1].Direction = System.Data.ParameterDirection.Output;
        //        ds = _dbAccess.GetDataSet("FEFinalHomePageInitialLoad", ref _sqlP);
        //        Status = Convert.ToInt32(_sqlP[1].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return ds;
        //}
     //public DataSet HomeAPI(Int64 ChapterId, ref int Status)
     //   {
     //       DataSet ds = null;
     //       string type = System.Configuration.ConfigurationManager.AppSettings["Environment"];
     //       try
     //       {
     //           _sqlP = new[]
     //           {
     //               new SqlParameter("@ChapterId",ChapterId),
     //               new SqlParameter("@QStatus",0),
     //               new SqlParameter("@Type",type)

     //           };
     //           _sqlP[1].Direction = System.Data.ParameterDirection.Output;
     //           ds = _dbAccess.GetDataSet("HomeAPI", ref _sqlP);
     //           Status = Convert.ToInt32(_sqlP[1].Value);
     //       }
     //       catch (Exception ex)
     //       {
     //           throw ex;
     //       }
     //       return ds;
     //   }
        //public DataSet APIMenus(Int64 ChapterId, ref int Status)
        //{
        //    DataSet ds = null;
        //    string type = System.Configuration.ConfigurationManager.AppSettings["Environment"];
        //    try
        //    {
        //        _sqlP = new[]
        //        {
        //            new SqlParameter("@ChapterId",ChapterId),
        //            new SqlParameter("@QStatus",0),
        //            new SqlParameter("@Type",type)

        //        };
        //        _sqlP[1].Direction = System.Data.ParameterDirection.Output;
        //        ds = _dbAccess.GetDataSet("APIMenus", ref _sqlP);
        //        Status = Convert.ToInt32(_sqlP[1].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return ds;
        //}
        //public DataSet FEGetListMainLayout(Int64 ChapterId, string headingName, string Email, ref int Status)
        //{
        //    DataSet ds = null;
        //    string type = System.Configuration.ConfigurationManager.AppSettings["Environment"];
        //    try
        //    {
        //        _sqlP = new[] 
        //            { 
        //                new SqlParameter("@Email",Email),
        //                new SqlParameter("@ChapterId",ChapterId),
        //                new SqlParameter("@QStatus",0),
        //                new SqlParameter("@Type",type),
        //                new SqlParameter("@headingName", headingName)
        //            };
        //        _sqlP[2].Direction = System.Data.ParameterDirection.Output;
        //        ds = _dbAccess.GetDataSet("FEGetListMainLayout", ref _sqlP);
        //        Status = Convert.ToInt32(_sqlP[2].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return ds;
        //}

        public DataSet APIFEGetListInitialLoad(ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {

                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("APIFEHomePageInitialLoad", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataTable FEGetWebsiteBannersList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetWebsiteBannersList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetCommitteeMembersList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetCommitteeMembersList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetNewsList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetFinalNewsList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetVisionDetails(Int64 ChapterId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetVisionDetails", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGeUpcommingEventsList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGeUpcommingEventsList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetServiceDonationsGetTotalAmount(Int64 ChapterId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetServiceDonationsGetTotalAmount", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetActiveServicesList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetActiveServicesList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetPhotosList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetPhotosList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetVideosList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetVideosList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetChaptersList(Int64 ChapterId, ref int qstatus)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEGetChaptersList", ref _sqlP);
                qstatus = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion

        public Int64 UpdateAppVersion(string Email, string Version, string Role, string Type, ref int status)
        {
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@Version",Version),
                    new SqlParameter("@Role",Role),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@Type",Type)
                };
                _sqlP[3].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateAppVersion", ref _sqlP);
                status = Convert.ToInt32(_sqlP[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return status;
        }

        #region AdminMenu

        public DataSet GetAdminMenuData(Int64 UserId, Int64 RoleId, ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@UserId",UserId),
                    new SqlParameter("@RoleId",RoleId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("AW_GetDataMainLayoutbkp", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion

        
        #region LogReport

        public DataTable GetLogReportListByVariable(string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
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
                    new SqlParameter("@Total",Total),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate)
                };

                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("LogReportGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion

        #region Dashboard

        public DataSet AdminDashboard(ref int Items, ref Int64 MembersActiveCount, ref decimal MembersActiveSumAmount, ref Int64 MembersInActiveCount,
                  ref decimal MembersInActiveSumAmount, ref decimal DonationsWeeklyAmount, ref decimal DonationsMonthlyAmount, ref Int64 DonationsWeeklyCount, ref Int64 DonationsMonthlyCount, ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@MembersActiveCount",MembersActiveCount),
                    new SqlParameter("@MembersActiveSumAmount",MembersActiveSumAmount),
                    new SqlParameter("@MembersInActiveCount",MembersInActiveCount),
                    new SqlParameter("@MembersInActiveSumAmount",MembersInActiveSumAmount),
                    new SqlParameter("@DonationsWeeklyAmount",DonationsWeeklyAmount),
                    new SqlParameter("@DonationsMonthlyAmount",DonationsMonthlyAmount),
                    new SqlParameter("@DonationsWeeklyCount",DonationsWeeklyCount),
                    new SqlParameter("@DonationsMonthlyCount",DonationsMonthlyCount),

                };

                _sqlP[1].Direction = _sqlP[2].Direction = _sqlP[3].Direction = _sqlP[4].Direction = _sqlP[5].Direction = _sqlP[6].Direction = _sqlP[7].Direction = _sqlP[8].Direction = _sqlP[9].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("AdminDashBoardOrderDetails", ref _sqlP);
                MembersActiveCount = (_sqlP[2].Value != DBNull.Value ? Convert.ToInt64(_sqlP[2].Value) : 0);
                MembersActiveSumAmount = (_sqlP[3].Value != DBNull.Value ? Convert.ToDecimal(_sqlP[3].Value) : 0);
                MembersInActiveCount = (_sqlP[4].Value != DBNull.Value ? Convert.ToInt64(_sqlP[4].Value) : 0);
                MembersInActiveSumAmount = (_sqlP[5].Value != DBNull.Value ? Convert.ToDecimal(_sqlP[5].Value) : 0);
                DonationsWeeklyAmount = (_sqlP[6].Value != DBNull.Value ? Convert.ToDecimal(_sqlP[6].Value) : 0);
                DonationsMonthlyAmount = (_sqlP[7].Value != DBNull.Value ? Convert.ToDecimal(_sqlP[7].Value) : 0);
                DonationsWeeklyCount = (_sqlP[8].Value != DBNull.Value ? Convert.ToInt64(_sqlP[8].Value) : 0);
                DonationsMonthlyCount = (_sqlP[9].Value != DBNull.Value ? Convert.ToInt64(_sqlP[9].Value) : 0);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion



    }
}
