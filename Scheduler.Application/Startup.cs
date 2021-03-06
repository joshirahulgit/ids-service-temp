﻿using Microsoft.Owin;
using Scheduler.Application;

[assembly: OwinStartup(typeof(Startup))]
namespace Scheduler.Application
{
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using Core;
    using System;
    using Business.Implementation;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            String cs = @"Server=172.31.25.70;uid=sa;pwd=stagingsa;Trusted_Connection=False;Application Name=MachinesLocalDebuggingApplication;";
            //Set initial configuration for application.
            
            //Set all the application settings here.
            IApplicationSetting appSet = new ApplicationSetting(100, 4, cs, cs);
            GlobalContext.Add(appSet);


            var httpConfiguration = new HttpConfiguration();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

//#if DEBUG
            //Allow cros just for debug mode.
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
//#endif

            app.UseWebApi(httpConfiguration);

            //app.

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
