using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Auth
{
    public static class AuthSetup
    {
        public static void AddGoogleAuthentication(this IServiceCollection services, IConfigurationSection credentials)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => options.LoginPath = "/auth/google")
                    .AddGoogle(g =>
                    {
                        g.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        g.ClientId = credentials["Key"];
                        g.ClientSecret = credentials["Secret"];
                    });
        }
    }
}
