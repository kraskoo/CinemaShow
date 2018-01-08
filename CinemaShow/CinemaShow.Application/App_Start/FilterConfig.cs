namespace CinemaShow.Application.App_Start
{
    using System;
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(Exception),
                View = "CustomError"
            });
        }
    }
}