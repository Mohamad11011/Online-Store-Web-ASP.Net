using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.LinkedIn;
using Testing1.Models;

namespace Testing1
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure db context, user manager and signin manager to use a single instance/ request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp as user login
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //Linkedin login
            app.UseLinkedInAuthentication(new LinkedInAuthenticationOptions()
            {

                ClientId = "785mdpg5y4gsbk",
                ClientSecret = "GOCSPX-AgNbwV2h2joeGi57"
            });

            //facebook login:
            app.UseFacebookAuthentication( new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            { 
                AppId= "1505384849857108",
              AppSecret ="7592e6db8e69d99dc9e7f706a1410129"
            });

            //Google login
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {

                    ClientId = "393328175869-givh963ft09qm3m05qu785ii99qlpn73.apps.googleusercontent.com",
                    ClientSecret = "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"
            });
        }
    }
}