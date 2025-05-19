using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Security.Claims;

[assembly: OwinStartup(typeof(Startup))]

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        /* Google */
        var googleOptions = new GoogleOAuth2AuthenticationOptions
        {
            ClientId = "135598277253-nsu5e2100c2mhcf2fgpkefueav0tmu4b.apps.googleusercontent.com",
            ClientSecret = "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",

        };
        googleOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
        googleOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
        app.UseGoogleAuthentication(googleOptions);
    }
}
