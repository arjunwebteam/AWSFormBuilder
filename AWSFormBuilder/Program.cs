
using AWSFormBuilder.Areas.FormBuilder.BLL;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Polly;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// ✅ SERVICES
// ============================================

// Distributed Memory Cache (REQUIRED before AddSession)
builder.Services.AddDistributedMemoryCache();

// MVC
//builder.Services.AddControllersWithViews();
#if DEBUG
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#else
builder.Services.AddControllersWithViews();
#endif// JSON PascalCase
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(
        builder.Configuration.GetValue<int>("Session:TimeoutMinutes") > 0
            ? builder.Configuration.GetValue<int>("Session:TimeoutMinutes")
            : 30);

    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "AWS_Session";
});

// Antiforgery
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});





// HttpContext
builder.Services.AddHttpContextAccessor();

// DataProtection
builder.Services.AddDataProtection();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/FormBuilder/Account/LogOn";
        options.LogoutPath = "/FormBuilder/Account/LogOut";
        options.AccessDeniedPath = "/FormBuilder/Account/LogOn";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2880);
        options.SlidingExpiration = true;
        options.Cookie.Name = "UserCookie";
        options.Cookie.HttpOnly = true;
    });


builder.Services.AddHttpClient();
// In Program.cs (ASP.NET Core 6+)
builder.Services.AddHttpClient("PayflowClient", client =>
{
    client.Timeout = TimeSpan.FromSeconds(45);
    client.DefaultRequestHeaders.Add("X-VPS-CLIENT-TIMEOUT", "45");
    // Add other default headers
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
});


//// Program.cs
//builder.Services.AddScoped<IBraintreeConfiguration, BraintreeConfiguration>();
// Authorization
builder.Services.AddAuthorization();

// IConfiguration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// ============================================
// ✅ BUILD APP
// ============================================

var app = builder.Build();


app.MapControllers();   // <-- ADD THIS LINE — required for FileManagerController (API controller)

// ============================================
// ✅ MIDDLEWARE
// ============================================

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Areas", "FormBuilder", "Content")),
    RequestPath = "/Areas/FormBuilder/Content"  
});
app.UseRouting();
// ============================================
// ✅ ROUTES (ALL)
// ============================================

// Root redirect
//app.MapGet("/", context =>
//{
//    context.Response.Redirect("/User/Home/Index");
//    return Task.CompletedTask;
//});

// ============================================
// INDEX
// ============================================
// ✅ PUT THESE AT THE TOP — before ANY wildcard routes
//app.MapControllerRoute(
//    name: "root",
//    pattern: "",
//    defaults: new { area = "User", controller = "Home", action = "Index" });
app.MapControllerRoute(
    name: "root",
    pattern: "",
    defaults: new { area = "FormBuilder", controller = "Account", action = "LogOn" });

app.MapControllerRoute(
    name: "EventRegistration",
    pattern: "event-registration",
    defaults: new { area = "User", controller = "Event", action = "EventRegistration" });
app.MapControllerRoute(
    name: "events",
    pattern: "{Type}-events",
    defaults: new { area = "User", controller = "Event", action = "Index" },
    constraints: new { Type = @"^(upcoming|past|current)$" }
);
app.MapControllerRoute("cultural-registration", "cultural-registration",
    new { area = "User", controller = "CulturalRegistrations", action = "Index" });

app.MapControllerRoute("cultural-acknowledgement", "{cname}/cultural-reg-acknowledgement",
    new { area = "User", controller = "CulturalRegistrations", action = "Acknowledgement" });

app.MapControllerRoute("cultural-reg-acknowledgement", "cultural-reg-acknowledgement",
    new { area = "User", controller = "CulturalRegistrations", action = "Acknowledgement" });
//// ✅ Then events index
//app.MapControllerRoute("events", "{Type}-events",
//    new { area = "User", controller = "Event", action = "Index" });

// ✅ Then all other routes BELOW
// ... members, gallery, etc.
// Members Routes
app.MapControllerRoute("my-donationview", "profile-donation-preview",
    new { area = "User", controller = "Members", action = "ViewServiceDonation" });

app.MapControllerRoute("registered-eventsview", "profile-event-preview",
    new { area = "User", controller = "Members", action = "ViewEventUser" });

app.MapControllerRoute("registered-membersview", "profile-memberinfo",
    new { area = "User", controller = "Members", action = "MemberInfo" });
app.MapControllerRoute("registered-membersview", "profile-cancelSubscription",
    new { area = "User", controller = "Members", action = "CancelSubscription" });


