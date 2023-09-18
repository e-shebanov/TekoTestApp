using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TekoTestApp.Models;  
using TekoTestApp.Services;
using TekoTestApp.ViewModels;

namespace TekoTestApp.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); 



        public ActionResult Index()
        {
            var employees = db.Employees.ToList(); 
            return View(employees); 
        }

        public ActionResult Details(int id)
        {
            EmployeeVacationService employeeVacationService = new EmployeeVacationService(db);
            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }


            var vacations = db.Vacations.Where(v => v.Employee.Id == id).ToList();

            var younger30MyDep = employeeVacationService.GetEmployeeVacationDataYounger30MyDep(id);
            var between30and50FemaleNotMyDep = employeeVacationService.GetEmployeeVacationDataBetween30and50FemaleNotMyDep(id);
            var older50AnyDep = employeeVacationService.GetEmployeeVacationDataOlder50AnyDep(id);
            var notIntersect = employeeVacationService.GetEmployeeVacationDataNoIntersection(id);

            var viewModel = new EmployeeDetailsViewModel
            {
                Employee = employee,
                Vacations = vacations,
                IntersectionFromMyDepYounger30 = younger30MyDep,
                Older50AnyDep = older50AnyDep,
                NotIntersection = notIntersect,
                Between30and50FemaleNotMyDep = between30and50FemaleNotMyDep
            };

            viewModel.NewVacation = new Vacation
            {
                Employee = employee
            };

            return View(viewModel);
        }




    }
}
