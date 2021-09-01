using System;
using System.Web;
using Service.provider;
using Autofac;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Service
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        static Startup()
        {
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAppProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };
        }

        public void ConfigureAuth(IAppBuilder app )
        {
            app.Use((context, next) =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("Authorization");
                return next.Invoke();
            });
            app.UseOAuthBearerTokens(OAuthOptions);
            app.Use(async (context, next) =>
            {
                String path = HttpContext.Current.Request.Url.PathAndQuery;

                if (path.EndsWith(".css") || path.EndsWith(".js"))
                {

                    //Set css and js files to be cached for 30 days
                    TimeSpan maxAge = new TimeSpan(30, 0, 0, 0);     //7 days
                    context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));

                }
                else if (path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".jpeg")
                || path.EndsWith(".png") || path.EndsWith(".webp") || path.EndsWith(".png") || path.EndsWith(".svg"))
                {
                    //custom headers for images goes here if needed
                    TimeSpan maxAge = new TimeSpan(30, 0, 0, 0);     //30days
                    context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                }
                else
                {
                    //Request for views fall here.
                    context.Response.Headers.Append("Cache-Control", "no-cache");
                    context.Response.Headers.Append("Cache-Control", "private, no-store");

                }
                await next();
            });
        }
    }
}