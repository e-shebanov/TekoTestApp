using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TekoTestApp.Models;

namespace TekoTestApp.Services
{
    // Создайте отдельный сервис для работы с данными о сотрудниках и отпусках
    public class EmployeeVacationService
    {
        private readonly ApplicationDbContext db;

        public EmployeeVacationService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public EmployeeVacationData GetEmployeeVacationDataYounger30MyDep(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                return null;
            }

            var vacations = db.Vacations.Where(v => v.Employee.Id == employeeId).ToList();

            var intersectingEmployees = db.Employees
                .Where(e => e.Age < 30 && e.Id != employeeId && e.Position == employee.Position)
                .ToList();

            var intersectingEmployeeIds = intersectingEmployees.Select(e => e.Id).ToList();

            var intersectingVacationsOfintersectingEmployees = new List<Vacation>();

            foreach (var vacation in vacations)
            {
                var intersecting = db.Vacations
                    .Where(v => intersectingEmployeeIds.Contains(v.Employee.Id) &&
                                (v.StartDate <= vacation.EndDate && v.EndDate >= vacation.StartDate))
                    .ToList();

                intersectingVacationsOfintersectingEmployees.AddRange(intersecting);
            }

            intersectingEmployees = intersectingEmployees
                .Where(employee_ => intersectingVacationsOfintersectingEmployees
                    .Any(vacation => vacation.Employee.Id == employee_.Id))
                .ToList();

            var result = new EmployeeVacationData
            {
                Employees = intersectingEmployees,
                Vacations = intersectingVacationsOfintersectingEmployees
            };

            return result;
        }

        public EmployeeVacationData GetEmployeeVacationDataBetween30and50FemaleNotMyDep(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                return null;
            }

            var vacations = db.Vacations.Where(v => v.Employee.Id == employeeId).ToList();

            var intersectingEmployees = db.Employees
                .Where(e => e.Age > 30 && e.Age < 50 && e.Id != employeeId &&
                            e.Gender == "Женщина" && e.Position != employee.Position)
                .ToList();

            var intersectingEmployeeIds = intersectingEmployees.Select(e => e.Id).ToList();

            var intersectingVacationsOfintersectingEmployees = new List<Vacation>();

            foreach (var vacation in vacations)
            {
                var intersecting = db.Vacations
                    .Where(v => intersectingEmployeeIds.Contains(v.Employee.Id) &&
                                (v.StartDate <= vacation.EndDate && v.EndDate >= vacation.StartDate))
                    .ToList();

                intersectingVacationsOfintersectingEmployees.AddRange(intersecting);
            }

            intersectingEmployees = intersectingEmployees
                .Where(employee_ => intersectingVacationsOfintersectingEmployees
                    .Any(vacation => vacation.Employee.Id == employee_.Id))
                .ToList();

            var result = new EmployeeVacationData
            {
                Employees = intersectingEmployees,
                Vacations = intersectingVacationsOfintersectingEmployees
            };

            return result;
        }

        public EmployeeVacationData GetEmployeeVacationDataOlder50AnyDep(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                return null;
            }

            var vacations = db.Vacations.Where(v => v.Employee.Id == employeeId).ToList();

            var intersectingEmployees = db.Employees
                .Where(e => e.Age > 50 && e.Id != employeeId)
                .ToList();

            var intersectingEmployeeIds = intersectingEmployees.Select(e => e.Id).ToList();

            var intersectingVacationsOfintersectingEmployees = new List<Vacation>();

            foreach (var vacation in vacations)
            {
                var intersecting = db.Vacations
                    .Where(v => intersectingEmployeeIds.Contains(v.Employee.Id) &&
                                (v.StartDate <= vacation.EndDate && v.EndDate >= vacation.StartDate))
                    .ToList();

                intersectingVacationsOfintersectingEmployees.AddRange(intersecting);
            }

            intersectingEmployees = intersectingEmployees
                .Where(employee_ => intersectingVacationsOfintersectingEmployees
                    .Any(vacation => vacation.Employee.Id == employee_.Id))
                .ToList();

            var result = new EmployeeVacationData
            {
                Employees = intersectingEmployees,
                Vacations = intersectingVacationsOfintersectingEmployees
            };

            return result;
        }
        public EmployeeVacationData GetEmployeeVacationDataNoIntersection(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                return null;
            }

            var vacations = db.Vacations.Where(v => v.Employee.Id == employeeId).ToList();

            var intersectingEmployees = db.Employees
                .Where(e => e.Id != employeeId)
                .ToList();

            var intersectingEmployeeIds = intersectingEmployees.Select(e => e.Id).ToList();

            var intersectingVacationsOfintersectingEmployees = new List<Vacation>();

            foreach (var vacation in vacations)
            {
                var intersecting = db.Vacations
                    .Where(v => intersectingEmployeeIds.Contains(v.Employee.Id) &&
                                (v.StartDate <= vacation.EndDate && v.EndDate >= vacation.StartDate))
                    .ToList();

                intersectingVacationsOfintersectingEmployees.AddRange(intersecting);
            }

            intersectingEmployees = intersectingEmployees
                .Where(employee_ => !intersectingVacationsOfintersectingEmployees
                    .Any(vacation => vacation.Employee.Id == employee_.Id))
                .ToList();

            var result = new EmployeeVacationData
            {
                Employees = intersectingEmployees,
                Vacations = vacations.Except(intersectingVacationsOfintersectingEmployees).ToList()
            };

            return result;
        }


    }

}