app.MapControllerRoute("registered-subscriptionDetails", "profile-subscriptionDetails",
    new { area = "User", controller = "Members", action = "SubscriptionDetails" });



app.MapControllerRoute("donations", "donations",
    new { area = "User", controller = "Members", action = "DonarIndex" });

app.MapControllerRoute("eventusers", "eventusers",
    new { area = "User", controller = "Members", action = "EventIndex" });

app.MapControllerRoute("balala-sambaralulist", "balala-sambaralulist",
    new { area = "User", controller = "Members", action = "BalalaSambaraluIndex" });

app.MapControllerRoute("my-donation", "profile-donations",
    new { area = "User", controller = "Members", action = "DonorsList" });

app.MapControllerRoute("registered-events", "profile-event",
    new { area = "User", controller = "Members", action = "FEMemEventsList" });

app.MapControllerRoute("reset-password", "reset-password",
    new { area = "User", controller = "Members", action = "ForgotPassword" });

app.MapControllerRoute(
    name: "subscription",
    pattern: "subscription",
    defaults: new { area = "FormBuilder", controller = "Account", action = "Subscription" });

app.MapControllerRoute("login", "login",
    new { area = "User", controller = "Members", action = "LogOn" });


app.MapControllerRoute("news", "news",
    new { area = "User", controller = "News", action = "Index" });


app.MapControllerRoute("test", "test",
    new { area = "User", controller = "Test", action = "Index" });


app.MapControllerRoute("eventqr", "eventqr",
    new { area = "User", controller = "Event", action = "EventQR" });


app.MapControllerRoute("logOff", "logOff",
    new { area = "User", controller = "Members", action = "LogOff" });


app.MapControllerRoute("forgot-password", "forgot-password",
    new { area = "User", controller = "Members", action = "ForgotPassword" });

app.MapControllerRoute("auth", "user-auth",
    new { area = "User", controller = "Members", action = "UserAuth" });

app.MapControllerRoute("renewal-membership", "renewal-membership",
    new { area = "User", controller = "Members", action = "MembershipRenewal" });

app.MapControllerRoute("RenewalAcknowledgement", "member-renewal-acknowledgement",
    new { area = "User", controller = "Members", action = "RenewalAcknowledgement" });



// Payment Routes
app.MapControllerRoute("member", "member/PaymentCancelled",
    new { area = "User", ontroller = "Members", action = "PaymentCancelled" });

//app.MapControllerRoute("index", "index",
//    new { area = "User", controller = "Home", action = "Index" });

app.MapControllerRoute("index-cname", "{cname}/index",
    new { area = "User", controller = "Home", action = "Index" });

// ============================================
// API ROUTES
// ============================================

app.MapControllerRoute("api-members-list", "api-active-members",
    new { area = "User", controller = "WebAPI", action = "MemberList" });

app.MapControllerRoute("apiCreateNewUser", "api-become-a-member",
    new { area = "User", controller = "WebAPI", action = "AddMember" });

app.MapControllerRoute("apileadership", "api-{Year}-leadership",
    new { area = "User", controller = "WebAPI", action = "CommitteeIndex" });

app.MapControllerRoute("apiphoto-gallery", "api-{Year}/photo-gallery",
    new { area = "User", controller = "WebAPI", action = "Photos" });

app.MapControllerRoute("photo-album", "{Year}/{CategoryName}/photo-gallery",
    new { area = "User", controller = "Gallery", action = "PhotosList" });

app.MapControllerRoute("apivideo-gallery", "api-video-gallery",
    new { area = "User", controller = "WebAPI", action = "Videos" });

// ============================================
// CONTACT
// ============================================

app.MapControllerRoute("contact-us", "contact-us",
    new { area = "User", controller = "Home", action = "ContactUs" });

app.MapControllerRoute("contact-thankyou", "thank-you",
    new { area = "User", controller = "Home", action = "Thankyou" });

// ============================================
// EVENTS
// ============================================

app.MapControllerRoute("EventDetails", "{cname}/event/{Type}/{EventName}",
    new { area = "User", controller = "Event", action = "EventDetails" });

//// ✅ Add this — Events Index with cname
//app.MapControllerRoute("events-cname", "{cname}/{Type}-events",
//    new { area = "User", controller = "Event", action = "Index" });

// ✅ Add this — EventsList (AJAX partial call)

// ============================================
// MEMBERS
// ============================================

app.MapControllerRoute("CreateNewUser", "become-a-member",
    new { area = "User", controller = "Members", action = "AddMember" });

