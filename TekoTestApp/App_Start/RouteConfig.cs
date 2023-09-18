using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TekoTestApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "Employee",
                url: "Employee/{action}/{id}",
                defaults: new { controller = "Employee", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Vacation",
                url: "Vacation",
                defaults: new { controller = "Vacation", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EmployeeDetails",
                url: "Employee/Details/{id}",
                defaults: new { controller = "Employee", action = "Details" }
            );

            routes.MapRoute(
                name: "VacationCreate",
                url: "Vacation/Create",
                defaults: new { controller = "Vacation", action = "Create" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            routes.MapRoute(
                name: "DeleteVacation",
                url: "Vacation/DeleteVacation/{employeeId}/{vacationId}",
                defaults: new { controller = "Vacation", action = "DeleteVacation" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Employee", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
