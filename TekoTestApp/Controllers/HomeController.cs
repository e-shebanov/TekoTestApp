using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TekoTestApp.Models;
using TekoTestApp.Services;

namespace TekoTestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateData()
        {
            DataGeneratorService dataGeneratorService = new DataGeneratorService();

            List<Employee> employees = dataGeneratorService.GenerateRandomEmployees(100);

            List<Vacation> vacations = dataGeneratorService.GenerateRandomVacations(employees);

            using (var context = new ApplicationDbContext())
            {
                context.Employees.AddRange(employees);
                context.Vacations.AddRange(vacations);

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult ViewEmployees()
        {
            return View();
        }

        public ActionResult ViewVacations()
        {
            return View();
        }
    }

}