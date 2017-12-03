using System;
using System.Data.Entity;
using BlueTapeCrew.Identity;
using BlueTapeCrew.Models;
using KatanaContrib.Security.Instagram;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace BlueTapeCrew
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie("ExternalCookie");

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie("TwoFactorCookie", TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie("TwoFactorRememberBrowserCookie");

            using (var db = new BtcEntities())
            {
                var settings = db.SiteSettings.FirstOrDefaultAsync().Result;

                if (!string.IsNullOrEmpty(settings.MicrosoftClientId))
                    app.UseMicrosoftAccountAuthentication(
                    clientId: settings.MicrosoftClientId,
                    clientSecret: settings.MicrosoftClientSecret);

                if (!string.IsNullOrEmpty(settings.TwitterClientId))
                    app.UseTwitterAuthentication(
                   consumerKey: settings.TwitterClientId,
                   consumerSecret: settings.TwitterClientSecret);

                if (!string.IsNullOrEmpty(settings.FacebookClientId))
                    app.UseFacebookAuthentication(
                    appId: settings.FacebookClientId,
                   appSecret: settings.FacebookClientSecret);

                if (!string.IsNullOrEmpty(settings.GoogleClientId))
                    app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                    {
                        ClientId = settings.GoogleClientId,
                        ClientSecret = settings.GoogleClientSecret
                    });

                if (!string.IsNullOrEmpty(settings.InstagramClientId))
                    app.UseInstagramAuthentication(new InstagramAuthenticationOptions()
                    {
                        ClientId = settings.InstagramClientId,
                        ClientSecret = settings.InstagramClientSecret
                    });
            }
        }
    }
}