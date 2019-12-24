using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MyAPI.Configuration.Context;
using MyAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Configuration
{
    public static class IdentityConfig
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/V1/Users"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(100),
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "ioasys";

            }).AddCookie()
              .AddOAuth("ioasys", options =>
              {
                  options.ClientId = configuration["ioasys:ClientId"];
                  options.ClientSecret = configuration["ioasys:ClientSecret"];
              });

            return services;
        }

    }

}
