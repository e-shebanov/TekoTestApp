using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TekoTestApp.Models;
using TekoTestApp.ViewModels;

namespace TekoTestApp.Controllers
{
    public class VacationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var viewModel = new VacationViewModel
            {
                Vacations = db.Vacations.Include("Employee").ToList(),
                NewVacation = new Vacation() // Инициализируем объект для формы
            };
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(VacationViewModel vacation)
        {
            if (ModelState.IsValid)
            {
                // Проверьте длину отпуска
                TimeSpan vacationLength = vacation.NewVacation.EndDate - vacation.NewVacation.StartDate;
                var employee = db.Employees.Find(vacation.NewVacation.Employee.Id);
                if (vacationLength.TotalDays <= 14)
                {

                    if (employee == null)
                    {
                        return HttpNotFound();
                    }

                    var newVacation = new Vacation
                    {
                        StartDate = vacation.NewVacation.StartDate,
                        EndDate = vacation.NewVacation.EndDate,
                        Employee = employee 
                    };

                    db.Vacations.Add(newVacation);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Employee", new { id = employee.Id });
                }
                else
                {
                    return RedirectToAction("Details", "Employee", new { id = employee.Id });
                }
            }

            return View(vacation);
        }

        [HttpPost]
        public ActionResult DeleteVacation(int employeeId, int vacationId)
        {
            var vacation = db.Vacations.Find(vacationId);

            if (vacation == null)
            {
                return HttpNotFound();
            }

            db.Vacations.Remove(vacation);
            db.SaveChanges();

            return RedirectToAction("Details", "Employee", new { id = employeeId });
        }



    }
}