app.MapControllerRoute("members-list", "active-members",
    new { area = "User", controller = "Members", action = "MemberList" });

// ============================================
// GALLERY
// ============================================

app.MapControllerRoute("volunteer", "volunteer-registration",
    new { area = "User", controller = "Volunteer", action = "AddVolunteer" });

app.MapControllerRoute("photo-gallery", "{Year}/photo-gallery",
    new { area = "User", controller = "Gallery", action = "Photos" });

app.MapControllerRoute("sign-in", "{cname}/member-login",
    new { area = "User", controller = "Members", action = "LogOn" });
app.MapControllerRoute("profile", "{cname}/profile",
    new { area = "User", controller = "Members", action = "Profile" });


app.MapControllerRoute("sponsorship", "sponsorship",
    new { area = "User", controller = "Gallery", action = "Sponsors" });
app.MapControllerRoute("video-gallery", "video-gallery",
    new { area = "User", controller = "Gallery", action = "Videos" });
// ============================================
// eVENTS
// ============================================

var shortUrl = builder.Configuration["ShortUrls:FormBuilder"];

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value?.ToLower() == "/FormBuilder")
    {
        context.Response.Redirect(shortUrl);
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "event-acknowledgement",
    pattern: "{cname}/event-registration-acknowledgement",
    defaults: new { area = "User", controller = "Event", action = "ThankYou" });

app.MapControllerRoute(
    name: "qrcode-individualtkt",
    pattern: "ticket-preview",
    defaults: new { area = "User", controller = "Event", action = "QRIndividualTicketPreview" });

app.MapControllerRoute(
    name: "eventAcknowledgement",
    pattern: "event-registration-acknowledgement",
    defaults: new { area = "User", controller = "Event", action = "ThankYou" });

app.MapControllerRoute(
    name: "formthankyou",
    pattern: "event-thankyou",
    defaults: new { area = "User", controller = "Event", action = "FormThankYou" });

app.MapControllerRoute(
    name: "PaymentEventForm1Acknowledgement",
    pattern: "event-paymenteventform1acknowledgement",
    defaults: new { area = "User", controller = "Event", action = "PaymentEventForm1Acknowledgement" });

app.MapControllerRoute(
    name: "event-acknowledgement1",
    pattern: "event-acknowledgement",
    defaults: new { area = "User", controller = "Event", action = "EventAcknowledgement" });

app.MapControllerRoute(
    name: "event",
    pattern: "event/PaymentCancelled",
    defaults: new { area = "User", controller = "Event", action = "PaymentCancelled" });

app.MapControllerRoute(
    name: "eventqr",
    pattern: "eventqr",
    defaults: new { area = "User", controller = "Event", action = "EventQR" });

app.MapControllerRoute(
    name: "qrcode",
    pattern: "qrcode-preview",
    defaults: new { area = "User", controller = "Event", action = "QRCodeView" });
// ============================================
// SERVICES
// ============================================

app.MapControllerRoute("services", "services",
    new { area = "User", controller = "Services", action = "ServicesIndex" });

app.MapControllerRoute("service-now", "donate-now",
    new { area = "User", controller = "Services", action = "Index" });

// ============================================
// COMMITTEE
// ============================================

app.MapControllerRoute("leadership", "{Year}-leadership",
    new { area = "User", controller = "Committee", action = "Index" });

// ============================================
// ERROR
// ============================================

app.MapControllerRoute("error-404", "error-404",
    new { area = "User", controller = "Error", action = "Error404" });

// ============================================
// ⚠️ LAST ROUTES (IMPORTANT ORDER)
// ============================================
app.MapControllerRoute(
    name: "memberAcknowledgement",
    pattern: "{cname}/member-registration-acknowledgement",
    new { area = "User", controller = "Members", action = "Acknowledgement" }
);

app.MapControllerRoute(
    name: "service-acknowledgement",
    pattern: "service-acknowledgement",
    new { area = "User", controller = "Services", action = "Acknowledgement" }
);
// Inner page
app.MapControllerRoute("innerpage-details", "{PageTitle}-details",
    new { area = "User", controller = "PageDetails", action = "GetPageDetails" });

app.MapControllerRoute("innerpage-details1", "p/{PageUrl}",
    new { area = "User", controller = "PageDetails", action = "GetPageDetails" });

// Areas
app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Catch-all
app.MapControllerRoute("redirection-details", "{PageUrl}",
    new { area = "User", controller = "Home", action = "RedirectionPage" });

// Default
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();


