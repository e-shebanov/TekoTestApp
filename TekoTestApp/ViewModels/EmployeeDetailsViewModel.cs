using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TekoTestApp.Models;

namespace TekoTestApp.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public List<Vacation> Vacations { get; set; }
        public Vacation NewVacation { get; set; }
        public EmployeeVacationData IntersectionFromMyDepYounger30 { get; set; }
        public EmployeeVacationData Between30and50FemaleNotMyDep {  get; set; }
        public EmployeeVacationData Older50AnyDep { get; set; }
        public EmployeeVacationData NotIntersection {  get; set; }


    }
}