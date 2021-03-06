﻿namespace CinemaShow.Application.App_Start
{
    using System.Net.Http.Headers;
    using System.Web.Http;

    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters
                .JsonFormatter
                .SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/json"));
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}