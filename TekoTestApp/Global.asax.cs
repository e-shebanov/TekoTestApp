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
            Database.SetInitializer(new ApplicationDbContextInitializer()); // Используем наш инициализатор
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
            // Создаем экземпляр DataGeneratorService
            DataGeneratorService dataGenerator = new DataGeneratorService();

            // Генерируем список сотрудников
            List<Employee> employees = dataGenerator.GenerateRandomEmployees(100); // Указывайте желаемое количество сотрудников

            // Генерируем список отпусков для сотрудников
            List<Vacation> vacations = dataGenerator.GenerateRandomVacations(employees);

            // Добавляем сгенерированные данные в контекст данных
            context.Employees.AddRange(employees);
            context.Vacations.AddRange(vacations);

            // Сохраняем изменения в базе данных
            context.SaveChanges();
        }
    }
}
