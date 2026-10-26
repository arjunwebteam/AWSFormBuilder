using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using RestSharp;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class Common
    {
        public static NameValueCollection GetPayPalCollection(string payPalInfo)
        {
            //place the responses into collection
            NameValueCollection PayPalCollection = new System.Collections.Specialized.NameValueCollection();
            string[] ArrayReponses = payPalInfo.Split('&');

            for (int i = 0; i < ArrayReponses.Length; i++)
            {
                string[] Temp = ArrayReponses[i].Split('=');
                PayPalCollection.Add(Temp[0], Temp[1]);
            }
            return PayPalCollection;
        }
        public static async Task<string> ValidateImageSSAsync(string absoluteUrl, string defaultUrl, string fallbackUrl)
        {
            if (string.IsNullOrWhiteSpace(absoluteUrl))
                return defaultUrl;

            Uri myUri;
            if (Uri.TryCreate(absoluteUrl, UriKind.Absolute, out myUri))
            {
                try
                {
                    if (myUri.IsFile)
                    {
                        string localPath = myUri.LocalPath;
                        return System.IO.File.Exists(localPath) ? absoluteUrl : fallbackUrl;
                    }
                    using (HttpClient client = new HttpClient()) // ✅ Use HttpClient not WebClient in Core
                    {
                        var response = await client.GetAsync(myUri);
                        return response.IsSuccessStatusCode ? absoluteUrl : fallbackUrl;
                    }
                }
                catch (Exception)
                {
                    return fallbackUrl;
                }
            }
            return fallbackUrl;
        }
        public static string ValidateImage(string absoluteUrl, string defaultUrl)
        {
            if (string.IsNullOrWhiteSpace(absoluteUrl))
                return defaultUrl;

            Uri myUri;
            if (Uri.TryCreate(absoluteUrl, UriKind.Absolute, out myUri))
            {
                try
                {
                    // For local file paths
                    if (myUri.IsFile)
                    {
                        string localPath = myUri.LocalPath;
                        return System.IO.File.Exists(localPath) ? absoluteUrl : defaultUrl;
                    }

                    // For URLs (web images)
                    using (WebClient client = new WebClient())
                    {
                        using (Stream stream = client.OpenRead(myUri))
                        {
                            return stream != null ? absoluteUrl : defaultUrl;
                        }
                    }
                }
                catch (Exception)
                {
                    return defaultUrl; // Catch any error and return the default URL
                }
            }

            return defaultUrl; // If the URL is not valid, return the default URL
        }

        public static string GettImagePath(string webRootPath, string folderPath, string imageName, string defaultImagePath)
        {
            string fullImagePath = Path.Combine(webRootPath, folderPath, imageName);
            if (File.Exists(fullImagePath))
            {
                return ("/" + folderPath.Replace("\\", "/") + "/" + imageName).Replace("//", "/");
            }
            else
            {
                return ("/" + defaultImagePath.Replace("\\", "/")).Replace("//", "/");
            }
        }
        public static string GetImagePath(IWebHostEnvironment env, string folderPath, string imageName, string defaultImagePath)
        {
            string fullImagePath = Path.Combine(env.WebRootPath, folderPath, imageName);

            if (File.Exists(fullImagePath))
            {
                return Path.Combine("/", folderPath.Replace("\\", "/"), imageName).Replace("\\", "/");
            }
            else
            {
                return Path.Combine("/", defaultImagePath.Replace("\\", "/")).Replace("\\", "/");
            }
        }


        private static byte[] inputVector = { 120, 122, 203, 107, 23, 242, 251, 98, 41, 192, 200, 47, 62, 121, 84, 221 };
        private static byte[] cryptKey = { 33, 228, 79, 4, 144, 123, 222, 191, 113, 198, 227, 25, 162, 142, 105, 176 };

        public static string EncryptString(string UnEncryptedPassword)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            System.Security.Cryptography.RijndaelManaged RMCrypto = new System.Security.Cryptography.RijndaelManaged();
            System.Security.Cryptography.CryptoStream cStream = new System.Security.Cryptography.CryptoStream(mStream, RMCrypto.CreateEncryptor(cryptKey, inputVector), System.Security.Cryptography.CryptoStreamMode.Write);
            System.IO.StreamWriter SWriter = new System.IO.StreamWriter(cStream);
            SWriter.Write(UnEncryptedPassword);
            SWriter.Flush();
            cStream.FlushFinalBlock();
            mStream.Flush();

            string encryptstring = Convert.ToBase64String(mStream.GetBuffer(), 0, (int)mStream.Length);
            encryptstring = encryptstring.Replace("+", "1PLUS1").Replace("==", "2EQUAL2");
            return encryptstring;
        }
        public static string UnEncryptString(string EncryptedPassword)
        {
            EncryptedPassword = EncryptedPassword.Replace("1PLUS1", "+").Replace("2EQUAL2", "==");
            byte[] bufr = Convert.FromBase64String(EncryptedPassword);
            System.IO.MemoryStream mStream = new System.IO.MemoryStream(bufr);
            System.Security.Cryptography.RijndaelManaged RMCrypto = new System.Security.Cryptography.RijndaelManaged();
            System.Security.Cryptography.CryptoStream cStream = new System.Security.Cryptography.CryptoStream(mStream, RMCrypto.CreateDecryptor(cryptKey, inputVector), System.Security.Cryptography.CryptoStreamMode.Read);
            System.IO.StreamReader sReader = new System.IO.StreamReader(cStream);
            return sReader.ReadToEnd();
        }
        //public static string SetPaging(Int64 RecordsPerPage, ref Int64 TotalRecords, Int64 CurrentPageNo, string pclass)
        //{
        //    Int64 Page_Mod = default(Int64);
        //    Int64 Page_Size = default(Int64);
        //    StringBuilder sbPage_HTML = new StringBuilder();
        //    string strRet = string.Empty;
        //    string strRSS = string.Empty;
        //    Int64 Page_Mid = 0;
        //    Int64 MaxPageSize = 5;

        //    if (TotalRecords > RecordsPerPage)
        //    {
        //        Page_Mid = (MaxPageSize / 2);
        //        Page_Mid = Page_Mid + 1;

        //        if (RecordsPerPage > 0)
        //        {
        //            Page_Mod = TotalRecords % RecordsPerPage;
        //            Page_Size = (TotalRecords / RecordsPerPage);
        //            if (Page_Mod > 0)
        //            {
        //                Page_Size = Page_Size + 1;
        //            }
        //        }
        //        else
        //        {
        //            Page_Size = 1;
        //        }

        //        if ((Page_Size > 1))
        //        {
        //            Int64 Start = ((CurrentPageNo - 1) * RecordsPerPage) + 1;
        //            Int64 End = ((CurrentPageNo - 1) * RecordsPerPage) + RecordsPerPage;
        //            if (End > TotalRecords)
        //                End = TotalRecords;
        //            sbPage_HTML.Append("<div><ul class=\"" + pclass + "\">");
        //            //sbPage_HTML.Append("<div class='pagination'>v><ul>");
        //        }

        //        bool isShow_Forward = true;

        //        if (CurrentPageNo > Page_Mid)
        //        {
        //            sbPage_HTML.Append("<li><a id='«'>" + "«" + "</a></li>");
        //        }

        //        for (int i = 1; i <= MaxPageSize; i++)
        //        {
        //            Int64 PageId = default(Int64);
        //            if (CurrentPageNo > Page_Mid)
        //            {
        //                PageId = CurrentPageNo + (i - Page_Mid);
        //            }
        //            else
        //            {
        //                PageId = i;
        //            }
        //            if (PageId <= Page_Size)
        //            {
        //                if (Convert.ToInt64(CurrentPageNo) == PageId)
        //                {
        //                    sbPage_HTML.Append("<li class=\"active\"><a ><span>" + PageId.ToString() + "</span></a></li>");

        //                }
        //                else
        //                {
        //                    sbPage_HTML.Append("<li><a id='" + PageId.ToString() + "'>" + PageId.ToString() + "</a></li>");
        //                    //sbPage_HTML.Append("<a id='pgr_" + PageId.ToString() + "' href='?pageno=" + PageId.ToString() + "'>" + PageId.ToString() + "</a>");

        //                }
        //            }
        //            if (PageId == Page_Size)
        //            {
        //                isShow_Forward = false;
        //            }
        //        }


        //        if (isShow_Forward == true)
        //        {
        //            sbPage_HTML.Append("<li><a id='»'>" + "»" + "</a></li>");
        //        }

        //        if ((Page_Size > 1))
        //        {
        //            sbPage_HTML.Append("</ul></div>");
        //        }

        //    }

        //    strRet = sbPage_HTML.ToString();
        //    return strRet;
        //}

        public static string SetPagingFE(Int64 RecordsPerPage, ref Int64 TotalRecords, Int64 CurrentPageNo, string pclass)
        {
            Int64 Page_Mod = default(Int64);
            Int64 Page_Size = default(Int64);
            StringBuilder sbPage_HTML = new StringBuilder();
            string strRet = string.Empty;
            string strRSS = string.Empty;
            Int64 Page_Mid = 0;
            Int64 MaxPageSize = 10;

            if (TotalRecords > RecordsPerPage)
            {
                Page_Mid = (MaxPageSize / 2);
                Page_Mid = Page_Mid + 1;

                if (RecordsPerPage > 0)
                {
                    Page_Mod = TotalRecords % RecordsPerPage;
                    Page_Size = (TotalRecords / RecordsPerPage);
                    if (Page_Mod > 0)
                    {
                        Page_Size = Page_Size + 1;
                    }
                }
                else
                {
                    Page_Size = 1;
                }

                if ((Page_Size > 1))
                {
                    Int64 Start = ((CurrentPageNo - 1) * RecordsPerPage) + 1;
                    Int64 End = ((CurrentPageNo - 1) * RecordsPerPage) + RecordsPerPage;
                    if (End > TotalRecords)
                        End = TotalRecords;
                    sbPage_HTML.Append("<div class=\"clearfix t-c\"><ul class=\"pagination pagination-mini pagination-centered \">");
                    //sbPage_HTML.Append("<div class='pagination'>v><ul>");
                }

                bool isShow_Forward = true;

                if (CurrentPageNo > Page_Mid)
                {
                    sbPage_HTML.Append("<li><a id='<'>First</a></li><li><a id='«'>" + "«" + "</a></li>");
                }

                for (int i = 1; i <= MaxPageSize; i++)
                {
                    Int64 PageId = default(Int64);
                    if (CurrentPageNo > Page_Mid)
                    {
                        PageId = CurrentPageNo + (i - Page_Mid);
                    }
                    else
                    {
                        PageId = i;
                    }
                    if (PageId <= Page_Size)
                    {
                        if (Convert.ToInt64(CurrentPageNo) == PageId)
                        {
                            sbPage_HTML.Append("<li class=\"active\"><a ><span>" + PageId.ToString() + "</span></a></li>");

                        }
                        else
                        {
                            sbPage_HTML.Append("<li><a id='" + PageId.ToString() + "'>" + PageId.ToString() + "</a></li>");
                            //sbPage_HTML.Append("<a id='pgr_" + PageId.ToString() + "' href='?pageno=" + PageId.ToString() + "'>" + PageId.ToString() + "</a>");

                        }
                    }
                    if (PageId == Page_Size)
                    {
                        isShow_Forward = false;
                    }
                }


                if (isShow_Forward == true)
                {
                    sbPage_HTML.Append("<li><a id='»'>" + "»" + "</a></li><li><a id='>'>Last</a></li>");
                }

                if ((Page_Size > 1))
                {
                    sbPage_HTML.Append("</ul></div>");
                }

            }

            strRet = sbPage_HTML.ToString();
            return strRet;
        }

        //public static void SendMailwithfrom(string to, string frommail, string subject, string body)
        //{
        //    try
        //    {
        //        string[] emails = to.Split(',');
        //        string from1 = frommail;
        //        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        foreach (string recipientemail in emails)
        //        {
        //            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        //            MailAddress from = new MailAddress(from1, frommail);
        //            MailAddress to1 = new MailAddress(recipientemail);
        //            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to1);
        //            mm.BodyEncoding = Encoding.UTF8;
        //            mm.Subject = subject;
        //            mm.From = from;
        //            mm.Body = body;
        //            mm.Priority = System.Net.Mail.MailPriority.High;
        //            mm.IsBodyHtml = true;

        //            System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient(settings.Smtp.Network.Host, settings.Smtp.Network.Port);
        //            Client.Credentials = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        //            Client.EnableSsl = false;
        //            Client.Send(mm);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static string SendMailSendinblue(string to, string subject, string body)
        //{
        //    string body1 = "";
        //    string txt = body.Trim();
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    if (txt.Contains(Environment.NewLine))
        //    {
        //        string[] splitTxt = txt.Split(Environment.NewLine.ToCharArray());
        //        foreach (string s in splitTxt)
        //        {
        //            if (s != string.Empty)
        //            {
        //                body1 += string.Format("{0}{1}", s.Trim(), " ");
        //            }
        //        }
        //        body = body1;
        //    }

        //    body = body.Replace("\r\n", " ");
        //    body = body.Replace("\"", "\\\"");

        //    string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();

        //    string mailname = ConfigurationManager.AppSettings["mailname"].ToString();
        //    var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["GridMailKey"].ToString());
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"" + to + "\"}],\"subject\":\"" + subject + "\"}],\"content\": [{\"type\": \"text/html\", \"value\": \"" + body + "\"}],\"from\":{\"email\":\"" + from1 + "\",\"name\":\"" + mailname + "\"}}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    var mailresponce = response.Content;
        //    return mailresponce;
        //}

        //public static string SendMailSendinbluewithfrom(string to, string frommail, string subject, string body)
        //{
        //    string body1 = "";
        //    string txt = body.Trim();
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    if (txt.Contains(Environment.NewLine))
        //    {
        //        string[] splitTxt = txt.Split(Environment.NewLine.ToCharArray());
        //        foreach (string s in splitTxt)
        //        {
        //            if (s != string.Empty)
        //            {
        //                body1 += string.Format("{0}{1}", s.Trim(), " ");
        //            }
        //        }
        //        body = body1;
        //    }

        //    body = body.Replace("\r\n", " ");
        //    body = body.Replace("\"", "\\\"");

        //    string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();

        //    string mailname = ConfigurationManager.AppSettings["mailname"].ToString();
        //    var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["GridMailKey"].ToString());
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"" + to + "\"}],\"subject\":\"" + subject + "\"}],\"content\": [{\"type\": \"text/html\", \"value\": \"" + body + "\"}],\"from\":{\"email\":\"" + from1 + "\",\"name\":\"" + mailname + "\"}}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    var mailresponce = response.Content;
        //    return mailresponce;

        //}

        //public static string SendMailSendGridwithfrom(string to, string frommail, string subject, string body)
        //{
        //    string body1 = "";
        //    string txt = body.Trim();
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    if (txt.Contains(Environment.NewLine))
        //    {
        //        string[] splitTxt = txt.Split(Environment.NewLine.ToCharArray());
        //        foreach (string s in splitTxt)
        //        {
        //            if (s != string.Empty)
        //            {
        //                body1 += string.Format("{0}{1}", s.Trim(), " ");
        //            }
        //        }
        //        body = body1;
        //    }

        //    body = body.Replace("\r\n", " ");
        //    body = body.Replace("\"", "\\\"");

        //    string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();

        //    //string mailname = ConfigurationManager.AppSettings["mailname"].ToString();
        //    //var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
        //    //var request = new RestRequest(Method.POST);
        //    //request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("content-type", "application/json");
        //    //request.AddHeader("api-key", ConfigurationManager.AppSettings["MailKey"].ToString());
        //    //request.AddHeader("accept", "application/json");
        //    //request.AddParameter("application/json", "{\"sender\":{\"name\":\"" + mailname + "\",\"email\":\"" + from1 + "\"},\"to\":[{\"email\":\"" + to + "\",\"name\":\"" + mailname + "\"}],\"subject\":\"" + subject + "\",\"htmlContent\":\"" + body + "\"}", ParameterType.RequestBody);

        //    //IRestResponse response = client.Execute(request);

        //    string mailname = ConfigurationManager.AppSettings["mailname"].ToString();
        //    var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["GridMailKey"].ToString());
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"" + to + "\"}],\"subject\":\"" + subject + "\"}],\"content\": [{\"type\": \"text/html\", \"value\": \"" + body + "\"}],\"from\":{\"email\":\"" + from1 + "\",\"name\":\"" + mailname + "\"}}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request); 
        //    var mailresponce = response.Content;
        //    return mailresponce;

        //}

        //public static string SendMail(string to, string subject, string body)
        //{
        //    string body1 = "";
        //    string txt = body.Trim();
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    if (txt.Contains(Environment.NewLine))
        //    {
        //        string[] splitTxt = txt.Split(Environment.NewLine.ToCharArray());
        //        foreach (string s in splitTxt)
        //        {
        //            if (s != string.Empty)
        //            {
        //                body1 += string.Format("{0}{1}", s.Trim(), " ");
        //            }
        //        }
        //        body = body1;
        //    }

        //    body = body.Replace("\r\n", " ");
        //    body = body.Replace("\"", "\\\"");

        //    string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();

        //    string mailname = ConfigurationManager.AppSettings["mailname"].ToString();
        //    var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["GridMailKey"].ToString());
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"" + to + "\"}],\"subject\":\"" + subject + "\"}],\"content\": [{\"type\": \"text/html\", \"value\": \"" + body + "\"}],\"from\":{\"email\":\"" + from1 + "\",\"name\":\"" + mailname + "\"}}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    var mailresponce = response.Content;
        //    return mailresponce;

        //}

        //public static string SendMailWithAttachmentGrid(string to, string subject, string AttUrl, string AttName, string body)
        //{
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    string body1 = "";
        //    string txt = body.Trim();
        //    if (txt.Contains(Environment.NewLine))
        //    {
        //        string[] splitTxt = txt.Split(Environment.NewLine.ToCharArray());
        //        foreach (string s in splitTxt)
        //        {
        //            if (s != string.Empty)
        //            {
        //                body1 += string.Format("{0}{1}", s.Trim(), " ");
        //            }
        //        }
        //        body = body1;
        //    }

        //    body = body.Replace("\r\n", " ");
        //    body = body.Replace("\"", "\\\"");

        //     string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();

        //    string mailname = ConfigurationManager.AppSettings["mailname"].ToString();
        //    var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["GridMailKey"].ToString());
        //    request.AddHeader("Content-Type", "application/json");

        //    request.AddParameter("application/json", "{  \r\n   \"sender\":{  \r\n      \"name\":\"" + mailname + "\",\r\n      \"email\":\"" + from1 + "\"\r\n   },\r\n   \"to\":[  \r\n      {  \r\n         \"email\":\"" + to + "\",\r\n         \"name\":\"" + mailname + "\"\r\n      }\r\n   ],\r\n  \"attachment\": [{\"url\": \"" + AttUrl + "\", \"name\": \"" + AttName + "\"}], \"subject\":\"" + subject + "\",\r\n   \"htmlContent\":\"" + body + "\"\r\n}", ParameterType.RequestBody);



        //    IRestResponse response = client.Execute(request);
        //    var mailresponce = response.Content;
        //    return mailresponce;

        //}


        //public static void SendMailwithAttachmentbkp(string to, string subject, string body, HttpPostedFileBase fileUploader, HttpPostedFileBase fileUploader1)
        //{
        //    try
        //    {
        //        string[] emails = to.Split(',');
        //        string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();
        //        foreach (string recipientemail in emails)
        //        {
        //            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        //            MailAddress from = new MailAddress(from1);
        //            MailAddress to1 = new MailAddress(recipientemail);
        //            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to1);
        //            mm.BodyEncoding = Encoding.UTF8;
        //            mm.Subject = subject;
        //            mm.Body = body;
        //            mm.Priority = System.Net.Mail.MailPriority.High;
        //            if (fileUploader != null)
        //            {

        //                string fileName = Path.GetFileName(fileUploader.FileName);

        //                mm.Attachments.Add(new System.Net.Mail.Attachment(fileUploader.InputStream, fileName));

        //            }
        //            if (fileUploader1 != null)
        //            {

        //                string fileName1 = Path.GetFileName(fileUploader1.FileName);

        //                mm.Attachments.Add(new System.Net.Mail.Attachment(fileUploader1.InputStream, fileName1));

        //            }
        //            mm.IsBodyHtml = true;
        //            System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient(settings.Smtp.Network.Host, settings.Smtp.Network.Port);
        //            Client.Credentials = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        //            if (ConfigurationManager.AppSettings["EnableSsl"] == "true")
        //            { Client.EnableSsl = true; }
        //            else { Client.EnableSsl = false; }
        //            Client.Send(mm);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static void SendMailwithAttachment1(string to, string subject, string body, System.Net.Mail.Attachment attachment)
        //{
        //    try
        //    {
        //        string[] emails = to.Split(',');
        //        string from1 = ConfigurationManager.AppSettings["adminemailid"].ToString();
        //        foreach (string recipientemail in emails)
        //        {
        //            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        //            MailAddress from = new MailAddress(from1);
        //            MailAddress to1 = new MailAddress(recipientemail);
        //            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(from, to1);
        //            mm.BodyEncoding = Encoding.UTF8;
        //            mm.Subject = subject;
        //            mm.Body = body;
        //            mm.Priority = System.Net.Mail.MailPriority.High;

        //            if (attachment != null)
        //            {
        //                mm.Attachments.Add(attachment);
        //            }

        //            mm.IsBodyHtml = true;
        //            System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient(settings.Smtp.Network.Host, settings.Smtp.Network.Port);
        //            Client.Credentials = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        //            if (ConfigurationManager.AppSettings["EnableSsl"] == "true")
        //            { Client.EnableSsl = true; }
        //            else { Client.EnableSsl = false; }
        //            Client.Send(mm);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static string CreateXMLForObject<T>(List<T> obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            // ✅ Remove XML declaration to avoid encoding mismatch with SQL Server
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,   // ✅ removes <?xml version="1.0" encoding="utf-16"?>
                Encoding = Encoding.UTF8,
                Indent = false
            };

            using (StringWriter sw = new StringWriter())
            using (XmlWriter xw = XmlWriter.Create(sw, settings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", ""); // ✅ removes xmlns declarations
                serializer.Serialize(xw, obj, ns);
                return sw.ToString();
            }
        }
        //public static string CreateXMLForObject(Object YourClassObject)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
        //    // Initializes a new instance of the XmlDocument class.          
        //    XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
        //    // Creates a stream whose backing store is memory. 
        //    using (MemoryStream xmlStream = new MemoryStream())
        //    {
        //        xmlSerializer.Serialize(xmlStream, YourClassObject);
        //        xmlStream.Position = 0;
        //        //Loads the XML document from the specified string.
        //        xmlDoc.Load(xmlStream);
        //        return xmlDoc.InnerXml;
        //    }
        //}

        public static string EncodeURL(string input)
        {
            return Regex.Replace(input.Trim(), "[&/:*?<>|.]", string.Empty).Replace(" ", "-").Replace("--", "-").Replace("---", "-").Replace("----", "-").ToLower();
        }

        public static string DecodeURL(string input)
        {
            return input.Trim().Replace("_", "/").Replace("-", " ").Replace(".", "-");
        }

        public static Guid generateGUID()
        {
            return Guid.NewGuid();
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string GetRandomString(int maxSize)
        {
            char[] chars = new char[80];
            chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string GetRandomNumber(int maxSize)
        {
            char[] chars = new char[80];
            chars =
            "123".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }

        public static string Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Encrypt(string input)
        {
            string key = "sblw-3hn8-sqoy19";
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input)
        {
            string key = "sblw-3hn8-sqoy19";
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static class CSVUtility
        {
            public static MemoryStream GetCSV(DataTable data)
            {
                string[] fieldsToExpose = new string[data.Columns.Count];
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    fieldsToExpose[i] = data.Columns[i].ColumnName;
                }

                return GetCSV(fieldsToExpose, data);
            }

            public static MemoryStream GetCSV(string[] fieldsToExpose, DataTable data)
            {
                MemoryStream stream = new MemoryStream();
                using (var writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < fieldsToExpose.Length; i++)
                    {
                        if (i != 0) { writer.Write(","); }
                        //writer.Write("\"");
                        writer.Write(fieldsToExpose[i]);
                        //writer.Write("\"");                    
                    }
                    //writer.Write("\n");
                    writer.Write(Environment.NewLine);

                    foreach (DataRow row in data.Rows)
                    {
                        for (int i = 0; i < fieldsToExpose.Length; i++)
                        {
                            if (i != 0) { writer.Write(","); }
                            //writer.Write("\"");
                            writer.Write(Regex.Replace(row[fieldsToExpose[i]].ToString(), @"\t|\n|\r", ""));
                            //writer.Write("\"");
                        }
                        //writer.Write("\n");
                        writer.Write(Environment.NewLine);
                    }
                }

                return stream;
            }
        }
        public static string SetPaging(Int64 RecordsPerPage, ref Int64 TotalRecords, Int64 CurrentPageNo, string pclass)
        {
            Int64 Page_Mod = default(Int64);
            Int64 Page_Size = default(Int64);
            StringBuilder sbPage_HTML = new StringBuilder();
            string strRet = string.Empty;
            string strRSS = string.Empty;
            Int64 Page_Mid = 0;
            Int64 MaxPageSize = 5;

            if (TotalRecords > RecordsPerPage)
            {
                Page_Mid = (MaxPageSize / 2);
                Page_Mid = Page_Mid + 1;

                if (RecordsPerPage > 0)
                {
                    Page_Mod = TotalRecords % RecordsPerPage;
                    Page_Size = (TotalRecords / RecordsPerPage);
                    if (Page_Mod > 0)
                    {
                        Page_Size = Page_Size + 1;
                    }
                }
                else
                {
                    Page_Size = 1;
                }

                if ((Page_Size > 1))
                {
                    Int64 Start = ((CurrentPageNo - 1) * RecordsPerPage) + 1;
                    Int64 End = ((CurrentPageNo - 1) * RecordsPerPage) + RecordsPerPage;
                    if (End > TotalRecords)
                        End = TotalRecords;
                    sbPage_HTML.Append("<div class=\"clearfix\"><ul class=\"" + pclass + "\">");
                    //sbPage_HTML.Append("<div class='pagination'>v><ul>");
                }

                bool isShow_Forward = true;

                if (CurrentPageNo > Page_Mid)
                {
                    sbPage_HTML.Append("<li class=\"page-item\"><a class=\"page-link\" id ='Previous'>" + "Previous" + "</a></li>");
                }

                for (int i = 1; i <= MaxPageSize; i++)
                {
                    Int64 PageId = default(Int64);
                    if (CurrentPageNo > Page_Mid)
                    {
                        PageId = CurrentPageNo + (i - Page_Mid);
                    }
                    else
                    {
                        PageId = i;
                    }
                    if (PageId <= Page_Size)
                    {
                        if (Convert.ToInt64(CurrentPageNo) == PageId)
                        {
                            sbPage_HTML.Append("<li class=\"page-item active\"><a class=\"page-link\">" + PageId.ToString() + "</a></li>");

                        }
                        else
                        {
                            sbPage_HTML.Append("<li class=\"page-item\"><a class=\"page-link\" id='" + PageId.ToString() + "'>" + PageId.ToString() + "</a></li>");
                            //sbPage_HTML.Append("<a id='pgr_" + PageId.ToString() + "' href='?pageno=" + PageId.ToString() + "'>" + PageId.ToString() + "</a>");

                        }
                    }
                    if (PageId == Page_Size)
                    {
                        isShow_Forward = false;
                    }
                }


                if (isShow_Forward == true)
                {
                    sbPage_HTML.Append("<li class=\"page-item\"><a class=\"page-link\" id='Next'>" + "Next" + "</a></li>");
                }

                if ((Page_Size > 1))
                {
                    sbPage_HTML.Append("</ul></div>");
                }

            }

            strRet = sbPage_HTML.ToString();
            return strRet;
        }

        public static string SetPaging1(Int64 RecordsPerPage, ref Int64 TotalRecords, Int64 CurrentPageNo, string pclass)
        {
            Int64 Page_Mod = default(Int64);
            Int64 Page_Size = default(Int64);
            StringBuilder sbPage_HTML = new StringBuilder();
            string strRet = string.Empty;
            string strRSS = string.Empty;
            Int64 Page_Mid = 0;
            Int64 MaxPageSize = 5;

            if (TotalRecords > RecordsPerPage)
            {
                Page_Mid = (MaxPageSize / 2);
                Page_Mid = Page_Mid + 1;

                if (RecordsPerPage > 0)
                {
                    Page_Mod = TotalRecords % RecordsPerPage;
                    Page_Size = (TotalRecords / RecordsPerPage);
                    if (Page_Mod > 0)
                    {
                        Page_Size = Page_Size + 1;
                    }
                }
                else
                {
                    Page_Size = 1;
                }

                if ((Page_Size > 1))
                {
                    Int64 Start = ((CurrentPageNo - 1) * RecordsPerPage) + 1;
                    Int64 End = ((CurrentPageNo - 1) * RecordsPerPage) + RecordsPerPage;
                    if (End > TotalRecords)
                        End = TotalRecords;
                    sbPage_HTML.Append("<div class=\"clearfix\" style=\"text-align: center;margin-left: auto;margin-right: auto;display: block ruby;\"><ul class=\"" + pclass + "\">");
                    //sbPage_HTML.Append("<div class='pagination'>v><ul>");
                }

                bool isShow_Forward = true;

                if (CurrentPageNo > Page_Mid)
                {
                    sbPage_HTML.Append("<li class=\"page-item\"><a class=\"page-link\" id ='Previous'>" + "Previous" + "</a></li>");
                }

                for (int i = 1; i <= MaxPageSize; i++)
                {
                    Int64 PageId = default(Int64);
                    if (CurrentPageNo > Page_Mid)
                    {
                        PageId = CurrentPageNo + (i - Page_Mid);
                    }
                    else
                    {
                        PageId = i;
                    }
                    if (PageId <= Page_Size)
                    {
                        if (Convert.ToInt64(CurrentPageNo) == PageId)
                        {
                            sbPage_HTML.Append("<li class=\"page-item active\"><a class=\"page-link\">" + PageId.ToString() + "</a></li>");

                        }
                        else
                        {
                            sbPage_HTML.Append("<li class=\"page-item\"><a class=\"page-link\" id='" + PageId.ToString() + "'>" + PageId.ToString() + "</a></li>");
                            //sbPage_HTML.Append("<a id='pgr_" + PageId.ToString() + "' href='?pageno=" + PageId.ToString() + "'>" + PageId.ToString() + "</a>");

                        }
                    }
                    if (PageId == Page_Size)
                    {
                        isShow_Forward = false;
                    }
                }


                //if (isShow_Forward == true)
                //{
                //    sbPage_HTML.Append("<li class=\"page-item\"><a class=\"page-link\" id='Next'>" + "Next" + "</a></li>");
                //}

                if ((Page_Size > 1))
                {
                    sbPage_HTML.Append("</ul></div>");
                }

            }

            strRet = sbPage_HTML.ToString();
            return strRet;
        }

        //public static string StripePaymentIntent(string payment_method_types, decimal amount, string currency)
        //{
        //    var client = new RestClient("https://api.stripe.com/v1/payment_intents");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", "Bearer " + System.Configuration.ConfigurationManager.AppSettings["StripeKey"]);
        //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //    request.AddHeader("Cookie", "machine_identifier=2H5JDnZ8qGTuqnL5Th%2FBbJsA%2FZ6xq%2B3CKeae%2BMncT6fvgGeoQf2PDWrdU%2FXq3kg4z3Q%3D; private_machine_identifier=HXTYySM4Iud%2Br1ekAKBbBNxUXkLVCWhE13fMHVziZVvx8VS70qnIJGUb0bOOsT0%2FVbs%3D; __stripe_orig_props=%7B%22referrer%22%3A%22%22%2C%22landing%22%3A%22https%3A%2F%2Fdashboard.stripe.com%2Faccount%2Fapikeys%22%7D");
        //    request.AddParameter("amount", amount);
        //    request.AddParameter("currency", currency);
        //    request.AddParameter("payment_method_types[]", payment_method_types);
        //    IRestResponse response = client.Execute(request);
        //    var mailresponce = response.Content;
        //    return mailresponce;
        //}

        #region AddingContactsIntoBrevo

        static async Task Main()
        {
            // Replace "YourApiEndpoint" with the actual API endpoint provided by Brevo
            string apiEndpoint = "https://api.brevo.com/addContact";

            // Replace with your actual contact data
            var contactData = new
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890"
                // Add other contact properties as needed
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Convert contactData to JSON format
                    var jsonContactData = Newtonsoft.Json.JsonConvert.SerializeObject(contactData);

                    // Make a POST request to the API endpoint
                    var response = await httpClient.PostAsync(apiEndpoint, new StringContent(jsonContactData, System.Text.Encoding.UTF8, "application/json"));

                    // Check if the request was successful (status code 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Contact added successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add contact. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static string AddContactList(string email, string firstName, string lastName, int[] listIds, string sms = "+1000-000-0000")
        {
            int status = 0;
           AWSFormBuilder.Areas.FormBuilder.BLL.AppInfo _AppInfo = new BLL.AppInfo();

            Entities.AppInfo objappinfo = new Entities.AppInfo();
            objappinfo = _AppInfo.GetAppInfoDetails(ref status);
            // Input validation
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email address cannot be empty.", nameof(email));

            if (listIds == null || listIds.Length == 0)
                throw new ArgumentException("At least one list ID must be provided.", nameof(listIds));

            // WARNING: Move this to config/environment variable — never hardcode API keys in source code.
            const string apiKey = "xkeysib-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

            var contactPayload = new
            {
                email = email,
                attributes = new
                {
                    FIRSTNAME = firstName,
                    LASTNAME = lastName,
                    SMS = sms
                },
                listIds = listIds
            };

            var jsonBody = JsonConvert.SerializeObject(contactPayload);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("api-key", objappinfo.BrevoKey);

                var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync("https://api.brevo.com/v3/contacts", httpContent)
                                         .GetAwaiter()
                                         .GetResult();

                string responseContent = response.Content.ReadAsStringAsync()
                                                  .GetAwaiter()
                                                  .GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Brevo API error [{(int)response.StatusCode}]: {responseContent}");
                }

                return responseContent;
            }
        }


        //public static string AddContactList(string email, string FIRSTNAME, string LASTNAME, int[] arr, string SMS)
        //{
        //    SMS = "+1000-000-0000";
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        //               | SecurityProtocolType.Tls11
        //               | SecurityProtocolType.Tls12
        //               | SecurityProtocolType.Ssl3;
        //    var client = new RestClient("https://api.brevo.com/v3/contacts");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("api-key", "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");
        //    request.AddHeader("Cookie", "__cf_bm=7kmPe.jXN.sX1d1haElqTiBmnOHuyngn8hDeM25QOHE-1697541054-0-AeerXw5guGGB46IqGh5PXa961aZe4dBeTXyeyrGXwlylyEJHzjR3aCost7xionooYdtuMLmOXlvTGymoDARalPE=");

        //    var emailBody = "{\"email\": \"" + email + "\",\"attributes\":{\"FIRSTNAME\":\"" + FIRSTNAME + "\",\"LASTNAME\":\"" + LASTNAME + "\",\"SMS\":\"" + SMS + "\"},\"listIds\":" + arr + "}";

        //    request.AddParameter("application/json", emailBody, ParameterType.RequestBody);

        //    IRestResponse response = client.Execute(request);
        //    var mailResponse = response.Content;
        //    return mailResponse;

        //}

        //public static string ScheduleEmails(string toEmail, string subject, string bodyContent, string rfc33399format)
        //{
        //    string senderEmail = ConfigurationManager.AppSettings["adminemailid"].ToString();
        //    //string senderEmail = fromMail; // Use the new parameter for sender's email
        //    String to = toEmail;
        //    string senderName = ConfigurationManager.AppSettings["mailname"].ToString();
        //    string recipientName = ConfigurationManager.AppSettings["mailname"].ToString();

        //    var client = new RestClient("https://api.brevo.com/v3/smtp/email");
        //    //client.Timeout = -1;
        //    var request = new RestRequest(Method.Post);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("api-key", "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");
        //    request.AddHeader("Cookie", "__cf_bm=7kmPe.jXN.sX1d1haElqTiBmnOHuyngn8hDeM25QOHE-1697541054-0-AeerXw5guGGB46IqGh5PXa961aZe4dBeTXyeyrGXwlylyEJHzjR3aCost7xionooYdtuMLmOXlvTGymoDARalPE=");

        //    var emailBody = new
        //    {
        //        sender = new
        //        {
        //            name = senderName,
        //            email = senderEmail
        //        },
        //        to = new[]
        //        {
        //            new
        //            {
        //               name = recipientName,
        //                email = to

        //            }
        //        },
        //        templateId = 2,
        //        //scheduledAt= "2023-12-17T11:25:00+05:30a",
        //        scheduledAt = rfc33399format,
        //        htmlContent = bodyContent,
        //        subject = subject,
        //        replyTo = new
        //        {
        //            email = senderEmail,
        //            name = senderName
        //        },
        //        tags = new[] { "tag1", "tag2" }
        //    };

        //    var body = JsonConvert.SerializeObject(emailBody);
        //    request.AddParameter("application/json", body, ParameterType.RequestBody);

        //    RestResponse response = client.Execute(request);
        //    var mailResponse = response.Content;
        //    return mailResponse;
        //}

        //public static void Send()
        //{
        //    // Set your email credentials and details
        //    string fromEmail = "ck.seema@innovateindia.in";
        //    string toEmail = "azmiashaik.786@gmail.com";
        //    string subject = "Scheduled Email";
        //    string body = "This is a scheduled email.";

        //    // Set the date and time to send the email
        //    DateTime sendDateTime = DateTime.Now.AddMonths(2); // Adjust as needed

        //    // Create a MailMessage object
        //    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(fromEmail, toEmail, subject, body);

        //    // Create a SmtpClient object
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential(fromEmail, "YourPassword"), // Replace with your password
        //        EnableSsl = true,
        //    };

        //    // Schedule the email using a Timer
        //    Timer timer = new Timer(SendEmail, mailMessage, (int)(sendDateTime - DateTime.Now).TotalMilliseconds, Timeout.Infinite);

        //    Console.WriteLine($"Email scheduled to be sent on {sendDateTime}");

        //    // Keep the program running
        //    Console.ReadLine();
        //}

        //static void SendEmail(object state)
        //{
        //    // Cast the state back to a MailMessage
        //    System.Net.Mail.MailMessage mailMessage = (System.Net.Mail.MailMessage)state;

        //    // Send the email
        //    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
        //    {
        //        smtpClient.Port = 587;
        //        smtpClient.Credentials = new NetworkCredential(mailMessage.From.Address, "YourPassword"); // Replace with your password
        //        smtpClient.EnableSsl = true;
        //        smtpClient.Send(mailMessage);
        //    }

        //    Console.WriteLine("Email sent successfully.");
        //}

        #endregion

        #region RecapchaV3

        public static async Task<bool> VerifyRecaptchaToken(string recaptchaToken)
        {
            BLL.AppInfo _appinfo = new BLL.AppInfo();
            int status1 = 0;
            Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);

            string RecaptchaSecretKey = objAppInfo.CapchaSecreatKey;
            string RecaptchaVerifyUrl = "https://www.google.com/recaptcha/api/siteverify";

            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", RecaptchaSecretKey),
                    new KeyValuePair<string, string>("response", recaptchaToken)
                });

                var response = await httpClient.PostAsync(RecaptchaVerifyUrl, content);
                var json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);

                // Verify the score and success
                return result.success == true && result.score >= 0.5; // Adjust score threshold if needed
            }
        }

        #endregion

        #region RecapchaV2-IamNotRobot

        public static async Task<bool> ValidateCaptchaAsync(string captchaResponse, string clientHost)
        {
            BLL.AppInfo _appinfo = new AppInfo();
            int status1 = 0;
            Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);

            // Secret key from configuration
            string secretKey = objAppInfo.CapchaSecreatKey;
            string captchaVerificationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}";

            // Return false if the captcha response is missing
            if (string.IsNullOrEmpty(captchaResponse))
            {
                return false;
            }

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send the verification request to Google reCAPTCHA API
                    var response = await httpClient.GetStringAsync(captchaVerificationUrl);

                    // Parse the response
                    var json = JObject.Parse(response);

                    // Check if the CAPTCHA validation succeeded
                    bool success = json.Value<bool>("success");

                    // Optional: Check additional fields like hostname for extra security
                    string hostname = json.Value<string>("hostname");
                    if (success && hostname == clientHost)
                    {
                        // For reCAPTCHA v3: Validate score (if applicable)
                        if (json.TryGetValue("score", out JToken scoreToken))
                        {
                            double score = scoreToken.Value<double>();
                            if (score >= 0.7) // Adjust score threshold based on your preference
                            {
                                return true;
                            }
                            else
                            {
                                // Log low-score attempts for monitoring
                                Console.WriteLine($"Low CAPTCHA score: {score}");
                            }
                        }
                        else
                        {
                            return true; // For v2 CAPTCHA, we rely only on success and hostname.
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exceptions for debugging purposes
                Console.WriteLine($"CAPTCHA validation error: {ex.Message}");
            }

            return false;
        }

        #endregion

        //#region AbuseIPDB API
        //public async Task<bool> IsSpamIP(string ipAddress)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Key", "YOUR_API_KEY"); // Replace with your AbuseIPDB API key
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");

        //    var response = await client.GetAsync($"https://api.abuseipdb.com/api/v2/check?ipAddress={ipAddress}");
        //    var json = await response.Content.ReadAsStringAsync();

        //    dynamic result = JsonConvert.DeserializeObject(json);
        //    return result.data.abuseConfidenceScore > 50; // Consider spam if the score exceeds 50
        //}

        //public bool IsBot(HttpRequest request)
        //{
        //    var userAgent = request.Headers["User-Agent"].ToString();
        //    return string.IsNullOrEmpty(userAgent) ||
        //           userAgent.Contains("bot") ||
        //           userAgent.Contains("crawler") ||
        //           userAgent.Contains("spider");
        //}

        public async Task<bool> IsSuspiciousRegion(string ipAddress)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync($"http://api.ipstack.com/{ipAddress}?access_key=YOUR_ACCESS_KEY");
            dynamic data = JsonConvert.DeserializeObject(response);
            return data.country_code == "CN" || data.country_code == "RU"; // Example: block China and Russia
        }

        //internal static DateTime GetTimeZoneAdjustedDateTime()
        //{
        //    throw new NotImplementedException();
        //}

        public static DateTime GetTimeZoneAdjustedDateTime()
        {
            return DateTime.Now; // ✅ simplest fix
        }





        //public static DateTime GetTimeZoneAdjustedDateTime()
        //{
        //    int status = 0;
        //    // Assuming status is defined somewhere in your code
        //    //string status = string.Empty;

        //    AWSFormBuilder.Entities.AppInfo objAppInfo = new AWSFormBuilder.Entities.AppInfo();
        //   AWSFormBuilder.Areas.FormBuilder.BLL.AppInfo _AppInfo = newAWSFormBuilder.Areas.FormBuilder.BLL.AppInfo();
        //    objAppInfo = _AppInfo.GetAppInfoDetails(ref status);

        //    string timezone = objAppInfo?.TimeZones ?? "UTC";
        //    DateTime utcNow = DateTime.UtcNow;

        //    try
        //    {
        //        if (timezone == "Indian Standard Time")
        //        {
        //            //return utcNow.AddHours(5.5);
        //            //DateTime utcNow = DateTime.UtcNow;
        //            //return utcNow.Add(TimeSpan.FromHours(5.5));
        //            //DateTime istNow = utcNow.Add(TimeSpan.FromHours(5.5));
        //            //DateTime utcNow = DateTime.UtcNow;
        //            DateTime istNow = utcNow.Add(TimeSpan.FromHours(5.5));
        //            return istNow;


        //        }
        //        else
        //        {
        //            //TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        //            //return TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZoneInfo);


        //            // DateTime utcNow = DateTime.UtcNow;
        //            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
        //            DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, easternZone);
        //            //objEventUserInfo.InsertedDate = easternTime;
        //            //objEventUserInfo.UpdatedTime = easternTime;
        //            return istNow;

        //        }
        //    }
        //    catch (TimeZoneNotFoundException)
        //    {
        //        // Fallback to UTC if the provided timezone is invalid
        //        return utcNow;
        //    }
        //    catch (InvalidTimeZoneException)
        //    {
        //        // Handle invalid time zone exception
        //        return utcNow;
        //    }

        //}




    }

    public interface IWebHostEnvironment
    {
        string WebRootPath { get; set; }
        string ContentRootPath { get; set; }
    }
}



