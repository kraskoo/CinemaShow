﻿namespace CinemaShow.Application
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;
    using System.Web.Routing;

    using App_Start;
    using Data;
    using Data.Migrations;

    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CinemaShowContext, Configuration>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}