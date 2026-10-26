using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Xml;
using Microsoft.Extensions.Configuration;
namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class AppInfo
    {
        private readonly AWSFormBuilder.Areas.FormBuilder.DAL.AppInfo _AppInfo;
  
        public AppInfo()
        {
            _AppInfo = new AWSFormBuilder.Areas.FormBuilder.DAL.AppInfo();
        }


        #region Admin

        #region Methods
        public Int64 UpdateAppInfoDetails(Entities.AppInfo objAppInfo, ref string LayoutLogo, ref string faviconlogo, ref string Loginlogo, ref string MailLogo)
        {
            Int64 _status = 0;
            _status = _AppInfo.UpdateAppInfoDetails(objAppInfo, ref LayoutLogo, ref faviconlogo, ref Loginlogo, ref MailLogo);

            return _status;
        }
        public Int64 APPUpdateAppInfoDetails(Entities.MobileAppInfo objAppInfo, ref string SplashMiddle, ref string SplashBottom, ref string HomeTopHeader, ref string Customloader, ref string OtherclasssHeader)
        {
            Int64 _status = 0;
            _status = _AppInfo.AppUpdateAppInfoDetails(objAppInfo, ref SplashMiddle, ref SplashBottom, ref HomeTopHeader, ref Customloader, ref OtherclasssHeader);

            return _status;
        }
        public Int64 GetAppInfoEmail(ref string CompanyEmail)
        {
            Int64 _status = 0;
            _status = _AppInfo.GetAppInfoEmail(ref CompanyEmail);
            return _status;
        }
        public Entities.MobileAppInfo AppGetAppInfoDetails(ref int Status)
        {
            DataTable dt = _AppInfo.APPGetAppInfoDetails(ref Status);
            Entities.MobileAppInfo objMobileAppInfo = new Entities.MobileAppInfo();

            if (Status == 1 && dt.Rows.Count == 1)
            {

                objMobileAppInfo.AppsettingId = (dt.Rows[0]["AppsettingId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["AppsettingId"]) : 0);
                objMobileAppInfo.SplashMiddle = (dt.Rows[0]["SplashMiddle"] != DBNull.Value ? dt.Rows[0]["SplashMiddle"].ToString() : null);
                objMobileAppInfo.SplashBottom = (dt.Rows[0]["SplashBottom"] != DBNull.Value ? dt.Rows[0]["SplashBottom"].ToString() : null);
                objMobileAppInfo.HomeTopHeader = (dt.Rows[0]["HomeTopHeader"] != DBNull.Value ? dt.Rows[0]["HomeTopHeader"].ToString() : null);
                objMobileAppInfo.Customloader = (dt.Rows[0]["Customloader"] != DBNull.Value ? dt.Rows[0]["Customloader"].ToString() : null);
                objMobileAppInfo.IOSApp = (dt.Rows[0]["IOSApp"] != DBNull.Value ? dt.Rows[0]["IOSApp"].ToString() : null);
                objMobileAppInfo.Androidapp = (dt.Rows[0]["Androidapp"] != DBNull.Value ? dt.Rows[0]["Androidapp"].ToString() : null);
                objMobileAppInfo.AppAndroidVersion = (dt.Rows[0]["AppAndroidVersion"] != DBNull.Value ? dt.Rows[0]["AppAndroidVersion"].ToString() : null);
                objMobileAppInfo.OtherclasssHeader = (dt.Rows[0]["OtherclasssHeader"] != DBNull.Value ? dt.Rows[0]["OtherclasssHeader"].ToString() : null);
                objMobileAppInfo.NotificationAppId = (dt.Rows[0]["NotificationAppId"] != DBNull.Value ? dt.Rows[0]["NotificationAppId"].ToString() : null);
                objMobileAppInfo.ServerKey = (dt.Rows[0]["ServerKey"] != DBNull.Value ? dt.Rows[0]["ServerKey"].ToString() : null);
                objMobileAppInfo.Androidchannelid = (dt.Rows[0]["Androidchannelid"] != DBNull.Value ? dt.Rows[0]["Androidchannelid"].ToString() : null);

                objMobileAppInfo.Iosversion = (dt.Rows[0]["Iosversion"] != DBNull.Value ? dt.Rows[0]["Iosversion"].ToString() : null);


            }
            return objMobileAppInfo;
        }

        #endregion

        #region Entities filling

        public Entities.AppInfo GetAppInfoDetails(ref int Status)
        {
            DataTable dt = _AppInfo.GetAppInfoDetails(ref Status);
            Entities.AppInfo objAppInfo = new Entities.AppInfo();

            if (Status == 1 && dt.Rows.Count == 1)
            {
                objAppInfo.AppInfoId = Convert.ToInt64(dt.Rows[0]["AppInfoId"]);
                objAppInfo.SiteName = dt.Rows[0]["SiteName"].ToString();
                objAppInfo.CompanyAddress = (dt.Rows[0]["CompanyAddress"] != DBNull.Value ? dt.Rows[0]["CompanyAddress"].ToString() : null);
                objAppInfo.CompanyWebSite = (dt.Rows[0]["CompanyWebSite"] != DBNull.Value ? dt.Rows[0]["CompanyWebSite"].ToString() : null);
                objAppInfo.CompanyEmail = (dt.Rows[0]["CompanyEmail"] != DBNull.Value ? dt.Rows[0]["CompanyEmail"].ToString() : null);
                objAppInfo.CompanyPhone = (dt.Rows[0]["CompanyPhone"] != DBNull.Value ? dt.Rows[0]["CompanyPhone"].ToString() : null);
                objAppInfo.PresidentEmail = (dt.Rows[0]["PresidentEmail"] != DBNull.Value ? dt.Rows[0]["PresidentEmail"].ToString() : null);
                objAppInfo.PresidentPhone = (dt.Rows[0]["PresidentPhone"] != DBNull.Value ? dt.Rows[0]["PresidentPhone"].ToString() : null);
                objAppInfo.SecretaryEmail = (dt.Rows[0]["SecretaryEmail"] != DBNull.Value ? dt.Rows[0]["SecretaryEmail"].ToString() : null);
                objAppInfo.SecretaryPhone = (dt.Rows[0]["SecretaryPhone"] != DBNull.Value ? dt.Rows[0]["SecretaryPhone"].ToString() : null);
                objAppInfo.CustomerCareNumber = (dt.Rows[0]["CustomerCareNumber"] != DBNull.Value ? dt.Rows[0]["CustomerCareNumber"].ToString() : null);
                objAppInfo.TollFreeNumber = (dt.Rows[0]["TollFreeNumber"] != DBNull.Value ? dt.Rows[0]["TollFreeNumber"].ToString() : null);
                objAppInfo.FacebookUrl = (dt.Rows[0]["FacebookUrl"] != DBNull.Value ? dt.Rows[0]["FacebookUrl"].ToString() : null);
                objAppInfo.TwitterUrl = (dt.Rows[0]["TwitterUrl"] != DBNull.Value ? dt.Rows[0]["TwitterUrl"].ToString() : null);
                objAppInfo.YoutubeUrl = (dt.Rows[0]["YoutubeUrl"] != DBNull.Value ? dt.Rows[0]["YoutubeUrl"].ToString() : null);
                objAppInfo.SupportEmail = (dt.Rows[0]["SupportEmail"] != DBNull.Value ? dt.Rows[0]["SupportEmail"].ToString() : null);
                objAppInfo.EnqueryEmail = (dt.Rows[0]["EnqueryEmail"] != DBNull.Value ? dt.Rows[0]["EnqueryEmail"].ToString() : null);
                objAppInfo.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objAppInfo.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objAppInfo.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objAppInfo.Topline = (dt.Rows[0]["Topline"] != DBNull.Value ? dt.Rows[0]["Topline"].ToString() : null);
                objAppInfo.PageItems = (dt.Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageItems"].ToString()) : 0);
                objAppInfo.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                objAppInfo.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
                objAppInfo.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                objAppInfo.BaseUrl = (dt.Rows[0]["BaseUrl"] != DBNull.Value ? dt.Rows[0]["BaseUrl"].ToString() : null);
                objAppInfo.UploadPath = (dt.Rows[0]["UploadPath"] != DBNull.Value ? dt.Rows[0]["UploadPath"].ToString() : null);
                objAppInfo.UserUploadPath = (dt.Rows[0]["UserUploadPath"] != DBNull.Value ? dt.Rows[0]["UserUploadPath"].ToString() : null);
                objAppInfo.UserSiteUrl = (dt.Rows[0]["UserSiteUrl"] != DBNull.Value ? dt.Rows[0]["UserSiteUrl"].ToString() : null);
                objAppInfo.ServerMapUrl = (dt.Rows[0]["ServerMapUrl"] != DBNull.Value ? dt.Rows[0]["ServerMapUrl"].ToString() : null);
                objAppInfo.AdminImageUrl = (dt.Rows[0]["AdminImageUrl"] != DBNull.Value ? dt.Rows[0]["AdminImageUrl"].ToString() : null);
                objAppInfo.AdminSiteUrl = (dt.Rows[0]["AdminSiteUrl"] != DBNull.Value ? dt.Rows[0]["AdminSiteUrl"].ToString() : null);
                objAppInfo.MailName = (dt.Rows[0]["MailName"] != DBNull.Value ? dt.Rows[0]["MailName"].ToString() : null);
                objAppInfo.SenderEmail = (dt.Rows[0]["SenderEmail"] != DBNull.Value ? dt.Rows[0]["SenderEmail"].ToString() : null);
                objAppInfo.MemberEmail = (dt.Rows[0]["MemberEmail"] != DBNull.Value ? dt.Rows[0]["MemberEmail"].ToString() : null);
                objAppInfo.ExhibitEmail = (dt.Rows[0]["ExhibitEmail"] != DBNull.Value ? dt.Rows[0]["ExhibitEmail"].ToString() : null);
                objAppInfo.EventsEmail = (dt.Rows[0]["EventsEmail"] != DBNull.Value ? dt.Rows[0]["EventsEmail"].ToString() : null);
                objAppInfo.ContactEmail = (dt.Rows[0]["ContactEmail"] != DBNull.Value ? dt.Rows[0]["ContactEmail"].ToString() : null);
                objAppInfo.DonationEmail = (dt.Rows[0]["DonationEmail"] != DBNull.Value ? dt.Rows[0]["DonationEmail"].ToString() : null);
                objAppInfo.VolunteerEmail = (dt.Rows[0]["VolunteerEmail"] != DBNull.Value ? dt.Rows[0]["VolunteerEmail"].ToString() : null);
                objAppInfo.SponsorshipEmail = (dt.Rows[0]["SponsorshipEmail"] != DBNull.Value ? dt.Rows[0]["SponsorshipEmail"].ToString() : null);
                objAppInfo.BrevoKey = (dt.Rows[0]["BrevoKey"] != DBNull.Value ? dt.Rows[0]["BrevoKey"].ToString() : null);
                objAppInfo.AndroidVersion = (dt.Rows[0]["AndroidVersion"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["AndroidVersion"]) : 0);
                objAppInfo.IOSVersion = (dt.Rows[0]["IOSVersion"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["IOSVersion"]) : 0);
                objAppInfo.DesktopVersion = (dt.Rows[0]["DesktopVersion"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DesktopVersion"]) : 0);
                objAppInfo.AppUpdate = (dt.Rows[0]["AppUpdate"] != DBNull.Value ? dt.Rows[0]["AppUpdate"].ToString() : null);
                objAppInfo.CapchaSiteKey = (dt.Rows[0]["CapchaSiteKey"] != DBNull.Value ? dt.Rows[0]["CapchaSiteKey"].ToString() : null);
                objAppInfo.CapchaSecreatKey = (dt.Rows[0]["CapchaSecreatKey"] != DBNull.Value ? dt.Rows[0]["CapchaSecreatKey"].ToString() : null);
                objAppInfo.ShowCapcha = (dt.Rows[0]["ShowCapcha"] != DBNull.Value ? dt.Rows[0]["ShowCapcha"].ToString() : null);
                objAppInfo.GooglePlusUrl = (dt.Rows[0]["GooglePlusUrl"] != DBNull.Value ? dt.Rows[0]["GooglePlusUrl"].ToString() : null);
                objAppInfo.InstagramUrl = (dt.Rows[0]["InstagramUrl"] != DBNull.Value ? dt.Rows[0]["InstagramUrl"].ToString() : null);
                objAppInfo.WhatsappNumber = (dt.Rows[0]["WhatsappNumber"] != DBNull.Value ? dt.Rows[0]["WhatsappNumber"].ToString() : null);

                ////Developer settings
                //objAppInfo.IsQRCode = (dt.Rows[0]["IsQRCode"] != DBNull.Value ? dt.Rows[0]["IsQRCode"].ToString() : null);
                //objAppInfo.EmaisToBrevo = (dt.Rows[0]["EmaisToBrevo"] != DBNull.Value ? dt.Rows[0]["EmaisToBrevo"].ToString() : null);
                //objAppInfo.IsPromoCodes = (dt.Rows[0]["IsPromoCodes"] != DBNull.Value ? dt.Rows[0]["IsPromoCodes"].ToString() : null);
                //objAppInfo.IsCultural = (dt.Rows[0]["IsCultural"] != DBNull.Value ? dt.Rows[0]["IsCultural"].ToString() : null);
                //objAppInfo.IsSports = (dt.Rows[0]["IsSports"] != DBNull.Value ? dt.Rows[0]["IsSports"].ToString() : null);
                //objAppInfo.IsHelpDocument = (dt.Rows[0]["IsHelpDocument"] != DBNull.Value ? dt.Rows[0]["IsHelpDocument"].ToString() : null);
                //objAppInfo.IsFeaturesDocument = (dt.Rows[0]["IsFeaturesDocument"] != DBNull.Value ? dt.Rows[0]["IsFeaturesDocument"].ToString() : null);
                //objAppInfo.TimeZones = (dt.Rows[0]["TimeZones"] != DBNull.Value ? dt.Rows[0]["TimeZones"].ToString() : null);

                //objAppInfo.chapterStatus = (dt.Rows[0]["chapterStatus"] != DBNull.Value ? dt.Rows[0]["chapterStatus"].ToString() : null);
                objAppInfo.WhatsappScript = (dt.Rows[0]["WhatsappScript"] != DBNull.Value ? dt.Rows[0]["WhatsappScript"].ToString() : null);
                objAppInfo.GoogleAnalyticsScript = (dt.Rows[0]["GoogleAnalyticsScript"] != DBNull.Value ? dt.Rows[0]["GoogleAnalyticsScript"].ToString() : null);
                objAppInfo.Email = (dt.Rows[0]["Email"] != DBNull.Value ? dt.Rows[0]["Email"].ToString() : null);
                objAppInfo.CAPTCHA = (dt.Rows[0]["CAPTCHA"] != DBNull.Value ? dt.Rows[0]["CAPTCHA"].ToString() : null);
                objAppInfo.LayoutLogo = (dt.Rows[0]["LayoutLogo"] != DBNull.Value ? dt.Rows[0]["LayoutLogo"].ToString() : null);
                objAppInfo.faviconlogo = (dt.Rows[0]["faviconlogo"] != DBNull.Value ? dt.Rows[0]["faviconlogo"].ToString() : null);
                objAppInfo.Loginlogo = (dt.Rows[0]["Loginlogo"] != DBNull.Value ? dt.Rows[0]["Loginlogo"].ToString() : null);
                objAppInfo.MailLogo = (dt.Rows[0]["MailLogo"] != DBNull.Value ? dt.Rows[0]["MailLogo"].ToString() : null);




            }
            return objAppInfo;
        }

        public Entities.AppInfo GetAppInfoDetailsByChapterId(Int64 ChapterId, ref int Status)
        {
            DataTable dt = _AppInfo.GetAppInfoDetailsByChapterId(ChapterId, ref Status);
            Entities.AppInfo objAppInfo = new Entities.AppInfo();

            if (Status == 1 && dt.Rows.Count == 1)
            {
                objAppInfo.AppInfoId = Convert.ToInt64(dt.Rows[0]["AppInfoId"]);
                objAppInfo.SiteName = dt.Rows[0]["SiteName"].ToString();
                objAppInfo.CompanyAddress = (dt.Rows[0]["CompanyAddress"] != DBNull.Value ? dt.Rows[0]["CompanyAddress"].ToString() : null);
                objAppInfo.CompanyWebSite = (dt.Rows[0]["CompanyWebSite"] != DBNull.Value ? dt.Rows[0]["CompanyWebSite"].ToString() : null);
                objAppInfo.CompanyEmail = (dt.Rows[0]["CompanyEmail"] != DBNull.Value ? dt.Rows[0]["CompanyEmail"].ToString() : null);
                objAppInfo.CompanyPhone = (dt.Rows[0]["CompanyPhone"] != DBNull.Value ? dt.Rows[0]["CompanyPhone"].ToString() : null);
                objAppInfo.PresidentEmail = (dt.Rows[0]["PresidentEmail"] != DBNull.Value ? dt.Rows[0]["PresidentEmail"].ToString() : null);
                objAppInfo.PresidentPhone = (dt.Rows[0]["PresidentPhone"] != DBNull.Value ? dt.Rows[0]["PresidentPhone"].ToString() : null);
                objAppInfo.SecretaryEmail = (dt.Rows[0]["SecretaryEmail"] != DBNull.Value ? dt.Rows[0]["SecretaryEmail"].ToString() : null);
                objAppInfo.SecretaryPhone = (dt.Rows[0]["SecretaryPhone"] != DBNull.Value ? dt.Rows[0]["SecretaryPhone"].ToString() : null);
                objAppInfo.CustomerCareNumber = (dt.Rows[0]["CustomerCareNumber"] != DBNull.Value ? dt.Rows[0]["CustomerCareNumber"].ToString() : null);
                objAppInfo.TollFreeNumber = (dt.Rows[0]["TollFreeNumber"] != DBNull.Value ? dt.Rows[0]["TollFreeNumber"].ToString() : null);
                objAppInfo.FacebookUrl = (dt.Rows[0]["FacebookUrl"] != DBNull.Value ? dt.Rows[0]["FacebookUrl"].ToString() : null);
                objAppInfo.TwitterUrl = (dt.Rows[0]["TwitterUrl"] != DBNull.Value ? dt.Rows[0]["TwitterUrl"].ToString() : null);
                objAppInfo.YoutubeUrl = (dt.Rows[0]["YoutubeUrl"] != DBNull.Value ? dt.Rows[0]["YoutubeUrl"].ToString() : null);
                objAppInfo.SupportEmail = (dt.Rows[0]["SupportEmail"] != DBNull.Value ? dt.Rows[0]["SupportEmail"].ToString() : null);
                objAppInfo.EnqueryEmail = (dt.Rows[0]["EnqueryEmail"] != DBNull.Value ? dt.Rows[0]["EnqueryEmail"].ToString() : null);
                objAppInfo.PageTitle = (dt.Rows[0]["PageTitle"] != DBNull.Value ? dt.Rows[0]["PageTitle"].ToString() : null);
                objAppInfo.MetaDescription = (dt.Rows[0]["MetaDescription"] != DBNull.Value ? dt.Rows[0]["MetaDescription"].ToString() : null);
                objAppInfo.MetaKeywords = (dt.Rows[0]["MetaKeywords"] != DBNull.Value ? dt.Rows[0]["MetaKeywords"].ToString() : null);
                objAppInfo.Topline = (dt.Rows[0]["Topline"] != DBNull.Value ? dt.Rows[0]["Topline"].ToString() : null);
                objAppInfo.PageItems = (dt.Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["PageItems"].ToString()) : 0);
                objAppInfo.UpdatedBy = (dt.Rows[0]["UpdatedBy"] != DBNull.Value ? dt.Rows[0]["UpdatedBy"].ToString() : null);
                objAppInfo.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
                objAppInfo.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);

            }
            return objAppInfo;
        }

        #endregion

        #endregion

        #region Front End

        public void FEGetListInitialFlyer(ref Entities.Flyers objFlyers, ref int status)
        {
            DataSet ds = _AppInfo.FEGetListInitialFlyer(ref status);
            // About Us	
            if (ds.Tables[0].Rows.Count != 0)
            {
                objFlyers.FlyerId = Convert.ToInt64(ds.Tables[0].Rows[0]["FlyerId"]);
                objFlyers.FlyerUrl = ds.Tables[0].Rows[0]["FlyerUrl"].ToString();
                objFlyers.RedirectUrl = ds.Tables[0].Rows[0]["RedirectUrl"].ToString();
                objFlyers.PageContent = ds.Tables[0].Rows[0]["PageContent"].ToString();
            }

        }


        //public void FEFinalGetListInitialLoad(
        //    Int64 ChapterId,

        //    ref Entities.PageDetails objPInnerPages,
        //    ref Entities.PageDetails objWInnerPages,



        //    ref List<Entities.Sponsors> lstSponsors,
        //    ref List<Entities.SponsorCategories> lstSponsorCategories,
        //    ref List<Entities.CommitteeCategories> lstCommitteeCategories,


        //    ref List<Entities.MenuItems> lstMenuItems,
        //    ref List<Entities.MenuItems> lstMenuItems2,
        //    ref List<Entities.MenuItems> lstMenuItems3,
        //    ref List<Entities.MenuItems> lstMenuItems4,
        //    ref List<Entities.MenuItems> FooterMenuItems,
        //     ref Entities.AppInfo objAppInfo,
        //     ref Entities.PageDetails objvInnerPages,
        //     ref List<Entities.MenuItems> QuickLinkItems,
        //     ref List<Entities.Chapters> lstChapters,
        //              ref List<Entities.Photos> lstPhotos,
        //    ref List<Entities.Videos> lstVideos,
        //    ref int status)
        //{
        //    DataSet ds = _AppInfo.FEFinalGetListInitialLoad(ChapterId, ref status);

        //    //WebsiteBanners List   


        //    //President Message
        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        objPInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    //Welcome Message
        //    if (ds.Tables[1].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[1];
        //        objWInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objWInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objWInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }




        //    // Sponsors List  
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objHTCASponsors);
        //        }
        //    }

        //    //Sponsor Categories 
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[3].Rows)
        //        {
        //            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }

        //    //Committee Categories 
        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            AWSFormBuilder.Entities.CommitteeCategories objCommitteeCategories = new AWSFormBuilder.Entities.CommitteeCategories();

        //            objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
        //            objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
        //            objCommitteeCategories.Type = dr["Type"].ToString();
        //            lstCommitteeCategories.Add(objCommitteeCategories);
        //        }
        //    }





        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //        }
        //    }

        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[6].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            FooterMenuItems.Add(objMenuItems);
        //        }
        //    }

        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        if (ds.Tables[7].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[7].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[7].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[7].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[7].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[7].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[7].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[7].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[7].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[7].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[7].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[7].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[7].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[7].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[7].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[7].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[7].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[7].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[7].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[7].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[7].Rows[0]["UpdatedTime"]);
        //            objAppInfo.PresidentPhone = (ds.Tables[7].Rows[0]["PresidentPhone"] != DBNull.Value ? ds.Tables[7].Rows[0]["PresidentPhone"].ToString() : "");
        //            objAppInfo.chapterStatus = (ds.Tables[7].Rows[0]["chapterStatus"] != DBNull.Value ? ds.Tables[7].Rows[0]["chapterStatus"].ToString() : "");

        //        }
        //    }




        //    // Welcome Message
        //    if (ds.Tables[8].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[8];
        //        objvInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objvInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objvInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[9].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            QuickLinkItems.Add(objMenuItems);
        //        }
        //    }

        //    // Chapter list 
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[10].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }







        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            AWSFormBuilder.Entities.Photos objPhotos = new AWSFormBuilder.Entities.Photos();

        //            objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"].ToString());
        //            //objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"].ToString());
        //            objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
        //            objPhotos.ImageDescription = (dr["ImageDescription"] != DBNull.Value ? dr["ImageDescription"].ToString() : null);
        //            objPhotos.AlbumLink = (dr["AlbumLink"] != DBNull.Value ? dr["AlbumLink"].ToString() : null);
        //            objPhotos.CategoryName = (dr["CategoryName"] != DBNull.Value ? dr["CategoryName"].ToString() : null);

        //            lstPhotos.Add(objPhotos);
        //        }
        //    }

        //    // Videos List  
        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[12].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstVideos.Add(objVideos);
        //        }
        //    }












        //}
        //public void FEFinalGetListInitialLoad(
        //    Int64 ChapterId,

        //    ref Entities.PageDetails objPInnerPages,
        //    ref Entities.PageDetails objWInnerPages,



        //    ref List<Entities.Sponsors> lstSponsors,
        //    ref List<Entities.SponsorCategories> lstSponsorCategories,
        //    ref List<Entities.CommitteeCategories> lstCommitteeCategories,


        //    ref List<Entities.MenuItems> lstMenuItems,
        //    ref List<Entities.MenuItems> lstMenuItems2,
        //    ref List<Entities.MenuItems> lstMenuItems3,
        //    ref List<Entities.MenuItems> lstMenuItems4,
        //    ref List<Entities.MenuItems> FooterMenuItems,
        //     ref Entities.AppInfo objAppInfo,
        //     ref Entities.PageDetails objvInnerPages,
        //     ref List<Entities.MenuItems> QuickLinkItems,
        //     ref List<Entities.Chapters> lstChapters,
        //              ref List<Entities.Photos> lstPhotos,
        //    ref List<Entities.Videos> lstVideos,
        //    ref int status)
        //{
        //    DataSet ds = _AppInfo.FEFinalGetListInitialLoad(ChapterId, ref status);

        //    //WebsiteBanners List   


        //    //President Message
        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        objPInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    //Welcome Message
        //    if (ds.Tables[1].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[1];
        //        objWInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objWInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objWInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }




        //    // Sponsors List  
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objHTCASponsors);
        //        }
        //    }

        //    //Sponsor Categories 
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[3].Rows)
        //        {
        //            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }

        //    //Committee Categories 
        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            AWSFormBuilder.Entities.CommitteeCategories objCommitteeCategories = new AWSFormBuilder.Entities.CommitteeCategories();

        //            objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
        //            objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
        //            objCommitteeCategories.Type = dr["Type"].ToString();
        //            lstCommitteeCategories.Add(objCommitteeCategories);
        //        }
        //    }





        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //        }
        //    }

        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[6].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            FooterMenuItems.Add(objMenuItems);
        //        }
        //    }

        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        if (ds.Tables[7].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[7].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[7].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[7].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[7].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[7].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[7].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[7].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[7].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[7].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[7].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[7].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[7].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[7].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[7].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[7].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[7].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[7].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[7].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[7].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[7].Rows[0]["UpdatedTime"]);
        //            objAppInfo.PresidentPhone = (ds.Tables[7].Rows[0]["PresidentPhone"] != DBNull.Value ? ds.Tables[7].Rows[0]["PresidentPhone"].ToString() : "");
        //            objAppInfo.chapterStatus = (ds.Tables[7].Rows[0]["chapterStatus"] != DBNull.Value ? ds.Tables[7].Rows[0]["chapterStatus"].ToString() : "");

        //        }
        //    }




        //   // Welcome Message
        //    if (ds.Tables[8].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[8];
        //        objvInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objvInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objvInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[9].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            QuickLinkItems.Add(objMenuItems);
        //        }
        //    }

        //    // Chapter list 
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[10].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }







        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            AWSFormBuilder.Entities.Photos objPhotos = new AWSFormBuilder.Entities.Photos();

        //            objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"].ToString());
        //            //objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"].ToString());
        //            objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
        //            objPhotos.ImageDescription = (dr["ImageDescription"] != DBNull.Value ? dr["ImageDescription"].ToString() : null);
        //            objPhotos.AlbumLink = (dr["AlbumLink"] != DBNull.Value ? dr["AlbumLink"].ToString() : null);
        //            objPhotos.CategoryName = (dr["CategoryName"] != DBNull.Value ? dr["CategoryName"].ToString() : null);

        //            lstPhotos.Add(objPhotos);
        //        }
        //    }

        //    // Videos List  
        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[12].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstVideos.Add(objVideos);
        //        }
        //    }












        //}

        //public void HomeAPI(
        //    Int64 ChapterId,

        //    ref Entities.PageDetails objPInnerPages,
        //    ref Entities.PageDetails objWInnerPages,



        //    ref List<Entities.Sponsors> lstSponsors,
        //    ref List<Entities.SponsorCategories> lstSponsorCategories,
        //    ref List<Entities.CommitteeCategories> lstCommitteeCategories,


        //    ref List<Entities.MenuItems> lstMenuItems,
        //    ref List<Entities.MenuItems> lstMenuItems2,
        //    ref List<Entities.MenuItems> lstMenuItems3,
        //    ref List<Entities.MenuItems> lstMenuItems4,
        //    ref List<Entities.MenuItems> FooterMenuItems,
        //     ref Entities.AppInfo objAppInfo,
        //     ref Entities.PageDetails objvInnerPages,
        //     ref List<Entities.MenuItems> QuickLinkItems,
        //     ref List<Entities.Chapters> lstChapters,
        //              ref List<Entities.Photos> lstPhotos,
        //    ref List<Entities.Videos> lstVideos,
        //    ref List<Entities.WebsiteBanners> lstWebsiteBanners,
        //    ref int status)
        //{
        //    DataSet ds = _AppInfo.HomeAPI(ChapterId, ref status);

        //    //WebsiteBanners List   


        //    //President Message
        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        objPInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    //Welcome Message
        //    if (ds.Tables[1].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[1];
        //        objWInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objWInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objWInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }




        //    // Sponsors List  
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objHTCASponsors);
        //        }
        //    }

        //    //Sponsor Categories 
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[3].Rows)
        //        {
        //            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }

        //    //Committee Categories 
        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            AWSFormBuilder.Entities.CommitteeCategories objCommitteeCategories = new AWSFormBuilder.Entities.CommitteeCategories();

        //            objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
        //            objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();
        //            objCommitteeCategories.Type = dr["Type"].ToString();
        //            lstCommitteeCategories.Add(objCommitteeCategories);
        //        }
        //    }





        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //        }
        //    }

        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[6].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            FooterMenuItems.Add(objMenuItems);
        //        }
        //    }

        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        if (ds.Tables[7].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[7].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[7].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[7].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[7].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[7].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[7].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[7].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[7].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[7].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[7].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[7].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[7].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[7].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[7].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[7].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[7].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[7].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[7].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[7].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[7].Rows[0]["UpdatedTime"]);
        //            objAppInfo.PresidentPhone = (ds.Tables[7].Rows[0]["PresidentPhone"] != DBNull.Value ? ds.Tables[7].Rows[0]["PresidentPhone"].ToString() : "");
        //            objAppInfo.chapterStatus = (ds.Tables[7].Rows[0]["chapterStatus"] != DBNull.Value ? ds.Tables[7].Rows[0]["chapterStatus"].ToString() : "");

        //        }
        //    }




        //   // Welcome Message
        //    if (ds.Tables[8].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[8];
        //        objvInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objvInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objvInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[9].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            QuickLinkItems.Add(objMenuItems);
        //        }
        //    }

        //    // Chapter list 
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[10].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }







        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            AWSFormBuilder.Entities.Photos objPhotos = new AWSFormBuilder.Entities.Photos();

        //            objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"].ToString());
        //            //objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"].ToString());
        //            objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : null);
        //            objPhotos.ImageDescription = (dr["ImageDescription"] != DBNull.Value ? dr["ImageDescription"].ToString() : null);
        //            objPhotos.AlbumLink = (dr["AlbumLink"] != DBNull.Value ? dr["AlbumLink"].ToString() : null);
        //            objPhotos.CategoryName = (dr["CategoryName"] != DBNull.Value ? dr["CategoryName"].ToString() : null);

        //            lstPhotos.Add(objPhotos);
        //        }
        //    }

        //    // Videos List  
        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[12].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstVideos.Add(objVideos);
        //        }
        //    }

        //    // Sponsors List  
        //    if (ds.Tables[13].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[13].Rows)
        //        {
        //            AWSFormBuilder.Entities.WebsiteBanners objWebsiteBanners = new AWSFormBuilder.Entities.WebsiteBanners();

        //            objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"].ToString());
        //            objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
        //            objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
        //            objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objWebsiteBanners.Target = dr["Target"].ToString();
        //            lstWebsiteBanners.Add(objWebsiteBanners);
        //        }
        //    }











        //}


        //public void APIMenus(
        //    Int64 ChapterId,



        //    ref List<Entities.MenuItems> lstMenuItems,
        //    ref List<Entities.MenuItems> lstMenuItems2,
        //    ref List<Entities.MenuItems> lstMenuItems3,
        //    ref List<Entities.MenuItems> lstMenuItems4,
        //     ref List<Entities.MenuItems> QuickLinkItems,
        //     ref List<Entities.Chapters> lstChapters,
        //    ref int status)
        //{
        //    DataSet ds = _AppInfo.APIMenus(ChapterId, ref status);




        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //        }
        //    }

        //    if (ds.Tables[1].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[1].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            QuickLinkItems.Add(objMenuItems);
        //        }
        //    }

        //    // Chapter list 
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }
        //}
        //public void FEGetListInitialLoad(
        //    Int64 ChapterId,
        //    ref List<Entities.News> lstNews,
        //    ref Entities.PageDetails objWInnerPages,
        //    ref Entities.PageDetails objPInnerPages,
        //    ref List<Entities.WebsiteBanners> lstWebsiteBanners,
        //    ref List<Entities.Events> lstUpcommingEvents,
        //    ref List<Entities.Sponsors> lstMediaSponsors,
        //    ref List<Entities.MenuItems> lstMenuItems,
        //    ref List<Entities.MenuItems> lstMenuItems2,
        //    ref List<Entities.MenuItems> lstMenuItems3,
        //    ref List<Entities.MenuItems> lstMenuItems4,
        //    ref List<Entities.MenuItems> FooterMenuItems,
        //    ref List<Entities.Photos> lstPhotos,
        //    ref List<Entities.Videos> lstVideos,
        //    ref Entities.AppInfo objAppInfo,
        //    ref List<Entities.Chapters> lstChapters,
        //    ref List<Entities.SponsorCategories> lstSponsorCategories,
        //    ref List<Entities.Events> lstEvents,
        //    ref Entities.PageDetails objNatsMissionInnerPages,
        //    ref List<Entities.Sponsors> lstSponsors,
        //    ref Entities.PageDetails objChapterInnerPages,
        //    ref List<Entities.Services> lstServices,
        //    ref List<Entities.Videos> lstWebinars,
        //    ref int status)
        //{
        //    DataSet ds = _AppInfo.FEGetListInitialLoad(ChapterId, ref status);

        //    //News List
        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Entities.News objNews = new Entities.News();

        //            objNews.NewsId = (dr["NewsId"] != DBNull.Value ? Convert.ToInt64(dr["NewsId"]) : 0);
        //            objNews.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterId"]) : 0);
        //            objNews.PostDate = (dr["PostDate"] != DBNull.Value ? Convert.ToDateTime(dr["PostDate"]) : DateTime.MinValue);
        //            objNews.NewsText = (dr["NewsText"] != DBNull.Value ? dr["NewsText"].ToString() : "");
        //            objNews.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : "");
        //            objNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "");
        //            objNews.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : "");

        //            lstNews.Add(objNews);
        //        }
        //    }

        //    //President Message
        //    if (ds.Tables[1].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[1];

        //        objWInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objWInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objWInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    //President Message
        //    if (ds.Tables[2].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[2];

        //        objPInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }


        //    //WebsiteBanners List   
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[3].Rows)
        //        {
        //            Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();

        //            objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"]);
        //            objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
        //            objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
        //            objWebsiteBanners.Target = dr["Target"].ToString();
        //            objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstWebsiteBanners.Add(objWebsiteBanners);
        //        }
        //    }

        //    // Upcomming Events List 
        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            Entities.Events objUpcommingEvents = new Entities.Events();

        //            objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EventName = dr["EventName"].ToString();
        //            objUpcommingEvents.Location = dr["Location"].ToString();
        //            objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
        //            objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
        //            objUpcommingEvents.City = dr["City"].ToString();
        //            objUpcommingEvents.StateName = dr["StateName"].ToString();
        //            objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        //            objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        //            objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.UpdatedBy = dr["UpdatedBy"].ToString();

        //            lstUpcommingEvents.Add(objUpcommingEvents);
        //        }
        //    }

        //    // Media List  
        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {

        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.Target = dr["Target"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstMediaSponsors.Add(objHTCASponsors);
        //        }
        //    }

        //    //AppInfo List  
        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        if (ds.Tables[6].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[6].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[6].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[6].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[6].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[6].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[6].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[6].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[6].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[6].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[6].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[6].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[6].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[6].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[6].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[6].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[6].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[6].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[6].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[6].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[6].Rows[0]["UpdatedTime"]);

        //        }
        //    }


        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[7].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //        }
        //    }

        //    if (ds.Tables[8].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[8].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            FooterMenuItems.Add(objMenuItems);
        //        }
        //    }

        //    // Photos List  
        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[9].Rows)
        //        {
        //            Entities.Photos objPhotos = new Entities.Photos();

        //            objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"]);
        //            objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"]);
        //            objPhotos.ImageUrl = dr["ImageUrl"].ToString();
        //            objPhotos.AlbumLink = dr["AlbumLink"].ToString();
        //            objPhotos.ImageDescription = dr["ImageDescription"].ToString();
        //            objPhotos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstPhotos.Add(objPhotos);
        //        }
        //    }

        //    // Videos List  
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[10].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstVideos.Add(objVideos);
        //        }
        //    }

        //    // Chapter list 
        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }

        //    //Sponsor Categories 
        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[12].Rows)
        //        {
        //            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }

        //    // Events List 
        //    if (ds.Tables[13].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[13].Rows)
        //        {
        //            Entities.Events objEvents = new Entities.Events();

        //            objEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objEvents.EventName = dr["EventName"].ToString();

        //            lstEvents.Add(objEvents);
        //        }
        //    }

        //    //NATS Mission
        //    if (ds.Tables[14].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[14];

        //        objNatsMissionInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objNatsMissionInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objNatsMissionInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    //Sponsers list
        //    if (ds.Tables[15].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[15].Rows)
        //        {

        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.Target = dr["Target"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objHTCASponsors);
        //        }
        //    }

        //    //Chapter News
        //    if (ds.Tables[16].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[16];

        //        objChapterInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
        //        objChapterInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objChapterInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }

        //    // Media List  
        //    if (ds.Tables[17].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[17].Rows)
        //        {

        //            Entities.Services objServices = new Entities.Services();

        //            objServices.ServiceId = Convert.ToInt64(dr["ServiceId"]);
        //            objServices.ServiceTitle = (dr["ServiceTitle"] != DBNull.Value ? dr["ServiceTitle"].ToString() : "");
        //            objServices.EstimationAmount = (dr["EstimationAmount"] != DBNull.Value ? Convert.ToInt32(dr["EstimationAmount"]) : 0);
        //            objServices.ShortDescription = (dr["ShortDescription"] != DBNull.Value ? dr["ShortDescription"].ToString() : "");
        //            objServices.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "");
        //            objServices.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "");
        //            objServices.TotalAmount = (dr["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TotalAmount"]) : 0);

        //            lstServices.Add(objServices);
        //        }
        //    }

        //    // Webinar List  
        //    if (ds.Tables[18].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[18].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstWebinars.Add(objVideos);
        //        }
        //    }

        //}


        public void FEGetListAppInfo(ref Entities.AppInfo objAppInfo, ref List<Entities.News> lstLatestNews, ref int status)
        {
            DataSet ds = _AppInfo.FEGetListAppInfo(ref status);

            //AppInfo List  
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[0].Rows[0]["AppInfoId"]);
                    objAppInfo.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    objAppInfo.CompanyAddress = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                    objAppInfo.CompanyWebSite = ds.Tables[0].Rows[0]["CompanyWebSite"].ToString();
                    objAppInfo.CompanyEmail = ds.Tables[0].Rows[0]["CompanyEmail"].ToString();
                    objAppInfo.CompanyPhone = ds.Tables[0].Rows[0]["CompanyPhone"].ToString();
                    objAppInfo.PresidentPhone = ds.Tables[0].Rows[0]["PresidentPhone"].ToString();
                    objAppInfo.PresidentEmail = ds.Tables[0].Rows[0]["PresidentEmail"].ToString();
                    objAppInfo.SecretaryEmail = ds.Tables[0].Rows[0]["SecretaryEmail"].ToString();
                    objAppInfo.SecretaryPhone = ds.Tables[0].Rows[0]["SecretaryPhone"].ToString();
                    objAppInfo.CustomerCareNumber = ds.Tables[0].Rows[0]["CustomerCareNumber"].ToString();
                    objAppInfo.TollFreeNumber = ds.Tables[0].Rows[0]["TollFreeNumber"].ToString();
                    objAppInfo.FacebookUrl = ds.Tables[0].Rows[0]["FacebookUrl"].ToString();
                    objAppInfo.TwitterUrl = ds.Tables[0].Rows[0]["TwitterUrl"].ToString();
                    objAppInfo.YoutubeUrl = ds.Tables[0].Rows[0]["YoutubeUrl"].ToString();
                    objAppInfo.SupportEmail = ds.Tables[0].Rows[0]["SupportEmail"].ToString();
                    objAppInfo.EnqueryEmail = ds.Tables[0].Rows[0]["EnqueryEmail"].ToString();
                    objAppInfo.PageTitle = ds.Tables[0].Rows[0]["PageTitle"].ToString();
                    objAppInfo.MetaDescription = ds.Tables[0].Rows[0]["MetaDescription"].ToString();
                    objAppInfo.MetaKeywords = ds.Tables[0].Rows[0]["MetaKeywords"].ToString();
                    objAppInfo.Topline = ds.Tables[0].Rows[0]["Topline"].ToString();
                    objAppInfo.PageItems = (ds.Tables[0].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[0].Rows[0]["PageItems"]) : 0);
                    objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedTime"]);
                }
            }

            //LatestNews List
            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.News objLatestNews = new Entities.News();

                    objLatestNews.NewsId = Convert.ToInt64(dr["NewsId"]);
                    objLatestNews.Title = dr["Title"].ToString();
                    objLatestNews.NewsText = dr["NewsText"].ToString();
                    objLatestNews.ImageUrl = dr["ImageUrl"].ToString();
                    objLatestNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
                    objLatestNews.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt64(dr["OrderNo"]) : 0);
                    objLatestNews.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

                    lstLatestNews.Add(objLatestNews);
                }
            }

          
        }

        //public Entities.PageDetails FEGetListMainLayout(
        //     Int64 ChapterId,
        //     string headingName,
        //     string Email,
        //     ref List<Entities.News> lstNews,
        //     ref List<Entities.Sponsors> lstSponsors,
        //     ref List<Entities.SponsorCategories> lstSponsorCategories,
        //     ref Entities.AppInfo objAppInfo,
        //     ref List<Entities.MenuItems> lstMenuItems,
        //     ref List<Entities.MenuItems> lstMenuItems2,
        //     ref List<Entities.MenuItems> lstMenuItems3,
        //     ref List<Entities.MenuItems> lstMenuItems4,
        //     ref List<Entities.MenuItems> FooterMenuItems,
        //      ref Entities.PageDetails objPInnerPages,
        //      ref List<Entities.Events> lstEvents,
        //     ref Entities.PageDetails objChapterInnerPages,
        //     ref Entities.PageDetails objAksharaInnerPages,
        //     ref List<Entities.Sponsors> lstMediaSponsors,
        //     ref List<Entities.Chapters> lstChapters,
        //      ref List<Entities.MenuItems> lstQuickLinkItems,
        //      ref Entities.PageDetails objseodetails,
        //     ref int status)
        //{
        //    AWSFormBuilder.Entities.PageDetails objInnerPages = new AWSFormBuilder.Entities.PageDetails();
        //    DataSet ds = _AppInfo.FEGetListMainLayout(ChapterId, headingName, Email, ref status);
        //    //  (Session["ChapterId"] != null ? Convert.ToInt32(Session["EmpCompanyID"].ToString()) : 0);

        //    //News List
        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Entities.News objNews = new Entities.News();

        //            objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
        //            objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
        //            objNews.NewsText = dr["NewsText"].ToString();
        //            objNews.Title = dr["Title"].ToString();
        //            objNews.ImageUrl = dr["ImageUrl"].ToString();

        //            lstNews.Add(objNews);
        //        }
        //    }

        //    objInnerPages.lstNews = lstNews;

        //    // Sponsors List  
        //    if (ds.Tables[1].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[1].Rows)
        //        {
        //            Entities.Sponsors objSponsors = new Entities.Sponsors();

        //            objSponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objSponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objSponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objSponsors.Target = dr["Target"].ToString();
        //            objSponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objSponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objSponsors);
        //        }
        //    }

        //    objInnerPages.lstSponsors = lstSponsors;

        //    //Sponsor Categories 
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }
        //    objInnerPages.lstSponsorCategories = lstSponsorCategories;

        //    //AppInfo List  
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        if (ds.Tables[3].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[3].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[3].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[3].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[3].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[3].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[3].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[3].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[3].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[3].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[3].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[3].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[3].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[3].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[3].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[3].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[3].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[3].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[3].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[3].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[3].Rows[0]["UpdatedTime"]);
        //        }
        //    }

        //    objInnerPages.lstSponsorCategories = lstSponsorCategories;

        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems = lstMenuItems;

        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems2 = lstMenuItems2;

        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems3 = lstMenuItems3;

        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems4 = lstMenuItems4;

        //        }
        //    }
        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            FooterMenuItems.Add(objMenuItems);
        //        }
        //    }
        //    objInnerPages.FooterMenuItems = FooterMenuItems;


        //    //President Message
        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        if (ds.Tables[6].Rows.Count == 1)
        //        {
        //            objPInnerPages.PageDetailId = Convert.ToInt64(ds.Tables[6].Rows[0]["PageDetailId"]);
        //            objPInnerPages.Heading = ds.Tables[6].Rows[0]["Heading"].ToString();
        //            objPInnerPages.Description = ds.Tables[6].Rows[0]["Description"].ToString();

        //            objInnerPages.objPInnerPages = objPInnerPages;
        //        }
        //    }

        //    // Upcomming Events List 
        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[7].Rows)
        //        {
        //            Entities.Events objEvents = new Entities.Events();

        //            objEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objEvents.EventName = dr["EventName"].ToString();

        //            lstEvents.Add(objEvents);
        //        }
        //    }

        //    objInnerPages.lstEvents = lstEvents;

        //    //Chapters list
        //    if (ds.Tables[8].Rows.Count != 0)
        //    {
        //        if (ds.Tables[8].Rows.Count == 1)
        //        {
        //            objChapterInnerPages.PageDetailId = Convert.ToInt64(ds.Tables[8].Rows[0]["PageDetailId"]);
        //            objChapterInnerPages.Heading = ds.Tables[8].Rows[0]["Heading"].ToString();
        //            objChapterInnerPages.Description = ds.Tables[8].Rows[0]["Description"].ToString();

        //            objChapterInnerPages.objPInnerPages = objChapterInnerPages;
        //        }
        //    }

        //    //Akshara Message
        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        if (ds.Tables[9].Rows.Count == 1)
        //        {
        //            objAksharaInnerPages.PageDetailId = Convert.ToInt64(ds.Tables[9].Rows[0]["PageDetailId"]);
        //            objAksharaInnerPages.Heading = ds.Tables[9].Rows[0]["Heading"].ToString();
        //            objAksharaInnerPages.Description = ds.Tables[9].Rows[0]["Description"].ToString();

        //            objAksharaInnerPages.objPInnerPages = objAksharaInnerPages;
        //        }
        //    }
        //    // Sponsors List  
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[10].Rows)
        //        {
        //            Entities.Sponsors objSponsors = new Entities.Sponsors();

        //            objSponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objSponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objSponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objSponsors.Target = dr["Target"].ToString();
        //            objSponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objSponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstMediaSponsors.Add(objSponsors);
        //        }
        //    }
        //    objInnerPages.lstMediaSponsors = lstMediaSponsors;

        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }
        //    objInnerPages.lstChapters = lstChapters;




        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[12].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            lstQuickLinkItems.Add(objMenuItems);




        //        }
        //    }

        //    objInnerPages.lstQuickLinkItems = lstQuickLinkItems;


        //    //President Message
        //    if (ds.Tables[13].Rows.Count != 0)
        //    {
        //        if (ds.Tables[13].Rows.Count == 1)
        //        {
        //            objseodetails.PageDetailId = Convert.ToInt64(ds.Tables[13].Rows[0]["PageDetailId"]);
        //            objseodetails.Heading = ds.Tables[13].Rows[0]["Heading"].ToString();
        //            objseodetails.Description = ds.Tables[13].Rows[0]["Description"].ToString();
        //            objseodetails.PageTitle = (ds.Tables[13].Rows[0]["PageTitle"] != DBNull.Value ? ds.Tables[13].Rows[0]["PageTitle"].ToString() : "");
        //            objseodetails.MetaDescription = (ds.Tables[13].Rows[0]["MetaDescription"] != DBNull.Value ? ds.Tables[13].Rows[0]["MetaDescription"].ToString() : "");
        //            objseodetails.MetaKeywords = (ds.Tables[13].Rows[0]["MetaKeywords"] != DBNull.Value ? ds.Tables[13].Rows[0]["MetaKeywords"].ToString() : "");

        //            objInnerPages.objseodetails = objseodetails;
        //        }
        //    }

        //    return objInnerPages;
        //}

        //public Entities.PageDetails FEGetListMainLayout(
        //     Int64 ChapterId,
        //     string headingName,
        //     string Email,
        //     ref List<Entities.News> lstNews,
        //     ref List<Entities.Sponsors> lstSponsors,
        //     ref List<Entities.SponsorCategories> lstSponsorCategories,
        //     ref Entities.AppInfo objAppInfo,
        //     ref List<Entities.MenuItems> lstMenuItems,
        //     ref List<Entities.MenuItems> lstMenuItems2,
        //     ref List<Entities.MenuItems> lstMenuItems3,
        //     ref List<Entities.MenuItems> lstMenuItems4,
        //     ref List<Entities.MenuItems> FooterMenuItems,
        //      ref Entities.PageDetails objPInnerPages,
        //      ref List<Entities.Events> lstEvents,
        //     ref Entities.PageDetails objChapterInnerPages,
        //     ref Entities.PageDetails objAksharaInnerPages,
        //     ref List<Entities.Sponsors> lstMediaSponsors,
        //     ref List<Entities.Chapters> lstChapters,
        //      ref List<Entities.MenuItems> lstQuickLinkItems,
        //      ref Entities.PageDetails objseodetails,
        //     ref int status)
        //{
        //    AWSFormBuilder.Entities.PageDetails objInnerPages = new AWSFormBuilder.Entities.PageDetails();
        //    DataSet ds = _AppInfo.FEGetListMainLayout(ChapterId, headingName, Email, ref status);
        //    //  (Session["ChapterId"] != null ? Convert.ToInt32(Session["EmpCompanyID"].ToString()) : 0);

        //    //News List
        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Entities.News objNews = new Entities.News();

        //            objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
        //            objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
        //            objNews.NewsText = dr["NewsText"].ToString();
        //            objNews.Title = dr["Title"].ToString();
        //            objNews.ImageUrl = dr["ImageUrl"].ToString();

        //            lstNews.Add(objNews);
        //        }
        //    }

        //    objInnerPages.lstNews = lstNews;

        //    // Sponsors List  
        //    if (ds.Tables[1].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[1].Rows)
        //        {
        //            Entities.Sponsors objSponsors = new Entities.Sponsors();

        //            objSponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objSponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objSponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objSponsors.Target = dr["Target"].ToString();
        //            objSponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objSponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objSponsors);
        //        }
        //    }

        //    objInnerPages.lstSponsors = lstSponsors;

        //    //Sponsor Categories 
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        //            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        //            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        //            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        //            lstSponsorCategories.Add(objSponsorCategories);
        //        }
        //    }
        //    objInnerPages.lstSponsorCategories = lstSponsorCategories;

        //    //AppInfo List  
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        if (ds.Tables[3].Rows.Count == 1)
        //        {
        //            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[3].Rows[0]["AppInfoId"]);
        //            objAppInfo.SiteName = ds.Tables[3].Rows[0]["SiteName"].ToString();
        //            objAppInfo.CompanyAddress = ds.Tables[3].Rows[0]["CompanyAddress"].ToString();
        //            objAppInfo.CompanyWebSite = ds.Tables[3].Rows[0]["CompanyWebSite"].ToString();
        //            objAppInfo.CompanyEmail = ds.Tables[3].Rows[0]["CompanyEmail"].ToString();
        //            objAppInfo.CompanyPhone = ds.Tables[3].Rows[0]["CompanyPhone"].ToString();
        //            objAppInfo.CustomerCareNumber = ds.Tables[3].Rows[0]["CustomerCareNumber"].ToString();
        //            objAppInfo.TollFreeNumber = ds.Tables[3].Rows[0]["TollFreeNumber"].ToString();
        //            objAppInfo.FacebookUrl = ds.Tables[3].Rows[0]["FacebookUrl"].ToString();
        //            objAppInfo.TwitterUrl = ds.Tables[3].Rows[0]["TwitterUrl"].ToString();
        //            objAppInfo.YoutubeUrl = ds.Tables[3].Rows[0]["YoutubeUrl"].ToString();
        //            objAppInfo.SupportEmail = ds.Tables[3].Rows[0]["SupportEmail"].ToString();
        //            objAppInfo.EnqueryEmail = ds.Tables[3].Rows[0]["EnqueryEmail"].ToString();
        //            objAppInfo.PageTitle = ds.Tables[3].Rows[0]["PageTitle"].ToString();
        //            objAppInfo.MetaDescription = ds.Tables[3].Rows[0]["MetaDescription"].ToString();
        //            objAppInfo.MetaKeywords = ds.Tables[3].Rows[0]["MetaKeywords"].ToString();
        //            objAppInfo.Topline = ds.Tables[3].Rows[0]["Topline"].ToString();
        //            objAppInfo.PageItems = (ds.Tables[3].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[3].Rows[0]["PageItems"]) : 0);
        //            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[3].Rows[0]["UpdatedTime"]);
        //        }
        //    }

        //    objInnerPages.lstSponsorCategories = lstSponsorCategories;

        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            if (Convert.ToInt32(dr["PageLevel"]) == 1)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems = lstMenuItems;

        //            if (Convert.ToInt32(dr["PageLevel"]) == 2)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems2.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems2 = lstMenuItems2;

        //            if (Convert.ToInt32(dr["PageLevel"]) == 3)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems3.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems3 = lstMenuItems3;

        //            if (Convert.ToInt32(dr["PageLevel"]) == 4)
        //            {
        //                Entities.MenuItems objMenuItems = new Entities.MenuItems();
        //                objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //                objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //                objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //                objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //                objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //                objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //                objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //                objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //                objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //                objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //                objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //                objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //                objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //                objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");
        //                lstMenuItems4.Add(objMenuItems);
        //            }
        //            objInnerPages.lstMenuItems4 = lstMenuItems4;

        //        }
        //    }
        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            FooterMenuItems.Add(objMenuItems);
        //        }
        //    }
        //    objInnerPages.FooterMenuItems = FooterMenuItems;


        //    //President Message
        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        if (ds.Tables[6].Rows.Count == 1)
        //        {
        //            objPInnerPages.PageDetailId = Convert.ToInt64(ds.Tables[6].Rows[0]["PageDetailId"]);
        //            objPInnerPages.Heading = ds.Tables[6].Rows[0]["Heading"].ToString();
        //            objPInnerPages.Description = ds.Tables[6].Rows[0]["Description"].ToString();

        //            objInnerPages.objPInnerPages = objPInnerPages;
        //        }
        //    }

        //    // Upcomming Events List 
        //    if (ds.Tables[7].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[7].Rows)
        //        {
        //            Entities.Events objEvents = new Entities.Events();

        //            objEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objEvents.EventName = dr["EventName"].ToString();

        //            lstEvents.Add(objEvents);
        //        }
        //    }

        //    objInnerPages.lstEvents = lstEvents;

        //    //Chapters list
        //    if (ds.Tables[8].Rows.Count != 0)
        //    {
        //        if (ds.Tables[8].Rows.Count == 1)
        //        {
        //            objChapterInnerPages.PageDetailId = Convert.ToInt64(ds.Tables[8].Rows[0]["PageDetailId"]);
        //            objChapterInnerPages.Heading = ds.Tables[8].Rows[0]["Heading"].ToString();
        //            objChapterInnerPages.Description = ds.Tables[8].Rows[0]["Description"].ToString();

        //            objChapterInnerPages.objPInnerPages = objChapterInnerPages;
        //        }
        //    }

        //    //Akshara Message
        //    if (ds.Tables[9].Rows.Count != 0)
        //    {
        //        if (ds.Tables[9].Rows.Count == 1)
        //        {
        //            objAksharaInnerPages.PageDetailId = Convert.ToInt64(ds.Tables[9].Rows[0]["PageDetailId"]);
        //            objAksharaInnerPages.Heading = ds.Tables[9].Rows[0]["Heading"].ToString();
        //            objAksharaInnerPages.Description = ds.Tables[9].Rows[0]["Description"].ToString();

        //            objAksharaInnerPages.objPInnerPages = objAksharaInnerPages;
        //        }
        //    }
        //    // Sponsors List  
        //    if (ds.Tables[10].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[10].Rows)
        //        {
        //            Entities.Sponsors objSponsors = new Entities.Sponsors();

        //            objSponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objSponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            objSponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objSponsors.Target = dr["Target"].ToString();
        //            objSponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objSponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstMediaSponsors.Add(objSponsors);
        //        }
        //    }
        //    objInnerPages.lstMediaSponsors = lstMediaSponsors;

        //    if (ds.Tables[11].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[11].Rows)
        //        {
        //            Entities.Chapters objChapters = new Entities.Chapters();

        //            objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
        //            objChapters.ChapterName = dr["ChapterName"].ToString();
        //            objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

        //            lstChapters.Add(objChapters);
        //        }
        //    }
        //    objInnerPages.lstChapters = lstChapters;




        //    if (ds.Tables[12].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[12].Rows)
        //        {
        //            Entities.MenuItems objMenuItems = new Entities.MenuItems();

        //            objMenuItems.MenuItemId = Convert.ToInt64(dr["MenuItemId"]);
        //            objMenuItems.DisplayName = dr["DisplayName"].ToString();
        //            objMenuItems.PageLevel = (dr["PageLevel"] != DBNull.Value ? Convert.ToInt32(dr["PageLevel"]) : 1);
        //            objMenuItems.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);
        //            objMenuItems.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "0");
        //            objMenuItems.Position = (dr["Position"] != DBNull.Value ? Convert.ToInt32(dr["Position"]) : 0);
        //            objMenuItems.PageDetailId = (dr["PageDetailId"] != DBNull.Value ? Convert.ToInt64(dr["PageDetailId"].ToString()) : 0);
        //            objMenuItems.Heading = (dr["Heading"] != DBNull.Value ? dr["Heading"].ToString() : "");
        //            objMenuItems.ParentPageName = (dr["ParentPageName"] != DBNull.Value ? dr["ParentPageName"].ToString() : "");
        //            objMenuItems.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
        //            objMenuItems.Target = (dr["Target"] != DBNull.Value ? dr["Target"].ToString() : "");
        //            objMenuItems.DocumentUrl = (dr["DocumentUrl"] != DBNull.Value ? dr["DocumentUrl"].ToString() : "");
        //            objMenuItems.ParentActive = (dr["ParentActive"] != DBNull.Value ? Convert.ToBoolean(dr["ParentActive"].ToString()) : false);
        //            objMenuItems.SubMenuItemCount = (dr["SubMenuItemCount"] != DBNull.Value ? Convert.ToInt32(dr["SubMenuItemCount"].ToString()) : 0);
        //            objMenuItems.OtherUrl = (dr["OtherUrl"] != DBNull.Value ? dr["OtherUrl"].ToString() : "");

        //            lstQuickLinkItems.Add(objMenuItems);




        //        }
        //    }

        //    objInnerPages.lstQuickLinkItems = lstQuickLinkItems;


        //    //President Message
        //    if (ds.Tables[13].Rows.Count != 0)
        //    {
        //        if (ds.Tables[13].Rows.Count == 1)
        //        {
        //            objseodetails.PageDetailId = Convert.ToInt64(ds.Tables[13].Rows[0]["PageDetailId"]);
        //            objseodetails.Heading = ds.Tables[13].Rows[0]["Heading"].ToString();
        //            objseodetails.Description = ds.Tables[13].Rows[0]["Description"].ToString();
        //            objseodetails.PageTitle = (ds.Tables[13].Rows[0]["PageTitle"] != DBNull.Value ? ds.Tables[13].Rows[0]["PageTitle"].ToString() : "");
        //            objseodetails.MetaDescription = (ds.Tables[13].Rows[0]["MetaDescription"] != DBNull.Value ? ds.Tables[13].Rows[0]["MetaDescription"].ToString() : "");
        //            objseodetails.MetaKeywords = (ds.Tables[13].Rows[0]["MetaKeywords"] != DBNull.Value ? ds.Tables[13].Rows[0]["MetaKeywords"].ToString() : "");

        //            objInnerPages.objseodetails = objseodetails;
        //        }
        //    }

        //    return objInnerPages;
        //}




        #endregion

        //#region API

        //public void APIFEGetListInitialLoad(
        // ref Entities.InnerPages objPInnerPages,
        // ref List<Entities.WebsiteBanners> lstWebsiteBanners,
        // ref List<Entities.Events> lstUpcommingEvents,         
        // ref List<Entities.Sponsors> lstSponsors,        
        // ref List<Entities.Videos> lstVideos,
        // ref List<Entities.Events> lstPastEvents,
        // ref List<Entities.Events> lstCurrentEvents,         
        // ref int status)
        //{
        //    DataSet ds = _AppInfo.APIFEGetListInitialLoad(ref status);
        //    string newsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "news/";
        //    string WebsiteBanners = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "WebsiteBanners/NormalImages/";
        //    string eventsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "events/banners/";
        //    string photourl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "photogallery/thumb/";
        //    string Flyersurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Flyers/NormalImages/";
        //    string Sponsorsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Sponsors/";

        //    //President Message
        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        DataTable dt = ds.Tables[0];

        //        objPInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
        //        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        //        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        //    }


        //    //WebsiteBanners List   
        //    if (ds.Tables[1].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[1].Rows)
        //        {
        //            Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();

        //            objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"]);
        //            objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
        //            //objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
        //            objWebsiteBanners.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? WebsiteBanners + dr["BannerUrl"].ToString() : "");
        //            objWebsiteBanners.Target = dr["Target"].ToString();
        //            objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstWebsiteBanners.Add(objWebsiteBanners);
        //        }
        //    }

        //    // Upcomming Events List 
        //    if (ds.Tables[2].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[2].Rows)
        //        {
        //            Entities.Events objUpcommingEvents = new Entities.Events();

        //            objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EventName = dr["EventName"].ToString();
        //            objUpcommingEvents.Location = dr["Location"].ToString();
        //            //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
        //            objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        //            objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
        //            objUpcommingEvents.City = dr["City"].ToString();
        //            objUpcommingEvents.StateName = dr["StateName"].ToString();
        //            objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        //            objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        //            objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        //            lstUpcommingEvents.Add(objUpcommingEvents);
        //        }
        //    }


        //    // Sponsors List  
        //    if (ds.Tables[3].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[3].Rows)
        //        {
        //            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        //            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        //            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        //            //objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        //            objHTCASponsors.LogoUrl = (dr["LogoUrl"] != DBNull.Value ? Sponsorsurl + dr["LogoUrl"].ToString() : "");
        //            objHTCASponsors.Target = dr["Target"].ToString();
        //            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        //            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        //            lstSponsors.Add(objHTCASponsors);
        //        }
        //    }           

        //    // Videos List  
        //    if (ds.Tables[4].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[4].Rows)
        //        {
        //            Entities.Videos objVideos = new Entities.Videos();

        //            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        //            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        //            objVideos.Heading = dr["Heading"].ToString();
        //            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        //            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        //            lstVideos.Add(objVideos);
        //        }
        //    }

        //    // Past Events List 
        //    if (ds.Tables[5].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[5].Rows)
        //        {
        //            Entities.Events objUpcommingEvents = new Entities.Events();

        //            objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EventName = dr["EventName"].ToString();
        //            objUpcommingEvents.Location = dr["Location"].ToString();
        //            //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
        //            objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        //            objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
        //            objUpcommingEvents.City = dr["City"].ToString();
        //            objUpcommingEvents.StateName = dr["StateName"].ToString();
        //            objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        //            objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        //            objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        //            lstUpcommingEvents.Add(objUpcommingEvents);
        //        }
        //    }

        //    // Upcomming Events List 
        //    if (ds.Tables[6].Rows.Count != 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[6].Rows)
        //        {
        //            Entities.Events objUpcommingEvents = new Entities.Events();

        //            objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
        //            objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.EventName = dr["EventName"].ToString();
        //            objUpcommingEvents.Location = dr["Location"].ToString();
        //            //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
        //            objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        //            objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
        //            objUpcommingEvents.City = dr["City"].ToString();
        //            objUpcommingEvents.StateName = dr["StateName"].ToString();
        //            objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        //            objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        //            objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        //            objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        //            lstUpcommingEvents.Add(objUpcommingEvents);
        //        }
        //    }
        //}

        ////public void APIFEGetListInitialLoad(
        //// ref List<Entities.News> lstNews,
        //// ref Entities.InnerPages objPInnerPages,
        //// ref List<Entities.WebsiteBanners> lstWebsiteBanners,
        //// ref List<Entities.Events> lstUpcommingEvents,
        //// ref Entities.InnerPages objWMInnerPages,
        //// ref List<Entities.Events> lstLatestEvents,
        //// ref List<Entities.Sponsors> lstSponsors,
        //// ref List<Entities.Sponsors> lstMediaSponsors,
        //// ref List<Entities.Photos> lstPhotos,
        //// ref List<Entities.Videos> lstVideos,
        //// ref Entities.AppInfo objAppInfo,
        //// ref List<Entities.SponsorCategories> lstSponsorCategories,
        //// ref Entities.Flyers objFlyers,
        //// ref List<Entities.CommitteeCategories> lstCommitteeCategories,

        //// ref int status)
        ////{
        ////    DataSet ds = _AppInfo.FEGetListInitialLoad(ref status);
        ////    string newsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "news/";
        ////    string WebsiteBanners = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "WebsiteBanners/NormalImages/";
        ////    string eventsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "events/banners/";
        ////    string photourl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "photogallery/thumb/";
        ////    string Flyersurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Flyers/NormalImages/";
        ////    string Sponsorsurl = System.Configuration.ConfigurationManager.AppSettings["adminimgurl"] + "Sponsors/NormalImages/";
        ////    //News List
        ////    if (ds.Tables[0].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[0].Rows)
        ////        {
        ////            Entities.News objNews = new Entities.News();

        ////            objNews.NewsId = Convert.ToInt64(dr["NewsId"]);
        ////            objNews.PostDate = Convert.ToDateTime(dr["PostDate"]);
        ////            objNews.NewsText = dr["NewsText"].ToString();
        ////            objNews.Title = dr["Title"].ToString();
        ////            //objNews.ImageUrl = dr["ImageUrl"].ToString();
        ////            objNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? newsurl + dr["ImageUrl"].ToString() : "");

        ////            lstNews.Add(objNews);
        ////        }
        ////    }

        ////    //President Message
        ////    if (ds.Tables[1].Rows.Count == 1)
        ////    {
        ////        DataTable dt = ds.Tables[1];

        ////        objPInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
        ////        objPInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        ////        objPInnerPages.Description = dt.Rows[0]["Description"].ToString();
        ////    }


        ////    //WebsiteBanners List   
        ////    if (ds.Tables[2].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[2].Rows)
        ////        {
        ////            Entities.WebsiteBanners objWebsiteBanners = new Entities.WebsiteBanners();

        ////            objWebsiteBanners.WebsiteBannerId = Convert.ToInt64(dr["WebsiteBannerId"]);
        ////            objWebsiteBanners.BannerTitle = dr["BannerTitle"].ToString();
        ////            //objWebsiteBanners.BannerUrl = dr["BannerUrl"].ToString();
        ////            objWebsiteBanners.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? WebsiteBanners + dr["BannerUrl"].ToString() : "");
        ////            objWebsiteBanners.Target = dr["Target"].ToString();
        ////            objWebsiteBanners.RedirectUrl = dr["RedirectUrl"].ToString();
        ////            objWebsiteBanners.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        ////            lstWebsiteBanners.Add(objWebsiteBanners);
        ////        }
        ////    }

        ////    // Upcomming Events List 
        ////    if (ds.Tables[3].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[3].Rows)
        ////        {
        ////            Entities.Events objUpcommingEvents = new Entities.Events();

        ////            objUpcommingEvents.EventId = Convert.ToInt64(dr["EventId"]);
        ////            objUpcommingEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        ////            objUpcommingEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        ////            objUpcommingEvents.EventName = dr["EventName"].ToString();
        ////            objUpcommingEvents.Location = dr["Location"].ToString();
        ////            //objUpcommingEvents.BannerUrl = dr["BannerUrl"].ToString();
        ////            objUpcommingEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        ////            objUpcommingEvents.EventDetails = dr["EventDetails"].ToString();
        ////            objUpcommingEvents.City = dr["City"].ToString();
        ////            objUpcommingEvents.StateName = dr["StateName"].ToString();
        ////            objUpcommingEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        ////            objUpcommingEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        ////            objUpcommingEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        ////            objUpcommingEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        ////            lstUpcommingEvents.Add(objUpcommingEvents);
        ////        }
        ////    }

        ////    //Welcome Message
        ////    if (ds.Tables[4].Rows.Count == 1)
        ////    {
        ////        DataTable dt = ds.Tables[4];
        ////        objWMInnerPages.InnerPageId = Convert.ToInt64(dt.Rows[0]["InnerPageId"]);
        ////        objWMInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
        ////        objWMInnerPages.Description = dt.Rows[0]["Description"].ToString();
        ////    }

        ////    // Latest Events List 
        ////    if (ds.Tables[5].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[5].Rows)
        ////        {
        ////            Entities.Events objLatestEvents = new Entities.Events();

        ////            objLatestEvents.EventId = Convert.ToInt64(dr["EventId"]);
        ////            objLatestEvents.StartDate = (dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : DateTime.MinValue);
        ////            objLatestEvents.EndDate = (dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : DateTime.MinValue);
        ////            objLatestEvents.EventName = dr["EventName"].ToString();
        ////            objLatestEvents.Location = dr["Location"].ToString();
        ////            //objLatestEvents.BannerUrl = dr["BannerUrl"].ToString();
        ////            objLatestEvents.BannerUrl = (dr["BannerUrl"] != DBNull.Value ? eventsurl + dr["BannerUrl"].ToString() : "");
        ////            objLatestEvents.EventDetails = dr["EventDetails"].ToString();
        ////            objLatestEvents.City = dr["City"].ToString();
        ////            objLatestEvents.StateName = dr["StateName"].ToString();
        ////            objLatestEvents.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
        ////            objLatestEvents.IsRegistration = (dr["IsRegistration"] != DBNull.Value ? Convert.ToBoolean(dr["IsRegistration"].ToString()) : false);
        ////            objLatestEvents.RegistrationStartDate = (dr["RegistrationStartDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationStartDate"]) : DateTime.MinValue);
        ////            objLatestEvents.RegistrationEndDate = (dr["RegistrationEndDate"] != DBNull.Value ? Convert.ToDateTime(dr["RegistrationEndDate"]) : DateTime.MinValue);

        ////            lstLatestEvents.Add(objLatestEvents);
        ////        }
        ////    }

        ////    // Sponsors List  
        ////    if (ds.Tables[6].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[6].Rows)
        ////        {
        ////            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        ////            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        ////            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        ////            //objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        ////            objHTCASponsors.LogoUrl = (dr["LogoUrl"] != DBNull.Value ? Sponsorsurl + dr["LogoUrl"].ToString() : "");
        ////            objHTCASponsors.Target = dr["Target"].ToString();
        ////            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        ////            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        ////            lstSponsors.Add(objHTCASponsors);
        ////        }
        ////    }

        ////    // Sponsors List  
        ////    if (ds.Tables[7].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[7].Rows)
        ////        {
        ////            Entities.Sponsors objHTCASponsors = new Entities.Sponsors();

        ////            objHTCASponsors.SponsorId = Convert.ToInt64(dr["SponsorId"]);
        ////            objHTCASponsors.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"]);
        ////            //objHTCASponsors.LogoUrl = dr["LogoUrl"].ToString();
        ////            objHTCASponsors.LogoUrl = (dr["LogoUrl"] != DBNull.Value ? Sponsorsurl + dr["LogoUrl"].ToString() : "");
        ////            objHTCASponsors.Target = dr["Target"].ToString();
        ////            objHTCASponsors.RedirectUrl = dr["RedirectUrl"].ToString();
        ////            objHTCASponsors.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);

        ////            lstMediaSponsors.Add(objHTCASponsors);
        ////        }
        ////    }


        ////    //Photos section
        ////    if (ds.Tables[8].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[8].Rows)
        ////        {
        ////            Entities.Photos objPhotos = new Entities.Photos();

        ////            objPhotos.PhotoId = Convert.ToInt64(dr["PhotoId"]);
        ////            objPhotos.PhotoCategoryId = Convert.ToInt64(dr["PhotoCategoryId"]);
        ////            objPhotos.ImageDescription = dr["ImageDescription"].ToString();
        ////            //objPhotos.ImageUrl = dr["ImageUrl"].ToString();
        ////            objPhotos.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? photourl + dr["ImageUrl"].ToString() : "");
        ////            objPhotos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        ////            lstPhotos.Add(objPhotos);
        ////        }
        ////    }

        ////    // Videos List  
        ////    if (ds.Tables[9].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[9].Rows)
        ////        {
        ////            Entities.Videos objVideos = new Entities.Videos();

        ////            objVideos.VideoId = Convert.ToInt64(dr["VideoId"]);
        ////            objVideos.VideoCategoryId = Convert.ToInt64(dr["VideoCategoryId"]);
        ////            objVideos.Heading = dr["Heading"].ToString();
        ////            objVideos.VideoUrl = dr["VideoUrl"].ToString();
        ////            objVideos.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);

        ////            lstVideos.Add(objVideos);
        ////        }
        ////    }

        ////    //AppInfo List  
        ////    if (ds.Tables[10].Rows.Count != 0)
        ////    {
        ////        if (ds.Tables[10].Rows.Count == 1)
        ////        {
        ////            objAppInfo.AppInfoId = Convert.ToInt64(ds.Tables[10].Rows[0]["AppInfoId"]);
        ////            objAppInfo.SiteName = ds.Tables[10].Rows[0]["SiteName"].ToString();
        ////            objAppInfo.CompanyAddress = ds.Tables[10].Rows[0]["CompanyAddress"].ToString();
        ////            objAppInfo.CompanyWebSite = ds.Tables[10].Rows[0]["CompanyWebSite"].ToString();
        ////            objAppInfo.CompanyEmail = ds.Tables[10].Rows[0]["CompanyEmail"].ToString();
        ////            objAppInfo.CompanyPhone = ds.Tables[10].Rows[0]["CompanyPhone"].ToString();
        ////            objAppInfo.PresidentPhone = ds.Tables[10].Rows[0]["PresidentPhone"].ToString();
        ////            objAppInfo.PresidentEmail = ds.Tables[10].Rows[0]["PresidentEmail"].ToString();
        ////            objAppInfo.SecretaryEmail = ds.Tables[10].Rows[0]["SecretaryEmail"].ToString();
        ////            objAppInfo.SecretaryPhone = ds.Tables[10].Rows[0]["SecretaryPhone"].ToString();
        ////            objAppInfo.CustomerCareNumber = ds.Tables[10].Rows[0]["CustomerCareNumber"].ToString();
        ////            objAppInfo.TollFreeNumber = ds.Tables[10].Rows[0]["TollFreeNumber"].ToString();
        ////            objAppInfo.FacebookUrl = ds.Tables[10].Rows[0]["FacebookUrl"].ToString();
        ////            objAppInfo.TwitterUrl = ds.Tables[10].Rows[0]["TwitterUrl"].ToString();
        ////            objAppInfo.YoutubeUrl = ds.Tables[10].Rows[0]["YoutubeUrl"].ToString();
        ////            objAppInfo.SupportEmail = ds.Tables[10].Rows[0]["SupportEmail"].ToString();
        ////            objAppInfo.EnqueryEmail = ds.Tables[10].Rows[0]["EnqueryEmail"].ToString();
        ////            objAppInfo.PageTitle = ds.Tables[10].Rows[0]["PageTitle"].ToString();
        ////            objAppInfo.MetaDescription = ds.Tables[10].Rows[0]["MetaDescription"].ToString();
        ////            objAppInfo.MetaKeywords = ds.Tables[10].Rows[0]["MetaKeywords"].ToString();
        ////            objAppInfo.Topline = ds.Tables[10].Rows[0]["Topline"].ToString();
        ////            objAppInfo.PageItems = (ds.Tables[10].Rows[0]["PageItems"] != DBNull.Value ? Convert.ToInt64(ds.Tables[10].Rows[0]["PageItems"]) : 0);
        ////            objAppInfo.UpdatedTime = Convert.ToDateTime(ds.Tables[10].Rows[0]["UpdatedTime"]);
        ////        }
        ////    }

        ////    //Sponsor Categories 
        ////    if (ds.Tables[11].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[11].Rows)
        ////        {
        ////            AWSFormBuilder.Entities.SponsorCategories objSponsorCategories = new AWSFormBuilder.Entities.SponsorCategories();

        ////            objSponsorCategories.SponsorCategoryId = Convert.ToInt64(dr["SponsorCategoryId"].ToString());
        ////            objSponsorCategories.SponsorsCount = Convert.ToInt64(dr["SponsorsCount"].ToString());
        ////            objSponsorCategories.CategoryName = dr["CategoryName"].ToString();
        ////            lstSponsorCategories.Add(objSponsorCategories);
        ////        }
        ////    }

        ////    //Flyers List   
        ////    if (ds.Tables[12].Rows.Count != 0)
        ////    {
        ////        objFlyers.FlyerId = Convert.ToInt64(ds.Tables[12].Rows[0]["FlyerId"]);
        ////        //objFlyers.FlyerUrl = ds.Tables[12].Rows[0]["FlyerUrl"].ToString();
        ////        objFlyers.FlyerUrl = (ds.Tables[12].Rows[0]["FlyerUrl"] != DBNull.Value ? Flyersurl + ds.Tables[12].Rows[0]["FlyerUrl"].ToString() : "");
        ////        objFlyers.RedirectUrl = ds.Tables[12].Rows[0]["RedirectUrl"].ToString();
        ////    }

        ////    //WebsiteBanners List   
        ////    if (ds.Tables[13].Rows.Count != 0)
        ////    {
        ////        foreach (DataRow dr in ds.Tables[13].Rows)
        ////        {
        ////            Entities.CommitteeCategories objCommitteeCategories = new Entities.CommitteeCategories();

        ////            objCommitteeCategories.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"]);
        ////            objCommitteeCategories.CategoryName = dr["CategoryName"].ToString();

        ////            lstCommitteeCategories.Add(objCommitteeCategories);
        ////        }
        ////    }


        ////}
        //#endregion



        public List<AWSFormBuilder.Entities.Members> FEGetCommitteeMembersList(Int64 ChapterId, ref int status)
        {
            List<AWSFormBuilder.Entities.Members> lstCommitteeMembers = new List<Entities.Members>();

            DataTable dt = _AppInfo.FEGetCommitteeMembersList(ChapterId, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.Members objCommittees = new AWSFormBuilder.Entities.Members();

                    objCommittees.MemberId = Convert.ToInt64(dr["MemberId"].ToString());
                    objCommittees.Name = (dr["Name"] != DBNull.Value ? dr["Name"].ToString() : null);
                    objCommittees.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objCommittees.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objCommittees.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objCommittees.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objCommittees.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    objCommittees.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objCommittees.DisplayOrder = (dr["DisplayOrder"] != DBNull.Value ? Convert.ToInt32(dr["DisplayOrder"]) : 0);
                    objCommittees.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objCommittees.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : "");
                    objCommittees.UpdatedBy = dr["UpdatedBy"].ToString();
                    objCommittees.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"].ToString());
                    objCommittees.CommitteeCategoryId = Convert.ToInt64(dr["CommitteeCategoryId"].ToString());
                    objCommittees.CategoryName = dr["CategoryName"].ToString();
                    objCommittees.Designation = (dr["Designation"] != DBNull.Value ? dr["Designation"].ToString() : "");
                    objCommittees.Type = dr["Type"].ToString();

                    lstCommitteeMembers.Add(objCommittees);
                }

            }
            return lstCommitteeMembers;
        }

        public List<Entities.News> FEGetNewsList(Int64 ChapterId, ref int status)
        {
            List<AWSFormBuilder.Entities.News> lstNews = new List<Entities.News>();

            DataTable dt = _AppInfo.FEGetNewsList(ChapterId, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.News objNews = new Entities.News();

                    objNews.NewsId = (dr["NewsId"] != DBNull.Value ? Convert.ToInt64(dr["NewsId"]) : 0);
                    objNews.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterId"]) : 0);
                    objNews.PostDate = (dr["PostDate"] != DBNull.Value ? Convert.ToDateTime(dr["PostDate"]) : DateTime.MinValue);
                    objNews.NewsText = (dr["NewsText"] != DBNull.Value ? dr["NewsText"].ToString() : "");
                    objNews.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : "");
                    objNews.ImageUrl = (dr["ImageUrl"] != DBNull.Value ? dr["ImageUrl"].ToString() : "");
                    objNews.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : "");

                    lstNews.Add(objNews);
                }

            }
            return lstNews;
        }

        public Entities.PageDetails FEGetVisionDetails(Int64 ChapterId, ref int Status)
        {
            DataTable dt = _AppInfo.FEGetVisionDetails(ChapterId, ref Status);
            Entities.PageDetails objNatsMissionInnerPages = new Entities.PageDetails();

            if (Status == 1 && dt.Rows.Count == 1)
            {
                objNatsMissionInnerPages.PageDetailId = Convert.ToInt64(dt.Rows[0]["PageDetailId"]);
                objNatsMissionInnerPages.Heading = dt.Rows[0]["Heading"].ToString();
                objNatsMissionInnerPages.Description = dt.Rows[0]["Description"].ToString();

            }
            return objNatsMissionInnerPages;
        }

    



        public Int64 UpdateAppVersion(string Email, string Version, string Role, string Type, ref int status)
        {
            if (Email != null && Email.Trim() != "")
            {
                _AppInfo.UpdateAppVersion(Email, Version, Role, Type, ref status);
            }
            return status;
        }

        public List<AWSFormBuilder.Entities.Chapters> FEGetChaptersList(Int64 ChapterId, ref int status)
        {
            List<AWSFormBuilder.Entities.Chapters> lstChapters = new List<Entities.Chapters>();

            DataTable dt = _AppInfo.FEGetChaptersList(ChapterId, ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Chapters objChapters = new Entities.Chapters();

                    objChapters.ChapterId = Convert.ToInt64(dr["ChapterId"]);
                    objChapters.ChapterName = dr["ChapterName"].ToString();
                    objChapters.Description = (dr["Description"] != DBNull.Value ? dr["Description"].ToString() : null);

                    lstChapters.Add(objChapters);
                }

            }
            return lstChapters;
        }

        #region AdminMainMenu

        public Entities.AppInfo GetAdminMenuData(
           Int64 UserId,
           Int64 RoleId,
           //ref List<Entities.AdminMenuItems> lstmenu,
           ref List<Entities.AdminMenuItems> lstMainMenu,
           ref List<Entities.AdminMenuItems> lstSubMenu,
           ref int status)
        {
            AWSFormBuilder.Entities.AppInfo objAppInfo = new AWSFormBuilder.Entities.AppInfo();
            DataSet ds = _AppInfo.GetAdminMenuData(UserId, RoleId, ref status);
            //  (Session["ChapterId"] != null ? Convert.ToInt32(Session["EmpCompanyID"].ToString()) : 0);

            //menu List
            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Entities.AdminMenuItems objmainmenu = new Entities.AdminMenuItems();

            //        objmainmenu.DisplayName = (dr["DisplayName"] != DBNull.Value ? dr["DisplayName"].ToString() : "");
            //        objmainmenu.MenuItemId = (dr["MenuItemId"] != DBNull.Value ? Convert.ToInt64(dr["MenuItemId"]) : 0);
            //        objmainmenu.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
            //        objmainmenu.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);

            //        lstmenu.Add(objmainmenu);
            //    }
            //}

            //objAppInfo.lstmenu = lstmenu;

            //MainMenu List
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Entities.AdminMenuItems objmainmenu = new Entities.AdminMenuItems();

                    objmainmenu.DisplayName = (dr["DisplayName"] != DBNull.Value ? dr["DisplayName"].ToString() : "");
                    objmainmenu.Logo = (dr["Logo"] != DBNull.Value ? dr["Logo"].ToString() : "");
                    objmainmenu.MenuItemId = (dr["MenuItemId"] != DBNull.Value ? Convert.ToInt64(dr["MenuItemId"]) : 0);
                    objmainmenu.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                    objmainmenu.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);

                    lstMainMenu.Add(objmainmenu);
                }
            }

            objAppInfo.lstMainMenu = lstMainMenu;

            // SubMenu List  
            if (ds.Tables[1].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Entities.AdminMenuItems objsubmenu = new Entities.AdminMenuItems();

                    objsubmenu.DisplayName = (dr["DisplayName"] != DBNull.Value ? dr["DisplayName"].ToString() : "");
                    objsubmenu.Logo = (dr["Logo"] != DBNull.Value ? dr["Logo"].ToString() : "");
                    objsubmenu.MenuItemId = (dr["MenuItemId"] != DBNull.Value ? Convert.ToInt64(dr["MenuItemId"]) : 0);
                    objsubmenu.PageUrl = (dr["PageUrl"] != DBNull.Value ? dr["PageUrl"].ToString() : "");
                    objsubmenu.IdPath = (dr["IdPath"] != DBNull.Value ? dr["IdPath"].ToString() : "");
                    objsubmenu.PageParentId = (dr["PageParentId"] != DBNull.Value ? Convert.ToInt64(dr["PageParentId"]) : 0);

                    lstSubMenu.Add(objsubmenu);
                }
            }

            objAppInfo.lstSubMenu = lstSubMenu;

            
            return objAppInfo;
        }

        #endregion

       



        #region LogReport

        public List<AWSFormBuilder.Entities.LogReport> GetLogReportListByVariable(string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.LogReport> lstLogReport = new List<AWSFormBuilder.Entities.LogReport>();
            DataTable dt = _AppInfo.GetLogReportListByVariable(StartDate, EndDate, Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _AppInfo.GetLogReportListByVariable(StartDate, EndDate, Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.LogReport objLogReport = new AWSFormBuilder.Entities.LogReport();

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

        #endregion














    }




}