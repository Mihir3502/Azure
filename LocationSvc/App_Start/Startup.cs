﻿using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.ActiveDirectory;
using System.Configuration;

[assembly:OwinStartup(typeof(LocationSvc.App_Start.Startup))]
namespace LocationSvc.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigurationAuth(app);
        }

        private void ConfigurationAuth(IAppBuilder app)
        {
            //var azureADBearerAuthOptions = new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            //{
            //    Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
            //    Audience = ConfigurationManager.AppSettings["ida:Audience"]
            //};
            //azureADBearerAuthOptions.TokenValidationParameters =
            //    new System.IdentityModel.Tokens.TokenValidationParameters() {
            //        ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
            //    };
            //app.UseWindowsAzureActiveDirectoryBearerAuthentication(azureADBearerAuthOptions);
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
               new WindowsAzureActiveDirectoryBearerAuthenticationOptions
               {
                   Audience = ConfigurationManager.AppSettings["ida:Audience"],
                   Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
               });

        }
    }
}