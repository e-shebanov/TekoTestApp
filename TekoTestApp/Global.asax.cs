using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TekoTestApp.Models;
using TekoTestApp.Services;

namespace TekoTestApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new ApplicationDbContextInitializer()); // ���������� ��� �������������
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class ApplicationDbContextInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // ������� ��������� DataGeneratorService
            DataGeneratorService dataGenerator = new DataGeneratorService();

            // ���������� ������ �����������
            List<Employee> employees = dataGenerator.GenerateRandomEmployees(100); // ���������� �������� ���������� �����������

            // ���������� ������ �������� ��� �����������
            List<Vacation> vacations = dataGenerator.GenerateRandomVacations(employees);

            // ��������� ��������������� ������ � �������� ������
            context.Employees.AddRange(employees);
            context.Vacations.AddRange(vacations);

            // ��������� ��������� � ���� ������
            context.SaveChanges();
        }
    }
}
