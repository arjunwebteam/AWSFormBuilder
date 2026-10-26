using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Entities
{
    public class AppInfo
    {
        public Int64 AppInfoId { get; set; }
        public Int64 ChapterId { get; set; }
        public string SiteName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyWebSite { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyPhone { get; set; }

        public string PresidentEmail { get; set; }

        public string PresidentPhone { get; set; }

        public string SecretaryEmail { get; set; }

        public string SecretaryPhone { get; set; }

        public string CustomerCareNumber { get; set; }

        public string TollFreeNumber { get; set; }

        public string FacebookUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string YoutubeUrl { get; set; }

        public string LinkedInUrl { get; set; }
        public string GooglePlus { get; set; }

        public string SupportEmail { get; set; }

        public string EnqueryEmail { get; set; }

        public string PageTitle { get; set; }

         public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string Topline { get; set; }
        public Int64 PageItems { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }
        public string BaseUrl { get; set; }
        public string UploadPath { get; set; }
        public string UserUploadPath { get; set; }
        public string UserSiteUrl { get; set; }
        public string ServerMapUrl { get; set; }
        public string AdminImageUrl { get; set; }
        public string AdminSiteUrl { get; set; }
        public string MailName { get; set; }
        public string SenderEmail { get; set; }
        public string MemberEmail { get; set; }
        public string ExhibitEmail { get; set; }
        public string EventsEmail { get; set; }
        public string ContactEmail { get; set; }
        public string DonationEmail { get; set; }
        public string VolunteerEmail { get; set; }
        public string SponsorshipEmail { get; set; }
        public string BrevoKey { get; set; }
        public Int32 AndroidVersion { get; set; }
        public Int32 IOSVersion { get; set; }
        public Int32 DesktopVersion { get; set; }
        public string AppUpdate { get; set; }
        public string CapchaSiteKey { get; set; }
        public string CapchaSecreatKey { get; set; }
        public string ShowCapcha { get; set; }
        public string GooglePlusUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string WhatsappNumber { get; set; }
        public string GoogleAnalyticsScript { get; set; }
        public string WhatsappScript { get; set; }
        public string IsQRCode { get; set; }
        public string EmaisToBrevo { get; set; }
        public string IsPromoCodes { get; set; }
        public string IsCultural { get; set; }
        public string IsSports { get; set; }
        public string IsHelpDocument { get; set; }
        public string IsFeaturesDocument { get; set; }
        public string TimeZone { get; set; }

        public string TimeZones { get; set; }


        public List<News> lstNews { get; set; }

        public InnerPages objInnerPages { get; set; }



        public List<TableCount> lstTableCounts { get; set; }
        public List<AdminMenuItems> lstMainMenu { get; set; }
        public List<AdminMenuItems> lstSubMenu { get; set; }
        public List<AdminMenuItems> lstmenu { get; set; }
        public List<Members> lstMembers { get; set; }
        public List<Members> lstactiveMembers { get; set; }
        public List<Members> lstinactiveMembers { get; set; }

        public string chapterStatus { get; set; }


        public string CAPTCHA { get; set; }

        public string Email { get; set; }


        public string LayoutLogo { get; set; }
        public string faviconlogo { get; set; }
        public string Loginlogo { get; set; }
        public string MailLogo { get; set; }

    }
}
